using UnityEngine;
using System.Collections;

public class DestroyInSeconds : MonoBehaviour
{
    [SerializeField]
    private float time;

	void Start ()
    {
        Destroy(this.gameObject, time);
	}

    public void SetTime(float t)
    {
        time = t;
    }
	
	void Update ()
    {
	
	}
}
