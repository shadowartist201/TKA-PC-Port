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

// Shader entry point
float4 MainPS(float4 pos : SV_POSITION, float4 color0 : COLOR0, float2 texCoord : TEXCOORD0) : COLOR
{
    // Normalized pixel coordinates (from -1 to 1)
    float2 iResolution = float2(1280, 720);
    float2 fragCoord = texCoord * iResolution;
    float2 uv = fragCoord / iResolution.xy;

    // Time and rotation parameters
    float time = iTime;
    float radius = 0.04;
    float samples = 20.0;
    float offset = 0.0;
    offset += (18.2212372 * -time);
    offset = fmod(offset, 6.28318); // Equivalent to mod(offset, 2.0 * PI)

    // Color accumulator
    float3 color = float3(0.0, 0.0, 0.0);

    for (float i = 0.0; i < samples; i++)
    {
        float offsetAmount = radius * (i / samples);

        // Rotation matrix with correct trigonometric functions for HLSL
        float2x2 rotationMatrix = float2x2(cos(offset), sin(offset),
                                           -sin(offset), cos(offset));

        float2 final = mul(rotationMatrix, float2(offsetAmount, 0.0));

        // Sample iChannel0 with calculated offset
        color += SpriteTexture.Sample(SpriteTextureSampler, uv + final).rgb;

    }

    color = color / samples;

    return float4(color, 1.0);
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
}