using UnityEngine;
using System.Collections;

public class MenuInput : MonoBehaviour
{
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
