using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpearGun : MonoBehaviour {

    //public Animator anim;
    public static bool shootState = false;

    public int ammo = 5;
    public GameObject spear;
    public Text ammoText;
    public GameObject prefabObject;
    public GameObject spawnPoint;
    private Rigidbody rb;
    public GameObject helment;
    private bool isReloading = false;

    private Animator anim;
    private GameObject Spear;
    private bool shot = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update () {

        DisplayAmmo();

        if (!CheckAirGaugeAnimationController.checkAirGuage)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                shootState = !shootState;
            }
        }

        if (shootState)
        {
            if (ammo > 0 && !shot)
            {
                //GameObject prefab = Instantiate(prefabObject, spawnPoint.transform.position, spawnPoint.transform.rotation);
                //prefab.transform.parent = spawnPoint.transform;
                shot = true;
            }
            anim.Play("Shoot");
            if (Input.GetButtonDown("Fire1") && ammo > 0)
            {
                Shoot();            
            }
            else if (ammo <= 0 && !isReloading)
            {
                StartCoroutine(Reload());
            }
            else if (Input.GetButtonDown("Fire1") && ammo <= 0)
            {
                //Add Dry Fire Sound
            }
        }
        else
            anim.Play("Shootn't");
    }

    void Shoot()
    {
        //Parent and hold in place until it is fired
        Spear = Instantiate(spear, spawnPoint.transform.position, helment.transform.rotation);
        Spear.name = "Spear";
        rb = Spear.transform.GetChild(1).GetComponent<Rigidbody>();
        rb.AddRelativeForce(1000 * Time.deltaTime * 60 * Vector3.up);
        //Destroy(clone, 3.0f);   
        if(!isReloading)
            StartCoroutine(Reload());
    }

    void DisplayAmmo ()
    {
        ammoText.text = "Ammo: " + ammo;
    }

    public IEnumerator Reload()
    {
        isReloading = true;
        //Start Reload Animation
        yield return new WaitForSeconds(Random.Range(5, 10));
        ammo = 1;
        shot = false;
        print("reloaded!");
        isReloading = false;
    }
}
