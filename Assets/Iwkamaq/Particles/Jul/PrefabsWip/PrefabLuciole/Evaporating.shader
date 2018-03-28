Shader "Custom/Evaporating"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)
		_GradientTex("Gradient Texture", 2D) = "white" {}
		_GradientThreshold("Gradient Threshold", float) = 0.5
		_NoiseTex("Noise Texture", 2D) = "white" {}
		_UVOffset("UV Offset", vector) = (1, 1, 1, 1)
		_Speed("Speed", float) = 1.0
		_Zoom("Zoom", float) = 1.0
		_ShapeMask("Shape Texture", 2D) = "white" {}
		
		_Shine_R("Shine R", float) = 1.0
		_Shine_G("Shine G", float) = 1.0
		_Shine_B("Shine B", float) = 1.0
		_Shine2_R("Shine R", float) = 1.0
		_Shine2_G("Shine G", float) = 1.0
		_Shine2_B("Shine B", float) = 1.0

		_TimeOffset("Time Offset", float) = 0.0
	}

	SubShader
	{
        Tags
		{ 
			"Queue" = "Transparent"
		}

		Pass
		{
			ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			// Properties
			sampler2D _MainTex;
			float4 _Color;
			sampler2D _GradientTex;
			float _GradientThreshold;
			sampler2D _NoiseTex;
			float _Speed;
			float _Zoom;
			float4 _UVOffset;
			sampler2D _ShapeMask;
			float _Shine_R;
			float _Shine_G;
			float _Shine_B;
			float _Shine2_R;
			float _Shine2_G;
			float _Shine2_B;
			float _TimeOffset;

			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 texCoord : TEXCOORD0;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float3 texCoord : TEXCOORD0;
			};

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;
				output.pos = UnityObjectToClipPos(input.vertex); 
				output.texCoord = input.texCoord;
				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{
				// the main texture rgba
				float4 albedo = tex2D(_MainTex, input.texCoord.xy);
				// the base color
				float4 col = float4(albedo.rgba * _Color.rgba);
				// noisePadding will be used to zoom and change the offsets of the noise texture (so that different objects can look more random)
				// to randomize the padding from the gameObject script: material.SetVector("_UVOffset", new Vector2(Random.Range(0, 1f), Random.Range(0, 1f)));
				float2 noisePadding = _Zoom * float2(input.texCoord.x + _UVOffset.x, input.texCoord.y +  _UVOffset.y);
				// the gradient texture
				float gradient = tex2Dlod(_GradientTex, float4(input.texCoord.xy, 0, 0));
				// the noise texture padded with the noisePadding and moving up with _Time.y*_Speed
				float noise = tex2Dlod(_NoiseTex, float4(noisePadding.x, noisePadding.y-_Time.y*_Speed, 0, 0));
				// for gradient and noise we can get only one color channel since these textures are just grayscale

				// choosing what will be transparent
				if(noise < (1.0f - gradient.r) * _GradientThreshold) col.a=1;
				else col.a=0;
				
				// color channels multiplier (to change colors near the disappearing areas)
				col.r *= 1.0f + gradient.r * _Shine2_R + gradient.r * (1.0f - noise) *_Shine_R;
				col.g *= 1.0f + gradient.r * _Shine2_G + gradient.r * (1.0f - noise) *_Shine_G;
				col.b *= 1.0f + gradient.r * _Shine2_B + gradient.r * (1.0f - noise) *_Shine_B;
				
				// masking texture (try changing those magic numbers to change the wobbly effect)
				// _TimeOffset is used to make different objects look different
				// to randomize the displacing effect from the gameObject script: material.SetFloat("_TimeOffset", Random.Range(0,1.0f));
				float4 shape = tex2D(_ShapeMask,  float4(input.texCoord.x + 0.4 * gradient.r * sin((_Time.y + _TimeOffset) * 4 + input.texCoord.y * 2) * noise, input.texCoord.y, 0, 0));
				// applying the masking texture to the alpha channel
				col.a *= shape.a;
				
				return col;
			}

			ENDCG
		}
	}
}