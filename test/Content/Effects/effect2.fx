#if OPENGL
    #define SV_POSITION POSITION
    #define VS_SHADERMODEL vs_3_0
    #define PS_SHADERMODEL ps_3_0
#else
    #define VS_SHADERMODEL vs_4_0_level_9_1
    #define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
float Offset;

float radius = 0.0140625f;
float xOffset = 0.0390625f;
float yOffset = 0.055555556f;

sampler TextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};

float4 MainPS(float4 pos : SV_POSITION, float4 color0 : COLOR0, float4 texCoord : TEXCOORD0) : COLOR
{
    float4 r2;
    float4 r3;
    float4 r4;
    
    r2 = SpriteTexture.Sample(TextureSampler, texCoord.xy);
    color0.z = yOffset + yOffset;
    color0.w = xOffset * 0.5f;
    color0.y = texCoord.x + Offset;
    r4.w = yOffset * texCoord.y;
    texCoord.zw = yOffset * float2(4.0f, 0.5f);
    r4.x = xOffset * color0.y;
    r3.y = color0.y + color0.w;
    r4.y = r3.y * xOffset;
    r4.z = texCoord.y * color0.z;
    r4 = r4 >= -r4;
    color0.x = xOffset + xOffset;
    r4.w = color0.z * r4.w + -yOffset;
    r4.xy = color0.xx * r4.xy + -xOffset;
    r4.z = texCoord.z * r4.z + -color0.z;
    color0.x = rcp(r4.z);
    color0.z = rcp(r4.x);
    r3.x = color0.y * color0.z;
    color0.y = rcp(r4.y);
    r3.y = r3.y * color0.y;
    color0.z = rcp(r4.w);
    r3.zw = color0.xz * texCoord.yy;
    r3 = frac(r3);
    texCoord.xy = r4.zw * r3.zw;
    color0.xz = r4.xy * r3.xy;
    texCoord.x = texCoord.x > yOffset;
    color0.y = -texCoord.w + max(texCoord.y, texCoord.y);
    color0.x = (texCoord.x == 0.0) ? color0.x : color0.z;
    color0.x = -color0.w + color0.x;
    color0.z = 0.5625f * color0.y;
    color0.x = dot(color0.xz, color0.xz) + 0.0f;
    color0.x = sqrt(abs(color0.x));
    color0.x = radius > color0.x;
    if (color0.x != 0.0)
    {
        return float4(-r2.xyz + float3(1.0f, 1.0f, 1.0f), r2.w);
    }
    else
    {
        return r2;
    }
}

technique Warp
{
    pass Pass1
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};