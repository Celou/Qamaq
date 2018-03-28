// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "CodeArtist.mx/Sprites/Foliage"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_ExtraColor ("ExtraColor", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_SpeedX ("SpeedX",Float)=1.0
    	_SpeedY ("SpeedY",Float)=1.0
    	_Intensity ("Stiffness",Float)=16.0
	    _NoiseATex ("NoiseA", 2D) = "white" { }
   		_NoiseBTex ("NoiseB", 2D) = "white" { }
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend One OneMinusSrcAlpha
	
		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _Color,_ExtraColor;
			sampler2D _NoiseATex,_NoiseBTex;
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			float _SpeedX,_SpeedY,_Intensity;
			fixed4 frag(v2f i) : SV_Target
			{
				
				fixed4 noiseAcol= tex2D(_NoiseATex, i.texcoord) ;
				fixed4 noiseBcol= tex2D(_NoiseBTex, i.texcoord) ;
				//fixed4 noiseAcol=tex2D (_NoiseATex, i.uv);
				//half4 noiseBcol=tex2D (_NoiseBTex, i.uv);
				
				
				fixed2 movedUV=i.texcoord;	
				fixed2 time;
				time.x=_Time.y*_SpeedX;
				time.y=_Time.y*_SpeedY;
			    movedUV.x+=(noiseAcol.r)*(sin((time.x)+4.0)/_Intensity);
			    movedUV.y+=(noiseAcol.r)*(sin((time.y)+4.0)/_Intensity);
			    movedUV.x+=(noiseBcol.r)*(cos((time.x)+6.0)/_Intensity);
			    movedUV.y+=(noiseBcol.r)*(cos((time.y)+6.0)/_Intensity);	   
				//half4 texcol=tex2D (_MainTex, movedUV);
				//texcol=texcol*_Color;
				fixed4 c = tex2D(_MainTex, movedUV) * i.color;
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
		
	}
}
