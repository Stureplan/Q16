using UnityEngine;
using System.Collections;

public class LightningFX : MonoBehaviour
{
    LineRenderer lr;
    Vector3[] lightningPos;
    Vector3 weaponPos;
    int amtOfLightning = 4;

    Color col = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
    float fadeSpeed = 4.0f;

    void Start ()
    {

    }

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lightningPos = new Vector3[amtOfLightning];
        
    }

    void Update ()
    {
        lightningPos[1] += GenerateXZVector(0.1f);
        lightningPos[2] += GenerateXZVector(0.1f);
        for (int i = 0; i < amtOfLightning; i++)
        {
            lr.SetPosition(i, lightningPos[i]);
        }

        col.a -= fadeSpeed * Time.deltaTime;
        lr.SetColors(col, col);

        if (col.a <= 0.0f)
        {
            Destroy(this.gameObject);
        }
	}

    public void SetWeaponPos(Vector3 pos)
    {
        weaponPos = pos;
    }

    public void ArrangeLightning()
    {
        lightningPos[0] = weaponPos + new Vector3(0, -0.2f, 0);
        lightningPos[3] = weaponPos + new Vector3(0, 20.0f, 0);

        lightningPos[1] = (lightningPos[3] - lightningPos[0]) / 4;
        lightningPos[1] += lightningPos[0];
        lightningPos[1] += GenerateXZVector(0.3f);

        lightningPos[2] = (lightningPos[3] - lightningPos[0]) / 4;
        lightningPos[2] *= 2;
        lightningPos[2] += lightningPos[0];
        lightningPos[2] += GenerateXZVector(0.3f);


        for (int i = 0; i < amtOfLightning; i++)
        {
            lr.SetPosition(i, lightningPos[i]);
        }
    }

    Vector3 GenerateXZVector(float amt)
    {
        Vector3 v = new Vector3(0, 0, 0);
        v.x = Random.Range(-amt, amt);
        v.z = Random.Range(-amt, amt);
        v.y = 0.0f;

        return v;
    }
}
