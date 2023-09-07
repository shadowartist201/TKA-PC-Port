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

float4 MainPS(float4 pos : SV_POSITION, float4 color0 : COLOR0, float2 texCoord : TEXCOORD0) : COLOR
{
    float2 iResolution = float2(1280, 720);
    float2 uv = texCoord;;
    //float iTime = timeInSeconds / 1000.0;
	
    uv.y += sin(uv.x * 8.0 + timeInSeconds * 15.0) / 30.0;
	
    float4 color = SpriteTexture.Sample(SpriteTextureSampler, uv);
	
    return color;
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};
