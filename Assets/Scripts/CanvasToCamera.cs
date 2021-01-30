using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CanvasToCamera : MonoBehaviour
{
    private void Update()
    {
        if (Camera.main != null)
        {
            if (GetComponent<Canvas>() != null)
            {
                Canvas canvas = GetComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = Camera.main;
            }
        }
    }
}