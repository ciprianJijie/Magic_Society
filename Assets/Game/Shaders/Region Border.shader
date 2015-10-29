Shader "MS/Region Border"
{
	Properties
	{
		_PrimaryColor 	("Primary Color", Color) 	= 	(1.0, 1.0, 1.0, 1.0)
		_SecondaryColor ("Secondary Color", Color) 	= 	(1.0, 1.0, 1.0, 1.0)
	}

	SubShader
	{
		Tags
		{
			"RenderType" = "Opaque"
		}

		CGPROGRAM
		#pragma surface surf Lambert
		struct Input
		{
			float4 color : COLOR;
		};

		float4 _PrimaryColor;
		float4 _SecondaryColor;

		void surf (Input IN, inout SurfaceOutput o)
		{
			o.Albedo = _PrimaryColor.rgb;
		}

		ENDCG
	}

	Fallback "Diffuse"
}
