Shader ",door/Passthrough" {

	Properties {
		_MainTex("Base (RGB)", 2D) = "white" {}
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
			
			fixed4 frag(v2f_img i) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, i.uv);
				return c;
			}
			ENDCG
		}
	}
	
	Fallback off
	
}
