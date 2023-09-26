using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerManager sacarVida;
    public Transform jugador;
    public float rangoPersecusion = 10.0f;
    public float velocidad = 5.0f;
    public float distanciaMinima = 1.0f;
    public Q_queue<Transform> puntosRecorrido;
    public Transform Point1Transform;
    public Transform Point2Transform;
    public Transform Point3Transform;
    public Transform Point4Transform;
    public Transform Point5Transform;
    public Transform Point6Transform;
    public Transform Point7Transform;
    public Transform Point8Transform;
    public Transform Point9Transform;

    private Transform puntoActual;
    private bool persiguiendoJugador = false;
    private bool enPausaDespuesDeColision = false;
    private float tiempoPausa = 1.0f; // 1 segundo de pausa

    private void Start()
    {
        sacarVida = GameObject.FindObjectOfType<PlayerManager>();
        puntosRecorrido = new Q_queue<Transform>();
        puntosRecorrido.Enqueue(Point1Transform);
        puntosRecorrido.Enqueue(Point2Transform);
        puntosRecorrido.Enqueue(Point3Transform);
        puntosRecorrido.Enqueue(Point4Transform);
        puntosRecorrido.Enqueue(Point5Transform);
        puntosRecorrido.Enqueue(Point6Transform);
        puntosRecorrido.Enqueue(Point7Transform);
        puntosRecorrido.Enqueue(Point8Transform);
        puntosRecorrido.Enqueue(Point9Transform);
        if (puntosRecorrido.Counter() > 0)
        {
            puntoActual = puntosRecorrido.Dequeue();
        }
    }

    private void Update()
    {
        if (enPausaDespuesDeColision)
        {
            // Si estamos en pausa, contar el tiempo de pausa y luego reanudar el movimiento.
            tiempoPausa -= Time.deltaTime;
            if (tiempoPausa <= 0)
            {
                enPausaDespuesDeColision = false;
            }
        }
        else
        {
            float distancia = Vector3.Distance(transform.position, jugador.position);

            if (distancia <= rangoPersecusion)//estamos en rango del player
            {
                persiguiendoJugador = true;

                // Si no estamos en pausa, seguimos moviéndonos hacia el jugador.
                Vector3 direccion = (jugador.position - transform.position).normalized;
                transform.position += direccion * velocidad * Time.deltaTime;
            }
            else
            {
                persiguiendoJugador = false;
            }

            if (!persiguiendoJugador && puntosRecorrido.Counter() > 0)
            {
                MoverHaciaPunto(puntoActual);

                if (Vector3.Distance(transform.position, puntoActual.position) <= distanciaMinima)
                {
                    puntoActual = puntosRecorrido.Dequeue();
                    puntosRecorrido.Enqueue(puntoActual);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sacarVida.LooseLife();

            // Detener el movimiento y entrar en estado de pausa.
            enPausaDespuesDeColision = true;
            tiempoPausa = 1.0f; // 1 segundo de pausa
        }
    }

    private void MoverHaciaPunto(Transform punto)
    {
        Vector3 direccion = (punto.position - transform.position).normalized;
        transform.position += direccion * velocidad * Time.deltaTime;
    }
}
