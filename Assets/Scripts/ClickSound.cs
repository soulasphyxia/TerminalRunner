using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip keyPressSound; 
    [SerializeField] private float minKeyPressDelay = 0.1f; 

    private float lastKeyPressTime = 0f; 

    void Update()
    {
        // Проверяем каждую клавишу
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key)) 
            {
                PlayKeyPressSound();
            }
        }
    }

    private void PlayKeyPressSound()
    {
        if (Time.time - lastKeyPressTime < minKeyPressDelay)
        {
            return;
        }

        lastKeyPressTime = Time.time;
        audioSource.PlayOneShot(keyPressSound);
    }
}
