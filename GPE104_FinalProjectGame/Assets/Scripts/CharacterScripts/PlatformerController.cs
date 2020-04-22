using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool bulletImmunity = false;
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
        tf = GetComponent<Transform>();

        //player starts with single fire
        equippedWeapon = weapon.SingleFire;
        animator.SetBool("SingleShot", true);
    }

    // Update is called once per frame
    void Update()
    {
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
        //if player press 1 - 4
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SingleSwitch();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AssualtSwitch();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShotgunSwitch();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            BFGSwitch();
        }
        //if player clicks f
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Shoot");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.Play("PlayerKick");
        }

        //  //hover mechancics
        if (Input.GetKey(KeyCode.X) && !grounded)
        {
            rbody.velocity = new Vector2(rbody.velocity.x, 0);
            rbody.gravityScale = 0;
        }
        //when the player releases spacebar after hovering
        if (Input.GetKeyUp(KeyCode.X) && !grounded)
        {
            rbody.gravityScale = 1;
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

        //when the no health
        if (health <= 0)
        {
            Die();
        }
    }
}
