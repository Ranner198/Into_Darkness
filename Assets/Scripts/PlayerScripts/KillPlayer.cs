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
        playerMovement = Player.GetComponent<PlayerMovement>();
        mainCam = Player.transform.GetChild(0).GetChild(0).GetComponent<Camera>();
        mouseLook = mainCam.GetComponent<MouseLookScript>();
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
