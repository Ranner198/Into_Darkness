using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    private Animator animator;
    private GameObject player;
    private bool collidedWithPlayer;

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
        print("enter trigger with _player");
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            collidedWithPlayer = true;
        }
        print("enter collided with _player");
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject == player)
        {
            collidedWithPlayer = false;
        }
        print("exit collided with _player");
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            animator.SetBool("IsNearPlayer", false);
        }
        print("exit trigger with _player");
    }

    void Attack()
    {
        if (collidedWithPlayer)
        {
            print("player has been hit");
        }
    }
}