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
    internal class VAO
    {
        public int ID;
        public VAO() { ID = GL.GenVertexArray();
            GL.BindVertexArray(ID);
        }

        public void LinkToVAO(int location, int size, VBO vbo)
        {
            Bind();
            vbo.Bind();
            GL.VertexAttribPointer(location, size, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(location);
            unBind();
        }
        public void Bind()
        {
            GL.BindVertexArray(ID);
        }
        public void unBind() {
            GL.BindVertexArray(0);
        }
        public void delete() {
            GL.DeleteVertexArray(ID);
        }
    }
}
