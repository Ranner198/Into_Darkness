using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{

    public GameObject Player;
    public Camera main;
    public PlayerMovement pm;
    public MouseLookScript mls;

    public bool lost = false;

    void Start()
    {
        pm = Player.GetComponent<PlayerMovement>();
        main = Player.transform.GetChild(0).GetComponent<Camera>();
        mls = main.GetComponent<MouseLookScript>();
    }

    void Update()
    {
        if (lost)
        {
            Player.GetComponent<Collider>().enabled = false;

            //pm.enabled = false;
            mls.enabled = false;

            Player.transform.Translate(Vector3.down * 50);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            print("Working");
            lost = true;
        }

    }
}