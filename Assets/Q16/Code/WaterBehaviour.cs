using UnityEngine;
using System.Collections;

public class WaterBehaviour : MonoBehaviour
{
    //TODO: Make Water assets
    public GameObject splashSmall;
    public GameObject splashMedium;
    public GameObject splashLarge;
    BoxCollider col;
    float top;


	void Start ()
    {
        col = GetComponent<BoxCollider>();
        top = col.bounds.center.y + col.bounds.extents.y;
	}
	
	void Update ()
    {
	    //animate neutral water
	}

    public void Splash(Vector3 pos, SPLASH_TYPE type)
    {
        //TODO: Make static and make sure there aren't 1 WaterBehaviour per water source,
        //instead we want one static function to spawn water fx

        GameObject splash;
        pos.y = top;
        
        switch(type)
        {
            case SPLASH_TYPE.SMALL:
                splash = (GameObject)Instantiate(splashSmall, pos, Quaternion.identity);
                Destroy(splash, 3.0f);
                break;
            case SPLASH_TYPE.MEDIUM:
                splash = (GameObject)Instantiate(splashMedium, pos, Quaternion.identity);
                Destroy(splash, 2.0f);
                break;
            case SPLASH_TYPE.LARGE:
                splash = (GameObject)Instantiate(splashLarge, pos, Quaternion.identity);
                Destroy(splash, 1.0f);
                break;
        }
    }
}
