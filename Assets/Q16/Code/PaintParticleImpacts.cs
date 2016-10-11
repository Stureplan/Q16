using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PaintParticleImpacts : MonoBehaviour
{
    ParticleSystem ps;
    public GameObject[] impactQuads;
    int timesImpacted = 0;

	void Start ()
    {
        ps = GetComponent<ParticleSystem>();
	}
	
	void Update ()
    {
	
	}
}
