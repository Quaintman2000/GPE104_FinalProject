using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class AIController : CharacterController
{
    //NonDesigner Friendly variables
    public Rigidbody2D rbody;
    public SpriteRenderer sr;
    public Transform groundPointTransform;
    public Transform tf;
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
        //face the player in the direction he's moving
        //when moving right
        if (rbody.velocity.x > 0)
        {
            //face right
            tf.rotation = Quaternion.Euler(0, 0, 0);
        }
        //when moving left
        else if (rbody.velocity.x < 0)
        {
            //face left
            tf.rotation = Quaternion.Euler(0, 180, 0);
        }

        ////send a raycast to see if the player is close enough to the ground to be consider "grounded"
        //RaycastHit2D hitInfo = Physics2D.Raycast(groundPointTransform.position, Vector2.down, 0.1f);
        ////if on the groud
        //if (hitInfo.collider != null)
        //{
        //    grounded = true;
        //    //replenish the extra jumps
        //    //  additionalJumps = 1;
        //}
        ////if not on the ground
        //else
        //{
        //    grounded = false;
        //}

        //if the player has no health
        if (health <= 0)
        {
            Die();
        }
    }
   
}
