using EvolveGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSoundController : MonoBehaviour
{
    public AudioClip FootstepsSound;
    public AudioClip RunstepsSound;
    public AudioClip JumpSound;
    private AudioSource effectsSource;
    private CharacterController cController;
    private bool wasGrounded = false;

    void Start()
    {
        //getcomponent очень требователен к ресурсам компьютера, поэтому мы выполн€ем его один раз, Усохран€ем ссылкиФ на компоненты
        effectsSource = GetComponent<AudioSource>();
        cController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (cController.isGrounded) //персонаж на земле
        {
            if (cController.velocity.sqrMagnitude > 0f) //персонаж двигаетс€, используетс€ квадратична€ магнитуда,
            //т.к. это операци€ менее требовательна к ресурсам, и нам не нужна точна€ скорость Ц нам нужен сам факт передвижени€
            {
                if (!effectsSource.isPlaying && !Input.GetKey(KeyCode.LeftControl) || (effectsSource.clip == RunstepsSound && !Input.GetKey(KeyCode.LeftShift)) || (!effectsSource.isPlaying && Input.GetKey(KeyCode.LeftControl)))
                {
                    effectsSource.clip = FootstepsSound;
                    effectsSource.Play();
                }
                if (effectsSource.clip == FootstepsSound && Input.GetKey(KeyCode.LeftShift))
                {
                    effectsSource.clip = RunstepsSound;
                    effectsSource.Play();
                }
                if ((effectsSource.clip == FootstepsSound && Input.GetKey(KeyCode.LeftControl)) || (effectsSource.clip == RunstepsSound && Input.GetKey(KeyCode.LeftControl)))
                {
                    effectsSource.Stop();
                }                
                
            }
            else //персонаж Ќ≈ двигаетс€
            {
                if (effectsSource.clip != JumpSound) //если аудиоклип в AudioSorce это не клип прыжка и не клип приземление
                    if (effectsSource.isPlaying)  //если звук проигрываетс€
                        effectsSource.Stop();  //выключаем проигрывание звуков

            }

            if (cController.isGrounded != wasGrounded) //проигрываем звук, если раньше персонаж был на земле, а теперь в воздухе
            {

                effectsSource.Stop();
            }
            wasGrounded = true; //запоминаем прошлое состо€ние персонажа Ц в данном случае персонаж на земле
        }
        else //персонаж ЌE на земле
        {
            if (effectsSource.clip != JumpSound) //если аудиоклип в AudioSorce это не клип прыжка и не клип приземление
            {
                if (effectsSource.isPlaying) //если звук проигрываетс€
                {
                    effectsSource.Stop();  //выключаем проигрывание звуков
                }
            }

            if (cController.isGrounded != wasGrounded) //проигрываем звук, если раньше персонаж был в воздухе, а теперь на земле
            {
                effectsSource.Stop();
                effectsSource.clip = JumpSound;
                effectsSource.Play();
            }
            wasGrounded = false; //запоминаем прошлое состо€ние персонажа Ц в данном случае персонаж в воздухе 
        }

    }
}