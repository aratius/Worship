Shader "Unlit/MemberProgress"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BaseColor ("BaseColor", Color) = (1., 1., 1., 1)
        _Progress ("Progress", float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            #define PI 3.14159265

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            fixed4 _BaseColor;
            float4 _MainTex_ST;
            float _Progress;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed dist = length(i.uv - fixed2(.5, .5));
                if(dist > .5 || dist < .45) discard;
                fixed angle = atan2(i.uv.y - .5, i.uv.x - .5);
                angle -= PI * .5;
                angle *= -1;
                angle = frac(angle / (PI * 2.)) * PI * 2.;
                fixed angleNormalized = angle / (PI * 2.);
                if(angleNormalized > _Progress) discard;

                fixed4 col = _BaseColor;
                return col;
            }
            ENDCG
        }
    }
}
