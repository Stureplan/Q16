using UnityEngine;
using System.Collections;

public class SkyboxAnimation : MonoBehaviour {

	float speed = 4.0f;
	float offset;

	void Update () 
	{
		offset = Time.time * speed;
		RenderSettings.skybox.SetFloat ("_Rotation", offset);
	}
}
