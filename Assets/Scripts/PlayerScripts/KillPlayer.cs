using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour {

    public GameObject Player;
    public Camera mainCam;
    public PlayerMovement playerMovement;
    public MouseLookScript mouseLook;

    public bool lost = false;

    void Start()
    {
<<<<<<< HEAD
        playerMovement = Player.GetComponent<PlayerMovement>();
        mainCam = Player.transform.GetChild(0).GetChild(0).GetComponent<Camera>();
        mouseLook = mainCam.GetComponent<MouseLookScript>();
=======
        pm = Player.GetComponent<PlayerMovement>();
        main = Player.transform.GetChild(0).GetComponent<Camera>();
        mls = main.GetComponent<MouseLookScript>();
    }

    void Update()
    {
        if (lost) {
            Player.GetComponent<Collider>().enabled = false;

            //pm.enabled = false;
            mls.enabled = false;

            Player.transform.Translate(Vector3.down * 50);
        }    
>>>>>>> origin/ranner
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.name == "Player") {
            SceneManager.LoadScene("GameOver");
            playerMovement.enabled = false;
            mouseLook.enabled = false;
        }
            
    }
}
