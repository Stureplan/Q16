using UnityEngine;
using System.Collections;

public class ExplosionBehaviour : MonoBehaviour 
{
	Material mat;


	float power = 5.0f;
	float rotation = 0.0f;
	Vector3 position;
	float scale = 1.0f;
	float alpha = 1.0f;
	Color col;

	void Start () 
	{
		mat = GetComponent<MeshRenderer>().material;
		col = mat.GetColor ("_TintColor");
		position = transform.position;

	}
	
	void Update () 
	{
		if (scale > 8.0f)
		{
			Destroy (this.gameObject);
		}



		scale += power * Time.deltaTime;
		rotation += power * Time.deltaTime;
		alpha -= 2.0f * Time.deltaTime;
		position.y += 3.0f * Time.deltaTime;

		transform.Rotate(new Vector3(0, rotation, 0));
		transform.localScale = new Vector3(scale, scale, scale);
		transform.position = position;

		col.a = alpha;

		mat.SetColor ("_TintColor", col);
		//mat.color = new Color(col.r, col.g, col.b, alpha);
	}
}
