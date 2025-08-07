using TMPro;
using UnityEngine;

public class CommentaryNode : MonoBehaviour
{
    public AudioClip audioClip;
    public TextMeshProUGUI text;

    CommentaryController commentaryPlayer;

    private void Start()
    {
        commentaryPlayer = CommentaryController.Instance;
    }

    public void Play()
    {
        if (commentaryPlayer == null) { return; }

        commentaryPlayer.PlayCommentary(audioClip, text);
    }
}
