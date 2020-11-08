using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeController : MonoBehaviour
{
    private void Awake()
    {
        int width = 2560; // or something else
        int height = 1440; // or something else
        bool isFullScreen = false; // should be windowed to run in arbitrary resolution
        int desiredFPS = 60; // or something else

        Screen.SetResolution(width, height, isFullScreen, desiredFPS);
    }
}