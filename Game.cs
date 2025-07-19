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
using StbImageSharp;
using CameraNamespace;
using OpenTK_yttutorial.Graphics;



namespace OpenTK_yttutorial
{
    public class Game : GameWindow
    {
        // Verticies for the triangle
        // These are the positions of the vertices in 3D space

        Camera camera;

        List<Vector3> vertices = new List<Vector3>()
        {   
            // Front face
            new Vector3(-0.5f, 0.5f, 0.5f), //Top left vertex - 0
            new Vector3(0.5f, 0.5f, 0.5f), //Top rigth vertex - 1
            new Vector3(0.5f, -0.5f, 0.5f), // bottom right vertex - 2
            new Vector3(-0.5f, -0.5f, 0.5f), // bottom left vertex  - 3
            //right face
            new Vector3(0.5f, 0.5f, 0.5f), //Top left vertex - 0
            new Vector3(0.5f, 0.5f, -0.5f), //Top rigth vertex - 1
            new Vector3(0.5f, -0.5f, -0.5f), // bottom right vertex - 2
            new Vector3(0.5f, -0.5f, 0.5f), // bottom left vertex  - 3
            //back face
            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
            //left face
            new Vector3(-0.5f, 0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),
            //top face
            new Vector3(-0.5f, 0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
            //bottom face
            new Vector3(-0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),
        };
        // Texture coordinates for the vertices
        // These are the UV coordinates for the texture mapping

        List<Vector2> texCoords = new List<Vector2>()
        {   
            // top face
            new Vector2(0f, 1f), //Top left vertex - 0
            new Vector2(1f, 1f), //Top rigth vertex - 1
            new Vector2(1f, 0f), // bottom right vertex - 2
            new Vector2(0f, 0f), // bottom left vertex  - 3
            // bottom face
            new Vector2(0f, 1f), //Top left vertex - 0
            new Vector2(1f, 1f), //Top rigth vertex - 1
            new Vector2(1f, 0f), // bottom right vertex - 2
            new Vector2(0f, 0f), // bottom left vertex  - 3
            // left face
            new Vector2(0f, 1f), //Top left vertex - 0
            new Vector2(1f, 1f), //Top rigth vertex - 1
            new Vector2(1f, 0f), // bottom right vertex - 2
            new Vector2(0f, 0f), // bottom left vertex  - 3
            // right face
            new Vector2(0f, 1f), //Top left vertex - 0
            new Vector2(1f, 1f), //Top rigth vertex - 1
            new Vector2(1f, 0f), // bottom right vertex - 2
            new Vector2(0f, 0f), // bottom left vertex  - 3
            // front face
            new Vector2(0f, 1f), //Top left vertex - 0
            new Vector2(1f, 1f), //Top rigth vertex - 1
            new Vector2(1f, 0f), // bottom right vertex - 2
            new Vector2(0f, 0f), // bottom left vertex  - 3
            // back face
            new Vector2(0f, 1f), //Top left vertex - 0
            new Vector2(1f, 1f), //Top rigth vertex - 1
            new Vector2(1f, 0f), // bottom right vertex - 2
            new Vector2(0f, 0f), // bottom left vertex  - 3
        };

        List<uint> indices = new List<uint>
        {
            //top face
            //top triangle
            0, 1, 2,
            //bottom triangle
            2, 3, 0,

            4, 5, 6,
            6, 7, 4,

            8, 9, 10,
            10, 11, 8,

            12, 13, 14,
            14, 15, 12,

            16, 17, 18,
            18, 19, 16,

            20, 21, 22,
            22, 23, 20,
        };

        // reder pipeline vars
        VAO vao;
        IBO ibo;
        ShaderProgram program;
        Texture texture;

        // transformation variables

        float yrot = 0f;
        //float xrot = 0f;
        //float zrot = 0f;


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

            vao = new VAO();

            VBO vbo = new VBO(vertices);
            vao.LinkToVAO(0, 3, vbo);
            VBO uvVBO = new VBO(texCoords);
            vao.LinkToVAO(1, 2, uvVBO);

            ibo = new IBO(indices);

            program = new ShaderProgram("Default.vert", "Default.frag");

            texture = new Texture("DirtTex.png");

                        
            GL.Enable(EnableCap.DepthTest);

            camera = new Camera(width, height, Vector3.Zero);
            CursorState = CursorState.Grabbed;
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            vao.delete();
            ibo.delete();
            program.Delete();
            texture.Delete();

        }


        // Render frame function
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            // Set the color to fill the screen with
            GL.ClearColor(0.3f, 0.3f, 1f, 1f);
            // Fill the screen with the color
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


            program.Bind();
            vao.Bind();
            texture.Bind();
            ibo.Bind();

            // transformation matrices
            Matrix4 model = Matrix4.Identity;
            Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix();

            
            model = Matrix4.CreateRotationY(yrot);
            yrot += 0.001f;

            Matrix4 translation = Matrix4.CreateTranslation(0f, 0f, -3f);

            model *= translation;

            int modelLocation = GL.GetUniformLocation(program.ID, "model");
            int viewLocation = GL.GetUniformLocation(program.ID, "view");
            int projectionLocation = GL.GetUniformLocation(program.ID, "projection");

            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref projection);

            GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);

            model += Matrix4.CreateTranslation(new Vector3(2f, 0f, 0f));
            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3); // draw the triangle | args = Primitive type, first vertex, last vertex


            // swap the buffers
            Context.SwapBuffers();

            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            MouseState mouse = MouseState;
            KeyboardState input = KeyboardState;

            base.OnUpdateFrame(args);
            camera.Update(input, mouse, args);

        }
        
    }
}
