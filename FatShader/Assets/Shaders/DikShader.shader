Shader "Custom/DikShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "Harro_Tim" {}
		_BumpTex("Normal Map", 2D) = "bump" {}
		_BumpMultiplier ("Bump Intensity", Range(0.0001, 1)) = 1
		_Amount("Fat", Range(0,5)) = 0

		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		Cull Off
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert
		#pragma vertex vert

		// Use shader model 3.0 target, to get nicer looking lighting
		//#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpTex;
		float _Amount;
		fixed4 _Color;
		half _BumpMultiplier;

		half _Metallic;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpTex;
		};

		void vert(inout appdata_full IN)
		{
			IN.vertex.xyz += IN.normal * _Amount;
		}

		
		void surf (Input IN, inout SurfaceOutput o) {
			IN.uv_MainTex.x = IN.uv_MainTex.x;

			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			float3 n = UnpackNormal(tex2D(_BumpTex, IN.uv_BumpTex));
			half _BumpIntensity = 1 / _BumpMultiplier;

			n.z = n.z * _BumpIntensity;

			o.Normal = normalize(n);
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			//o.Metallic = _Metallic;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
