using UnityEngine;
using UnityEngine.UI;

public class SpearGun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public int ammo = 5;
    public Text ammoText;
    public GameObject spear;
    public GameObject spawnPoint;
    public Camera fpsCam;
	
	// Update is called once per frame
	void Update () {

        DisplayAmmo();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

	}

    void Shoot()
    {
        /*
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
        */

        //check to make sure there is enough ammo
        if (ammo > 0)
        {
            Object clone = Instantiate(spear, spawnPoint.transform.position, fpsCam.transform.rotation);
            Destroy(clone, 3.0f);
        }

    }

    void DisplayAmmo ()
    {
        ammoText.text = "Ammo: " + ammo;
    }

}
