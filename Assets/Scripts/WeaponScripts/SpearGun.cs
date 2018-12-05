using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpearGun : MonoBehaviour {

    //public Animator anim;
    public static bool shootState = false;
    public int ammo = 5, maxAmmo = 5;
    public GameObject AmmoImageHolder;
    public Image[] AmmoImages;
    public GameObject spear;
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

    public AudioClip reloadSound;
    public AudioClip shootSound;
    public AudioSource audioSource;

    void Start()
    {
        anim = GetComponent<Animator>();
        PlayerMovement.player.SetAmmo(ammo);

        //Load in images
        for (int i = 0; i > maxAmmo; i++)
        {
            AmmoImages[i] = AmmoImageHolder.GetComponent<Image>();
        }
    }

    void Update () {

        //Check and make sure ammo doesnt exceed max ammo variable
        if (PlayerMovement.player.GetAmmo() > maxAmmo)
            PlayerMovement.player.SetAmmo(maxAmmo);
        //Show Ammo Left
        DisplayAmmo();
        //If airguage isn't pulled up allow transition to shooting state
        //Raise Arm
        if (Input.GetButton("Fire2") && VRCoolDown < 0 && !shootState)
        {
            VRCoolDown = 2;
            print("Working");
            if (!isReloading)
                shootState = true;
            return;
        }
        //Lower Arm
        else if (Input.GetButton("Fire2") && VRCoolDown < 0 && shootState)
        {
            VRCoolDown = 2;
            print("Working");
            if (!isReloading)
                shootState = false;
            return;
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
                audioSource.PlayOneShot(shootSound, 2f);
            }

            if (!loaded && !isReloading)
            {
                StartCoroutine(Reload());
                audioSource.PlayOneShot(reloadSound, 2f);
            }

            if (PlayerMovement.player.GetAmmo() <= 0)
            {
                shootState = false;
                anim.Play("Shootn't");
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

    void DisplayAmmo()
    {
        for (int i = maxAmmo-1; i > PlayerMovement.player.GetAmmo()-1; i--)
        {
            AmmoImages[i].enabled = false;
        }

        for (int i = 0; i < PlayerMovement.player.GetAmmo()-1; i++)
        {
            AmmoImages[i].enabled = true;
        }
    }

    public IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(1.75f);
        //anim.Play("Shoot");
        loaded = true;
        shot = false;
        isReloading = false;
        shootState = true;
    }
}
