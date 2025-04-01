Shader "Custom/GlowingDissolveURP"
{
    Properties
    {
        _BaseMap ("Base Texture", 2D) = "white" {}
        _DissolveTexture ("Dissolve Noise", 2D) = "white" {}
        _DissolveAmount ("Dissolve Amount", Range(0, 1)) = 0
        _EdgeColor ("Edge Glow Color", Color) = (1,1,0,1)
        _EdgeWidth ("Edge Width", Range(0, 0.2)) = 0.05
        _AmbientIntensity ("Ambient Light Intensity", Range(0, 1)) = 0.5
        _Tiling ("Texture Tiling", Vector) = (1, 1, 0, 0) // New property for tiling (X and Y)
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Name "DissolvePass"
            Tags { "LightMode"="UniversalForward" }
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma require lighting
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldNormal : NORMAL;
                float3 worldPos : TEXCOORD1;
            };

            TEXTURE2D(_BaseMap); SAMPLER(sampler_BaseMap);
            TEXTURE2D(_DissolveTexture); SAMPLER(sampler_DissolveTexture);

            float _DissolveAmount;
            float4 _EdgeColor;
            float _EdgeWidth;
            float _AmbientIntensity;
            float4 _Tiling; // Tiling vector

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = TransformObjectToHClip(v.vertex);
                o.uv = v.uv;
                o.worldNormal = TransformObjectToWorldNormal(v.normal);
                o.worldPos = TransformObjectToWorld(v.vertex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // Apply tiling to texture coordinates for both textures
                float2 tiledUV = i.uv * _Tiling.xy;

                // Sample base texture with tiling applied
                half4 baseColor = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, tiledUV);
                // Sample dissolve texture with tiling applied
                float dissolveValue = SAMPLE_TEXTURE2D(_DissolveTexture, sampler_DissolveTexture, tiledUV).r;
                
                // Create dissolve effect
                float edgeThreshold = _DissolveAmount - _EdgeWidth;
                float dissolveAlpha = smoothstep(edgeThreshold, _DissolveAmount, dissolveValue);
                
                // Apply glowing effect on edges
                float edgeGlow = smoothstep(_DissolveAmount, _DissolveAmount + _EdgeWidth, dissolveValue);
                half4 finalColor = lerp(_EdgeColor, baseColor, edgeGlow);
                
                // Lighting calculation with ambient light
                Light mainLight = GetMainLight();
                float3 normal = normalize(i.worldNormal);
                float3 lightDir = normalize(mainLight.direction);
                float diff = max(dot(normal, lightDir), 0.0);
                float3 ambientLight = _AmbientIntensity * mainLight.color;
                finalColor.rgb *= (diff * mainLight.color + ambientLight);
                
                finalColor.a *= dissolveAlpha;
                
                clip(finalColor.a - 0.01); // Clipping for performance
                return finalColor;
            }
            ENDHLSL
        }
    }
}
