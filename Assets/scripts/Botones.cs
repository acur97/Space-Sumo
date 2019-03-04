using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour {

	public void Iniciar()
    {
        Initiate.Fade("juego", Color.black, 1);
    }

    public void Pausa()
    {
        Time.timeScale = 0;
    }

    public void Reanudar()
    {
        Time.timeScale = 1;
    }

    public void Reiniciar()
    {
        Initiate.Fade("juego", Color.black, 1);
        Time.timeScale = 1;
    }

    public void Menu()
    {
        Initiate.Fade("menu", Color.black, 1);
        Time.timeScale = 1;
    }

    public void Salir()
    {
        Debug.Log("Exit the application");
        Application.Quit();
    }
}
