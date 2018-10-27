using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderFromArray : MonoBehaviour {

    public Sprite[] numbers;
    public GameObject[] spriteObjects;
    public SpriteRenderer[] SR;

    //Amt Variables
    public float mins;
    public float tens;

    void Start()
    {
        //Get all the sprite renderer componetes to access the sprites
        for (int i = 0; i < spriteObjects.Length; i++)
        {
            SR[i] = spriteObjects[i].GetComponent<SpriteRenderer>();
        }
    }

    void Update() {

        //Update temp variable to Timer
        var temp = PlayerMovement.player.GetOxygenLevel();
        Debug.Log(PlayerMovement.player.GetOxygenLevel());
        mins = 0;
        tens = 0;

        SR[0].GetComponent<SpriteRenderer>().sprite = numbers[0];

        while (temp >= 60)
        {
            temp -= 60;
            mins++;
        }
        while (temp >= 10)
        {
            temp -= 10;
            tens++;
        }
        Debug.Log(temp);
        SR[1].GetComponent<SpriteRenderer>().sprite = numbers[Mathf.FloorToInt(mins)];
        SR[2].GetComponent<SpriteRenderer>().sprite = numbers[Mathf.FloorToInt(tens)];
        SR[3].GetComponent<SpriteRenderer>().sprite = numbers[Mathf.FloorToInt(temp)];
	}
}
