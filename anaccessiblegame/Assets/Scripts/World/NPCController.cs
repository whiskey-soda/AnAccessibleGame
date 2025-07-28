using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class NPCController : Movement
{
    SpriteRenderer spriteRenderer;
    bool isFadingIn = false;
    [Space]
    [SerializeField] float fadeInDuration = .3f;
    float fadeInTimeElapsed = 0;

    protected override void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        base.Awake();
    }

    public void FadeIn()
    {
        isFadingIn = true;
        fadeInTimeElapsed = 0;


        // change alpha to be fully transparent to start fading in
        Color newColor = spriteRenderer.color;
        newColor.a = 0;
        spriteRenderer.color = newColor;

    }

    protected override void Update()
    {
        if (isFadingIn)
        {
            // increment elapsed time before changing alpha, or the alpha never reaches 1
            fadeInTimeElapsed += Time.deltaTime;

            // change alpha
            Color newColor = spriteRenderer.color;
            newColor.a = fadeInTimeElapsed / fadeInDuration;
            spriteRenderer.color = newColor;

            // update timer
            if (fadeInTimeElapsed > fadeInDuration) { isFadingIn=false; }
        }

        base.Update();
    }

}
