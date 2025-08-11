#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

// Define a texture sampler
Texture2D SpriteTexture;

sampler SpriteTextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};

float iTime;
float shakeAmount;
float shakeVelocity;

cbuffer MyConstants
{
    float offset[20];
}


// Input/output structs 
float4 MainPS(float4 pos : SV_POSITION, float4 color0 : COLOR0, float2 texCoord : TEXCOORD0) : COLOR
{
    // Normalized pixel coordinates
    //float2 iResolution = float2(1280, 720);
    //float2 fragCoord = texCoord * iResolution;
    float2 uv = texCoord;

    // Time variable 
    float time = iTime;

    // Shake 
    uv.x += shakeAmount;

    // Blur 
    const int numSamples = 20;
    const float blurStrength = 0.10;

    float3 color = float3(0.0,0.0,0.0);

    if (shakeVelocity <= 0.0)
    {
        for (int i = 0; i < numSamples; i++)
        {
            //float offset = (float(i) - float(numSamples - 1) / 2.0) * blurStrength * shakeVelocity;
            color += SpriteTexture.Sample(SpriteTextureSampler, uv + float2(offset[i], 0.0)).rgb;

        }
        color /= float(numSamples);
    }
    else
    {
        color = SpriteTexture.Sample(SpriteTextureSampler, uv).rgb;
    }

    return float4(color,1.0);
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
}