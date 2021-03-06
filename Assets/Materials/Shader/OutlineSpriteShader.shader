﻿//script originally by Ryan Nielson (http://nielson.io/2016/04/2d-sprite-outlines-in-unity/)
// External outline added by Chris Garcia (thespinforce@gmail.com)
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Custom/OutlineSprite"
{
	Properties
	{
		_Color("Tint", Color) = (1, 1, 1, 1)

		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0

		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		[PerRendererData] _OutlineColor("Outline Color", Color) = (1,1,1,1)
		[PerRendererData] _OutlineSize("Outline Size", int) = 1
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		//Blend SrcAlpha One

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"

			/* parameter */
			fixed4 _Color;
			int _OutlineSize;
			fixed4 _OutlineColor;
			sampler2D _MainTex;
			float4 _MainTex_TexelSize;

			struct appdata_t /* vertex */
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f /*pxelshader*/
			{
				float4 vertex   : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord  : TEXCOORD0;
			};
			
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
		#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap(OUT.vertex);
		#endif

				return OUT;
			}
			
			//TODO: needed?
			//sampler2D _AlphaText 

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, IN.texcoord) * IN.color;

				
				c.rgb *= c.a;
				return c;
			}

			ENDCG
		}
	}
}
