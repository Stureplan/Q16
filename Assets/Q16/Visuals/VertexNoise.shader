// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:3,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:14,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-3198-RGB,voffset-827-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:5312,x:31634,y:33326,varname:node_5312,prsc:2;n:type:ShaderForge.SFN_Add,id:2558,x:32081,y:33288,varname:node_2558,prsc:2|A-2020-OUT,B-5312-XYZ;n:type:ShaderForge.SFN_NormalVector,id:9494,x:31630,y:33189,prsc:2,pt:True;n:type:ShaderForge.SFN_Multiply,id:827,x:32472,y:33330,varname:node_827,prsc:2|A-2020-OUT,B-800-OUT;n:type:ShaderForge.SFN_Tex2d,id:1314,x:31885,y:33461,ptovrint:False,ptlb:node_1314,ptin:_node_1314,varname:node_1314,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e958c6041cfe445e987c73751e8d4082,ntxv:2,isnm:False|UVIN-646-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:7171,x:31174,y:33616,varname:node_7171,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:646,x:31649,y:33461,varname:node_646,prsc:2,spu:0.1,spv:0.1|UVIN-7171-UVOUT;n:type:ShaderForge.SFN_Lerp,id:800,x:32284,y:33584,varname:node_800,prsc:2|A-5947-OUT,B-1314-RGB,T-1104-OUT;n:type:ShaderForge.SFN_Vector1,id:5947,x:32108,y:33461,varname:node_5947,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Tex2d,id:3198,x:32206,y:32723,ptovrint:False,ptlb:node_3198,ptin:_node_3198,varname:node_3198,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:72f6a03694a06f54c92c83c790287c5a,ntxv:0,isnm:False|UVIN-7888-UVOUT;n:type:ShaderForge.SFN_Normalize,id:2020,x:31897,y:33176,varname:node_2020,prsc:2|IN-9494-OUT;n:type:ShaderForge.SFN_Slider,id:1104,x:31876,y:33792,ptovrint:False,ptlb:NoiseAmount,ptin:_NoiseAmount,varname:node_1104,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.25,max:1;n:type:ShaderForge.SFN_Rotator,id:7888,x:31963,y:32593,varname:node_7888,prsc:2|UVIN-6658-UVOUT,SPD-6201-OUT;n:type:ShaderForge.SFN_TexCoord,id:6658,x:31522,y:32734,varname:node_6658,prsc:2,uv:0;n:type:ShaderForge.SFN_Vector1,id:6201,x:31817,y:32742,varname:node_6201,prsc:2,v1:1;proporder:1314-3198-1104;pass:END;sub:END;*/

Shader "Shader Forge/VertexNoise" {
    Properties {
        _node_1314 ("node_1314", 2D) = "black" {}
        _node_3198 ("node_3198", 2D) = "white" {}
        _NoiseAmount ("NoiseAmount", Range(-1, 1)) = 0.25
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _TimeEditor;
            uniform sampler2D _node_1314; uniform float4 _node_1314_ST;
            uniform sampler2D _node_3198; uniform float4 _node_3198_ST;
            uniform float _NoiseAmount;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float3 node_2020 = normalize(v.normal);
                float node_5947 = 0.5;
                float4 node_3622 = _Time + _TimeEditor;
                float2 node_646 = (o.uv0+node_3622.g*float2(0.1,0.1));
                float4 _node_1314_var = tex2Dlod(_node_1314,float4(TRANSFORM_TEX(node_646, _node_1314),0.0,0));
                v.vertex.xyz += (node_2020*lerp(float3(node_5947,node_5947,node_5947),_node_1314_var.rgb,_NoiseAmount));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float4 node_3622 = _Time + _TimeEditor;
                float node_7888_ang = node_3622.g;
                float node_7888_spd = 1.0;
                float node_7888_cos = cos(node_7888_spd*node_7888_ang);
                float node_7888_sin = sin(node_7888_spd*node_7888_ang);
                float2 node_7888_piv = float2(0.5,0.5);
                float2 node_7888 = (mul(i.uv0-node_7888_piv,float2x2( node_7888_cos, -node_7888_sin, node_7888_sin, node_7888_cos))+node_7888_piv);
                float4 _node_3198_var = tex2D(_node_3198,TRANSFORM_TEX(node_7888, _node_3198));
                float3 emissive = _node_3198_var.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _TimeEditor;
            uniform sampler2D _node_1314; uniform float4 _node_1314_ST;
            uniform float _NoiseAmount;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float3 node_2020 = normalize(v.normal);
                float node_5947 = 0.5;
                float4 node_4077 = _Time + _TimeEditor;
                float2 node_646 = (o.uv0+node_4077.g*float2(0.1,0.1));
                float4 _node_1314_var = tex2Dlod(_node_1314,float4(TRANSFORM_TEX(node_646, _node_1314),0.0,0));
                v.vertex.xyz += (node_2020*lerp(float3(node_5947,node_5947,node_5947),_node_1314_var.rgb,_NoiseAmount));
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
