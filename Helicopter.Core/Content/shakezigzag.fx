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

float iTime;

// Constants  
const float startAngle = 3.14159 / 2.0;
const float endAngle = 3.0 * 3.14159 / 2.0;
const float mirrorAngle = 3.14159;
const float radius = 0.01;
const float speed = 10.0;
const float loopDuration = 3.0;
const float forwardDuration = 0.7;

// Main shader function (must be named 'PS' for pixel shaders in SM3)
float4 PS(float4 pos : SV_POSITION, float4 color0 : COLOR0, float2 texCoord : TEXCOORD0) : COLOR
{
    // Normalized coords (from -1 to 1)
    float2 iResolution = float2(1280, 720);
    float2 fragCoord = texCoord * iResolution;
    float2 uv = fragCoord / iResolution.xy;

    // Time value from uniform
    float time = iTime;

    // Incorporate speed
    float scaledTime = time * speed;

    // Loop & direction control
    float cycleProgress = fmod(scaledTime, loopDuration) / loopDuration;
    float direction = step(forwardDuration, cycleProgress);
    float progress = lerp(cycleProgress, 1.0 - cycleProgress, direction);

    // Adjustment with smoothing (smoothstep is SM3+)
    float adjustedProgress = smoothstep(0.0, 1.0, abs(progress - 0.5) * 2.0);
    float overshoot = smoothstep(0.0, 1.15, abs(progress - 0.5) * 2.0);
    overshoot = lerp(overshoot, 1.0, smoothstep(1.0, 1.15, overshoot));

    // Angles and positions tracking
    float previousAngle = startAngle;
    float angle = startAngle + adjustedProgress * (endAngle - startAngle);
    float previousYPosition = sin(previousAngle);
    float currentYPosition = sin(angle);

    // Direction change detection
    float isChangingDirection = step(0.98, abs(previousYPosition - currentYPosition));

    // Bounce effect on change
    adjustedProgress = lerp(adjustedProgress, overshoot, isChangingDirection);

    // Update angle for next frame
    previousAngle = angle;

    // Offset UVs with mirroring
    float2 offset = radius * float2(cos(mirrorAngle - angle), sin(mirrorAngle - angle));
    uv += offset;

    // Sample the input texture (requires 'tex2D' in SM3)
    float4 texColor = SpriteTexture.Sample(SpriteTextureSampler, uv);

    return texColor;
}

// Technique (adjust accordingly)
technique Technique1
{
    pass Pass1
    {
        PixelShader = compile PS_SHADERMODEL PS();
    }
}
