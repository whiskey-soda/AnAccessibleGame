using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Parallax : MonoBehaviour
{
    // code adapted from: "Unity Parallax Tutorial - How to infinite scrolling background" by Dani on YouTube

    float length, startPos;
    [SerializeField] Transform cameraTransform;
    [SerializeField] float parallaxStrength;

    private void Awake()
    {
        startPos = cameraTransform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void LateUpdate()
    {
        float displacementFromCam = cameraTransform.position.x * (1 - parallaxStrength);
        float dist = cameraTransform.position.x * parallaxStrength;

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        // loop background if it moves outside of the camera view
        if (displacementFromCam > startPos + length) { startPos += length; }
        else if (displacementFromCam < startPos - length) { startPos -= length; }
    }
}
