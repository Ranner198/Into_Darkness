using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpearGun : MonoBehaviour {

    //public Animator anim;
    public static bool shootState = false;

    public int ammo = 5;
    public Text ammoText;
    public GameObject spear;
    public GameObject spawnPoint;
    private Rigidbody rb;
    public GameObject helment;
    private bool isReloading = false;

	void Update () {

        DisplayAmmo();

        if (Input.GetButtonDown("Fire2"))
        {
            shootState = !shootState;
            print(shootState);
        }

        if (shootState)
        {
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
    }

    void Shoot()
    {
        GameObject Spear = Instantiate(spear, spawnPoint.transform.position, helment.transform.rotation);
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
        print("reloaded!");
        isReloading = false;
    }
}
