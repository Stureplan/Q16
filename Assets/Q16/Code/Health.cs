using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Health : MonoBehaviour 
{
	//GameState state;
	int health = 100;

	Image img;
	Text text;
	public Sprite[] healthStates;
	

	// Use this for initialization
	void Start () 
	{
		img = GameObject.Find ("Face").GetComponent<Image>();
		text = GameObject.Find ("Text").GetComponent<Text>();		
	}

	// Update is called once per frame
	void Update () 
	{
		if (health <= 0)
		{
			Kill ();
		}
        
		if (health > 80) 				 { img.sprite = healthStates[0]; }
		if (health > 60 && health <= 80) { img.sprite = healthStates[1]; }
		if (health > 40 && health <= 60) { img.sprite = healthStates[2]; }
		if (health > 20 && health <= 40) { img.sprite = healthStates[3]; }
		if (health <= 20) 				 { img.sprite = healthStates[4]; }
		text.text = health.ToString();
		
	}

	public void Damage(int amt)
	{
		health -= amt;
	}

	public void Kill()
	{
        Stats.ResetData();
        SceneManager.LoadScene(2);
	}
}
