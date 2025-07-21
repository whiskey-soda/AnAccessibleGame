using UnityEngine;

public class SoundController : MonoBehaviour
{

    public static SoundController Instance;

    [SerializeField] AudioSource sfxAudioSourcePrefab;

    private void Awake()
    {
        // singleton code
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(this); }
    }

    public void PlaySoundEffect(AudioClip audioClip)
    {
        AudioSource audioSource = Instantiate(this.sfxAudioSourcePrefab, transform);

        audioSource.clip = audioClip;
        audioSource.Play();

        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

}
