// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/WaterSurface" {
	Properties 
	{
		[PerRendererData] _MainTex ("SpriteTexture", 2D)= white{}
		_Color ("Tint" , Color) = (1,1,1,1) 
		[MaterialToggle] PixelSnap("Pixel Snap", Float) = 0
	}
	SubShader {
		Tags { "RenderType"="Transparent" "IgnoreProjector"="True" "Queue" = "Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
		LOD 200

		Cull Off
		Lighting On
		ZWrite Off
		Blend One OneMinusSrcAlpha

	    Pass{

			     Stencil
				{
					Ref 5
					Comp Always
					Pass Replace
				}

		    
		CGPROGRAM
	
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_PIXELSNAP_ON
			#include "UnityCG.cginc"


			#pragma target 3.0

			sampler2D _MainTex;

			struct appdata_t {
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			fixed4_Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex=UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color ;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;

				
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D(_MainTex ,  uv);

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				if(_AlphaSplitEnabled)
					color.a = tex2D(_AlphaTex,uv).r;

#endif 

				return color;
			}

	fixed4 frag(v2f IN) : SV_Target
	{
		fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
		c.rgb *= c.a;
		return c;

	}




		ENDCG
		}
	}
	FallBack "Diffuse"
}
