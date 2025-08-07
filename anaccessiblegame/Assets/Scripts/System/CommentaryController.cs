using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommentaryController : MonoBehaviour
{
    [SerializeField] AudioSource commentaryAudioSource;
    [SerializeField] Animator commentaryMenuAnimator;
    [SerializeField] Button openButton;
    [SerializeField] Button closeButton;
    [SerializeField] GameObject textParent;
    [SerializeField] GameObject commentaryToggle;

    public static CommentaryController Instance;

    private void Awake()
    {
        // singleton code
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(this); }
    }

    private void Start()
    {
        // disable all commentary functionality
        ToggleCommentary(false);

        // if player has unlocked commentary, enable the toggle for use. otherwise disable it
        if (PlayerPrefs.GetInt("CommentaryUnlocked") == 1) { SetToggleVisibility(true); }
        else { SetToggleVisibility(false); }
    }

    public void PlayCommentary(AudioClip commentaryAudio, TextMeshProUGUI commentaryText)
    {
        // stop any current commentary audio
        commentaryAudioSource.Stop();

        // turn off all subtitle texts
        foreach (TextMeshProUGUI text in textParent.GetComponentsInChildren<TextMeshProUGUI>())
        {
            text.gameObject.SetActive(false);
        }

        // enable the specified subtitle text
        commentaryText.gameObject.SetActive(true);

        // resize scrollview to fit height of the current text
        RectTransform scrollViewRect = textParent.GetComponent<RectTransform>();
        RectTransform textRect = commentaryText.GetComponent<RectTransform>();
        scrollViewRect.sizeDelta = new Vector2(scrollViewRect.sizeDelta.x, textRect.sizeDelta.y + 15);

        // play commentary audio
        commentaryAudioSource.clip = commentaryAudio;
        commentaryAudioSource.Play();

    }

    public void OpenCommentaryMenu()
    {
        commentaryMenuAnimator.Play("SlideIn");
        openButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(true);
    }

    public void CloseCommentaryMenu()
    {
        commentaryMenuAnimator.Play("SlideOut");
        openButton.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// enables or disables commentary menu and nodes
    /// </summary>
    /// <param name="isOn"></param>
    public void ToggleCommentary(bool isOn)
    {
        commentaryMenuAnimator.gameObject.SetActive(isOn);
        foreach (CommentaryNode commentaryNode in FindObjectsByType<CommentaryNode>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            commentaryNode.gameObject.SetActive(isOn);
        }
    }

    public void UnlockCommentary()
    {
        PlayerPrefs.SetInt("CommentaryUnlocked", 1);
        SetToggleVisibility(true);
    }

    public void SetToggleVisibility(bool visibility)
    {

        commentaryToggle.SetActive(visibility);
    }

}
