using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverSumo : MonoBehaviour {

    public CamaraVarios camV;
	public enum Jugadores {Jugador1, Jugador2};
    public Jugadores Jugador;
    public float Velocidad;
    public float FuerzaSalto;
    public float radioFuerza;
    public float fuerzaExplocion;
    public float tiempoEsperaExplocion;
    public GameObject Particulas;
    public Transform puntoParticulas;
    [Space]
    public AlGanar alGanar;

    private Rigidbody rb;
    private bool puedeExplotar = true;
    private Quaternion zero = new Quaternion(0, 0, 0, 0);

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Jugador == Jugadores.Jugador1)
        {
            Vector3 MoverSumo = new Vector3(Input.GetAxis("Horizontal2") * Velocidad, 0, Input.GetAxis("Vertical2") * Velocidad);
            transform.Translate(MoverSumo);

            if (transform.localPosition.y < 0.51 && Input.GetKeyDown(KeyCode.LeftShift))
            {
                Vector3 salto = new Vector3(0, FuerzaSalto, 0);
                rb.AddForce(salto);
            }

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                if (puedeExplotar)
                {
                    Explotar();
                }
            }
        }
        if (Jugador == Jugadores.Jugador2)
        {
            Vector3 MoverSumo = new Vector3(Input.GetAxis("Horizontal") * Velocidad, 0, Input.GetAxis("Vertical") * Velocidad);
            transform.Translate(MoverSumo);

            if (transform.localPosition.y < 0.51 && Input.GetKeyDown(KeyCode.RightShift))
            {
                Vector3 salto = new Vector3(0, FuerzaSalto, 0);
                rb.AddForce(salto);
            }

            if (Input.GetKeyDown(KeyCode.RightAlt))
            {
                if (puedeExplotar)
                {
                    Explotar();
                }
            }
        }
    }

    public void Explotar()
    {
        StartCoroutine(EsperaPoder());

        Collider[] colliders = Physics.OverlapSphere(transform.position, radioFuerza);

        foreach (Collider ObjetosCercanos in colliders)
        {
            Rigidbody rb = ObjetosCercanos.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(fuerzaExplocion, transform.position, radioFuerza);
                Instantiate(Particulas, puntoParticulas.position, zero, puntoParticulas);
            }
        }
    }

    IEnumerator EsperaPoder()
    {
        puedeExplotar = false;
        Velocidad = Velocidad / 2;
        yield return new WaitForSeconds(tiempoEsperaExplocion);
        puedeExplotar = true;
        Velocidad = Velocidad * 2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Muerte")
        {
            if (Jugador == Jugadores.Jugador1)
            {
                alGanar.Ganar(1);
            }
            if (Jugador == Jugadores.Jugador2)
            {
                alGanar.Ganar(2);
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
            camV.enabled = false;
        }
    }
}