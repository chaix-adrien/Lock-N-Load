Shader "Custom/Offset" {

Properties {
    _Color("Tint (RGBA)", Color) = (1,1,1,1)
    [NoScaleOffset] _MainTex ("Texture (RGBA)", 2D) = "white" {}
    _Offset("Offset", Vector) = (1, 1, 0, 0)
}

SubShader
{
    Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector" = "True" "PreviewType"="Plane" }

    //subshaders are used for compatibility. If the first subshader isn't compatible, it'll attempt to use the one below it.
    Pass
    {

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        ZTest LEqual
        Lighting Off

        CGPROGRAM
            //begin CG block

            #pragma vertex vert
            //we will use a vertex function, named "vert". vert_img is defined in UnityCG.cginc

            #pragma fragment frag
            //we will use a fragment function, named "frag"

            #include "UnityCG.cginc"
            //use a CGInclude file defining several useful functions, including our vertex function

            //declare our external properties
            uniform fixed4 _Color;
            uniform sampler2D _MainTex;
            uniform half2 _Offset;

            //declare input and output structs for vertex and fragment functions

            struct appdata
            {
                float4 vertex : POSITION;
                half2  texcoord : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                half2  uv : TEXCOORD0;
                float4 color : COLOR;
            };

            v2f vert( appdata v )
            {
                v2f o;
                o.pos = UnityObjectToClipPos (v.vertex);
                o.uv = v.texcoord + _Time.xx * _Offset;
				o.color = v.color;
                return o;
            }
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;
#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				return color;
			}


            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 c = SampleSpriteTexture (i.uv) * i.color;
				c.rgb *= c.a;
                return c; 
            }
        ENDCG
    }
}
Fallback "Diffuse" //If all of our subshaders aren't compatible, use subshaders from a different shader file
}