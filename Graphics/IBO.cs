using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Security.Cryptography.X509Certificates;

namespace OpenTK_yttutorial.Graphics
{
    internal class IBO
    {

        public int ID;
        public IBO(List<uint> data)
        {
            ID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Count * sizeof(uint), data.ToArray(), BufferUsageHint.StaticDraw);

        }
        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);

        }
        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

        }
        public void delete()
        {
            GL.DeleteBuffer(ID);
        }
    }
}
