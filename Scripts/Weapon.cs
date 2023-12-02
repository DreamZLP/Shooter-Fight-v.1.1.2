using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Threading;
using EvolveGames;
using UnityEditor;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public int damage;
    public float fireRate = 1;
    public float range = 15;
    public ParticleSystem muzzleFlash;
    public Transform bulletSpawn;
    public AudioClip shotSFX;   
    public AudioClip reloadSFX;
    public AudioClip zatvorSFX;
    public AudioSource effectsSource;
    public Camera cam;
    public float nextFire = 0f;
    public GameObject akm;
    //private Vector3 originalPos;
    private bool hardDiffuculty;
    static public bool hitEnemy = false;
    static public bool moreDmg = false;
    public GameObject hitEffectOthers;
    public GameObject hitEffectEnemy;
    public TextMeshProUGUI currentAmmoText;
    public TextMeshProUGUI fullAmmoText;
    public int currentAmmoAkm;
    public int fullAmmoAkm;
    public int currentAmmoMp7;
    public int fullAmmoMp7;
    // public Recoil Recoil_Script;
    public float currentRecoilXPos;
    public float currentRecoilYPos;
    [Range(0, 7f)] public float recoilAmountY;
    [Range(0, 3f)] public float recoilAmountX;
    private float timePressed;
    [Range(0, 10f)] public float maxRecoilTime = 4;
    private PlayerController playerController;
    private ItemChange itemChange;
    float reloadTimer;
    bool bShooting = true;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Difficulty") == 0)
        {
            hardDiffuculty = false;
            moreDmg = false;
        }
        if (PlayerPrefs.GetInt("Difficulty") == 1)
        {
            hardDiffuculty = true;
            moreDmg = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        itemChange = GameObject.Find("PlayerController").GetComponent<ItemChange>();
        if (currentAmmoAkm < 15 || currentAmmoMp7 < 6)
        {
            currentAmmoText.color = Color.red;
        }
        else
        {
            currentAmmoText.color = Color.white;
        }
        if (fullAmmoAkm == 0 || fullAmmoMp7 == 0)
        {
            fullAmmoText.color = Color.red;
        }
        else
        {
            fullAmmoText.color = Color.white;
        }
        if (itemChange.ItemIdInt == 0)
        {
            currentAmmoText.text = currentAmmoAkm.ToString();
            fullAmmoText.text = "/" + fullAmmoAkm.ToString();
            if (Input.GetButtonDown("Fire1") && !Input.GetButton("Fire1") && currentAmmoAkm > 0 && !playerController.ifPause && !itemChange.ani.GetBool("Hide") && bShooting)
            {
                akm.GetComponent<Animator>().SetTrigger("Fire");
                recoilAmountY = 1.45f;
                currentAmmoAkm -= 1;
                currentAmmoText.text = currentAmmoAkm.ToString();              
                fullAmmoText.text = "/" + fullAmmoAkm.ToString();
                //Ammo.text = currentAmmo + "/" + fullAmmo;
                Shoot();
                RecoilMath();
            }
            else if (Input.GetButton("Fire1") && Time.time > nextFire && currentAmmoAkm > 0 && !playerController.ifPause && !itemChange.ani.GetBool("Hide") && bShooting)
            {
                akm.GetComponent<Animator>().SetTrigger("Fire");
                currentAmmoAkm -= 1;
                currentAmmoText.text = currentAmmoAkm.ToString();
                fullAmmoText.text = "/" + fullAmmoAkm.ToString();
                //Ammo.text = currentAmmo + "/" + fullAmmo;
                nextFire = Time.time + 1f / fireRate;
                Shoot();
                RecoilMath();
            }
            if (Input.GetKeyDown(KeyCode.R) && currentAmmoAkm != 30 && fullAmmoAkm != 0 && !Input.GetButtonDown("Fire1") && !Input.GetButton("Fire1") && !itemChange.ani.GetBool("Hide"))
            {
                bShooting = false;
                reloadTimer = 1.5f;
                akm.GetComponent<Animator>().SetTrigger("Reload");
                effectsSource.PlayOneShot(reloadSFX);
                Reolad();
                currentAmmoText.text = currentAmmoAkm.ToString();
                fullAmmoText.text = "/" + fullAmmoAkm.ToString();
                
                //Ammo.text = currentAmmo + "/" + fullAmmo;
            }
            if (reloadTimer > 0)
            {
                reloadTimer -= Time.deltaTime;
            }
            if (reloadTimer <= 0)
            {
                bShooting = true; //можно стрелять
            }
        }
        else
        {
            currentAmmoText.text = currentAmmoMp7.ToString();
            fullAmmoText.text = "/" + fullAmmoMp7.ToString();
            if (Input.GetButtonDown("Fire1")  && currentAmmoMp7 > 0 && !playerController.ifPause && !itemChange.ani.GetBool("Hide") && bShooting)
            {
                akm.GetComponent<Animator>().SetTrigger("Fire");
                recoilAmountY = 1.45f;
                currentAmmoMp7 -= 1;
                currentAmmoText.text = currentAmmoMp7.ToString();
                fullAmmoText.text = "/" + fullAmmoMp7.ToString();
                //Ammo.text = currentAmmo + "/" + fullAmmo;
                Shoot();
                RecoilMath();
            }            
            if (Input.GetKeyDown(KeyCode.R) && currentAmmoMp7 != 12 && fullAmmoMp7 != 0 && !Input.GetButtonDown("Fire1") && !itemChange.ani.GetBool("Hide") && bShooting)
            {
                bShooting = false;
                reloadTimer = 1f;
                akm.GetComponent<Animator>().SetTrigger("Reload");
                effectsSource.PlayOneShot(reloadSFX);
                Reolad();
                currentAmmoText.text = currentAmmoMp7.ToString();
                fullAmmoText.text = "/" + fullAmmoMp7.ToString();
                //Ammo.text = currentAmmo + "/" + fullAmmo;
            }
            if (reloadTimer > 0)
            {
                reloadTimer -= Time.deltaTime;
            }
            if (reloadTimer <= 0)
            {
                bShooting = true; //можно стрелять
            }
        }
        if (Input.GetButtonDown("Fire1") && currentAmmoAkm == 0 || currentAmmoMp7 == 0 && (Input.GetButtonDown("Fire1")))
        {
            effectsSource.PlayOneShot(zatvorSFX);
        }
        if(EnemyScript.giveAmmo)
        {
            if (fullAmmoAkm - 10 <= 90)
            {
                fullAmmoAkm += 10;
                currentAmmoText.text = currentAmmoAkm.ToString();
                fullAmmoText.text = "/" + fullAmmoAkm.ToString();
                //Ammo.text = currentAmmo + "/" + fullAmmo;
                EnemyScript.giveAmmo = false;
            }            
        }        
    }

    public void RecoilMath()
    {
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        if (itemChange.ItemIdInt == 1)
        {
            recoilAmountY = 2.45f;
            if (playerController.isCrough == true)
            {
                recoilAmountY = 1.45f;
            }
        }
        else
        {
            if (playerController.isCrough == true)
            {
                recoilAmountY = 2.45f;
            }
        }
        currentRecoilXPos = ((Random.value - .5f) / 2) * recoilAmountX;
        currentRecoilYPos = ((Random.value - .5f) / 2) * (timePressed >= maxRecoilTime ? recoilAmountY / 4 : recoilAmountY);
        playerController.rotationX -= Mathf.Abs(currentRecoilYPos);
        playerController.rotationY -= currentRecoilXPos;
    }

    public void Reolad()
    {
        itemChange = GameObject.Find("PlayerController").GetComponent<ItemChange>();
        if (itemChange.ItemIdInt == 0)
        {
            int reason = 30 - currentAmmoAkm;
            if (fullAmmoAkm >= reason)
            {
                fullAmmoAkm -= reason;
                currentAmmoAkm += reason;
            }
            else
            {
                currentAmmoAkm = currentAmmoAkm + fullAmmoAkm;
                fullAmmoAkm = 0;
            }
        }
        else
        {
            int reason = 12 - currentAmmoMp7;
            if (fullAmmoMp7 >= reason)
            {
                fullAmmoMp7 -= reason;
                currentAmmoMp7 += reason;
            }
            else
            {
                currentAmmoMp7 = currentAmmoMp7 + fullAmmoMp7;
                fullAmmoMp7 = 0;
            }
        }
        
    }

    void Shoot() 
    {        
        muzzleFlash.Play(bulletSpawn);
        effectsSource.PlayOneShot(shotSFX);
        RaycastHit hit;        
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            if (hit.collider.tag == "Enemy_01" || hit.collider.tag == "Enemy_02" || hit.collider.tag == "Enemy_03" || hit.collider.tag == "Enemy_04" || hit.collider.tag == "Enemy_05")
            {
                if(hardDiffuculty)
                {
                    hitEnemy = true;
                }
            }
            if (hit.collider.tag == "Enemy_01")
            {
                EnemyScript.takeDamage(damage, 1);                
            }
            if (hit.collider.tag == "Enemy_02")
            {
                EnemyScript.takeDamage(damage, 2);
            }
            if (hit.collider.tag == "Enemy_03")
            {
                EnemyScript.takeDamage(damage, 3);
            }
            if (hit.collider.tag == "Enemy_04")
            {
                EnemyScript.takeDamage(damage, 4);
            }
            if (hit.collider.tag == "Enemy_05")
            {
                EnemyScript.takeDamage(damage, 5);
            }
            GameObject impact = Instantiate(hitEffectOthers, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact,2f);
        }
    }
}
