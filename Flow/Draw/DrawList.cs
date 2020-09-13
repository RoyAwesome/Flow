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
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vector2 Pos;
        public Vector2 UV;
        public Color Color;
    }

    internal class DrawListSharedData
    {
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

        public void PathRect(Vector2 Min, Vector2 Max, float Rounding /*, CornerFlags */)
        {
            if(Rounding <= 0.0f)
            {
                PathLineTo(Min);
                PathLineTo(new Vector2(Max.X, Min.Y));
                PathLineTo(Max);
                PathLineTo(new Vector2(Min.X, Max.Y));
            }
            else
            {
                //Rounded Corners
            }
        }        
        #endregion

        #region Shapes
        public void AddRect(Vector2 Min, Vector2 Max, Color Color, float Rounding, float Thickness = 1.0f)
        {
            if(Color.A == 0)
            {
                return;
            }

            PathRect(Min + new Vector2(.5f, .5f), Max - new Vector2(.5f, .5f), Rounding);
            PathStroke(Color, true, Thickness);
        }

        public void AddRectFilled(Vector2 Min, Vector2 Max, Color color, float Rounding)
        {
            if(color.A == 0)
            {
                return;
            }

            //TODO: Rounding
            if(Rounding > 0.0f)
            {

            }
            else
            {
                PathRect(Min, Max, Rounding);
                PathFilled(color);
            }
        }
        #endregion
    }
}
