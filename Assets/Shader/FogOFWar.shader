Shader "Custom/UnlitFogOFWar"
{
	Properties
	{
        _Color ("Main Color", Color) = (1,1,1,1)
    	_FogRadius ("FogRadius", Float) = 1.0
    	_FogMaxRadius ("FogMaxRadius", Float) = 0.5
    	_ZLayer ("RenderOnZLayer", Float) = 0.5
		_Player ("Player", Vector) = (0,0,0,1)
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
    	Blend SrcAlpha OneMinusSrcAlpha
		ZWrite OFF

		Pass
		{
			CGPROGRAM
			#pragma vertex vert 
			#pragma fragment frag
			
		
			fixed4 _Color;
			half4 _Player;
			half _FogRadius;
			half _FogMaxRadius;
			half _ZLayer;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				half4 _vertex : SV_POSITION;
				fixed4 _color : COLOR;
			};

			float powerForPos(half4 pos, half2 nearVertex) { // make Cone to Circle for performance
	 			float attenumat = clamp(_FogRadius - abs(length(pos.xz - nearVertex.xy)), 0.0, _FogRadius);
	 			if(attenumat > _FogRadius*_FogMaxRadius) {
	 				attenumat = _FogRadius*_FogMaxRadius;  
	 			}
	    		return (1.0/_FogMaxRadius)*(attenumat/_FogRadius);
	    	}
			
			v2f vert (appdata v, v2f outData) 
			{
				
				half2 posVertex = mul (unity_ObjectToWorld, v.vertex).xz;
				outData._vertex = UnityObjectToClipPos(v.vertex);
				outData._vertex.z = _ZLayer;
				outData._color = _Color;
		    	outData._color.a = (1.0 - clamp(_Color.a + powerForPos(_Player, posVertex) + powerForPos(half4(0,0,0,0), posVertex), 0, 1.0));
		    	return outData;
			}
			
			fixed4 frag (in v2f i ) : COLOR
			{
				return i._color;
			}
			ENDCG
		}
	}
}
