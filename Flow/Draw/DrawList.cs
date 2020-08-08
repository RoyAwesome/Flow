using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Linq;

namespace Watertight.Flow.Draw
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
    }

    class DrawList
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

        private List<Vector2> _PathBuilder;

        internal DrawList(DrawListSharedData SharedData)
        {
            this.SharedData = SharedData;
        }

        internal void ResetForNewFrame()
        {
            _CommandBuffer.Clear();
            _VertexBuffer.Clear();
            _IndexBuffer.Clear();

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

        public void AddPolyLine(Vector2[] Points, Color color, bool Closed, float Thickness)
        {
            if(Points.Count() < 2)
            {
                return;
            }

            Vector2 OpaqueUV = SharedData.OpaquePixel;
            int count = Points.Length;
            if(Closed)
            {
                count -= 1;
            }

            //TODO: Anti-Aliased line

            uint idx_count = (uint)count * 6;
            uint vtx_count = (uint)count * 4;

            
            for(int i1 = 0; i1 < count; i1++)
            {
                int i2 = (i1 + 1) == Points.Length ? 0 : i1 + 1;

                Vector2 p1 = Points[i1];
                Vector2 p2 = Points[i2];

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

                _VertexBuffer.AddRange(Verts);

                ushort vtx = (ushort)_VertexBuffer.Count;
                ushort[] Indices = new ushort[]
                {
                    (ushort)(vtx + 0), (ushort)(vtx + 1), (ushort)(vtx + 2),
                    (ushort)(vtx + 0), (ushort)(vtx + 2), (ushort)(vtx + 3),
                };

                _IndexBuffer.AddRange(Indices);

            }

            UpdateCommandList(idx_count);

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


        #endregion

    }
}
