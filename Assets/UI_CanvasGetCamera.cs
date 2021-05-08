using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CanvasGetCamera : MonoBehaviour
{
    Canvas canvas;
    void Start()
    {
        if (canvas.worldCamera == null)
        {
            canvas.worldCamera = Camera.main;
        }
    }

}
