using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearLogic : MonoBehaviour {

    private Rigidbody rb;
    private float despawnTimer = 11;
    private bool stuck = false;

    private ParticleSystem PS;
    private GameObject[] shark;
    private SharkScript _SharkScript;
    private GameObject bleedPosition;

    void Start () {
        rb = GetComponent<Rigidbody>();
        PS = transform.parent.GetChild(0).GetComponent<ParticleSystem>();
        bleedPosition = transform.GetChild(2).gameObject;
    }

    void Update()
    {
        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * -7 * Time.deltaTime, rb.velocity.z);

        if (despawnTimer >= 0 && !stuck)
            despawnTimer -= Time.deltaTime;

        if (despawnTimer < 0)
            DestroyImmediate(gameObject);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Terrain")
        {
            rb.isKinematic = true;
            transform.SetParent(coll.transform);
            stuck = true;
            Destroy(this);
        }
        if (coll.tag == "Shark")
        {
            transform.parent.SetParent(coll.transform);
            print("Hit a Shark, Name: " + coll.gameObject.name);
            
            shark = GameObject.FindGameObjectsWithTag("Shark");
            for (int i = 0; i < shark.Length; i++)
            {
                if (shark[i].name == coll.name)
                {
                    SharkScript _SharkScript = shark[i].GetComponent<SharkScript>();
                    StartCoroutine(_SharkScript.GetComponent<SharkScript>().Hit());
                }
            }
            rb.isKinematic = true;
            stuck = true;
            ParticleSystem Bleeding = Instantiate(PS, transform.position, Quaternion.Inverse(transform.rotation));
            PS.Play();
            PS.transform.position = bleedPosition.transform.position;
            Destroy(this);
        }
    }
}
