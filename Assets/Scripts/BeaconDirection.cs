using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconDirection : MonoBehaviour {

    private GameObject target, player;

    public string subName = "Sub", playerName = "Player";

    public Terrain terrain;

    private float Distance;

    void Start () {
        target = GameObject.FindGameObjectWithTag(subName);
        player = GameObject.FindGameObjectWithTag(playerName);
    }

	void Update () {
        /*
        Vector3 Direction = -(player.transform.position - target.transform.position).normalized;

        Distance = (player.transform.position - target.transform.position).magnitude;

        float MapHypotnuse = Mathf.Sqrt(Mathf.Pow(terrain.terrainData.size.x, 2) + (Mathf.Pow(terrain.terrainData.size.z, 2)));

        if (Distance > MapHypotnuse/2)
        {
            Debug.DrawRay(player.transform.position, -Direction * 9, Color.red);
        }
        else
        {
            Debug.DrawRay(player.transform.position, Direction * 9, Color.red);
        }
        */
    }
}
