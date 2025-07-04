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



namespace OpenTK_yttutorial
{
    public class Game : GameWindow
    {
        // Verticies for the triangle
        // These are the positions of the vertices in 3D space

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

        uint[] indices =
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
        int vao;
        int shaderProgram;

        int vbo;
        int textureVBO;
        int ebo;
        int TextureId;

        // transformation variables

        float yrot = 0f;
        float xrot = 0f;
        float zrot = 0f;


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
            // Generate the vbo
            vao = GL.GenVertexArray();

            // Bind the vao
            GL.BindVertexArray(vao);

            // --- Verticies vbo ----

            //generate a buffer
            vbo = GL.GenBuffer();
            //bine the buffer as an array buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            //store data in the vbo
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * Vector3.SizeInBytes, vertices.ToArray(), BufferUsageHint.StaticDraw);


            //put vertex vbo in slot 0

            //point slot 0 of the vao to the currently bound vbo
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            // enable to slot
            GL.EnableVertexArrayAttrib(vao, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            // --- Texture vbo ---
            textureVBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, textureVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, texCoords.Count * Vector2.SizeInBytes, texCoords.ToArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(1);


            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //this is to unbind the vbo


            ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            GL.BindVertexArray(0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

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

            // Set the uniform for the texture unit
            GL.UseProgram(shaderProgram);
            int texLocation = GL.GetUniformLocation(shaderProgram, "Tex0");
            GL.Uniform1(texLocation, 0); // Texture unit 0


            // Textures
            TextureId = GL.GenTexture();
            //active the texture in the unit
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, TextureId);

            // texture parameteres

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            // load image
            StbImage.stbi_set_flip_vertically_on_load(1);
            ImageResult dirtTexture = ImageResult.FromStream(File.OpenRead("../../../Textures/DirtTex.png"), ColorComponents.RedGreenBlueAlpha);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, dirtTexture.Width, dirtTexture.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, dirtTexture.Data);

            // unbind texture
            GL.BindTexture(TextureTarget.Texture2D, 0);

            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            GL.DeleteVertexArray(vao);
            GL.DeleteBuffer(vbo);
            GL.DeleteBuffer(ebo);
            GL.DeleteTexture(TextureId);
            GL.DeleteProgram(shaderProgram);
        }


        // Render frame function
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            // Set the color to fill the screen with
            GL.ClearColor(0.3f, 0.3f, 1f, 1f);
            // Fill the screen with the color
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // draw our triangle
            GL.UseProgram(shaderProgram); // bind vao
            GL.BindVertexArray(vao); // use shader program
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);

            GL.BindTexture(TextureTarget.Texture2D, TextureId);


            // transformation matrices
            Matrix4 model = Matrix4.Identity;
            Matrix4 view = Matrix4.Identity;
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60.0f), width/(float)height, 0.1f, 100.0f);

            
            model = Matrix4.CreateRotationY(yrot);
            yrot += 0.001f;

            Matrix4 translation = Matrix4.CreateTranslation(0f, 0f, -3f);

            model *= translation;

            int modelLocation = GL.GetUniformLocation(shaderProgram, "model");
            int viewLocation = GL.GetUniformLocation(shaderProgram, "view");
            int projectionLocation = GL.GetUniformLocation(shaderProgram, "projection");

            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref projection);

            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3); // draw the triangle | args = Primitive type, first vertex, last vertex


            // swap the buffers
            Context.SwapBuffers();

            base.OnRenderFrame(args);
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
