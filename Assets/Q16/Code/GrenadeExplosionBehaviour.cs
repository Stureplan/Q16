using UnityEngine;
using System.Collections;

public class GrenadeExplosionBehaviour : MonoBehaviour
{
    Material mat;


    float power = 2.0f;
    float rotation = 0.0f;
    Vector3 position;
    float scale = 1.0f;
    float alpha = 1.0f;
    Color col;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        col = mat.GetColor("_TintColor");
        position = transform.position;

    }

    void Update()
    {
        if (scale > 7.0f)
        {
            Destroy(this.gameObject);
        }



        scale += power * Time.deltaTime;
        rotation += power * Time.deltaTime;
        alpha -= 1.5f * Time.deltaTime;
        position.y += 0.5f * Time.deltaTime;

        transform.Rotate(new Vector3(0, rotation, 0));
        transform.localScale = new Vector3(scale, scale, scale);
        transform.position = position;

        col.a = alpha;

        mat.SetColor("_TintColor", col);
    }
}
