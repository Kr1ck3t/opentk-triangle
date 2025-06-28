using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;



namespace OpenTK_yttutorial
{
    public class Game : GameWindow
    {
        float[] vertices =
        {
            -0.5f, -0.5f, 0f, //Top vertex
            0.5f, -0.5f, 0f, // bottom left vertex 
            0.0f, 0.5f, 0f, // bottom right vertex
        };

        // reder pipeline vars
        int vao;
        int shaderProgram;

        int vbo;

        int width, height;
        public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            // Centre the screen to monitor
            this.CenterWindow(new Vector2i(width, height));

            this.width = width;
            this.height = height;

        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            this.width = e.Width;
            this.height = e.Height;
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            vao = GL.GenVertexArray();
            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            //everything between these 2 statmenets will e referenceing vbo
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // bind the vao
            GL.BindVertexArray(vao);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexArrayAttrib(vao, 0);




            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //this is to unbind the vbo
            GL.BindVertexArray(0);

            // Create teh shader program

            shaderProgram = GL.CreateProgram();

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, LoadShaderSource("Default.vert"));
            GL.CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, LoadShaderSource("Default.frag"));
            GL.CompileShader(fragmentShader);

            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);

            GL.LinkProgram(shaderProgram);

            //Delete teh shaders (good practice]
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            GL.DeleteVertexArray(vao);
            GL.DeleteProgram(shaderProgram);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.ClearColor(0.2f, 0.8f, 1f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Draw the triangle
            GL.UseProgram(shaderProgram);
            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            Context.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

        }


        //Function to load text file adn return content as string

        public static string LoadShaderSource(string filePath)
        {
            string shaderSource = "";

            try
            {
                using (StreamReader reader = new StreamReader("../../../shaders/" + filePath))
                {
                    shaderSource = reader.ReadToEnd();
                }
                //Console.WriteLine("problem area");
                //Console.WriteLine(shaderSource);

            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to load sahder source file: " + e.Message);
            }

            return shaderSource;

        }

    }
}
