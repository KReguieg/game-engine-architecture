// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/FogOfWarGeom" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
    	_FogRadius ("FogRadius", Float) = 1.0
    	_FogMaxRadius ("FogMaxRadius", Float) = 0.5
		_Player ("Player", Vector) = (0,0,0,1)
	}

	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
    	Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		Cull Off 

		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Lambert vertex:vert alpha:blend
		
		sampler2D _MainTex;
		fixed4 _Color;
		half4 _Player;
		half _FogRadius;
		half _FogMaxRadius;
	
		struct Input {
	    	half alpha;
		};
		
		float powerForPos(half4 pos, half2 nearVertex) {
 			float attenumat = clamp(_FogRadius - abs(length(pos.xz - nearVertex.xy)), 0.0, _FogRadius);
 			if(attenumat > _FogRadius*_FogMaxRadius) {
 				attenumat = _FogRadius*_FogMaxRadius;  
 			}
    		return (1.0/_FogMaxRadius)*(attenumat/_FogRadius);
    	}
		
		void vert (inout appdata_base vertexData, out Input outData)
		{
		    half2 posVertex = mul (unity_ObjectToWorld, vertexData.vertex).xz;
		    outData.alpha = (1.0 - clamp(_Color.a 
        				+ ( powerForPos(_Player, posVertex) 
        				+ powerForPos(half4(0,0,0,0), posVertex) ), 0, 1.0));
		}
		
		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = _Color.rgb;
        	o.Alpha = IN.alpha;
    	}

		ENDCG
	}

Fallback "Transparent/VertexLit"
}
