using UnityEngine;
using System.Collections.Generic;

public class CRTLightController : MonoBehaviour
{
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] Color gameplayColor;
    [SerializeField] Color pauseColor;

    [SerializeField] List<Light> lights;

    // Update is called once per frame
    void Update()
    {
        Color lightColor = Color.white;

        if (pauseMenu.menuOpen) { lightColor = pauseColor; }
        else { lightColor = gameplayColor; }


        foreach(Light light in lights)
        {
            light.color = lightColor;
        }
    }
}
