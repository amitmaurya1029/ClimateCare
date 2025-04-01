Shader "Custom/NeonDissolve"
{
    Properties
    {
        _BaseMap ("Base Texture", 2D) = "white" {}  
        _DissolveAmount ("Dissolve Amount", Range(0,1)) = 0  
        _EdgeWidth ("Edge Width", Range(0, 0.2)) = 0.05  
        _EdgeColor ("Edge Color", Color) = (0,1,1,1) // Cyan neon color  
        _EmissionStrength ("Emission Strength", Range(0, 10)) = 5  
        _NeonIntensity ("Neon Intensity", Range(1, 10)) = 5  
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="AlphaTest" }  

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float worldY : TEXCOORD1;
            };

            sampler2D _BaseMap;
            float _DissolveAmount;
            float _EdgeWidth;
            float4 _EdgeColor;
            float _EmissionStrength;
            float _NeonIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = TransformObjectToHClip(v.vertex);
                o.uv = v.uv;
                o.worldY = TransformObjectToWorld(v.vertex).y; // Get world Y position
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 col = tex2D(_BaseMap, i.uv);

                // Compute dissolve mask based on world Y position
                float dissolveMask = i.worldY - _DissolveAmount * 5.0; // Controls dissolve progress

                // Clip pixels below the dissolve threshold
                clip(dissolveMask);

                // Smooth dissolve edge
                float edge = smoothstep(0, _EdgeWidth, dissolveMask);

                // Neon glow effect
                float glowFactor = edge * _NeonIntensity;
                half3 glow = _EdgeColor.rgb * glowFactor * _EmissionStrength;

                // Add glow to base color
                return half4(col.rgb + glow, col.a);
            }
            ENDHLSL
        }
    }
}
