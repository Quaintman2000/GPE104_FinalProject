using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomScript : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //disable the collider at start so things can pass through
        var boxCollider = animator.gameObject.GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;

        //disable rigidbody so it doesnt fall through the earth
        var rigidbody2d = animator.gameObject.GetComponent<Rigidbody2D>();
        rigidbody2d.gravityScale = 0;

        //play the explosion sound
        var controller = animator.gameObject.GetComponent<CharacterController>();
        controller.Boom();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //destroy the enemy when done with animation
        Destroy(animator.gameObject);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
