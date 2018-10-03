using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterToPlayerCircleArea : MonoBehaviour {

    public GameObject Player;

	void Update () {

        Vector3 moveUp = Player.transform.position;

        moveUp.y += 5;

        transform.position = moveUp;

	}
}
