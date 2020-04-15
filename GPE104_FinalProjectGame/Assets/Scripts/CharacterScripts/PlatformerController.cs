using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//adds the rigidbody 2d component with it once added
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlatformerController : CharacterController
{
    //NonDesigner Friendly variables
    public Rigidbody2D rbody;
    public SpriteRenderer sr;
    public Transform groundPointTransform;
    public Animator animator;
    public bool shotgunEquipped = false;
    public bool assualtRifleEquipped = false;
    public bool bfgEquipped = false;
    public bool singleFireEquipped = false;

    //designer variables
    public float movementSpeed = 5;
    public bool grounded = false;
    public float jumpForce = 350;

    private void Awake()
    {
        ////checks to see if theres a duplicate player on the scene
        //if(GameObject.Find("Player") != null)
        //{
        //    Debug.LogError("Tried to create duplicate");
        //    Destroy(this.gameObject);
        //}
        //else
        //{

        //}
    }
    // Start is called before the first frame update
    void Start()
    {
        //get the neccesary components
        rbody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        groundPointTransform = transform.Find("GroundedPoint");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //X-Axis movment Controls
        float xMovement = Input.GetAxis("Horizontal") * movementSpeed;
        rbody.velocity = new Vector2(xMovement, rbody.velocity.y);

        //Jump mechanics
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            //applies vertical "jumpForce" to the player to make him jump
            rbody.AddForce(Vector2.up * jumpForce);
        }

        //face the player in the direction he's moving
        //when moving left
        if (xMovement > 0)
        {
            sr.flipX = false; 
            // flip the firepoint
            firePoint.position = new Vector3(this.transform.position.x + 0.38f, firePoint.position.y, 0);
            firePoint.rotation = Quaternion.Euler(0, 0, 0);
            if (grounded)
            {
                //if the player presses S while walking
                if (Input.GetKey(KeyCode.S))
                {
                    animator.Play("PlayerRoll");
                    Debug.Log("roll");
                }
                else
                {
                    animator.Play("PlayerWalk");
                }
            }
            else
            {
                //if moving up
                if (rbody.velocity.y > 0)
                {
                    animator.Play("PlayerJump");
                }
                else
                {
                    animator.Play("PlayerFall");
                }
            }
        }
        //when moving right
        else if (xMovement < 0)
        {
            //face right
            sr.flipX = true;
            // flip the firepoint
            firePoint.position = new Vector3(this.transform.position.x - 0.38f, firePoint.position.y, 0);
            firePoint.rotation = Quaternion.Euler(0, 180,0 );
            //if on the ground
            if (grounded)
            {
                //if the player presses S while walking
                if (Input.GetKey(KeyCode.S))
                {
                    animator.Play("PlayerRoll");
                    Debug.Log("roll");
                }
                else
                {
                    animator.Play("PlayerWalk");
                }
            }
            else
            {
                //if moving up
                if (rbody.velocity.y > 0)
                {
                    animator.Play("PlayerJump");
                }
                else
                {
                    animator.Play("PlayerFall");
                }
            }

        }
        //if not moving horizontally
        else
        {
            //if on the ground
            if (grounded)
            {
                animator.Play("PlayerIdle");
            }
            else
            {
                //if moving up
                if (rbody.velocity.y > 0)
                {
                    animator.Play("PlayerJump");
                }
                else
                {
                    animator.Play("PlayerFall");
                }
            }
        }

        //Shooting
        //if player left clicks
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (shotgunEquipped)
            {
                animator.Play("PlayerDoubleShoot");
                ShotgunShoot(10);
            }
            else if (assualtRifleEquipped)
            {
                animator.Play("PlayerDoubleShoot");
            }
            else if (bfgEquipped)
            {
                animator.Play("PlayerChargeShoot");
                SingleShoot(bigBulletPrefab);
            }
            else
            {
                animator.Play("PlayerSingleShoot");
                SingleShoot(bulletPrefab);
            }
        }
        //  //hover mechancics
        //if(Input.GetKey(KeyCode.Space) && !grounded && rbody.position.y >= 1)
        //{
        //    rbody.velocity = new Vector2(rbody.velocity.x, 0);
        //}

        //send a raycast to see if the player is close enough to the ground to be consider "grounded"
        RaycastHit2D hitInfo = Physics2D.Raycast(groundPointTransform.position, Vector2.down, 0.1f);
        //if on the groud
        if (hitInfo.collider != null)
        {
            grounded = true;
            //replenish the extra jumps
            //  additionalJumps = 1;
        }
        //if not on the ground
        else
        {
            grounded = false;
        }
    }
}
