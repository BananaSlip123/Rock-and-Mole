

#pragma multi_compile _ _SHADOWS_SOFT
#pragma multi_compile _ _ADDITIONAL_LIGHTS
#pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS


#ifndef ADDITIONAL_LIGHT_INCLUDED
#define ADDITIONAL_LIGHT_INCLUDED

#ifndef definedSHADERGRAPH_PREVIEW
struct EdgeConstants
{
    float diffuse;
    float specular;
    float specularOffset;
    float distanceAttenuation;
    float shadowAttenuation;
    float rim;
    float rimOffset;
};

struct SurfaceVariable
{
    float3 normal;
    float3 view;
    float smoothness;
    float shininess;
    float rimThreshold;
    float3 position;
    EdgeConstants ec;
};


float3 CalculateCelShading(Light l, SurfaceVariable s)
{
    float shadowAttenuation = smoothstep(0.0f, s.ec.shadowAttenuation, l.shadowAttenuation);
    float distanceAttenuation = smoothstep(0.0f, s.ec.distanceAttenuation, l.distanceAttenuation);
    
    float attenuation = shadowAttenuation * distanceAttenuation;
    
    float diffuse = saturate(dot(s.normal, l.direction));
    diffuse *= attenuation;
    

    
    float3 h = SafeNormalize(l.direction + s.view);
    float specular = saturate(dot(s.normal, h));
    specular = pow(specular, s.shininess);
    specular *= diffuse * s.smoothness;
    

    
    float rim = 1 - dot(s.view, s.normal);
    rim *= pow(diffuse, s.rimThreshold);
    
    diffuse = smoothstep(0.0f, s.ec.diffuse, diffuse);
    specular = s.smoothness * smoothstep((1 - s.smoothness) * s.ec.specular + s.ec.specularOffset, s.ec.specular + s.ec.specularOffset, specular);
    rim = s.smoothness * smoothstep(s.ec.rim - 0.5f * s.ec.rimOffset, s.ec.rim + 0.5f * s.ec.rimOffset,rim);
    
    return l.color * (diffuse + max(specular, rim));
}
#endif

void LightingCelShaded_float(float3 WorldNormal, float Smoothness, float3 View, float RimThreshold, float3 WorldPosition, 
    float EdgeDiffuse, float EdgeSpecular, float EdgeSpecularOffset, float EdgeDistanceAttenuation,
    float EdgeShadowAttenuation, float EdgeRim, float EdgeRimOffset, out float3 Color)
{
#if defined(SHADERGRAPH_PREVIEW)
    Color = float3(0.5f, 0.5f, 0.5f);
#else
    SurfaceVariable su;
    su.normal = normalize(WorldNormal);
    su.view = SafeNormalize(View);
    su.smoothness = Smoothness;
    su.shininess = exp2(10*Smoothness+1);
    su.rimThreshold = RimThreshold;
    su.position = WorldPosition;
    
    EdgeConstants ed;
    ed.diffuse = EdgeDiffuse;
    ed.specular = EdgeSpecular;
    ed.specularOffset = EdgeSpecularOffset;
    ed.distanceAttenuation = EdgeDistanceAttenuation;
    ed.shadowAttenuation = EdgeShadowAttenuation;
    ed.rim = EdgeRim;
    ed.rimOffset = EdgeRimOffset;
    
    su.ec = ed;
    
#if SHADOWS_SCREEN
    float4 clipPos = TransformWorldToHClip(WorldPosition);
    float4 shadowCoord = ComputeScreenPos(clipPos);
#else
    float4 shadowCoord = TransformWorldToShadowCoord(WorldPosition);
#endif
    
    Light mainLight = GetMainLight(shadowCoord);
    Color = CalculateCelShading(mainLight, su);
    
    int pixelLightCount = GetAdditionalLightsCount();
    for (int i = 0; i < pixelLightCount; i++)
    {
        mainLight = GetAdditionalLight(i, WorldPosition, 1);
        Color += CalculateCelShading(mainLight, su);
    }
    
 #endif
}

#endif // ADDITIONAL_LIGHT_INCLUDED