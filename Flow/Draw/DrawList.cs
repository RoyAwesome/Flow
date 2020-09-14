using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Linq;

namespace Watertight.FlowUI.Draw
{
    public enum EDrawCornerFlags : byte
    {
        None = 0,
        TopLeft = 1 << 0, // 0x1
        TopRight = 1 << 1, // 0x2
        BotLeft = 1 << 2, // 0x4
        BotRight = 1 << 3, // 0x8
        Top = TopLeft | TopRight,   // 0x3
        Bot = BotLeft | BotRight,   // 0xC
        Left = TopLeft | BotLeft,    // 0x5
        Right = TopRight | BotRight,  // 0xA
        All = 0xF     
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vector2 Pos;
        public Vector2 UV;
        public Color Color;
    }

    internal class DrawListSharedData
    {
        internal const int ArcFast_TessalationMultiplier = 1;

        public Vector2 OpaquePixel
        {
            get;
            set;
        } = Vector2.Zero;

        public Vector2 TexUVWhitePixel
        {
            get;
            set;
        } = Vector2.Zero;

        public Vector2[] ArcFastVertexLookupTable
        {
            get;
        }

        public byte[] CircleSegmentCounts
        {
            get;
        } = new byte[64];

        public float CurveTesselationTolerance
        {
            get;
            set;
        }
        public float CircleSegmentMaxError
        {
            get;
            set;
        }

        public DrawListSharedData()
        {
            ArcFastVertexLookupTable = new Vector2[12 * ArcFast_TessalationMultiplier];

            for(int i = 0; i < ArcFastVertexLookupTable.Length; i++)
            {
                float a = ((float)i * 2 * MathF.PI) / (float)ArcFastVertexLookupTable.Length;
                ArcFastVertexLookupTable[i] = new Vector2(MathF.Cos(a), MathF.Sin(a));
            }

        }

        public void SetCircleSegmentMaxError(float max_error)
        {
            if (CircleSegmentMaxError == max_error)
            {
                return;
            }

            CircleSegmentMaxError = max_error;
            for(int i = 0; i < CircleSegmentCounts.Length; i++)
            {
                float radius = i + 1.0f;
                int segment_count = AutoSegmentCalc(radius, max_error);
                CircleSegmentCounts[i] = (byte)Math.Min(segment_count, 255);
            }
        }
        const int AutoSegmentMin = 12;
        const int AutoSegmentMax = 512;

        private static int AutoSegmentCalc(float Rad, float max_error)
        {
            return Math.Clamp((int)((MathF.PI * 2) / MathF.Acos((Rad - max_error) / Rad)), AutoSegmentMin, AutoSegmentMax);
        }
    }

    public class DrawList
    {
        public struct DrawCmd
        {
            
            //public uint VertexOffset;

            public uint IndexOffset;
            public uint Elements;
        }

        
        public IReadOnlyCollection<DrawCmd> CommandBuffer
        {
            get => _CommandBuffer;
        }
        private Stack<DrawCmd> _CommandBuffer = new Stack<DrawCmd>();

        public IReadOnlyList<Vertex> VertexBuffer
        {
            get => _VertexBuffer;
        } 
        private List<Vertex> _VertexBuffer = new List<Vertex>();

        public IReadOnlyList<ushort> IndexBuffer
        {
            get => _IndexBuffer;
        }
        private List<ushort> _IndexBuffer = new List<ushort>();

        public string DebugString
        {
            get;
            internal set;
        }

        private DrawListSharedData SharedData
        {
            get;
            set;
        }

        private List<Vector2> _PathBuilder = new List<Vector2>();

        internal DrawList(DrawListSharedData SharedData)
        {
            this.SharedData = SharedData;
        }

        internal void ResetForNewFrame()
        {
            _CommandBuffer.Clear();
            _VertexBuffer.Clear();
            _IndexBuffer.Clear();

            _PathBuilder.Clear();

            _CommandBuffer.Push(new DrawCmd());

            PathClear();

        }

        private void UpdateCommandList(uint indices)
        {
            DrawCmd cmd;
            if (!_CommandBuffer.TryPop(out cmd))
            {
                cmd = new DrawCmd()
                {
                    IndexOffset = (uint)_IndexBuffer.Count,
                };

            }
            cmd.Elements += indices;
            _CommandBuffer.Push(cmd);
        }


       

        #region Primitives

        private void Normalize2fOverZero(ref float VX, ref float VY)
        {           
            float d2 = VX * VX + VY * VY;
            if(d2 > 0.0f)
            {
                float inv_len = 1.0f / MathF.Sqrt(d2);
                VX *= inv_len;
                VY *= inv_len;
            }            
        }

        private void FixNormal2F(ref float VX, ref float VY)
        {
            float d2 = VX * VX + VY * VY;
            if (d2 < 0.5f)
            {
                float inv_lenSqrd = 1.0f / d2;
                VX *= inv_lenSqrd;
                VY *= inv_lenSqrd;
            }
        }

        public void AddPolyLine(IEnumerable<Vector2> Points, Color color, bool Closed, float Thickness)
        {
            if(Points.Count() < 2)
            {
                return;
            }

            Vector2 OpaqueUV = SharedData.TexUVWhitePixel;
            int count = Points.Count();
            count = Closed ? count : count - 1;

            //TODO: Anti-Aliased line
            {
                uint idx_count = (uint)count * 6;
                uint vtx_count = (uint)count * 4;


                for (int i1 = 0; i1 < count; i1++)
                {
                    int i2 = (i1 + 1) == Points.Count() ? 0 : i1 + 1;

                    Vector2 p1 = Points.ElementAt(i1);
                    Vector2 p2 = Points.ElementAt(i2);

                    float dx = p2.X - p1.X;
                    float dy = p2.Y - p1.Y;
                    Normalize2fOverZero(ref dx, ref dy);

                    dx *= Thickness * 0.5f;
                    dy *= Thickness * 0.5f;

                    Vertex[] Verts = new Vertex[]
                    {
                        new Vertex()
                        {
                            Pos = new Vector2(p1.X + dy, p1.Y - dx),
                            UV = OpaqueUV,
                            Color = color,
                        },
                         new Vertex()
                        {
                            Pos = new Vector2(p2.X + dy, p2.Y - dx),
                            UV = OpaqueUV,
                            Color = color,
                        },
                          new Vertex()
                        {
                            Pos = new Vector2(p2.X - dy, p2.Y + dx),
                            UV = OpaqueUV,
                            Color = color,
                        },
                           new Vertex()
                        {
                            Pos = new Vector2(p1.X - dy, p1.Y + dx),
                            UV = OpaqueUV,
                            Color = color,
                        },
                    };


                    ushort vtx = (ushort)_VertexBuffer.Count;
                    _VertexBuffer.AddRange(Verts);

                    ushort[] Indices = new ushort[]
                    {
                        (ushort)(vtx + 0), (ushort)(vtx + 1), (ushort)(vtx + 2),
                        (ushort)(vtx + 0), (ushort)(vtx + 2), (ushort)(vtx + 3),
                    };

                    _IndexBuffer.AddRange(Indices);

                }

                UpdateCommandList(idx_count);
            }

        }

        public void AddConvexPolyFilled(IEnumerable<Vector2> Points, Color color)
        {
            uint PointsCount = (uint)Points.Count();

            if(PointsCount < 3)
            {
                return;
            }

            //TODO: Anti Aliasing

            {
                uint idx_count = (PointsCount - 2) * 3;
                uint vtx_count = PointsCount;


                ushort vtx = (ushort)_VertexBuffer.Count;
                for (int i = 0; i < vtx_count; i++)
                {
                    _VertexBuffer.Add(new Vertex
                    {
                        Pos = Points.ElementAt(i),
                        UV = SharedData.TexUVWhitePixel,
                        Color = color
                    });                  
                }

               
                for (int i = 2; i < PointsCount; i++)
                {
                    ushort[] inds = new ushort[]
                    {
                        vtx, (ushort)(vtx + i - 1), (ushort)(vtx + i),
                    };
                    _IndexBuffer.AddRange(inds);
                }

                UpdateCommandList(idx_count);
            }

        }

        #endregion

        #region Path Building
        public void PathClear()
        {
            _PathBuilder.Clear();
        }

        public void PathLineTo(Vector2 pos)
        {
            _PathBuilder.Add(pos);
        }

        public void PathToLineMergeDuplicate(Vector2 pos)
        {
            if(!_PathBuilder.Contains(pos))
            {
                PathLineTo(pos);
            }
        }

        public void PathStroke(Color Color, bool Closed, float Thickness = 1.0f)
        {
            AddPolyLine(_PathBuilder, Color, Closed, Thickness);
            PathClear();
        }

        public void PathFilled(Color color)
        {
            AddConvexPolyFilled(_PathBuilder, color);
            PathClear();
        }

        public void PathRect(Vector2 Min, Vector2 Max, float Rounding, EDrawCornerFlags RoundingFlags = EDrawCornerFlags.All)
        {
            Rounding = Math.Min(Rounding, Math.Abs(Max.X - Min.X) * (RoundingFlags.HasFlag(EDrawCornerFlags.Top) || RoundingFlags.HasFlag(EDrawCornerFlags.Bot) ? 0.5f : 1.0f) - 1.0f);
            Rounding = Math.Min(Rounding, Math.Abs(Max.Y - Min.Y) * (RoundingFlags.HasFlag(EDrawCornerFlags.Left) || RoundingFlags.HasFlag(EDrawCornerFlags.Right) ? 0.5f : 1.0f) - 1.0f);


            if (Rounding <= 0.0f)
            {
                PathLineTo(Min);
                PathLineTo(new Vector2(Max.X, Min.Y));
                PathLineTo(Max);
                PathLineTo(new Vector2(Min.X, Max.Y));
            }
            else
            {
                //Rounded Corners

                float RoundingTL = RoundingFlags.HasFlag(EDrawCornerFlags.TopLeft) ? Rounding : 0.0f;
                float RoundingBL = RoundingFlags.HasFlag(EDrawCornerFlags.BotLeft) ? Rounding : 0.0f;
                float RoundingTR = RoundingFlags.HasFlag(EDrawCornerFlags.TopRight) ? Rounding : 0.0f;
                float RoundingBR = RoundingFlags.HasFlag(EDrawCornerFlags.BotRight) ? Rounding : 0.0f;

                PathArcToFast(new Vector2(Min.X + RoundingTL, Min.Y + RoundingTL), RoundingTL, 6, 9);
                PathArcToFast(new Vector2(Max.X - RoundingTR, Min.Y + RoundingTR), RoundingTR, 9, 12);
                PathArcToFast(new Vector2(Max.X - RoundingBR, Max.Y - RoundingBR), RoundingBR, 0, 3);
                PathArcToFast(new Vector2(Min.X + RoundingBL, Max.Y - RoundingBL), RoundingBL, 3, 6);
            }
        }       
        
        void PathArcToFast(Vector2 Center, float Radius, int a_min_of_12, int a_max_of_12)
        {
            if(Radius == 0.0f || a_min_of_12 > a_max_of_12)
            {
                _PathBuilder.Add(Center);
                return;
            }

            if(DrawListSharedData.ArcFast_TessalationMultiplier != 1)
            {
                a_min_of_12 *= DrawListSharedData.ArcFast_TessalationMultiplier;
                a_max_of_12 *= DrawListSharedData.ArcFast_TessalationMultiplier;
            }

            for(int a = a_min_of_12; a <= a_max_of_12; a++)
            {
                Vector2 c = SharedData.ArcFastVertexLookupTable[a % SharedData.ArcFastVertexLookupTable.Length];
                _PathBuilder.Add(new Vector2(Center.X + c.X * Radius, Center.Y + c.Y * Radius));
            }
        }
        #endregion

        #region Shapes
        public void AddRect(Vector2 Min, Vector2 Max, Color Color, float Rounding, float Thickness = 1.0f, EDrawCornerFlags RoundingFlags = EDrawCornerFlags.All)
        {
            if(Color.A == 0)
            {
                return;
            }

            PathRect(Min + new Vector2(.5f, .5f), Max - new Vector2(.5f, .5f), Rounding, RoundingFlags);
            PathStroke(Color, true, Thickness);

        }

        public void AddRectFilled(Vector2 Min, Vector2 Max, Color color, float Rounding, EDrawCornerFlags RoundingFlags = EDrawCornerFlags.All)
        {
            if(color.A == 0)
            {
                return;
            }
       
            PathRect(Min, Max, Rounding, RoundingFlags);
            PathFilled(color);

        }
        #endregion
    }
}
