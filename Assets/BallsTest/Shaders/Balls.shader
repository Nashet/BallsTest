Shader "Custom\Balls"
{
	Properties
	{
		[PerRendererData]_MainTex("_MainTex", 2D) = "white" {}
		_LighteningMultiplier("_LighteningMultiplier", Float) = 0.4 // just a linear multiplier
		_VerticalScaler("_VerticalScaler", Float) = 130  //controls how fast color will change relatively to Y axis
	}
		SubShader
		{
			Pass
			{
			Name "Balls"

			Tags
			{
				"Queue" = "Transparent"
				"RenderType" = "Transparent"
			}
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex   MyVertexShaderFunction 
			#pragma fragment  MyFragmentShaderFunction			 
			#include "UnityCG.cginc"

			sampler2D _MainTex;

			half _LighteningMultiplier, _VerticalScaler;


			// custom struct recieving data from unity
			struct my_needed_data_from_unity
			{
				float4 vertex   : POSITION;  // The vertex position in model space.          
				float4 texcoord : TEXCOORD0; // The first UV coordinate.                     
				float4 color    : COLOR;     //    The color value of this vertex specifically. 
			};

			// custom Vertex to Fragment struct
			struct my_v2f
			{
				float4  pos : SV_POSITION;
				float2  uv : TEXCOORD0;
				float4  color : COLOR;
				float4 screenPos : TEXCOORD1;
			};

			my_v2f  MyVertexShaderFunction(my_needed_data_from_unity  v)
			{
				my_v2f  result;
				result.pos = UnityObjectToClipPos(v.vertex);
				result.uv = v.texcoord.xy;
				result.color = v.color;
				result.screenPos = ComputeScreenPos(v.vertex);
				result.screenPos.xyz /= result.screenPos.w;
				return result;
			}

			float4 MyFragmentShaderFunction(my_v2f  i) : COLOR
			{
				float4 texcolor = tex2D(_MainTex, i.uv); // texture's pixel color 
				float4 vertexcolor = i.color; // this is coming from UnityEngine.UI.Image.Color

				float lightening = i.screenPos.y / _VerticalScaler;

				lightening += 1;// to be positive

				float3 whiteColor = (1.0, 1.0, 1.0);

				texcolor.rgb = vertexcolor.rgb + whiteColor * lightening * _LighteningMultiplier; // keeps alpha from texture

				return texcolor;
			}

			ENDCG
		}
	}
	Fallback "Diffuse"
}