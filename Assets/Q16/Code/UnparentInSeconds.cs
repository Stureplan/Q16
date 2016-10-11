using UnityEngine;
using System.Collections;

public class UnparentInSeconds : MonoBehaviour
{
    [SerializeField]
    private float time;

    private float currentTime = 0.0f;

    void Start ()
    {
	    
	}
	
	void Update ()
    {
        if (currentTime < time)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            transform.parent = null;
        }
    }
}
