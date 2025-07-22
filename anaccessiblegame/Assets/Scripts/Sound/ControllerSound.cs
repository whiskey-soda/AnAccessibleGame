using UnityEngine;

public class ControllerSound : MonoBehaviour
{
    [SerializeField] AudioClip hover;
    [SerializeField] AudioClip press;
    [SerializeField] AudioClip release;
    [Space]
    [SerializeField] float pitchVariance = .05f;

    public void PlayHoverSound()
    {
        if (SoundController.Instance != null) { SoundController.Instance.PlaySoundEffect(hover, pitchVariance); }
    }

    public void PlayPressSound()
    {
        if (SoundController.Instance != null) { SoundController.Instance.PlaySoundEffect(press, pitchVariance); }
    }

    public void PlayReleaseSound()
    {
        if (SoundController.Instance != null) { SoundController.Instance.PlaySoundEffect(release, pitchVariance); }
    }

}
