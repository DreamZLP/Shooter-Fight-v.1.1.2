using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{
    float timer;
    Transform player;
    float attackRange = 15;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            animator.SetBool("isPatroling", true);
        }
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (distance < attackRange || Weapon.hitEnemy == true)
        {
            animator.SetBool("isAttacking", true);
        }
        if (distance > 15)
        {
            animator.SetBool("isAttacking", false);
            Weapon.hitEnemy = false;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
