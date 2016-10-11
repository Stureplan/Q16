using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LavaGun : Weapon
{
    List<GameObject> lavaObjects;
    public LineRenderer lr;
    public GameObject lavaPrefab;
    public GameObject front;
    public ParticleSystem smokePS;

    GameObject currentLavaPrefab;

    int lavaSegments = 0;
    float currentStrength = 0.0f;


	void Start ()
    {
        lavaObjects = new List<GameObject>();
        lr.SetVertexCount(0);

        CD = 0.1f;
        cooldown = new Cooldown(CD);
        index = 8;
	}
	
	void Update ()
    {
        cooldown.UpdateTimer();


        ManageLines();

        if (currentStrength > 0.1f)
        {
            currentStrength -= Time.deltaTime * 4.0f;
        }
	}

    void AddLavaSegment(GameObject go)
    {
        lavaObjects.Add(go);
        lavaSegments++;
        if (lavaSegments > 0)
        {
            lr.SetVertexCount(lavaSegments);
        }
    }

    void ManageLines()
    {
        for(int i = 0; i < lavaSegments; i++)
        {
            lr.SetPosition(i, lavaObjects[i].transform.position);
        }
    }

    public void RemoveLavaObject(GameObject go)
    {
        lavaObjects.Remove(go);
        lavaSegments--;
        lr.SetVertexCount(lavaSegments);
    }

    public override void Fire(Vector3 pos, Vector3 dir)
    {
        smokePS.Emit(5);

        Quaternion rot = Quaternion.LookRotation(dir);
        currentLavaPrefab = (GameObject)Instantiate(lavaPrefab, front.transform.position, rot);

        LavaProjectileBehaviour lpb = currentLavaPrefab.GetComponent<LavaProjectileBehaviour>();
        lpb.Initialize(this, currentStrength);
        AddLavaSegment(currentLavaPrefab);

        currentStrength += 1.0f;
        
        cooldown.ResetTimer();
    }
}
