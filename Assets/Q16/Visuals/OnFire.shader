// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:32719,y:32712,varname:node_4013,prsc:2|diff-8185-OUT,normal-1495-RGB,emission-8823-OUT,voffset-9106-OUT;n:type:ShaderForge.SFN_Tex2d,id:3025,x:31956,y:32541,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_3025,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ccd9ef7e3e272a14d885d71def3d2b26,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1495,x:32316,y:32935,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_1495,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:07004896375466747aaa896a0a19d7a5,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:5111,x:31608,y:33053,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_5111,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:8796,x:31277,y:32872,ptovrint:False,ptlb:FireSlider,ptin:_FireSlider,varname:node_8796,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.3392522,max:1;n:type:ShaderForge.SFN_Tex2d,id:1967,x:31956,y:32706,ptovrint:False,ptlb:BurnTexture,ptin:_BurnTexture,varname:node_1967,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:72f6a03694a06f54c92c83c790287c5a,ntxv:0,isnm:False|UVIN-8945-OUT;n:type:ShaderForge.SFN_Lerp,id:8185,x:32309,y:32685,varname:node_8185,prsc:2|A-3025-RGB,B-1967-RGB,T-4045-OUT;n:type:ShaderForge.SFN_Panner,id:9284,x:31608,y:32706,varname:node_9284,prsc:2,spu:0,spv:0.4|UVIN-1263-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1263,x:31434,y:32711,varname:node_1263,prsc:2,uv:0;n:type:ShaderForge.SFN_Step,id:9934,x:31784,y:32883,varname:node_9934,prsc:2|A-1407-OUT,B-5111-R;n:type:ShaderForge.SFN_Clamp01,id:4045,x:31956,y:32883,varname:node_4045,prsc:2|IN-9934-OUT;n:type:ShaderForge.SFN_Multiply,id:8823,x:32490,y:32759,varname:node_8823,prsc:2|A-8185-OUT,B-4045-OUT;n:type:ShaderForge.SFN_OneMinus,id:1407,x:31608,y:32883,varname:node_1407,prsc:2|IN-8796-OUT;n:type:ShaderForge.SFN_Multiply,id:8945,x:31784,y:32706,varname:node_8945,prsc:2|A-665-OUT,B-9284-UVOUT;n:type:ShaderForge.SFN_Vector1,id:665,x:31484,y:32632,varname:node_665,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:9106,x:32390,y:33133,varname:node_9106,prsc:2|A-4045-OUT,B-2367-RGB,C-5354-OUT;n:type:ShaderForge.SFN_Vector1,id:5354,x:31863,y:33330,varname:node_5354,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Tex2d,id:2367,x:31863,y:33201,varname:node_2367,prsc:2,tex:e08c295755c0885479ad19f518286ff2,ntxv:2,isnm:False|UVIN-9593-UVOUT,TEX-3630-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:3630,x:31628,y:33350,ptovrint:False,ptlb:FireTexture,ptin:_FireTexture,varname:node_3630,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e08c295755c0885479ad19f518286ff2,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Panner,id:9593,x:31413,y:33212,varname:node_9593,prsc:2,spu:0,spv:0.1|UVIN-2860-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2860,x:31216,y:33212,varname:node_2860,prsc:2,uv:0;proporder:3025-1495-5111-8796-1967-3630;pass:END;sub:END;*/

Shader "Shader Forge/OnFire" {
    Properties {
        _Texture ("Texture", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        _Noise ("Noise", 2D) = "white" {}
        _FireSlider ("FireSlider", Range(-1, 1)) = 0.3392522
        _BurnTexture ("BurnTexture", 2D) = "white" {}
        _FireTexture ("FireTexture", 2D) = "bump" {}
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
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _FireSlider;
            uniform sampler2D _BurnTexture; uniform float4 _BurnTexture_ST;
            uniform sampler2D _FireTexture; uniform float4 _FireTexture_ST;
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
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 _Noise_var = tex2Dlod(_Noise,float4(TRANSFORM_TEX(o.uv0, _Noise),0.0,0));
                float node_4045 = saturate(step((1.0 - _FireSlider),_Noise_var.r));
                float4 node_6201 = _Time + _TimeEditor;
                float2 node_9593 = (o.uv0+node_6201.g*float2(0,0.1));
                float3 node_2367 = UnpackNormal(tex2Dlod(_FireTexture,float4(TRANSFORM_TEX(node_9593, _FireTexture),0.0,0)));
                v.vertex.xyz += (node_4045*node_2367.rgb*0.1);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float4 node_6201 = _Time + _TimeEditor;
                float2 node_8945 = (2.0*(i.uv0+node_6201.g*float2(0,0.4)));
                float4 _BurnTexture_var = tex2D(_BurnTexture,TRANSFORM_TEX(node_8945, _BurnTexture));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float node_4045 = saturate(step((1.0 - _FireSlider),_Noise_var.r));
                float3 node_8185 = lerp(_Texture_var.rgb,_BurnTexture_var.rgb,node_4045);
                float3 diffuseColor = node_8185;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = (node_8185*node_4045);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _FireSlider;
            uniform sampler2D _BurnTexture; uniform float4 _BurnTexture_ST;
            uniform sampler2D _FireTexture; uniform float4 _FireTexture_ST;
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
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 _Noise_var = tex2Dlod(_Noise,float4(TRANSFORM_TEX(o.uv0, _Noise),0.0,0));
                float node_4045 = saturate(step((1.0 - _FireSlider),_Noise_var.r));
                float4 node_7893 = _Time + _TimeEditor;
                float2 node_9593 = (o.uv0+node_7893.g*float2(0,0.1));
                float3 node_2367 = UnpackNormal(tex2Dlod(_FireTexture,float4(TRANSFORM_TEX(node_9593, _FireTexture),0.0,0)));
                v.vertex.xyz += (node_4045*node_2367.rgb*0.1);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float4 node_7893 = _Time + _TimeEditor;
                float2 node_8945 = (2.0*(i.uv0+node_7893.g*float2(0,0.4)));
                float4 _BurnTexture_var = tex2D(_BurnTexture,TRANSFORM_TEX(node_8945, _BurnTexture));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float node_4045 = saturate(step((1.0 - _FireSlider),_Noise_var.r));
                float3 node_8185 = lerp(_Texture_var.rgb,_BurnTexture_var.rgb,node_4045);
                float3 diffuseColor = node_8185;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
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
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _TimeEditor;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _FireSlider;
            uniform sampler2D _FireTexture; uniform float4 _FireTexture_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float4 _Noise_var = tex2Dlod(_Noise,float4(TRANSFORM_TEX(o.uv0, _Noise),0.0,0));
                float node_4045 = saturate(step((1.0 - _FireSlider),_Noise_var.r));
                float4 node_3864 = _Time + _TimeEditor;
                float2 node_9593 = (o.uv0+node_3864.g*float2(0,0.1));
                float3 node_2367 = UnpackNormal(tex2Dlod(_FireTexture,float4(TRANSFORM_TEX(node_9593, _FireTexture),0.0,0)));
                v.vertex.xyz += (node_4045*node_2367.rgb*0.1);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
