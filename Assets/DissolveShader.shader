Shader "Custom/DissolveShader"
{
    Properties
    {
        _BaseMap ("Base Texture", 2D) = "white" {}  
        _DissolveTex ("Dissolve Texture", 2D) = "white" {}  
        _DissolveAmount ("Dissolve Amount", Range(0,1)) = 0  
        _EdgeWidth ("Edge Width", Range(0, 0.2)) = 0.05  
        _EdgeColor ("Edge Color", Color) = (1,1,0,1)  
        _EmissionStrength ("Emission Strength", Range(0, 10)) = 5  
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
            };

            sampler2D _BaseMap;
            sampler2D _DissolveTex;
            float _DissolveAmount;
            float _EdgeWidth;
            float4 _EdgeColor;
            float _EmissionStrength;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = TransformObjectToHClip(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 col = tex2D(_BaseMap, i.uv);
                float dissolveValue = tex2D(_DissolveTex, i.uv).r;

                // Calculate dissolve mask
                float dissolveMask = dissolveValue - _DissolveAmount;

                // Clip pixels below the dissolve threshold
                clip(dissolveMask);

                // Glow effect on the edge
                float edge = smoothstep(0, _EdgeWidth, dissolveMask);
                half3 glow = _EdgeColor.rgb * edge * _EmissionStrength;

                return half4(col.rgb + glow, col.a);
            }
            ENDHLSL
        }
    }
}
