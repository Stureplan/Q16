// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1,x:34362,y:32994,varname:node_1,prsc:2|normal-2597-RGB,emission-6679-OUT,voffset-3661-OUT;n:type:ShaderForge.SFN_Vector1,id:8,x:34051,y:33454,varname:node_8,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Tex2d,id:512,x:33205,y:32817,ptovrint:False,ptlb:node_512,ptin:_node_512,varname:node_512,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3b55d3b846a5ac949a363340cf54a243,ntxv:0,isnm:False|UVIN-5642-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:2597,x:33879,y:32704,ptovrint:False,ptlb:node_2597,ptin:_node_2597,varname:node_2597,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:4b8d081e9d114c7f1100f5ab44295342,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Panner,id:5642,x:32713,y:32735,varname:node_5642,prsc:2,spu:0.6,spv:0.1|UVIN-7977-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:7977,x:32532,y:32735,varname:node_7977,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:2164,x:34051,y:33276,varname:node_2164,prsc:2|A-1627-RGB,B-2806-OUT;n:type:ShaderForge.SFN_NormalVector,id:2806,x:33811,y:33255,prsc:2,pt:False;n:type:ShaderForge.SFN_Tex2d,id:1627,x:33811,y:33110,ptovrint:False,ptlb:node_1627,ptin:_node_1627,varname:node_1627,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9989de9e80948e44ead278b7bee477d7,ntxv:2,isnm:False|UVIN-9719-UVOUT;n:type:ShaderForge.SFN_Panner,id:9719,x:33310,y:33087,varname:node_9719,prsc:2,spu:0.5,spv:0.5|UVIN-2831-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2831,x:33131,y:33087,varname:node_2831,prsc:2,uv:0;n:type:ShaderForge.SFN_Blend,id:6679,x:33669,y:32874,varname:node_6679,prsc:2,blmd:20,clmp:True|SRC-7220-RGB,DST-512-RGB;n:type:ShaderForge.SFN_Tex2d,id:7220,x:33205,y:32645,ptovrint:False,ptlb:node_7220,ptin:_node_7220,varname:node_7220,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9989de9e80948e44ead278b7bee477d7,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Multiply,id:570,x:33391,y:33457,varname:node_570,prsc:2|A-7288-RGB,B-5177-OUT;n:type:ShaderForge.SFN_NormalVector,id:5177,x:33088,y:33500,prsc:2,pt:False;n:type:ShaderForge.SFN_Tex2d,id:7288,x:33088,y:33308,ptovrint:False,ptlb:Heightmap,ptin:_Heightmap,varname:node_7288,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c0e5cee9c90822d4a9736f4d23711a5d,ntxv:2,isnm:False|UVIN-9719-UVOUT;n:type:ShaderForge.SFN_ConstantClamp,id:3661,x:33627,y:33424,varname:node_3661,prsc:2,min:-0.1,max:0.1|IN-570-OUT;proporder:2597-1627-512-7220-7288;pass:END;sub:END;*/

Shader "Shader Forge/PlasmaBall" {
    Properties {
        _node_2597 ("node_2597", 2D) = "bump" {}
        _node_1627 ("node_1627", 2D) = "black" {}
        _node_512 ("node_512", 2D) = "white" {}
        _node_7220 ("node_7220", 2D) = "black" {}
        _Heightmap ("Heightmap", 2D) = "black" {}
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
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers opengl gles xbox360 ps3 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _TimeEditor;
            uniform sampler2D _node_512; uniform float4 _node_512_ST;
            uniform sampler2D _node_2597; uniform float4 _node_2597_ST;
            uniform sampler2D _node_7220; uniform float4 _node_7220_ST;
            uniform sampler2D _Heightmap; uniform float4 _Heightmap_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 node_2762 = _Time + _TimeEditor;
                float2 node_9719 = (o.uv0+node_2762.g*float2(0.5,0.5));
                float4 _Heightmap_var = tex2Dlod(_Heightmap,float4(TRANSFORM_TEX(node_9719, _Heightmap),0.0,0));
                v.vertex.xyz += clamp((_Heightmap_var.rgb*v.normal),-0.1,0.1);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _node_2597_var = UnpackNormal(tex2D(_node_2597,TRANSFORM_TEX(i.uv0, _node_2597)));
                float3 normalLocal = _node_2597_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float4 _node_7220_var = tex2D(_node_7220,TRANSFORM_TEX(i.uv0, _node_7220));
                float4 node_2762 = _Time + _TimeEditor;
                float2 node_5642 = (i.uv0+node_2762.g*float2(0.6,0.1));
                float4 _node_512_var = tex2D(_node_512,TRANSFORM_TEX(node_5642, _node_512));
                float3 emissive = saturate((_node_512_var.rgb/_node_7220_var.rgb));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers opengl gles xbox360 ps3 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _TimeEditor;
            uniform sampler2D _Heightmap; uniform float4 _Heightmap_ST;
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
                float4 node_3086 = _Time + _TimeEditor;
                float2 node_9719 = (o.uv0+node_3086.g*float2(0.5,0.5));
                float4 _Heightmap_var = tex2Dlod(_Heightmap,float4(TRANSFORM_TEX(node_9719, _Heightmap),0.0,0));
                v.vertex.xyz += clamp((_Heightmap_var.rgb*v.normal),-0.1,0.1);
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
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers opengl gles xbox360 ps3 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _TimeEditor;
            uniform sampler2D _node_512; uniform float4 _node_512_ST;
            uniform sampler2D _node_7220; uniform float4 _node_7220_ST;
            uniform sampler2D _Heightmap; uniform float4 _Heightmap_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_6488 = _Time + _TimeEditor;
                float2 node_9719 = (o.uv0+node_6488.g*float2(0.5,0.5));
                float4 _Heightmap_var = tex2Dlod(_Heightmap,float4(TRANSFORM_TEX(node_9719, _Heightmap),0.0,0));
                v.vertex.xyz += clamp((_Heightmap_var.rgb*v.normal),-0.1,0.1);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 _node_7220_var = tex2D(_node_7220,TRANSFORM_TEX(i.uv0, _node_7220));
                float4 node_6488 = _Time + _TimeEditor;
                float2 node_5642 = (i.uv0+node_6488.g*float2(0.6,0.1));
                float4 _node_512_var = tex2D(_node_512,TRANSFORM_TEX(node_5642, _node_512));
                o.Emission = saturate((_node_512_var.rgb/_node_7220_var.rgb));
                
                float3 diffColor = float3(0,0,0);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
