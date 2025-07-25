using UnityEngine;

public class WebsiteLink : MonoBehaviour
{
    [SerializeField] string link;

    public void OpenLink()
    {
        Application.OpenURL(link);
    }

}
