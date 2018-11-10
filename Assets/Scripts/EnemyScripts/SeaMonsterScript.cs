using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMonsterScript : MonoBehaviour {

    public EnemyClass monster = new EnemyClass(12, 100);
    public float time = 10;

    private GameObject player;
    private Terrain terrain;
    private Rigidbody rigidBody;
    private Animator animator;

    // Use this for initialization
    void Start () {
        monster.SetState(2);
        monster.GenerateAggro();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).gameObject;
        if (terrain == null)
            terrain = FindObjectOfType<Terrain>();

        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        timer();
    }
	
	// Update is called once per frame
	void Update () {
        if (monster.GetState() == 2)
        {
            monster.Attack(player, gameObject, terrain, 5f);
            animator.SetFloat("Direction", 1);
            animator.Play("Passive", 1, float.NegativeInfinity);

            StartCoroutine(BiteAndRun());
        }
    }

    IEnumerator BiteAndRun()
    {
        yield return new WaitForSeconds(1.15f);
        monster.SetState(5);
        animator.Play("Passive");
        yield return new WaitForSeconds(.5f);
        monster.SetState(3);
        yield return new WaitForSeconds(8f);
        monster.SetState(0);
    }

    void timer() {
        time -= Time.deltaTime;
    }
}