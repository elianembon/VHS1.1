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

        var decisionTreeBuilder = new EnemyDecisionTreeBuilder();
        var rootNode = decisionTreeBuilder.BuildDecisionTree();
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
        if (puntosRecorrido.Counter() > 0)
        {
            MoverHaciaPunto(puntoActual);

            if (Vector3.Distance(transform.position, puntoActual.position) <= distanciaMinima)
            {
                puntoActual = puntosRecorrido.Dequeue();
                puntosRecorrido.Enqueue(puntoActual);
            }
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

