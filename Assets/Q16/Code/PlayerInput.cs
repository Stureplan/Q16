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













    public float walkSpeed = 6.0f;
    public float runSpeed = 10.0f;
    public float jumpSpeed = 4.0f;
    public float gravity = 10.0f;
    public float slideSpeed = 2.5f;
    // Small amounts of this results in bumping when walking down slopes, but large amounts results in falling too fast
    public float antiBumpFactor = .75f;


    // If true, diagonal speed (when strafing + moving forward or back) can't exceed normal move speed; otherwise it's about 1.4 times faster
    private bool limitDiagonalSpeed = false;
    private bool enableRunning = true;
    // If the player ends up on a slope which is at least the Slope Limit as set on the character controller, then he will slide down
    private bool slideWhenOverSlopeLimit = true;
    // If checked and the player is on an object tagged "Slide", he will slide down it regardless of the slope limit
    private bool slideOnTaggedObjects = false;
    // If checked, then the player can change direction while in the air
    private bool airControl = false;

    // Player must be grounded for at least this many physics frames before being able to jump again; set to 0 to allow bunny hopping
    public int antiBunnyHopFactor = 0;

    private Vector3 moveDirection = Vector3.zero;
    private bool grounded = false;
    private CharacterController controller;
    private Transform myTransform;
    private float speed;
    private RaycastHit hit;
    private float fallStartLevel;
    private bool falling;
    private float slideLimit;
    private float rayDistance;
    private Vector3 contactPoint;
    private bool playerControl = false;
    private int jumpTimer;


    private Vector3 exForces;










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






        



        myTransform = transform;
        speed = walkSpeed;
        rayDistance = (cc.height * .5f) + cc.radius;
        slideLimit = cc.slopeLimit - .1f;
        jumpTimer = antiBunnyHopFactor;
    }











    void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Right");
        float inputY = Input.GetAxisRaw("Forward");
        // If both horizontal and vertical are used simultaneously, limit speed (if allowed), so the total doesn't exceed normal move speed
        float inputModifyFactor = (inputX != 0.0f && inputY != 0.0f && limitDiagonalSpeed) ? .7071f : 1.0f;

        if (grounded)
        {
            bool sliding = false;
            // See if surface immediately below should be slid down. We use this normally rather than a ControllerColliderHit point,
            // because that interferes with step climbing amongst other annoyances
            if (Physics.Raycast(myTransform.position, -Vector3.up, out hit, rayDistance))
            {
                if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                {
                    sliding = true;
                }
            }
            // However, just raycasting straight down from the center can fail when on steep slopes
            // So if the above raycast didn't catch anything, raycast down from the stored ControllerColliderHit point instead
            else
            {
                Physics.Raycast(contactPoint + Vector3.up, -Vector3.up, out hit);
                if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                {
                    sliding = true;
                }
            }

            // If we were falling, and we fell a vertical distance greater than the threshold, run a falling damage routine
            if (falling)
            {
                falling = false;
                FallingDamageAlert(fallStartLevel - myTransform.position.y);
            }

            if (enableRunning)
            {
                //speed = Input.GetButton("Run") ? runSpeed : walkSpeed;
            }

            // If sliding (and it's allowed), or if we're on an object tagged "Slide", get a vector pointing down the slope we're on
            if ((sliding && slideWhenOverSlopeLimit))
            {
                Vector3 hitNormal = hit.normal;
                moveDirection = new Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);
                Vector3.OrthoNormalize(ref hitNormal, ref moveDirection);
                moveDirection *= slideSpeed;
                playerControl = false;
            }
            // Otherwise recalculate moveDirection directly from axes, adding a bit of -y to avoid bumping down inclines
            else
            {
                moveDirection = new Vector3(inputX * inputModifyFactor, -antiBumpFactor, inputY * inputModifyFactor);
                moveDirection = myTransform.TransformDirection(moveDirection) * speed;
                playerControl = true;
            }

            // Jump! But only if the jump button has been released and player has been grounded for a given number of frames
            if (!Input.GetButton("Jump"))
                jumpTimer++;
            else if (jumpTimer >= antiBunnyHopFactor)
            {
                moveDirection.y = jumpSpeed;
                jumpTimer = 0;
            }
        }
        else
        {
            // If we stepped over a cliff or something, set the height at which we started falling
            if (!falling)
            {
                falling = true;
                fallStartLevel = myTransform.position.y;
            }

            // If air control is allowed, check movement but don't touch the y component
            if (airControl && playerControl)
            {
                moveDirection.x = inputX * speed * inputModifyFactor;
                moveDirection.z = inputY * speed * inputModifyFactor;
                moveDirection = myTransform.TransformDirection(moveDirection);
            }
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;



        // Move the controller, and set grounded true or false depending on whether we're standing on something
        grounded = (cc.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;



        //FORCES --v
        if (exForces.magnitude > 0.2f)
        {
            //if (!grounded)
            {
                cc.Move(exForces * Time.deltaTime);
            }

            //if (grounded) { exForces = Vector3.zero; }
            //if (!grounded)
            exForces = Vector3.Lerp(exForces, Vector3.zero, Time.deltaTime);
        }
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
        //cc.Move(finalMove * Time.deltaTime);

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
        contactPoint = hit.point;

        exForces = Vector3.zero;

        //If we get pushed somewhere and collide with a wall or anything,
        //disable the pushing force.
        //forces.x = 0.0f; forces.z = 0.0f;

        if (Vector3.Angle(hit.normal, Vector3.up) > 15.0f)
        {
            //forces = Vector3.Reflect(-Vector3.up, hit.normal);
        }



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
        //AddForce(v, 15.0f);
    }

    public void AddForce(Vector3 dir, float force)
    {
        dir.Normalize();

        exForces += dir * force / mass;
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

    void FallingDamageAlert(float fallDistance)
    {
        //Play hit ground sfx
       
    }
}
