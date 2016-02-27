Shader "Custom/ColorShine" {
	Properties {
		_MainTex ("image", 2D) = "white" {}
	
		_leftTop("_leftTop",Color) = (1,1,1,1)
		_leftBottom("_leftBottom",Color) = (1,1,1,1)
		_rightTop("_rightTop",Color) = (1,1,1,1)
		_rightBottom("_rightBottom",Color) = (1,1,1,1)
	}
	
	CGINCLUDE
        #include "UnityCG.cginc"           
        
        sampler2D _MainTex;
		
		float4 _leftTop;
		float4 _leftBottom;
		float4 _rightTop;
		float4 _rightBottom;
		
        struct v2f {    
            float4 pos:SV_POSITION;    
            float2 uv : TEXCOORD0;   
        };  
  
        v2f vert(appdata_base v) {  
            v2f o;  
            o.pos = mul (UNITY_MATRIX_MVP, v.vertex);  
            o.uv = v.texcoord.xy;  
            return o;  
        }  
  
        fixed4 frag(v2f i) : COLOR0 {
       		fixed4 k = tex2D(_MainTex, i.uv);
            k = (1-i.uv.y)*(_leftBottom*(1-i.uv.x)+ _rightBottom*i.uv.x) + i.uv.y*(_leftTop*(1-i.uv.x)+ _rightTop*i.uv.x);
            return k;
        }  
    ENDCG    
  
    SubShader {   
        Tags {"Queue" = "Transparent"}     
        ZWrite Off     
        Blend SrcAlpha OneMinusSrcAlpha     
        Pass {    
            CGPROGRAM    
            #pragma vertex vert    
            #pragma fragment frag    
            #pragma fragmentoption ARB_precision_hint_fastest     
  
            ENDCG    
        }
    }
    FallBack Off  
}
