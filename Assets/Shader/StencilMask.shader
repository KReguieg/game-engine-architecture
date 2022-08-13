/*
Tutorial from Daniel Ilett
https://www.youtube.com/watch?v=EzM8LGzMjmc
*/

Shader "Custom/Stencil/Mask"
{
	Properties
	{
		[IntRange] _StencilValue ("Stencil Value", Range(0 ,255)) = 0	
	}

	SubShader{

		Tags {
			"RenderType" = "Opaque"
			"RenderPipeline" = "UniversalPipeline"
			"Queue" = "Geometry"
		}

		Pass {
			Blend Zero One
			ZWrite Off
			
			Stencil
			{
				Ref [_StencilValue]
				Comp Always
				Pass Replace
				Fail Keep
			}
		}
	}
}