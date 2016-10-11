using UnityEngine;
using System.Collections;

public class PortalAnimation : MonoBehaviour {

	float speed = 2.5f;
	Vector2 offset;

	Material mat;
	Texture2D tex;

	void Start () 
	{
		mat = GetComponent<MeshRenderer>().material;
		offset = mat.GetTextureOffset("_MainTex");
	}
	
	void Update () 
	{
		offset.y = Time.time * speed;
		offset.x = Mathf.PerlinNoise(Time.time, 0) * 0.5f;



		mat.SetTextureOffset("_MainTex", offset);
	}
}
