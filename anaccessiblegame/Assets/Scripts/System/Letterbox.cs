using UnityEngine;

public class Letterbox : MonoBehaviour
{
    [SerializeField] int screenTargetX = 800;
    [SerializeField] int screenTargetY = 600;


    void Update()
    {
        Vector2 resTarget = new Vector2(screenTargetX, screenTargetY);
        Vector2 resViewport = new Vector2(Screen.width, Screen.height);
        Vector2 resNormalized = resTarget / resViewport; // target res in viewport space
        Vector2 size = resNormalized / Mathf.Max(resNormalized.x, resNormalized.y);
        Camera.main.rect = new Rect(default, size) { center = new Vector2(0.5f, 0.5f) };
    }

}
