Shader "Game/S_MapSquare"
{
    Properties
    {
        [MainTexture][NoScaleOffset]_Mark("Mark", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)
        _Highlight("Highlight", Float) = 0.0
        _MapUv("Map UV", Vector) = (0.0, 0.0, 0.0, 0.0)
        _MapRowNum("Map Row Num", Float) = 10
        _MapColNum("Map Col Num", Float) = 10


        [Header(Settings)]
        [Toggle(_DOUBLE_SIDED)] _DoubleSided("Double Sided", Float) = 0
        [Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull", Float) = 2
        [Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend("Src Blend", Float) = 1
        [Enum(UnityEngine.Rendering.BlendMode)] _DstBlend("Dst Blend", Float) = 0
    }
    SubShader
    {
        Tags 
        { 
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Opaque"
            "UniversalMaterialType" = "Unlit"
            "Queue"="Geometry"
            "DisableBatching"="False"
        }
        LOD 100

        Pass
        {
            Name "Forward"

            Tags
            {
                "LightMode"="UniversalForward"
            }

            Cull [_Cull]
            Blend [_SrcBlend] [_DstBlend]
            ZTest LEqual
            ZWrite On

            HLSLPROGRAM

            // Universal Pipeline keywords
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile_fragment _ _REFLECTION_PROBE_BLENDING
            #pragma multi_compile_fragment _ _REFLECTION_PROBE_BOX_PROJECTION
            #pragma multi_compile_fragment _ _SHADOWS_SOFT
            #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
            #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
            #pragma multi_compile_fragment _ _LIGHT_LAYERS
            #pragma multi_compile_fragment _ _LIGHT_COOKIES
            #pragma multi_compile _ _FORWARD_PLUS
            #pragma multi_compile_fragment _ _WRITE_RENDERING_LAYERS

            // Unity defined keywords
            #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
            #pragma multi_compile _ SHADOWS_SHADOWMASK
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile _ DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fragment _ LOD_FADE_CROSSFADE
            #pragma multi_compile_fog
            #pragma multi_compile_fragment _ DEBUG_DISPLAY

            #pragma shader_feature_local_fragment _DOUBLE_SIDED
            #pragma shader_feature_local_fragment _EMISSION
            #pragma shader_feature_local_fragment _NORMAL_MAP
            #pragma shader_feature_local_fragment _IS_FACE
            #pragma shader_feature_local_fragment _SPECULAR
            #pragma shader_feature_local_fragment _RIM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            CBUFFER_START(UnityPerMaterial)
            float4 _Mark_ST;
            float4 _Color;
            half _Highlight;
            float4 _MapUv;

            float _MapRowNum;
            float _MapColNum;
            CBUFFER_END

            TEXTURE2D(_Mark);
            SAMPLER(sampler_Mark);

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float2 uv : TEXCOORD0;
                float4 positionCS : SV_POSITION;
            };

            Varyings vert(Attributes input)
            {
                VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
                
                Varyings output = (Varyings)0;
                output.uv = TRANSFORM_TEX(input.uv, _Mark);
                output.positionCS = vertexInput.positionCS;
                return output;
            }

            float4 frag(Varyings input) : SV_TARGET
            {
                // square uv and map uv
                float2 squareUV = input.uv;
                float2 mapUV = _MapUv.xy;
                mapUV.x += squareUV.x / _MapRowNum;
                mapUV.y += squareUV.y / _MapColNum;

                float4 mark = SAMPLE_TEXTURE2D(_Mark, sampler_Mark, squareUV);
                float highlight = 1.0 + _Highlight;

                float4 color = _Color;

                color *= highlight;

                return color;
            }

            ENDHLSL
        }
    }
}
