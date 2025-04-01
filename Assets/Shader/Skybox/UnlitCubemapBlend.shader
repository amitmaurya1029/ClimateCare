Shader "Custom/SkyboxCubemapBlend"
{
    Properties
    {
        _CubemapA ("Cubemap A", CUBE) = "white" {}
        _CubemapB ("Cubemap B", CUBE) = "white" {}
        _BlendFactor ("Blend Factor", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue" = "Background" "RenderType"="Background" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 viewDir : TEXCOORD0;
            };

            samplerCUBE _CubemapA;
            samplerCUBE _CubemapB;
            float _BlendFactor;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.viewDir = normalize(mul((float3x3)unity_ObjectToWorld, v.vertex.xyz));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 colorA = texCUBE(_CubemapA, i.viewDir);
                fixed4 colorB = texCUBE(_CubemapB, i.viewDir);
                return lerp(colorA, colorB, _BlendFactor);
            }
            ENDCG
        }
    }
}