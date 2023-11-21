using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerManager sacarVida;
    public Transform jugador;
    public float rangoPersecusion = 4f;
    public float speed = 4.0f;
    public float distanciaMinima = 1.0f;
    public Q_queue<Transform> puntosRecorrido;
    public Transform[] Points;


    public Transform puntoActual;
    private bool enPausaDespuesDeColision;
    private float tiempoPausa = 0f; // 1 segundo de pausa
    private float rangoPersecusionInit;

    private Tree<Enemy> decisionTree;

    void Start()
    {
        puntosRecorrido = new Q_queue<Transform>();
        rangoPersecusionInit = rangoPersecusion;
        enPausaDespuesDeColision = false;
        sacarVida = GameObject.FindObjectOfType<PlayerManager>();
        if (Points != null && Points.Length > 0)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                puntosRecorrido.Enqueue(Points[i]);
            }
        }
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

        if (enPausaDespuesDeColision)
        {
            
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
            sacarVida.LooseLife(20f);

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

   
}