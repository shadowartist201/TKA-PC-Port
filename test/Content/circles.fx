#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
float timeInSeconds;

sampler SpriteTextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};

float2 movingTiles(float2 _st, float _zoom, float _speed, float iTime){
    _st *= _zoom; //zoom
    float scaledTime = iTime * _speed;
    if (frac(_st.y * 0.5) > 0.5) //if top row
    {
        _st.x += scaledTime * 2.0;
    } 
    else //if bottom row
    {
        _st.x += 0.5 + scaledTime * 2.0;
    }
    return frac(_st);
}

float circle(float2 _st, float _radius){
    float2 pos = float2(0.5, 0.5)-_st;
    return smoothstep(1.0 - _radius, 1.0 - _radius + _radius*0.2, 1.0 - dot(pos, pos) * 3.14);
}

float4 MainPS(float4 pos : SV_POSITION, float4 color0 : COLOR0, float2 texCoord : TEXCOORD0) : COLOR
{
    float2 iResolution = float2(1280, 720);
    float iTime = timeInSeconds;
    
    float2 st = texCoord;
    st.x *= iResolution.x / iResolution.y; //get texture coords in pixels for x-axis
	
    st = movingTiles(st, 18.0, 3.5, iTime);
	
    float3 color = float3(circle(st, 0.45), circle(st, 0.45), circle(st, 0.45));
    float4 color1 = SpriteTexture.Sample(SpriteTextureSampler, texCoord); //upside down box
    float4 color2 = 1.0 - color1; //white upside down box
    color2.a = color2.a + 1.0f;
    float4 outputColor;
	
	return outputColor = lerp(color1, color2, float4(color, 1.0));
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};