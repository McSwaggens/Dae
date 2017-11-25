#version 330 core


#vert

layout (location = 0) in vec2 vertexPosition;
layout (location = 1) in vec2 _uv;
layout (location = 2) in vec2 position;
layout (location = 3) in vec2 characterPosition;
layout (location = 4) in vec3 _foreground;
layout (location = 5) in vec3 _background;

varying vec2 uv;
out flat vec2 fontStartLoc;
out flat vec3 foreground;
out flat vec3 background;
uniform vec2 size;
uniform float time;

void main ()
{
	uv = _uv;
	foreground = _foreground;
	background = _background;
	fontStartLoc = (1.0 / 16.0) * vec2(characterPosition.x, 15.0-characterPosition.y);
	gl_Position = vec4((position + (vertexPosition * size)), 0, 1);
}

#frag

varying vec2 uv;
in vec3 foreground;
in vec3 background;
in vec2 fontStartLoc;
out vec4 color;

uniform float time;
uniform sampler2D font;

float N0P1ToP01 (float f)
{
	return (f * 1);
}

void main ()
{
	vec2 uvv = uv * (1.0 / 16.0);
	vec3 texColor = texture2D(font, fontStartLoc + uvv).rgb;

	vec3 outputColor = (texColor * foreground) + ((1-texColor) * background);

	color = vec4(outputColor, 1);
}