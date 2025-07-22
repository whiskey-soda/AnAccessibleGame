using UnityEngine;

public class HeadBonk : MonoBehaviour
{
    public bool headBonking { get; private set; } = false;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            headBonking = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            headBonking = true;
        }
    }
}
