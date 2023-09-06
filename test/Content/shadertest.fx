#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
sampler s0;
float timeInSeconds;

sampler2D SpriteTextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};

float2 movingTiles(float2 _st, float _zoom, float _speed){
    _st = mul(_st, _zoom);
    if (frac(mul(_st.y, 0.5)) > 0.5)
    {
        _st.x += frac(1.0) + _speed * 6.0;
    } 
    else 
    {
        _st.x -= frac(1.5) - _speed * 6.0;
    }
    return frac(_st);
}

float circle(float2 _st, float _radius){
    float2 pos = float2(0.5,0.5)-_st;
    return smoothstep(1.0 - _radius, 1.0 - _radius + mul(_radius, 0.2), 1. - mul(dot(pos, pos), 3.14));
}

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 st = input.TextureCoordinates;
    float2 pv = st;
    st.x = st.x * (1280.0 / 720.0);
	
    st = movingTiles(st, 18.0, timeInSeconds);
    float temp = circle(st, 0.55);
	
    float3 color = float3(temp, temp, temp);
    float4 color1 = tex2D(s0, float2(pv.x, pv.y));
    float4 color2 = 1.0 - color1;
    float4 outputColor;
	
	return outputColor = lerp(color1, color2, float4(color, 0.0));
    //return color1;
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};