using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading;

public class PlayerManager : MonoBehaviour
{
    public static double playerHealth;
    private double temp_health;
    public static bool gameOver;
    public TextMeshProUGUI playerHealthText;
    public GameObject redOverlay;
    public GameObject lossExit;
    public AudioClip shotSFX;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100;
        gameOver = false;
        temp_health = playerHealth;
    }

    // Update is called once per frame
    void Update()
    {        
        if (playerHealth != temp_health) 
        {
            redOverlay.SetActive(true);
        }
        else
        {
            redOverlay.SetActive(false);
        }  
        temp_health = playerHealth;
        if (EnemyScript.giveHP)
        {
            if (playerHealth - 10 <= 90)
            {
                playerHealth += 10;
            }
        }
        if (playerHealth < 70 && playerHealth > 39)
        {
            playerHealthText.color = Color.yellow;
        }
        else
        {
            if (playerHealth < 40)
            {
                playerHealthText.color = Color.red;
            }
        }
        playerHealthText.text = "" + playerHealth;
        
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
        if (gameOver)
            {
                SceneManager.LoadScene("Map_v1");
            }
        }
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
        if (gameOver)
            {
                playerHealth = 100;       
            }
        }
    }

    public static void Damage(double damageCount)
    {
        if (playerHealth > 0)
        {
            playerHealth -= damageCount;
        }
        if (playerHealth <= 0)
        {
            gameOver = true;

        }
    }
}
