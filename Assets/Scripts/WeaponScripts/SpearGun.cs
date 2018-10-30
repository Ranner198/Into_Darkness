using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpearGun : MonoBehaviour {

    //public Animator anim;
    public static bool shootState = false;
    public int ammo = 5, maxAmmo = 20;
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

    private float VRCoolDown = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        PlayerMovement.player.SetAmmo(ammo);
    }

    void Update () {

        //Check and make sure ammo doesnt exceed max ammo variable
        if (PlayerMovement.player.GetAmmo() > maxAmmo)
            PlayerMovement.player.SetAmmo(maxAmmo);
        //Show Ammo Left
        DisplayAmmo();
        //If airguage isn't pulled up allow transition to shooting state
        if (!CheckAirGaugeAnimationController.checkAirGuage)
        {

            if (Input.GetButton("Fire2") && VRCoolDown < 0 && !shootState)
            {
                VRCoolDown = 2;
                print("Working");
                if (!isReloading)
                    shootState = true;
                return;
            }
            else if (Input.GetButton("Fire2") && VRCoolDown < 0 && shootState)
            {
                VRCoolDown = 2;
                print("Working");
                if (!isReloading)
                    shootState = false;
                return;
            }
        }

        if (VRCoolDown >= 0)
            VRCoolDown -= Time.deltaTime;

        //On shoot state
        if (shootState)
        {
            print(true);
            if (frameCount < 1)
                frameCount += Time.deltaTime / 2;
  
            if (loaded)
                anim.Play("Shoot");
            float input = Input.GetAxis("Fire");
            print(input);
            if (Input.GetButtonDown("Fire1") || input > 0.1f && loaded && PlayerMovement.player.GetAmmo() > 0)
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
        isReloading = false;
        shootState = true;
    }
}
