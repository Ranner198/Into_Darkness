using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    private Animator animator;
    private GameObject player;
    private bool collidedWithPlayer;

    public bool DebugMode = false;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Attack();
    }   

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            animator.SetBool("IsNearPlayer", true);
        }
        if (DebugMode) print("enter trigger with _player");
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            collidedWithPlayer = true;
        }
        if (DebugMode) print("enter collided with _player");
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject == player)
        {
            collidedWithPlayer = false;
        }
        if (DebugMode) print("exit collided with _player");
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            animator.SetBool("IsNearPlayer", false);
        }
        if (DebugMode) print("exit trigger with _player");
    }

    void Attack()
    {
        if (collidedWithPlayer)
        {
            if (DebugMode) print("player has been hit");
        }
    }
}