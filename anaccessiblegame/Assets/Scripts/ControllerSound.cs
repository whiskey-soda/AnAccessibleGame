using UnityEngine;

public class ControllerSound : MonoBehaviour
{
    [SerializeField] AudioClip hover;
    [SerializeField] AudioClip press;
    [SerializeField] AudioClip release;

    public void PlayHoverSound()
    {
        if (SoundController.Instance != null) { SoundController.Instance.PlaySoundEffect(hover); }
    }

    public void PlayPressSound()
    {
        if (SoundController.Instance != null) { SoundController.Instance.PlaySoundEffect(press); }
    }

    public void PlayReleaseSound()
    {
        if (SoundController.Instance != null) { SoundController.Instance.PlaySoundEffect(release); }
    }

}
