#version 330 core

#vert

layout (location = 0) in vec2 vertexPosition;
layout (location = 1) in vec2 _uv;
layout (location = 2) in vec2 position;

out vec2 uv;
uniform float time;

void main ()
{
	uv = _uv;
	gl_Position = vec4(position + vertexPosition, 0, 1);
}

#frag

in vec2 uv;
out vec4 color;

void main ()
{
	color = vec4(1, 1, 1, 1);
}