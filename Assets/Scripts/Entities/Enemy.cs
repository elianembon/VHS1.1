using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerManager sacarVida;
    public Transform jugador;
    public float rangoPersecusion = 10.0f;
    public float velocidad = 5.0f;
    //public float force = 0f;
    public float distanciaMinima = 1.0f; // Distancia mínima para considerar que ha llegado al punto.
    public Q_queue<Transform> puntosRecorrido; // Cola de puntos de recorrido.
    public Transform Point1Transform;
    public Transform Point2Transform;
    public Transform Point3Transform;
    public Transform Point4Transform;

    private Transform puntoActual; // Punto de recorrido actual.
    private bool persiguiendoJugador = false; // Indica si el enemigo está persiguiendo al jugador.

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sacarVida = GameObject.FindObjectOfType<PlayerManager>();

        // Inicializa la cola de puntos de recorrido.
        puntosRecorrido = new Q_queue<Transform>();

        // Agrega los puntos de recorrido a la cola 
        
        puntosRecorrido.Enqueue(Point1Transform);
        puntosRecorrido.Enqueue(Point2Transform);
        puntosRecorrido.Enqueue(Point3Transform);
        puntosRecorrido.Enqueue(Point4Transform);
      

        // Inicializa el punto actual con el primer punto de recorrido.
        if (puntosRecorrido.Counter() > 0)
        {
            puntoActual = puntosRecorrido.Dequeue();
        }
    }


    private void Update()
    {
        float distancia = Vector3.Distance(transform.position, jugador.position);

        // Comprueba si el jugador está dentro del rango de persecución.
        if (distancia <= rangoPersecusion)
        {
            persiguiendoJugador = true;
            // Detiene el movimiento actual.
            rb.velocity = Vector2.zero;

            // Calcula la dirección hacia el jugador.
            Vector3 direccion = (jugador.position - transform.position).normalized;

            // Mueve al enemigo hacia el jugador.
            rb.velocity = direccion * (velocidad * Time.deltaTime);
        }
        else
        {
            persiguiendoJugador = false;
        }

        // Si no está persiguiendo al jugador y la cola de puntos de recorrido no está vacía, realiza el recorrido.
        if (!persiguiendoJugador && puntosRecorrido.Counter() > 0)
        {
            MoverHaciaPunto(puntoActual);

            // Si ha llegado al punto actual, asigna el siguiente punto de la cola.
            if (Vector3.Distance(transform.position, puntoActual.position) <= distanciaMinima)
            {
                puntoActual = puntosRecorrido.Dequeue();
                // Vuelve a encolar el punto para reiniciar el recorrido.
                puntosRecorrido.Enqueue(puntoActual);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sacarVida.LooseLife();
            //Vector2 forceDirection = (collision.transform.position - transform.position).normalized;
            //rb.AddForce(-forceDirection * force, ForceMode2D.Impulse);
        }
    }

    // Método para mover al enemigo hacia un punto.
    private void MoverHaciaPunto(Transform punto)
    {
        Vector3 direccion = (punto.position - transform.position).normalized;
        rb.velocity = direccion * (velocidad * Time.deltaTime);
    }
}
