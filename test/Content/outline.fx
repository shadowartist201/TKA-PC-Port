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
    float2 fragCoord = texCoord * iResolution;
    float2 d = iResolution.xy;
    
    float filterAmplification = 3.5;
    
    float2 uv = fragCoord / iResolution.xy;

    float2 uv_e = floor(uv * d) / d;

    float3 col0 = SpriteTexture.Sample(SpriteTextureSampler, uv).rgb;
    
    float3 col1 = SpriteTexture.Sample(SpriteTextureSampler, uv_e).rgb;
    float temp = max(0.0, 1.0 - length(col1 - col0) * filterAmplification);

    float3 edge_outline = float3(temp,temp,temp);
    
    return float4(1.0 - edge_outline, 1.0) * SpriteTexture.Sample(SpriteTextureSampler, uv);
    
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};