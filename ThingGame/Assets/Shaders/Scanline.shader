Shader ",door/Scanline" {

	Properties {
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Cols("Columns", Float) = 360
		_Lines("Lines", Float) = 180
		_Intensity("Intensity", Range(0, 1)) = 0.3
	}
	
	SubShader {
		Pass {
			ZTest Always
			Cull Off
			ZWrite Off
			Fog { Mode Off }
			
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _Cols;
			uniform float _Lines;
			uniform float _Intensity;

			fixed4 scanline(fixed2 uv)
			{
				fixed row = uv.y * _Lines + 0.5;
				fixed occlude = clamp(abs(1.0 - fmod(row, 2.0)), 0, 1);
				
				return 1 + 0.667 * _Intensity - (_Intensity * occlude);
			}
			
			fixed4 phosphor(fixed2 uv)
			{
				fixed col = uv.x * _Cols * 2 + 0.5;
				fixed row = uv.y * _Lines * 4 + 0.5;
				
				fixed ofs = frac((col + row) / 4.0);
				fixed4 mask = fixed4(0.9, 0.9, 0.9, 1.0);
				if (ofs < 0.333) 
					mask.r = 1.2;
				else if (ofs < 0.667)
					mask.g = 1.2;
				else
					mask.b = 1.2;
				
				return mask;
			}

			fixed4 frag(v2f_img i) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, i.uv);
				
				c.rgb *= scanline(i.uv.xy);
				c.rgb *= phosphor(i.uv.xy);
				
				return c;
			}
			ENDCG
		}
	}
	
	Fallback off
	
}
