// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/VertColorWithShadow"
{
	Properties
	{
		_MainColor("Main Color", COLOR) = (0,0,0,0)
		_ShadowColor("Shadow Color", COLOR) = (1,0,0,1)
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque" 
		"Queue" = "Geometry-1" }
		LOD 100

	Pass
	{ 
		Tags{ "LightMode" = "ForwardBase" }

		CGPROGRAM
	
	#pragma multi_compile_fwdbase
	#pragma vertex vert
	#pragma fragment frag

	#include "UnityCG.cginc"
	#include "AutoLight.cginc"

	float4 _MainColor;
	float4 _ShadowColor;

	struct appdata
	{
		float4 vertex : POSITION;
	};

	struct v2f
	{
		float4 pos : SV_POSITION;
		UNITY_VERTEX_OUTPUT_STEREO
		float4 mainColor : COLOR;
		LIGHTING_COORDS(1,2)
			
	};

	v2f vert(appdata v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.mainColor = _MainColor;
		TRANSFER_VERTEX_TO_FRAGMENT(o);

		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{

		fixed atten = 1.0 - LIGHT_ATTENUATION(i);
	if (atten > 0)
	{
		fixed3 shadowColor = atten * _ShadowColor.rgb;
		//fixed4 finalColor = i.mainColor + fixed4(shadowColor, 1.0);
		fixed4 finalColor;
		if (atten >= 1)
			finalColor = fixed4(1, 0, 0, atten);
		else if (atten >= 0.9)
			finalColor = fixed4(0, 0, 1, atten);
		else if (atten >= 0.8)
			finalColor = fixed4(1, 0, 1, atten);
		else if (atten >= 0.7)
			finalColor = fixed4(0, 1, 1, atten);
		else if (atten >= 0.6)
			finalColor = fixed4(1, 1, 1, atten);
		else if (atten >= 0.5)
			finalColor = fixed4(0.5, 1, 0.5, atten);
		else if (atten >= 0.4)
			finalColor = fixed4(0, 1, 0, atten);
		else if (atten >= 0.3)
			finalColor = fixed4(1, 1, 0, atten);
		else if (atten >= 0.2)
			finalColor = fixed4(0.5, 0.5, 0.5, atten);
		else if (atten >= 0.1)
			finalColor = fixed4(0.5, 0.5, 1, atten);
		else if (atten > 0)
			finalColor = fixed4(1, 0.5, 0.5, atten);

		return finalColor;
	}
	else
	{

		return float4(0, 0, 0, 0);
	}
	}
		ENDCG
	}
	
		// Pass to render object as a shadow caster
		Pass
	{
		Name "ShadowCaster"
		Tags{ "LightMode" = "ShadowCaster" }

		ZWrite On ZTest LEqual Cull Off

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_shadowcaster
#include "UnityCG.cginc"

		struct v2f {
		V2F_SHADOW_CASTER;
	};

	v2f vert(appdata_base v)
	{
		v2f o;
		TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
			return o;
	}

	float4 frag(v2f i) : SV_Target
	{
		return float4(1.0, 1.0, 1.0, 1.0);
	}
		ENDCG
	}
	}
}

