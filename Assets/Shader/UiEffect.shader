Shader "Custom/UiEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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


			fixed4 frag (v2f i) : SV_Target
			{
				float size = 50;
				float swipePoint = (_Time.y / 5 % 1) * (_ScreenParams.x + _ScreenParams.y) * 2;
				float dist = clamp(abs(swipePoint - (i.vertex.x +  i.vertex.y)),0 , size);
				float val = 1-(dist / size);
				fixed4 col = fixed4(val, val, val, 1);
				return col;
			}
			ENDCG
		}
	}
}
