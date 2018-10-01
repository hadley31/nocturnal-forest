Shader "Hidden/NewImageEffectShader"
{
	Properties
	{
		_MainTex ("Screen Texture", 2D) = "white" {} //Do not set in unity
		_DayColor("Day Color", Color) = (0,0,0,0)
		_EveningColor("Evening Color", Color) = (0,0,0,0)
		_NightColor ("NightColor", Color) = (0,0,0,0)
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			fixed4 _DayColor;
			fixed4 _NightColor;
			fixed4 _EveningColor;
			uniform float blendFactor;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				fixed4 col1 = col * _DayColor;
				fixed4 col2 = col * _NightColor;
				fixed4 col3 = col * _EveningColor;

				fixed4 finalCol;

				if (blendFactor < 1.0)
				{
					finalCol = lerp(col1, col3, blendFactor);
				}
				else if (blendFactor >= 1.0 && blendFactor < 2.0)
				{
					finalCol = lerp(col3, col2, blendFactor-1.0);
				}
				else if (blendFactor >= 2.0)
				{
					finalCol = lerp(col2, col1, blendFactor - 2.0);
				}
				
				return finalCol;
			}
			ENDCG
		}
	}
}
