using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(RectTransform))]
public class HVelIndicator : MonoBehaviour
{
    [SerializeField] Movement movement;
    [Space]
    [SerializeField] Slider slider;
    [SerializeField] float minSliderVal = .142f;
    [SerializeField] float yDisplacement = -20;
    [Space]
    [SerializeField] GameObject uiElement;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject circle;
    [Space]
    [SerializeField] Camera cam2D;

    RectTransform rect;

    Accessibility accessibility;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        accessibility = Accessibility.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // activate the indicator if timestop is on, deactivate if timestop is off
        if (accessibility != null)
        {
            if (uiElement.activeSelf != accessibility.timeStop)
            {
                uiElement.SetActive(accessibility.timeStop);
            }
        }

        slider.value = Mathf.Abs(movement.xVelocity) / movement.sprint_maxMoveSpeed;

        // slider goes to the right if player is moving to the right,
        // and to the left if the player is moving to the left
        if (Mathf.Sign(movement.xVelocity) > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else { transform.localScale = new Vector2(-1, transform.localScale.y); }

        // slider turns into a circle at low velocities
        if (slider.value < minSliderVal )
        {
            arrow.SetActive(false);
            circle.SetActive(true);
        }
        else
        {
            arrow.SetActive(true);
            circle.SetActive(false);
        }

        // indicator sticks to player
        rect.anchoredPosition = cam2D.WorldToScreenPoint(movement.transform.position);
        rect.anchoredPosition += new Vector2(0, yDisplacement);
    }
}
