#if OPENGL
    #define SV_POSITION POSITION
    #define VS_SHADERMODEL vs_3_0
    #define PS_SHADERMODEL ps_3_0
#else
    #define VS_SHADERMODEL vs_4_0_level_9_1
    #define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
float Timer;
float Strength = 0.01f;

sampler TextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};

float4 MainPS(float4 pos : SV_POSITION, float4 color0 : COLOR0, float4 texCoord : TEXCOORD0) : COLOR
{
    color0.xy = Timer * float2(5.7999244f, 2.8999622f) + float2(0.5f, 0.5f);
    color0.xy = frac(color0.xy);
    color0.xy = color0.xy * float2(6.2831855f, 6.2831855f) + float2(-3.1415927f, -3.1415927f);
    color0.x = cos(color0.x);
    color0.y = sin(color0.y);
    color0.xy = color0.xy * Strength + texCoord.xy;
    color0 = SpriteTexture.Sample(TextureSampler, color0.xy);
    return color0;
}

technique Drunk
{
    pass Pass1
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};