#ifndef ADDITIONAL_LIGHT_INCLUDED
#define ADDITIONAL_LIGHT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

void MainLight_float(float3 WorldPos, out float3 Direction, out float3 Color, out float DistanceAtten, out float ShadowAtten)
{
#ifdef SHADERGRAPH_PREVIEW
     Direction = normalize(float3(1.0f, 1.0f, 0.0f));
     Color = 1.0f;
     DistanceAtten = 1.0f;
    ShadowAtten = 1.0f;
#else
    Light mainLight = GetMainLight();
    Direction = mainLight.direction;
    Color = mainLight.color;
    DistanceAtten = mainLight.distanceAttenuation;
    ShadowAtten = mainLight.shadowAttenuation;
#endif
}

void AdditionalLight_float(float3 WorldPos, float3 WorldNormal, float2 CutoffThresholds, out float3 LightColor)
{
    LightColor = 0.0f;

#ifndef SHADERGRAPH_PREVIEW
    int lightCount = GetAdditionalLightsCount();

    for (int i = 0; i < lightCount; ++i)
    {
        Light light = GetAdditionalLight(i, WorldPos);

        float3 color = dot(light.direction, WorldNormal);
        color = saturate(color);
        color *= light.color;
        color *= light.distanceAttenuation * light.shadowAttenuation;
        
        LightColor += color;
    }
#endif
}

void AdditionalLightSpecular_float(float3 WorldPos, float3 WorldNormal, float3 ViewDirection, float Smoothness, out float3 LightColor)
{
    LightColor = 0.0f;

#ifndef SHADERGRAPH_PREVIEW
    int lightCount = GetAdditionalLightsCount();

    for (int i = 0; i < lightCount; ++i)
    {
        Light light = GetAdditionalLight(i, WorldPos);

        float3 direction = light.direction + ViewDirection;
        direction = normalize(direction);
        float3 color = dot(direction, WorldNormal);
        color = saturate(color);
        
        float dirWorldNormal = dot(light.direction,WorldNormal);
        dirWorldNormal = step(0, dirWorldNormal);
        
        color *= dirWorldNormal;
        color = pow(color, Smoothness);
             
        color *= light.color;
        color *= light.distanceAttenuation * light.shadowAttenuation;
        

        LightColor += color;
    }
#endif
}
#endif // ADDITIONAL_LIGHT_INCLUDED