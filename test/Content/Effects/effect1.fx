#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
float hInterval = 0.00078125f;
float vInterval = 0.0013888889f;

sampler TextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};

float4 MainPS(float4 pos : SV_POSITION, float4 color0 : COLOR0, float4 texCoord : TEXCOORD0) : COLOR
{
    float4 r2;
    float4 r3;
    
    color0.z = max(-hInterval, -hInterval);
    r2.xyz = -abs(color0.xxx) > 0.0f;
    color0.x = -1.0f;
    for (int i = 0; i < 3; i++)
    {
        color0.y = -1.0f;
        color0.w = -vInterval;
        for (int j = 0; j < 3; j++)
        {
            texCoord.zw = color0.zw + texCoord.xy;
            r3 = SpriteTexture.Sample(TextureSampler, texCoord.zw);
            texCoord.zw = color0.xy == 0.0f;
            texCoord.z = texCoord.z * texCoord.w;
            if (texCoord.z != 0.0)
            {
                r2.xyz = r3.xyz * 8.0f + r2.xyz;
            }
            else
            {
                r2.xyz = -r3.xyz + r2.xyz;
            }
            color0.y = 1.0f + color0.y;
            texCoord.z = 2.0f > color0.y;
            color0.w = color0.w + vInterval;
        }
        color0.x = 1.0f + color0.x;
        color0.y = 2.0f > color0.x;
        color0.z = color0.z + hInterval;
    }
    return float4(r2.xyz, 1.0f);
}

technique EdgeDetectDark
{
    pass Pass1
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};