using EvolveGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffectsController : MonoBehaviour
{
    public AudioSource playercontrollerSource;
    public AudioSource windSource;
    public AudioSource weaponSourceFirst;
    public AudioSource weaponSourceSecond;
    public AudioSource shotSFXSource_1;
    public AudioSource shotSFXSource_2;
    public AudioSource shotSFXSource_3;
    public AudioSource shotSFXSource_4;
    public AudioSource shotSFXSource_5;
    public Slider effectsSlider;
    private PlayerController playerController;

    private void Start()
    {
        effectsSlider.GetComponent<Slider>().value = 0.25f;
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }

    public void Awake()
    {
        effectsSlider.value = playercontrollerSource.volume;
        effectsSlider.value = windSource.volume;
        effectsSlider.value = weaponSourceFirst.volume;
        effectsSlider.value = weaponSourceSecond.volume;
        effectsSlider.value = shotSFXSource_1.volume;
        effectsSlider.value = shotSFXSource_2.volume;
        effectsSlider.value = shotSFXSource_3.volume;
        effectsSlider.value = shotSFXSource_4.volume;
        effectsSlider.value = shotSFXSource_5.volume;
    }

    public void ChangeValueEffects(float value)
    {
        playercontrollerSource.volume = value;
        windSource.volume = value;
        weaponSourceFirst.volume = value;
        weaponSourceSecond.volume = value;
        shotSFXSource_1.volume = value;
        shotSFXSource_2.volume = value;
        shotSFXSource_3.volume = value;
        shotSFXSource_4.volume = value;
        shotSFXSource_5.volume = value;
    }

    void Update()
    {
        if (PlayerManager.gameOver)
        {
            playercontrollerSource.volume = 0f;
            windSource.volume = 0f;
            weaponSourceFirst.volume = 0f;
            weaponSourceSecond.volume = 0f;
            shotSFXSource_1.volume = 0f;
            shotSFXSource_2.volume = 0f;
            shotSFXSource_3.volume = 0f;
            shotSFXSource_4.volume = 0f;
            shotSFXSource_5.volume = 0f;
        }

        if (playerController.ifPause) 
        {
            playercontrollerSource.volume = 0f;
            weaponSourceFirst.volume = 0f;
            weaponSourceSecond.volume = 0f;
            shotSFXSource_1.volume = 0f;
            shotSFXSource_2.volume = 0f;
            shotSFXSource_3.volume = 0f;
            shotSFXSource_4.volume = 0f;
            shotSFXSource_5.volume = 0f;
        }
        else if (!playerController.ifPause)
        {
            playercontrollerSource.volume = effectsSlider.value;
            weaponSourceFirst.volume = effectsSlider.value;
            weaponSourceSecond.volume = effectsSlider.value;
            shotSFXSource_1.volume = effectsSlider.value;
            shotSFXSource_2.volume = effectsSlider.value;
            shotSFXSource_3.volume = effectsSlider.value;
            shotSFXSource_4.volume = effectsSlider.value;
            shotSFXSource_5.volume = effectsSlider.value;   
        }
    }
}

