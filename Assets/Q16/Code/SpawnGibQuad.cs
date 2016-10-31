using UnityEngine;
using System.Collections;

public class SpawnGibQuad : MonoBehaviour
{
    enum QUAD_TYPE
    {
        RED_BLOOD1,
        RED_BLOOD2,
        RED_BLOOD3,
        YELLOW_BLOOD1,
    }

    public GameObject[] quadPrefabs;
    bool hasSpawned = false;




	void Start ()
    {
        //int a = System.Enum.GetValues(typeof(QUAD_TYPE)).Length;
	}
	
	void Update ()
    {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (hasSpawned == false)
        {
            if (col.gameObject.tag == "World")
            {
                QUAD_TYPE type = (QUAD_TYPE)Random.Range(0, quadPrefabs.Length);
                SpawnQuad(type, col);
            }
        }
    }

    void SpawnQuad(QUAD_TYPE type, Collision col)
    {
        ContactPoint contact = col.contacts[0];
       
        Vector3 pt = contact.point;
        Quaternion rot = Quaternion.LookRotation(contact.normal);

        Instantiate(quadPrefabs[(int)type], pt, rot);

        hasSpawned = true;
    }
}
