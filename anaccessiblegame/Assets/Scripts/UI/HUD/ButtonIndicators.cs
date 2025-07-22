using UnityEngine;
using UnityEngine.UI;

public class ButtonIndicators : MonoBehaviour
{
    [SerializeField] GameObject jumpIndicator;
    [SerializeField] Image jumpIcon;
    [SerializeField] Color jumpActiveColor;
    [SerializeField] Color jumpInactiveColor;
    [SerializeField] Jump jump;
    [Space]
    [SerializeField] GameObject runIndicator;
    [SerializeField] Image runIcon;
    [SerializeField] Color runActiveColor;
    [SerializeField] Color runInactiveColor;
    [SerializeField] Movement movement;

    Accessibility accessibility;

    private void Start()
    {
        accessibility = Accessibility.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // activate indicators when settings are enabled
        // deactivate indicators when settings are disabled
        if (accessibility != null)
        {
            if (jumpIndicator.activeSelf != accessibility.toggleJump)
            {
                jumpIndicator.SetActive(accessibility.toggleJump);
            }    

            if (runIndicator.activeSelf != accessibility.toggleRun)
            {
                runIndicator.SetActive(accessibility.toggleRun);
            }
        }

        // change jump icon color while jumping
        if (jump.isJumping) { jumpIcon.color = jumpActiveColor; ; }
        else { jumpIcon.color = jumpInactiveColor; }

        // change run icon color while running
        if (movement.isSprinting) { runIcon.color = runActiveColor; }
        else { runIcon.color = runInactiveColor; }
    }
}
