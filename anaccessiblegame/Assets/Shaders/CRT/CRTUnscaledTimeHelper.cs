using UnityEngine;

public class CRTUnscaledTimeHelper : MonoBehaviour
{
    [SerializeField] Material material;

    private void Update()
    {
        material.SetFloat("_Unscaled_Time", Time.unscaledTime);
    }

}
