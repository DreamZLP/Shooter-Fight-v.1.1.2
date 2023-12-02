using EvolveGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AttackBehavior : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    GameObject enemy;
    GameObject enemyObj;
    GameObject enemyObjChild;
    float attackRange = 15;
    public double damageCount = 1;
    public float fireRate = 1;
    public float range = 100;
    public float nextFire = 0f;
    public GameObject hitEffect;
    public AudioClip shotSFX;
    public AudioSource audioSource;
    public string EnemyEye;
    private float tikFirst = 0.0f;
    private float tikSecond = 0.0f;
    private float tikCounts = 0.0f;
    private PlayerController playerController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        audioSource = agent.GetComponent<AudioSource>();
        enemy = agent.transform.gameObject;
        enemyObj = enemy.transform.Find("EnemyCanvas").gameObject;
        //enemyObjChild = enemyObj.transform.GetComponentInChildren<Camera>();
        if (Weapon.moreDmg)
        {
            tikCounts = 0.05f;
        }
        else
        {
            tikCounts = 0.10f;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if ((distance < attackRange || Weapon.hitEnemy == true) && !playerController.ifPause)
        {            
            animator.SetBool("isAttacking", true);
            agent.speed = 0;
            tikFirst += Time.deltaTime;
            if (tikFirst > 0.1f)
            {
                audioSource.PlayOneShot(shotSFX);
                tikFirst = 0.0f;
                if (PlayerManager.gameOver)
                {
                    audioSource = null;
                }
            }
            animator.transform.LookAt(player.position);            
            RaycastHit hit;
            if (Physics.Raycast(animator.transform.position, animator.transform.forward, out hit, range))
            {  
                GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                if (hit.collider.tag == "Player")
                {
                    tikSecond += Time.deltaTime;
                    if (tikSecond > tikCounts)
                    {
                        PlayerManager.Damage(damageCount);
                        tikSecond = 0.0f;
                    }                   
                }
                Destroy(impact, 2f);
            }
        }
        if (distance > 15)
        {
            animator.SetBool("isAttacking", false);
            animator.SetBool("isPatroling", true);
            agent.speed = 4;
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }

}
