Shader "Shapes/Disc Additive" {
	SubShader {
		Tags {
			"IgnoreProjector" = "True"
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
			"DisableBatching" = "True"
		}
		Pass {
			Cull Off
			ZWrite Off
			Blend One One
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_instancing
				#pragma multi_compile __ INNER_RADIUS
				#pragma multi_compile __ SECTOR
				#define ADDITIVE
				#include "../../Core/Disc Core.cginc"
			ENDCG
		}
		Pass {
			Name "Picking"
			Tags { "LightMode" = "Picking" }
			Cull Off
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_instancing
				#pragma multi_compile __ INNER_RADIUS
				#pragma multi_compile __ SECTOR
				#define ADDITIVE
				#pragma instancing_options assumeuniformscaling nomatrices nolightprobe nolightmap
				#define SCENE_VIEW_PICKING
				#include "../../Core/Disc Core.cginc"
			ENDCG
		}
		Pass {
			Name "Selection"
			Tags { "LightMode" = "SceneSelectionPass" }
			Cull Off
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_instancing
				#pragma multi_compile __ INNER_RADIUS
				#pragma multi_compile __ SECTOR
				#define ADDITIVE
				#pragma instancing_options assumeuniformscaling nomatrices nolightprobe nolightmap
				#define SCENE_VIEW_OUTLINE_MASK
				#include "../../Core/Disc Core.cginc"
			ENDCG
		}
	}
}
