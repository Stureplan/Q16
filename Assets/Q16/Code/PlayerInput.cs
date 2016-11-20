using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    // REFERENCES
    CharacterController cc;
    GunSway gunSway;
	public Camera cam;

    public GameObject weaponPos;
    Inventory inventory;
    PlayerHealth hp;

    // MOUSE
	float mHorizontalSpeed = 2.0f;
	float mVerticalSpeed = 2.0f;

	float h = 0.0f;
	float v = 0.0f;

    // MOVEMENT
	float forwardMoveSpeed = 10.0f;
	float sideMoveSpeed = 10.0f;
    float verticalVelocity = 0.0f;
    float jumpStrength = 10.0f;


    // NEW PUSH MOVE
    Vector3 forces = Vector3.zero;
    float mass = 1.0f;
    float friction = 1.0f;


    Text textMoveSpeed;
    Text textChunks;

    int chunks = 0;

    float damageModifier = 1.0f;

    void Start () 
	{
		cc = GetComponent<CharacterController>();
        hp = GetComponent<PlayerHealth>();
        inventory = GetComponent<Inventory>();
        gunSway = weaponPos.GetComponent<GunSway>();

        Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;


        Settings.Setup();

        textMoveSpeed = GameObject.Find ("Movespeed").GetComponent<Text>();
        textChunks = GameObject.Find("ChunkModifier").GetComponent<Text>();
        chunks = Stats.info.timesLoaded * 3;

        forwardMoveSpeed = forwardMoveSpeed + Stats.info.enemiesKilled* 0.2f;
		sideMoveSpeed    = sideMoveSpeed    + Stats.info.enemiesKilled* 0.2f;


        textMoveSpeed.text = "SPEED: " + forwardMoveSpeed.ToString();
        textChunks.text = "CHUNK MOD: 10 + " + chunks.ToString();
    }


    void Update () 
	{
        // MOUSE LOOK
        h = Input.GetAxis("Mouse X") * mHorizontalSpeed;
        v -= Input.GetAxis("Mouse Y") * mVerticalSpeed;
        
        v = Mathf.Clamp(v, -70, 70);

        transform.Rotate(0.0f, h, 0.0f);
        cam.transform.localRotation = Quaternion.Euler(new Vector3(v, 0.0f, 0.0f));
        

        Vector3 movement = Vector3.zero;
        movement.z = Input.GetAxisRaw("Forward") * forwardMoveSpeed;
        movement.x = Input.GetAxisRaw("Right") * sideMoveSpeed;
        movement.y = 0.0f;

        Vector3 gunSwayForce = movement;

        gunSway.Sway(gunSwayForce, new Vector2(h, v));

        movement = transform.TransformDirection(movement);


        if (!cc.isGrounded)
        {
            gunSwayForce = movement + (forces * 20.0f);
            friction = 0.8f;
        }
        else
        {
            //friction += 15.0f * Time.deltaTime;
        }


        //if we're in air---v
        if (!cc.isGrounded)
        {
            movement = movement / 2.0f;
        }

        Vector3 finalMove = movement + forces;
        cc.Move(finalMove * Time.deltaTime);

        if (forces.magnitude > 0.0f)
        {
            // if we're still affected by external movement, lerp it out until zero
            forces.x = Mathf.Lerp(forces.x, 0.0f, Time.deltaTime * friction);
            forces.z = Mathf.Lerp(forces.z, 0.0f, Time.deltaTime * friction);

            if (forces.y > 0.0f)
            {
                forces.y = Mathf.Lerp(forces.y, 0.0f, Time.deltaTime * friction);
            }
        }
        if (!cc.isGrounded)
        {
            forces.y += Physics.gravity.y * 2.0f * Time.deltaTime;
        }






        // JUMP
        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            Jump(jumpStrength);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.ChangeWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventory.ChangeWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inventory.ChangeWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            inventory.ChangeWeapon(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            inventory.ChangeWeapon(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            inventory.ChangeWeapon(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            inventory.ChangeWeapon(6);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            inventory.ChangeWeapon(7);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            inventory.ChangeWeapon(8);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Settings.Cmd("ToggleMusic", "");
        }

        //TODO: Move this out of debug mode and into real options
        if (Input.GetKeyDown(KeyCode.G))
        {
            Settings.Cmd("ToggleFullscreen", "");
        }


        if (Input.GetButton ("Fire"))
		{
            if (inventory.CheckWeaponCD())
            {
                inventory.FireWeapon(weaponPos.transform.position, cam.transform.forward);
                Stats.info.amountShot++;
            }
		}

        if (Input.GetButtonDown("Slowmo"))
        {
            int amt = inventory.GetAmtOfSlowmo();
            if (amt > 0)
            {
                inventory.IncreaseDecreaseSlowmo(-1);

                Time.timeScale = 0.5f;
                //TODO: Add timer to reset timeScale
            }
        }
	}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //If we get pushed somewhere and collide with a wall or anything,
        //disable the pushing force.
        forces.x = 0.0f; forces.z = 0.0f;
    }

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Railgun")
		{
            inventory.SetHasWeapon(2, true);
			Destroy (col.gameObject);
			Stats.info.foundSecret = true;
            //TODO: Set GUI text
		}

        if (col.tag == "Portal")
        {
            Stats.info.timesLoaded++;
            SceneManager.LoadScene(0);
        }

        if (col.tag == "Killbox")
        {
            hp.Kill();
        }

        if (col.tag == "QuadDamage")
        {
            //gameObject.AddComponent<new QuadDamage>();
        }

        if (col.tag == "Slowmo")
        {
            inventory.IncreaseDecreaseSlowmo(1);
        }
    }

    public void Jump(float strength)
    {
        //original
        //forces.y = strength;


        Vector3 v = cc.velocity;
        v.y = strength;
        AddForce(v, 15.0f);
    }

    public void AddForce(Vector3 dir, float force)
    {
        dir.Normalize();

        forces = dir * force / mass;
    }

    public void AddAirborneForce(Vector3 dir, float force)
    {
        if (!cc.isGrounded)
        {
            dir.Normalize();

            forces = dir * force / mass;
        }
    }

    public void SetDamageModifier(float dmg)
    {
        damageModifier = dmg;
    }
}
