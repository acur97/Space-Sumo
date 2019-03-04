using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlGanar : MonoBehaviour {

    public CerrarBase cerrarBase;
    public MoverSumo moverSumo1;
    public MoverSumo moverSumo2;
    public GameObject CanvasPlay;
    public GameObject CanvasPausa;
    public GameObject CanvasGanar;
    public Text titulo;

    private void Awake()
    {
        CanvasPlay.SetActive(true);
        CanvasPausa.SetActive(false);
        CanvasGanar.SetActive(false);
    }

    public void Ganar(int quienMurio)
    {
        cerrarBase.enabled = false;
        CanvasPlay.SetActive(false);
        CanvasGanar.SetActive(true);

        if (quienMurio == 1)
        {
            moverSumo2.enabled = false;
            titulo.text = "¡Jugador 2 gana!";
        }
        if (quienMurio == 2)
        {
            moverSumo1.enabled = false;
            titulo.text = "¡Jugador 1 gana!";
        }
    }
}
