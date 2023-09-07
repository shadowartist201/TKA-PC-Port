#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

sampler SpriteTextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};

float4 MainPS(float4 pos : SV_POSITION, float4 color0 : COLOR0, float2 texCoord : TEXCOORD0) : COLOR
{
    float2 iResolution = float2(1280, 720);
    float2 uv = texCoord;
    
    float3 TL = SpriteTexture.Sample(SpriteTextureSampler, uv + float2(-1, 1) / iResolution.xy).rgb;
    float3 TM = SpriteTexture.Sample(SpriteTextureSampler, uv + float2(0, 1) / iResolution.xy).rgb;
    float3 TR = SpriteTexture.Sample(SpriteTextureSampler, uv + float2(1, 1) / iResolution.xy).rgb;
    
    float3 ML = SpriteTexture.Sample(SpriteTextureSampler, uv + float2(-1, 0) / iResolution.xy).rgb;
    float3 MR = SpriteTexture.Sample(SpriteTextureSampler, uv + float2(1, 0) / iResolution.xy).rgb;
    
    float3 BL = SpriteTexture.Sample(SpriteTextureSampler, uv + float2(-1, -1) / iResolution.xy).rgb;
    float3 BM = SpriteTexture.Sample(SpriteTextureSampler, uv + float2(0, -1) / iResolution.xy).rgb;
    float3 BR = SpriteTexture.Sample(SpriteTextureSampler, uv + float2(1, -1) / iResolution.xy).rgb;
                         
    float3 GradX = -TL + TR - 2.0 * ML + 2.0 * MR - BL + BR;
    float3 GradY = TL + 2.0 * TM + TR - BL - 2.0 * BM - BR;
    
    float3 color4;
    color4.r = length(float2(GradX.r, GradY.r));
    color4.g = length(float2(GradX.g, GradY.g));
    color4.b = length(float2(GradX.b, GradY.b));
    return float4(color4, 1.0);
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};