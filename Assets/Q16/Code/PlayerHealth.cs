using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour, IDamageable
{
    //   <IDamageable>   //
    public SENDER_TYPE Type() { return SENDER_TYPE.PLAYER; }
    public void Damage(int dmg, DAMAGE_TYPE type, SenderInfo sender)
    {
        if (sender.s_Type == SENDER_TYPE.PLAYER)
        {
            return;
        }

        health -= dmg;
    }
    public void Explosion(int dmg, DAMAGE_TYPE type, SenderInfo sender, Vector3 point, float force)
    {
        if (sender.s_Type != SENDER_TYPE.PLAYER)
        {
            Damage(dmg, DAMAGE_TYPE.EXPLOSION, sender);
        }

        if (type == DAMAGE_TYPE.EXPLOSION)
        {
            PlayerInput m = GetComponent<PlayerInput>();

            m.AddForce(transform.position - point, force);
            
            //m.AddForce(transform.position - point, 20.0f);

            //wacky old jump physics
            //m.Jump(15.0f);
        }
    }
    public int Health() { return health; }
    public void DeathDirection(Vector3 dir, float power) {  }

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

	public void Kill()
	{
        Stats.ResetData();
        SceneManager.LoadScene(2);
	}
}
