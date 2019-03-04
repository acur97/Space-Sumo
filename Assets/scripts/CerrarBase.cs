using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerrarBase : MonoBehaviour {

    public float EsperaParaIniciar;

    private void Update()
    {
        if (Time.time > EsperaParaIniciar && transform.localScale.x >= 2)
        {
            transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime, 1, transform.localScale.z - Time.deltaTime);
        }
    }
}
