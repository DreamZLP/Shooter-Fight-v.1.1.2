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
        //getcomponent ����� ������������ � �������� ����������, ������� �� ��������� ��� ���� ���, ���������� ������ �� ����������
        effectsSource = GetComponent<AudioSource>();
        cController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (cController.isGrounded) //�������� �� �����
        {
            if (cController.velocity.sqrMagnitude > 0f) //�������� ���������, ������������ ������������ ���������,
            //�.�. ��� �������� ����� ������������� � ��������, � ��� �� ����� ������ �������� � ��� ����� ��� ���� ������������
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
            else //�������� �� ���������
            {
                if (effectsSource.clip != JumpSound) //���� ��������� � AudioSorce ��� �� ���� ������ � �� ���� �����������
                    if (effectsSource.isPlaying)  //���� ���� �������������
                        effectsSource.Stop();  //��������� ������������ ������

            }

            if (cController.isGrounded != wasGrounded) //����������� ����, ���� ������ �������� ��� �� �����, � ������ � �������
            {

                effectsSource.Stop();
            }
            wasGrounded = true; //���������� ������� ��������� ��������� � � ������ ������ �������� �� �����
        }
        else //�������� �E �� �����
        {
            if (effectsSource.clip != JumpSound) //���� ��������� � AudioSorce ��� �� ���� ������ � �� ���� �����������
            {
                if (effectsSource.isPlaying) //���� ���� �������������
                {
                    effectsSource.Stop();  //��������� ������������ ������
                }
            }

            if (cController.isGrounded != wasGrounded) //����������� ����, ���� ������ �������� ��� � �������, � ������ �� �����
            {
                effectsSource.Stop();
                effectsSource.clip = JumpSound;
                effectsSource.Play();
            }
            wasGrounded = false; //���������� ������� ��������� ��������� � � ������ ������ �������� � ������� 
        }

    }
}