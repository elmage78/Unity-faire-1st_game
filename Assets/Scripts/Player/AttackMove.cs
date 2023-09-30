using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class AttackAnimation
{
    public static bool AttackAnimationActive;
}

public class AttackMove : StateMachineBehaviour
{
    CinemachineVirtualCamera Camera;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AttackAnimation.AttackAnimationActive = true;
        Camera = FindAnyObjectByType<CinemachineVirtualCamera>();
        Camera.GetComponent<Shake>().ShakeCamera(2.5f, 2.5f, 0.15f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AttackAnimation.AttackAnimationActive = false;
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
