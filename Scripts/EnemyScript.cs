using EvolveGames;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    private static int hp_enemy_01;
    private static int hp_enemy_02;
    private static int hp_enemy_03;
    private static int hp_enemy_04;
    private static int hp_enemy_05;
    public Slider healthBar_enemy_01;
    public Slider healthBar_enemy_02;
    public Slider healthBar_enemy_03;
    public Slider healthBar_enemy_04;
    public Slider healthBar_enemy_05;
    private GameObject enemy_01;
    private GameObject enemy_02;
    private GameObject enemy_03;
    private GameObject enemy_04;
    private GameObject enemy_05;
    public GameObject enemy_01_prefab;
    public GameObject enemy_02_prefab;
    public GameObject enemy_03_prefab;
    public GameObject enemy_04_prefab;
    public GameObject enemy_05_prefab;
    private int randNum;
    static public bool giveAmmo;
    static public bool giveHP;
    public List<Transform> spawnPoints;
    public TextMeshProUGUI Status_Zadachi;
    public GameObject loos_text;
    public static int countKillingEnemys;
    public Transform parent;
    private int score;
    private SoundEffectsController soundEffectsController;


    void Start()
    {
        soundEffectsController = GameObject.Find("SoundEffectsController").GetComponent<SoundEffectsController>();
        spawnPoints = new List<Transform>(spawnPoints);
        countKillingEnemys = 0;
        giveAmmo = false;
        hp_enemy_01 = 100;
        hp_enemy_02 = 100;
        hp_enemy_03 = 100;
        hp_enemy_04 = 100;
        hp_enemy_05 = 100;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            healthBar_enemy_01.value = hp_enemy_01;
        }
        if (hp_enemy_01 <= 0)
        {

            //healthBar_enemy_01.gameObject.SetActive(false);           
            enemy_01 = GameObject.FindGameObjectWithTag("Enemy_01").gameObject;           
            if (enemy_01 == GameObject.FindGameObjectWithTag("Enemy_01"))
            {
                countKillingEnemys++;               
            }            
            Destroy(enemy_01.transform.parent.gameObject);
            enemy_01 = null;
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                hp_enemy_01 = 100;
            }
            Weapon.hitEnemy = false;
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {               
                giveAmmoStatus();
                spawnEnemy(enemy_01_prefab);
                enemy_01 = GameObject.FindGameObjectWithTag("Enemy_01").gameObject;
                soundEffectsController.shotSFXSource_1 = enemy_01.GetComponentInParent<AudioSource>();
                hp_enemy_01 = 100;               
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            healthBar_enemy_02.value = hp_enemy_02;
        }
        if (hp_enemy_02 <= 0)
        {
            //healthBar_enemy_02.gameObject.SetActive(false);
            enemy_02 = GameObject.FindGameObjectWithTag("Enemy_02").gameObject;
            if (enemy_02 == GameObject.FindGameObjectWithTag("Enemy_02"))
            {
                countKillingEnemys++;               
            }            
            Destroy(enemy_02.transform.parent.gameObject);
            enemy_02 = null;
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                hp_enemy_02 = 100;
            }
            Weapon.hitEnemy = false;
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                giveAmmoStatus();
                spawnEnemy(enemy_02_prefab);
                hp_enemy_02 = 100;
                enemy_02 = GameObject.FindGameObjectWithTag("Enemy_02").gameObject;
                soundEffectsController.shotSFXSource_2 = enemy_02.transform.parent.gameObject.GetComponent<AudioSource>();
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            healthBar_enemy_03.value = hp_enemy_03;
        }
        if (hp_enemy_03 <= 0)
        {
            //healthBar_enemy_03.gameObject.SetActive(false);
            enemy_03 = GameObject.FindGameObjectWithTag("Enemy_03").gameObject;
            if (enemy_03 == GameObject.FindGameObjectWithTag("Enemy_03"))
            {
                countKillingEnemys++;                
            }            
            Destroy(enemy_03.transform.parent.gameObject);
            enemy_03 = null;
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                hp_enemy_03 = 100;
            }
            Weapon.hitEnemy = false;
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                giveAmmoStatus();
                spawnEnemy(enemy_03_prefab);
                hp_enemy_03 = 100;
                enemy_03 = GameObject.FindGameObjectWithTag("Enemy_03").gameObject;
                soundEffectsController.shotSFXSource_3 = enemy_03.transform.parent.gameObject.GetComponent<AudioSource>();
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            healthBar_enemy_04.value = hp_enemy_04;
        }
        if (hp_enemy_04 <= 0)
        {
            //healthBar_enemy_04.gameObject.SetActive(false);
            enemy_04 = GameObject.FindGameObjectWithTag("Enemy_04").gameObject;
            if (enemy_04 == GameObject.FindGameObjectWithTag("Enemy_04"))
            {
                countKillingEnemys++;                
            }            
            Destroy(enemy_04.transform.parent.gameObject);
            enemy_04 = null;
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                hp_enemy_04 = 100;
            }
            Weapon.hitEnemy = false;
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                giveAmmoStatus();
                spawnEnemy(enemy_04_prefab);
                hp_enemy_04 = 100;
                enemy_04 = GameObject.FindGameObjectWithTag("Enemy_04").gameObject;
                soundEffectsController.shotSFXSource_4 = enemy_04.transform.parent.gameObject.GetComponent<AudioSource>();
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            healthBar_enemy_05.value = hp_enemy_05;
        }
        if (hp_enemy_05 <= 0)
        {
            //healthBar_enemy_05.gameObject.SetActive(false);
            enemy_05 = GameObject.FindGameObjectWithTag("Enemy_05").gameObject;
            if (enemy_05 == GameObject.FindGameObjectWithTag("Enemy_05"))
            {
                countKillingEnemys++;                
            }            
            Destroy(enemy_05.transform.parent.gameObject);
            enemy_05 = null;
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                hp_enemy_05 = 100;
            }
            Weapon.hitEnemy = false;
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                giveAmmoStatus();
                spawnEnemy(enemy_05_prefab);
                hp_enemy_05 = 100;
                enemy_05 = GameObject.FindGameObjectWithTag("Enemy_05").gameObject;
                soundEffectsController.shotSFXSource_5 = enemy_05.transform.parent.gameObject.GetComponent<AudioSource>();
            }
        }
        if (PlayerManager.gameOver)
        {
            loos_text.GetComponent<TextMeshPro>().text = "Набрано очков: " + score + ". \n Вернуться в главное меню?";
            //countKillingEnemys = 0;
            hp_enemy_01 = 100;
            hp_enemy_02 = 100;
            hp_enemy_03 = 100;
            hp_enemy_04 = 100;
            hp_enemy_05 = 100;            
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Status_Zadachi.text = "Убито противников: " + countKillingEnemys + "/5";
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Status_Zadachi.text = "Набрано очков: " + countKillingEnemys * 10;
            score = countKillingEnemys * 10;
        }

    }

    public void giveAmmoStatus()
    {
        randNum = Random.Range(0, 100);
        if (randNum >= 65)
        {
            giveAmmo = true;
        }
    }

    public void giveHpStatus()
    {
        if (Weapon.moreDmg)
        {
            randNum = Random.Range(0, 100);
            if (randNum >= 85)
            {
                giveHP = true;
            }
        }
    }

    public void spawnEnemy(GameObject gameObject)
    {
        int spawn = Random.Range(0, spawnPoints.Count);
        Instantiate(gameObject, spawnPoints[spawn].transform.position, Quaternion.identity, parent);
    }

    public static void takeDamage(int damage, int number)
    {
        if (number == 1)
        {
            hp_enemy_01 -= damage;
        }
        if (number == 2)
        {
            hp_enemy_02 -= damage;
        }
        if (number == 3)
        {
            hp_enemy_03 -= damage;
        }
        if (number == 4)
        {
            hp_enemy_04 -= damage;
        }
        if (number == 5)
        {
            hp_enemy_05 -= damage;
        }

    }
}
