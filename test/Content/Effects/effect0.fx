#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
float2 Offset;

sampler TextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};

float4 MainPS(float4 pos : SV_POSITION, float4 color0 : COLOR0, float4 texCoord : TEXCOORD0) : COLOR
{
    float4 r2;
    float4 r3;
    
    r2 = float4(0.0f, 0.0f, 0.0f, 1.0f);
    texCoord.zw = Offset * 0.01f;
    color0.xy = texCoord.zw * color0.ww;
    for (int i = 0; i < 9; i++)
    {
        texCoord.xy = -color0.xy + texCoord.xy;
        r3 = SpriteTexture.Sample(TextureSampler, texCoord.xy);
        r2 = r3 + r2;
    }
    return r2 * 0.111f;

}

technique BlurDirectional
{
    pass Pass1
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};