using UnityEngine;

public class SoundController : MonoBehaviour
{

    public static SoundController Instance;

    [SerializeField] AudioSource sfxAudioSourcePrefab;
    [SerializeField] AudioSource musicAudioSource;

    public bool soundEffectsEnabled { get; private set; } = false;

    private void Awake()
    {
        // singleton code
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(this); }
    }

    public void PlaySoundEffect(AudioClip audioClip)
    {
        // do nothing if sound effects are disabled
        if (!soundEffectsEnabled) { return; }

        AudioSource audioSource = Instantiate(this.sfxAudioSourcePrefab, transform);

        audioSource.clip = audioClip;
        audioSource.Play();

        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    public void PlaySoundEffect(AudioClip audioClip,float pitchVariance)
    {
        // do nothing if sound effects are disabled
        if (!soundEffectsEnabled) { return; }

        AudioSource audioSource = Instantiate(this.sfxAudioSourcePrefab, transform);

        // randomize pitch before playing
        audioSource.pitch = Random.Range(1 - pitchVariance, 1 + pitchVariance);

        audioSource.clip = audioClip;
        audioSource.Play();

        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    public void SetSoundEffectsEnabled(bool enable)
    {
        soundEffectsEnabled = enable;
    }

    public void SetMusicEnabled(bool enable)
    {
        musicAudioSource.mute = !enable;

        if (enable) { musicAudioSource.Play(); }
    }

    public void StopAllSounds()
    {
        foreach (AudioSource audioSource in FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
        {
            audioSource.Stop();
        }
    }

}
