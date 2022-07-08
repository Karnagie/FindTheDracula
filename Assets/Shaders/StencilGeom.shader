Shader "Examples/Stencil"
{
    Properties
    {
		[IntRange] _StencilID ("Stencil ID", Range(0, 255)) = 0
    }
	SubShader
    {
    	UsePass "Universal Render Pipeline/Complex Lit/SHADOWCASTER"
    	
        Tags 
		{ 
			"RenderType" = "Opaque"
			"Queue" = "Geometry"
			"RenderPipeline" = "UniversalPipeline"
		}

        Pass
        {
			Blend Zero One
			ZWrite Off

			Stencil
			{
				Ref [_StencilID]
				Comp Always
				Pass Replace
				Fail Keep
			}
        }
    }
}


