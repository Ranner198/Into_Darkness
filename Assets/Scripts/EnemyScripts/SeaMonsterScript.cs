using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMonsterScript : MonoBehaviour {

    public EnemyClass monster = new EnemyClass(12, 100);
    public bool debugMode = false;

    private GameObject player;
    private Terrain terrain;   
    private Rigidbody rb;
    private Animator anim;

    // Use this for initialization
    void Start () {
        monster.SetState(0);

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).gameObject;
        if (terrain == null)
            terrain = FindObjectOfType<Terrain>();

        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (monster.GetState() == 0)
        {
            //passive
            monster.PassiveFix(player, gameObject, terrain, 55);
            if (debugMode)
                print("Passive");
            anim.SetFloat("Direction", -1);
            anim.Play("Passive", -1, float.NegativeInfinity);
        }
    }
}
