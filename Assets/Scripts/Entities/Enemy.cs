using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    public PlayerManager sacarVida;
    public Transform jugador;
    public float rangoPersecusion = 4f;
    public float speed = 4.0f;
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
    public Transform Point10Transform;
    public Transform Point11Transform;

    private Transform puntoActual;
    private bool persiguiendoJugador = false;
    private bool enPausaDespuesDeColision = false;
    private float tiempoPausa = 1.0f; // 1 segundo de pausa
    private float rangoPersecusionInit;
    private Graph graph;

    private void Start()
    {
        rangoPersecusionInit = rangoPersecusion;

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
        puntosRecorrido.Enqueue(Point10Transform);
        puntosRecorrido.Enqueue(Point11Transform);

        graph = new Graph();

        for (int i = 0; i < puntosRecorrido.Counter(); i++)
        {
            for (int j = i + 1; j < puntosRecorrido.Counter(); j++)
            {
                float distancia = Vector3.Distance(puntosRecorrido.GetElement(i).position, puntosRecorrido.GetElement(j).position);
                graph.AddEdge(puntosRecorrido.GetElement(i), puntosRecorrido.GetElement(j), distancia);
            }
        }

        // Inicializar puntoActual usando Dijkstra
        puntoActual = Dijkstra(puntosRecorrido.GetElement(0));
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

            // Verifica si se presiona la tecla Shift izquierda (Shift izq.)
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // Duplica el rango de persecución temporalmente
                float nuevoRango = rangoPersecusion * 2f;

                // Asigna el nuevo valor al rango de persecución
                rangoPersecusion = nuevoRango;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                // Divide el rango de persecución temporalmente
                float nuevoRango = rangoPersecusion / 2f;

                // Asigna el nuevo valor al rango de persecución
                rangoPersecusion = nuevoRango;
            }
            else
            {
                // Si ninguna tecla se presiona, restaura el valor inicial
                rangoPersecusion = rangoPersecusionInit;
            }

            if (distancia <= rangoPersecusion)//estamos en rango del player
            {
                persiguiendoJugador = true;

                // Si no estamos en pausa, seguimos moviendonos hacia el jugador.
                Vector3 direccion = (jugador.position - transform.position).normalized;
                transform.position += direccion * speed * Time.deltaTime;
            }
            else
            {
                persiguiendoJugador = false;
            }

            if (!persiguiendoJugador && puntosRecorrido.Counter() > 1)
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

    private void MoverHaciaPunto(Transform punto)
    {
        // Calcula la dirección hacia el punto
        Vector3 direccion = (punto.position - transform.position).normalized;

        // Mueve el enemigo hacia el punto con la velocidad específica
        transform.position += direccion * speed * Time.deltaTime;
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

    private Transform Dijkstra(Transform startNode)
    {
        Dictionary<Transform, float> distances = new Dictionary<Transform, float>();
        Dictionary<Transform, Transform> previous = new Dictionary<Transform, Transform>();
        List<Transform> unvisitedNodes = new List<Transform>();

        

        while (unvisitedNodes.Count > 0)
        {
            Transform currentNode = null;

            foreach (Transform node in unvisitedNodes)
            {
                if (currentNode == null || distances[node] < distances[currentNode])
                {
                    currentNode = node;
                }
            }

            unvisitedNodes.Remove(currentNode);

            foreach (var neighbor in graph.AdjacencyList[currentNode])
            {
                float tentativeDistance = distances[currentNode] + neighbor.Value;
                if (tentativeDistance < distances[neighbor.Key])
                {
                    distances[neighbor.Key] = tentativeDistance;
                    previous[neighbor.Key] = currentNode;
                }
            }
        }

        // Recuperar el camino más corto
        Transform targetNode = puntosRecorrido.Peek(); // Puedes cambiar este nombre si lo deseas
        List<Transform> path = new List<Transform>();
        while (previous[targetNode] != null)
        {
            path.Insert(0, targetNode);
            targetNode = previous[targetNode];
        }

        return path.Count > 0 ? path[0] : null;
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
