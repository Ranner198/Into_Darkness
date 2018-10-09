using UnityEngine;
using UnityEngine.UI;

public class SpearGun : MonoBehaviour {

    public int ammo = 5;
    public Text ammoText;
    public GameObject spear;
    public GameObject spawnPoint;
    private Rigidbody rb;
    public GameObject helment;
    //Add a reload function with animation and a timer into it

	// Update is called once per frame
	void Update () {

        DisplayAmmo();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
            ammo = 5;
    }

    void Shoot()
    {
        //check to make sure there is enough ammo
        if (ammo > 0)
        {
            GameObject Spear = Instantiate(spear, spawnPoint.transform.position, helment.transform.rotation);
            Spear.name = "Spear";
            rb = Spear.transform.GetChild(1).GetComponent<Rigidbody>();
            rb.AddRelativeForce(1000 * Time.deltaTime * 60 * Vector3.up);
            //Destroy(clone, 3.0f);           
            ammo--;
        }

    }

    void DisplayAmmo ()
    {
        ammoText.text = "Ammo: " + ammo;
    }

}
