using UnityEngine;
using UnityEngine.U2D.IK;

[RequireComponent(typeof(RectTransform))]
public class WorldAnchoredUI : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform worldAnchor;
    [SerializeField] Vector2 displacement;

    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        // ui element sticks to world anchor with a displacement
        rect.anchoredPosition = cam.WorldToScreenPoint(worldAnchor.position);
        rect.anchoredPosition += displacement;
    }
}
