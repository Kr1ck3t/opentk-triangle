#version 330 core

in vec2 texCoord;

uniform sampler2D Tex0;

out vec4 FragColor;

void main()
{
    FragColor = texture(Tex0, texCoord);
}