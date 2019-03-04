using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraVarios : MonoBehaviour {

    public List<Transform> sumos;
    public Vector3 offset;
    public float suavizado = 0.5f;
    public float minZoom = 40;
    public float maxZoom = 10;
    public float limitZoom = 50;
    public float fovInicial = 75;

    private Vector3 velocidad;
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        cam.fieldOfView = fovInicial;
    }

    private void LateUpdate()
    {
        if (sumos.Count == 0)
        {
            return;
        }

        Move();
        Zoom();
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, ObtenerMejorDistancia() / limitZoom);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    void Move()
    {
        Vector3 puntoCentro = ObtenerPuntoCentro();

        Vector3 nuevaPosicion = puntoCentro + offset;

        transform.position = Vector3.SmoothDamp(transform.position, nuevaPosicion, ref velocidad, suavizado);
    }

    float ObtenerMejorDistancia()
    {
        var limites = new Bounds(sumos[0].position, Vector3.zero);
        for (int i = 0; i < sumos.Count; i++)
        {
            limites.Encapsulate(sumos[i].position);
        }

        return limites.size.x;
    }

    Vector3 ObtenerPuntoCentro()
    {
        if (sumos.Count == 1)
        {
            return sumos[0].position;
        }

        var limites = new Bounds(sumos[0].position, Vector3.zero);
        for (int i = 0; i < sumos.Count; i++)
        {
            limites.Encapsulate(sumos[i].position);
        }

        return limites.center;
    }
}
