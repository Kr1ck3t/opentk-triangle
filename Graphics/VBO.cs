using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using OpenTK;
using OpenTK.Graphics.OpenGL4;


namespace OpenTK_yttutorial.Graphics
{
    internal class VBO
    {
        public int ID;
        public VBO(List<Vector3> data)
        {
            ID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Count * Vector3.SizeInBytes, data.ToArray(), BufferUsageHint.StaticDraw);

        }
        public VBO(List<Vector2> data)
        {
            ID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Count * Vector2.SizeInBytes, data.ToArray(), BufferUsageHint.StaticDraw);
        }
        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
        }
        public void unBind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
        public void delete()
        {
            GL.DeleteBuffer(ID);
        }
    }
}