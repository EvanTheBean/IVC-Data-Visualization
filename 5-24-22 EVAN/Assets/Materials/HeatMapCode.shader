Shader "Unlit/HeatMapCode"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            float4 _MainTex_ST;

            float4 _Color0;
            float4 _Color1;
            float4 _Color2;
            float4 _Color3;
            float4 _Color4;


            float _Range0;
            float _Range1;
            float _Range2;
            float _Range3;
            float _Range4;
            float _Diameter;
            float _Strength;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float4 colors[5];
            float pointranges[5];

            float _Hits[3 * 32];
            float _HitCount = 0;

            void initalize()
            {
                colors[0] = float4(0,0,0,0);
                colors[1] = float4(1, 0, 0, 1);
                colors[2] = float4(0, 1, 0, 1);
                colors[3] = float4(0, 0, 1, 1);
                colors[4] = float4(1, 1, 1, 1);
                pointranges[0] = 0;
                pointranges[1] = 0.25;
                pointranges[2] = 0.5;
                pointranges[3] = 0.75;
                pointranges[4] = 1.0;

                _HitCount = 1;
                _Hits[0] = 0;
                _Hits[1] = 1;
                _Hits[2] = 2;
            }

            float distsq(float2 a, float2 b)
            {
                float area_of_effect_size = 1.0f;

                return  pow(max(0.0, 1.0 - distance(a, b) / area_of_effect_size), 2.0);
            }

            float4 pixelHeat(float weight)
            {
                if (weight <= pointranges[0])
                {
                    return colors[0];
                }
                
                if (weight >= pointranges[4])
                {
                    return colors[4];
                }

                for (int i = 1; i < 5; i++)
                {
                    if (weight < pointranges[i])
                    {
                        float dist_from_lower_point = weight - pointranges[i - 1];
                        float size_of_point_range = pointranges[i] - pointranges[i - 1];
                        float ratio = dist_from_lower_point / size_of_point_range;
                        float4 color_range = colors[i] - colors[i - 1];
                        float4 color_contribution = color_range * ratio;

                        return colors[i - 1] + color_contribution;
                    }
                }

                return colors[0];
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

            float2 uv = i.uv;
            uv = uv * 4 - float2(2.0, 2.0);

            float totalWeight = 0;
            for (int i = 0; i < _HitCount; i++)
            {
                float pt_intensity = _Hits[i * 3 + 2];
                float2 work_pt = float2(_Hits[i * 3], _Hits[i*3 +1]) * pt_intensity;

                totalWeight += 0.5 * distsq(uv, work_pt) * pt_intensity;

            }

            float4 heat = pixelHeat(totalWeight);

            return col + heat;
            }
            ENDCG
        }
    }
}
