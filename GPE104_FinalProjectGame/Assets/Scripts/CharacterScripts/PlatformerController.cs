using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//adds the rigidbody 2d component with it once added
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlatformerController : CharacterController
{
    //NonDesigner Friendly variables
    private Rigidbody2D rbody;
    private SpriteRenderer sr;
    private Transform groundPointTransform;
    private Transform tf;

    public HealthBar healthBar;
    public HealthBar fuelBar;
    public bool bulletImmunity = false;


    //ammo stats
    public int ammoInMag;
    public int shotgunAmmoCapacity;
    public int BFGAmmoCapacity;

    //bfg shot delay variables
    public float bfgDelay = 1.0f;
    private float coolDownTime;

    public float fuel;
    public int maxFuel;
    bool BFGCanShoot
    {
        get
        {
            return (coolDownTime <= 0);
        }
    }
    private void Awake()
    {
        ////checks to see if theres a duplicate player on the scene
        if (GameObject.Find("Player") != null)
        {
            Debug.LogError("Tried to create duplicate");
            Destroy(this.gameObject);
        }
        else
        {
            GameManager.instance.player = this.gameObject;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //get the neccesary components
        rbody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        groundPointTransform = transform.Find("GroundedPoint");
        animator = GetComponent<Animator>();
        tf = GetComponent<Transform>();

        //player starts with single fire
        equippedWeapon = weapon.SingleFire;
        animator.SetBool("SingleShot", true);

        //set up health and health bar
        healthBar = GameObject.Find("PlayerhealthBar").GetComponent<HealthBar>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        //fuel bar
        fuelBar = GameObject.Find("fuelBar").GetComponent<HealthBar>();
        fuel = maxFuel;
        fuelBar.SetMaxHealth(maxFuel);

        //GameManager Stuff
        GameManager.instance.playerController = this;
        GameManager.instance.player = this.gameObject;
        GameManager.instance.playerTransform = this.gameObject.transform;
    }
    public override void Die()
    {
        GameManager.instance.playerLives -= 1;
        GameManager.instance.lives[GameManager.instance.playerLives].gameObject.SetActive(false);
        base.Die();

        if (GameManager.instance.playerLives == 0)
        {
            GameManager.instance.currentState = GameManager.gameState.GameLose;
            GameManager.instance.LoadLevel("Lose Screen");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Die();
        }

        //update the health bar to player's current health
        healthBar.SetHealth(health);

        //fuel bar
        fuelBar.SetHealth(fuel);

        //X-Axis movment Controls
        float xMovement = Input.GetAxis("Horizontal") * movementSpeed;
        rbody.velocity = new Vector2(xMovement, rbody.velocity.y);

        //set speed value parameter to trigger the run animation
        animator.SetFloat("Speed", Mathf.Abs(xMovement));
        animator.SetBool("Grounded", grounded);
        animator.SetFloat("VertSpeed", rbody.velocity.y);

        //Jump mechanics
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            //applies vertical "jumpForce" to the player to make him jump
            rbody.AddForce(Vector2.up * jumpForce);
        }

        //if the player presses down while walking
        if (Input.GetAxis("Vertical") == -1 && xMovement != 0)
        {
            animator.SetBool("IsRolling", true);
        }

        //face the player in the direction he's moving
        //when moving right
        if (xMovement > 0)
        {
            //face right
            tf.rotation = Quaternion.Euler(0, 0, 0);
        }
        //when moving left
        else if (xMovement < 0)
        {
            //face left
            tf.rotation = Quaternion.Euler(0, 180, 0);
        }

        //Shooting
        //if player press 1 - 3
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SingleSwitch();
            GameManager.instance.ammoPreview.sprite = GameManager.instance.bullet;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShotgunSwitch();
            GameManager.instance.ammoPreview.sprite = GameManager.instance.shotgun;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            BFGSwitch();
            GameManager.instance.ammoPreview.sprite = GameManager.instance.bigBullet;
        }


        //if player clicks f
        if (Input.GetKeyDown(KeyCode.F))
        {
            //the bfg is equipped
            if (equippedWeapon == weapon.BFG)
            {
                //if bfg can shoot
                if (BFGCanShoot)
                {
                    animator.SetTrigger("Shoot");
                    //reset the timer
                    coolDownTime = bfgDelay;
                }
            }
            else
            {
                animator.SetTrigger("Shoot");
            }
        }
        //cools down the timer
        if (!BFGCanShoot)
        {
            coolDownTime -= Time.deltaTime;
        }



        //hover mechancics
        if (Input.GetKey(KeyCode.X) && !grounded && fuel > 0)
        {
            rbody.velocity = new Vector2(rbody.velocity.x, 0);
            rbody.gravityScale = 0;
            fuel -= Time.deltaTime;
        }
        //refuel
        else if ((fuel < maxFuel))
        {
            rbody.gravityScale = 1;
            if (grounded)
            {
                fuel += Time.deltaTime;
            }
        }

        //send a raycast to see if the player is close enough to the ground to be consider "grounded"
        RaycastHit2D hitInfo = Physics2D.Raycast(groundPointTransform.position, Vector2.down, 0.1f);
        //if on the groud
        if (hitInfo.collider != null)
        {
            grounded = true;
        }
        //if not on the ground
        else
        {
            grounded = false;
        }


    }

}
