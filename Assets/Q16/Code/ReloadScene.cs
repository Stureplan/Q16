using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ReloadScene : MonoBehaviour 
{
	string[] tips;
	Text text;
	

	// Use this for initialization
	void Start () 
	{
		tips = new string[] { 	"There's rocketjumping",
								"John Romero wasn't actually here",
								"The enemies have a max acquisition range",
								"Somewhere, there's a hidden railgun" };


		text = GameObject.Find ("Text").GetComponent<Text>();

		int currentTip = Random.Range (0, tips.Length);
		text.text = "HINT: " + tips[currentTip];
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.R))
		{
            SceneManager.LoadScene(0);
		}
	}
}
