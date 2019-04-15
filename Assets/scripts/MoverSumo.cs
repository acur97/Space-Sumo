using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoverSumo : MonoBehaviour {

    public VariableJoystick joystick1;
    public Transform jug1;
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
    private NavMeshAgent agent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (Jugador == Jugadores.Jugador2)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    private void Update()
    {
        if (Jugador == Jugadores.Jugador1)
        {
            //Vector3 MoverSumo = new Vector3(Input.GetAxis("Horizontal2") * Velocidad, 0, Input.GetAxis("Vertical2") * Velocidad);
            Vector3 MoverSumo = new Vector3(joystick1.Horizontal * Velocidad, 0, joystick1.Vertical * Velocidad);
            transform.Translate(MoverSumo);
        }
        if (Jugador == Jugadores.Jugador2)
        {
            if (Vector3.Distance(jug1.position, transform.position) > 3)
            {
                agent.destination = jug1.position;
            }
            else
            {
                ExplotarBtn();
                //Salto();
            }
            //Vector3 MoverSumo = new Vector3(Input.GetAxis("Horizontal") * Velocidad, 0, Input.GetAxis("Vertical") * Velocidad);
            //Vector3 MoverSumo = new Vector3(joystick2.Horizontal * Velocidad, 0, joystick2.Vertical * Velocidad);
            //transform.Translate(MoverSumo);
        }
    }

    public void ExplotarBtn()
    {
        if (puedeExplotar)
        {
            Explotar();
        }
    }

    public void Salto()
    {
        if (transform.localPosition.y < 0.51)
        {
            Vector3 salto = new Vector3(0, FuerzaSalto, 0);
            rb.AddForce(salto);
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

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.tag == "Muerte")
    //    {
    //        if (Jugador == Jugadores.Jugador1)
    //        {
    //            alGanar.Ganar(1);
    //        }
    //        if (Jugador == Jugadores.Jugador2)
    //        {
    //            alGanar.Ganar(2);
    //        }
    //        Destroy(collision.gameObject);
    //        Destroy(gameObject);
    //        camV.enabled = false;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Muerte"))
        {
            if (Jugador == Jugadores.Jugador1)
            {
                alGanar.Ganar(1);
            }
            if (Jugador == Jugadores.Jugador2)
            {
                alGanar.Ganar(2);
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
            camV.enabled = false;
        }
    }
}