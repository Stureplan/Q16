using UnityEngine;
using System.Collections;

public class ScaleBlood : MonoBehaviour
{
    public Transform scaledChild;
    float scaleTo;
    float scaleSpeed = 2.0f;
    bool shouldScale = false;

	// Use this for initialization
	void Start ()
    {


	    if (Vector3.Angle(transform.up, Vector3.up) < 10.0f)
        {
            shouldScale = true;
            scaleTo = Random.Range(5.0f, 15.0f);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (shouldScale)
        {
            if (scaledChild.localScale.y < scaleTo)
            {
                float currScale = scaledChild.localScale.y;
                currScale += Time.deltaTime * scaleSpeed;


                scaledChild.localScale = new Vector3(1.0f, currScale, 1.0f);
            }
        }
	}
}
