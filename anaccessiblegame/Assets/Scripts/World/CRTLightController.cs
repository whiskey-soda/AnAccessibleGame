using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CRTLightController : MonoBehaviour
{
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] Color gameplayColor;
    [SerializeField] Color pauseColor;

    [SerializeField] List<Light> lights;
    [Space]
    [SerializeField] Image tvStatic;

    // Update is called once per frame
    void Update()
    {
        Color lightColor = Color.white;

        // colors are white if pause menu or static is onscreen.
        // purple if gameplay is onscreen
        if (pauseMenu.isOpen || tvStatic.color.a != 0) { lightColor = pauseColor; }
        else { lightColor = gameplayColor; }


        foreach(Light light in lights)
        {
            light.color = lightColor;
        }
    }
}
