using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpearGun : MonoBehaviour {

    //public Animator anim;
    public static bool shootState = false;
    public int ammo = 20;
    public GameObject spear;
    public Text ammoText;
    public GameObject prefabObject;
    public GameObject spawnPoint;
    public GameObject helment;

    private Animator anim;
    private GameObject Spear;
    private bool shot = false;
    private float frameCount;//Anim Frame Number
    private Rigidbody rb;
    private bool isReloading = false;
    private bool loaded = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        PlayerMovement.player.SetAmmo(20);
    }

    void Update () {

        //Show Ammo Left
        DisplayAmmo();
        Debug.Log(PlayerMovement.player.GetAmmo());
        //If airguage isn't pulled up allow transition to shooting state
        if (!CheckAirGaugeAnimationController.checkAirGuage)
        {
            //Get Right Mouse Button
            if (Input.GetButtonDown("Fire2"))
            {
                //Toggle Shooting Game State
                if(!isReloading)
                    shootState = !shootState;
            }
        }

        //On shoot state
        if (shootState)
        {
            if (frameCount < 1)
                frameCount += Time.deltaTime / 2;
  
            if (loaded)
                anim.Play("Shoot");

            if (Input.GetButtonDown("Fire1") && loaded)
            {
                Shoot();
                loaded = false;
                shootState = false;
            }

            if (!loaded && !isReloading)
            {
                StartCoroutine(Reload());
            }

            if (Input.GetButtonDown("Fire1") && PlayerMovement.player.GetAmmo() <= 0)
            {
                //Add Dry Fire Sound
            }
        }
        else
        {
            if (frameCount > 0)
            {
                anim.SetFloat("Direction", -1);
                anim.Play("Shoot", 0, frameCount);
                frameCount -= Time.deltaTime / 2;
            }
            else
              anim.Play("Shootn't");
        }
    }

    void Shoot()
    {
        //Parent and hold in place until it is fired
        Spear = Instantiate(spear, spawnPoint.transform.position, helment.transform.rotation);
        Spear.name = "Spear";
        rb = Spear.transform.GetChild(1).GetComponent<Rigidbody>();
        rb.AddRelativeForce(1000 * Time.deltaTime * 60 * Vector3.up);
        PlayerMovement.player.AddAmmo(-1); 
    }

    void DisplayAmmo ()
    {
        ammoText.text = "Ammo: " + PlayerMovement.player.GetAmmo();
    }

    public IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(Random.Range(5, 8));
        //anim.Play("Shoot");
        loaded = true;
        shot = false;
        print("reloaded!");
        isReloading = false;
        shootState = true;
    }
}
