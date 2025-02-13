using UnityEngine;

public enum SoundType
{
    MUSIC,
    BUTTON
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private static AudioSource audioSource;

    private void Awake()
    {
        instance = this;       
    }

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        //instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }
}
