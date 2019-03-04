using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasZoffset : MonoBehaviour {

    public float multiplicadorOffset = 1;

    private int Height;
    private RectTransform rect;

    private void Awake()
    {
        Height = Screen.height;
        rect = GetComponent<RectTransform>();
        rect.localPosition = new Vector3(rect.localPosition.x, rect.localPosition.y, (Height * multiplicadorOffset));
    }
}
