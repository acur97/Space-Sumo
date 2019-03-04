using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamaraMenu : MonoBehaviour {

    public float multiplicador = 10;

    private float Mx;
    private float My;
    private Vector3 Mpos;
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Mpos = Input.mousePosition;

        Mx = (Mpos.x / Screen.width) - 0.5f;
        My = (Mpos.y / Screen.height) - 0.5f;

        rect.localEulerAngles = new Vector3((-My * multiplicador), (Mx * multiplicador), 0);
    }
}