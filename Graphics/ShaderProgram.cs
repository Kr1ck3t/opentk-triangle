using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Audio.OpenAL;

namespace OpenTK_yttutorial.Graphics
{
    internal class ShaderProgram
    {
        public int ID;
        public ShaderProgram(string VertexShaderFilePath, string FragmentShaderFilePath) {
            ID = GL.CreateProgram();

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, LoadShaderSource(VertexShaderFilePath));
            GL.CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, LoadShaderSource(FragmentShaderFilePath));
            GL.CompileShader(fragmentShader);

            GL.AttachShader(ID, vertexShader);
            GL.AttachShader(ID, fragmentShader);

            GL.LinkProgram(ID);

            //Delete teh shaders (good practice]
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            // Set the uniform for the texture unit
            GL.UseProgram(ID);
            int texLocation = GL.GetUniformLocation(ID, "Tex0");
            GL.Uniform1(texLocation, 0); // Texture unit 0
        }

        public void Bind() { GL.UseProgram(ID); }
        public void Unbind() { GL.UseProgram(0); }
        public void Delete() { GL.DeleteShader(ID); }

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
                Console.WriteLine("Failed to load shader source file: " + e.Message);
            }

            return shaderSource;

        }


    }
}