using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{
    public CanvasGroup canvas;
    public bool fadein = false;
    public bool fadeout = false;

    public float fadeTime = 1;

    private void Update()
    {
        if (fadein == true)
        {
            if (canvas.alpha < 1)
            {
                canvas.alpha += fadeTime * Time.deltaTime;
                if (canvas.alpha >= 1)
                {
                    fadein = false;
                }
            }
        }
        if (fadeout == true)
        {
            if (canvas.alpha >= 0)
            {
                canvas.alpha -= fadeTime * Time.deltaTime;
                if (canvas.alpha == 0)
                {
                    fadeout = false;
                }
            }
        }
    }

    public void FadeIn()
    {
        fadein = true;
    }
    public void FadeOut()
    {
        fadeout = true;
    }
}
