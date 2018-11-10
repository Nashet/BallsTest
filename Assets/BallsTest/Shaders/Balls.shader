Shader "Custom\Balls"
{
	Properties
	{
		_MainTex("_MainTex", 2D) = "white" {}      // Note _MainTex is a special name: This can also be accessed from C# via mainTexture property. 			
		_ScreenHeight("_ScreenHeight", Float) = 200.0
		_LighteningMultiplier("_LighteningMultiplier", Float) = 0.0002
	}
		SubShader
		{
			Pass
			{
			Name "Balls"

			// ---
			// For Alpha transparency:   https://docs.unity3d.com/462/Documentation/Manual/SL-SubshaderTags.html
			Tags
			{
				"Queue" = "Transparent"
				"RenderType" = "Transparent"
			}
			Blend SrcAlpha OneMinusSrcAlpha
			// ---

			CGPROGRAM
			#pragma vertex   MyVertexShaderFunction 
			#pragma fragment  MyFragmentShaderFunction
			#pragma fragmentoption ARB_precision_hint_fastest 
			#include "UnityCG.cginc"

			sampler2D _MainTex;

			float Colorize;
			half _ScreenHeight, _LighteningMultiplier;

			// http://wiki.unity3d.com/index.php/Shader_Code : 
			// There are some pre-defined structs e.g.: v2f_img, appdata_base, appdata_tan, appdata_full, v2f_vertex_lit
			//
			// but if you want to create a custom struct, then the see Acceptable Field types and names at http://wiki.unity3d.com/index.php/Shader_Code 
			// my custom struct recieving data from unity
			struct my_needed_data_from_unity
			{
				float4 vertex   : POSITION;  // The vertex position in model space.          //  Name&type must be the same!
				float4 texcoord : TEXCOORD0; // The first UV coordinate.                     //  Name&type must be the same!
				float4 color    : COLOR;     //    The color value of this vertex specifically. //  Name&type must be the same!
			};

			// my custom Vertex to Fragment struct
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

				float lightening = (i.screenPos.y + _ScreenHeight / 6.0) / _LighteningMultiplier; // shifts gradient start from center to bottom

				lightening += 0.2;
				//lightening = max(0, lightening);

				float3 whiteColor = (1.0, 1.0, 1.0);

				texcolor.rgb = vertexcolor.rgb + whiteColor * lightening; // keeps alpha from texture
				return texcolor;
			}

			ENDCG
		}
		}
			Fallback "Diffuse"
}