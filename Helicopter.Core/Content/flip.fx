#if OPENGL
    #define SV_POSITION POSITION
    #define VS_SHADERMODEL vs_3_0
    #define PS_SHADERMODEL ps_3_0
#else
    #define VS_SHADERMODEL vs_4_0_level_9_1
    #define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
float iTime;
float rSpeed;
bool repeat;
bool negative;

sampler SpriteTextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};

float4 MainPS(float4 pos : SV_POSITION, float4 color0 : COLOR0, float2 texCoord : TEXCOORD0) : COLOR
{
    float2 uv = texCoord;
    float perRotateTime = 3.14159 / rSpeed;
    float endRotateTime = perRotateTime;
    float angle = iTime * rSpeed;
    float2 cod = float2((uv.y - 0.5) / cos(angle) + 0.5, uv.x);
    float4 color = float4(0.0, 0.0, 0.0, 1.0);
	
    if (angle > 3.14159 && repeat) //repeat
    {
        angle = 0.0;
    }
    if (iTime > endRotateTime && !repeat)
    {
        color = SpriteTexture.Sample(SpriteTextureSampler, float2(uv.x, 1.0 - uv.y));
    }
    else if (cod.x <= 1.0 && cod.x >= 0.0)
    {
        if (angle <= 1.5707)
        {
            color = SpriteTexture.Sample(SpriteTextureSampler, float2(cod.y, cod.x));
        }
        else if (angle <= 3.14159)
        {
            color = SpriteTexture.Sample(SpriteTextureSampler, float2(cod.y, cod.x));
        }
    }
    else
    {
        color = float4(0.0, 0.0, 0.0, 1.0);
    }
    
    if (negative)
    {
        return float4(1.0 - color.r, 1.0 - color.g, 1.0 - color.b, color.a);
    }
    else
    {
        return color;
    }
   
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};
