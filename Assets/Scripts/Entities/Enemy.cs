using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerManager sacarVida;
    public Transform jugador;
    public float rangoPersecusion = 4f;
    public float speed = 4.0f;
    public float distanciaMinima = 1.0f;
    public GrafoMA grafo;  // Usa tu implementación del grafo en lugar de Q_queue y puntosRecorrido

    public Transform puntoActual;
    private bool enPausaDespuesDeColision;
    private float tiempoPausa = 0f; // 1 segundo de pausa
    private float rangoPersecusionInit;

    private Tree<Enemy> decisionTree;

    void Start()
    {
        rangoPersecusionInit = rangoPersecusion;
        enPausaDespuesDeColision = false;

        sacarVida = GameObject.FindObjectOfType<PlayerManager>();

        // Utiliza tu propia implementación del grafo (por ejemplo, GrafoMA)
        grafo = new GrafoMA();

        if (grafo != null) //check if grafo object is not null
        {
            grafo.InicializarGrafo();
            grafo.AgregarVertice(1);
            grafo.AgregarVertice(2);
            grafo.AgregarVertice(3);

            // Añade las aristas según tu escenario
            grafo.AgregarArista(1, 1, 2, 1);
            grafo.AgregarArista(2, 2, 3, 1);

            if (puntoActual != null) // check if puntoActual object is not null
            {
                puntoActual = grafo.ObtenerVertice(1) as Transform;
            }

        }
        else
        {
            Debug.Log("Grafo no está instanciado correctamente");
        }

        // En caso de que Enemy esté bien instanciado.
        
            var decisionTreeBuilder = new EnemyDecisionTreeBuilder();
            var rootNode = decisionTreeBuilder.BuildDecisionTree(grafo);
            decisionTree = new Tree<Enemy>(rootNode);
        
    }

    void Update()
    {
        Debug.Log($"Tiempo de pausa: {tiempoPausa}");

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
            decisionTree.Root.Execute(this);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sacarVida.LooseLife();

            // Detener el movimiento y entrar en estado de pausa.
            enPausaDespuesDeColision = true;
            tiempoPausa = 1.0f; // Restablecer el tiempo de pausa
        }
    }

    public void MoverHaciaPunto(Transform punto)
    {
        Vector3 direccion = (punto.position - transform.position).normalized;
        transform.position += direccion * speed * Time.deltaTime;
    }

   public void Patrol()
    {
        int indiceActual = Array.IndexOf(grafo.Etiqs, puntoActual.GetInstanceID());
        ConjuntoTDA adyacentes = grafo.VerticesAdyacentes(indiceActual);

        if (!adyacentes.ConjuntoVacio())
        {
            int nodoAleatorio = adyacentes.Elegir();
            puntoActual = grafo.ObtenerVertice(nodoAleatorio) as Transform;

            MoverHaciaPunto(puntoActual);
        }
    }

    public void IncreaseVelocity()
    {
        speed += 1;
    }

    public void DecreaseVelocity()
    {
        speed -= 1;
    }

    public void IncreaseRange()
    {
        rangoPersecusion += 1;
    }

    public void DecreaseRange()
    {
        rangoPersecusion -= 1;
    }
}