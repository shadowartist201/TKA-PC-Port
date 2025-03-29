#if OPENGL
    #define SV_POSITION POSITION
    #define VS_SHADERMODEL vs_3_0
    #define PS_SHADERMODEL ps_3_0
#else
    #define VS_SHADERMODEL vs_4_0_level_9_1
    #define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
float2 WaveDimensions;
float Timer;

sampler TextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};

float4 MainPS(float4 pos : SV_POSITION, float4 color0 : COLOR0, float4 texCoord : TEXCOORD0) : COLOR
{
    texCoord.z = texCoord.x * WaveDimensions.x;
    texCoord.w = Timer;
    color0.x = texCoord.w * color0.w + texCoord.z;
    color0.x = color0.x * 0.15915494f + 0.5f;
    color0.x = frac(color0.x);
    color0.x = color0.x * 6.2831855f + -3.1415927f;
    color0.x = sin(color0.x);
    texCoord.z = color0.x * WaveDimensions.y + texCoord.y;
    color0 = SpriteTexture.Sample(TextureSampler, texCoord.xz);
    return color0;
}

technique SinWaves
{
    pass Pass1
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};