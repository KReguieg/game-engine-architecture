// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/BuildPlane"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
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
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float3 worldPos : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			fixed4 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.worldPos = mul (unity_ObjectToWorld, v.vertex).xyz;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = fixed4(0,0,0,0);
				col.y = (int)abs(i.worldPos.x) % 2 & (int)(abs(i.worldPos.x)-0.8) % 2 |
					    (int)abs(i.worldPos.z) % 2 & (int)(abs(i.worldPos.z) - 0.8) % 2 |
					    ((abs(i.worldPos.x) <= 0.1)) |
					    ((abs(i.worldPos.z) <= 0.1));
				return col;
			}
			ENDCG
		}
	}
}
