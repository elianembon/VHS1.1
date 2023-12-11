using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public PlayerManager sacarVida;
    public Transform jugador;
    public float rangoPersecusion = 4f;
    public float distanciaMinima = 1.0f;
    public Q_queue<Transform> puntosRecorrido;
    public Transform[] Points;

    [SerializeField] public EntityStats _stats;
    public Transform puntoActual;
    private bool enPausaDespuesDeColision;
    private float tiempoPausa = 0f; // 1 segundo de pausa
    private float rangoPersecusionInit;

    private Tree<Enemy> decisionTree;

    private NavMeshAgent navMeshAgent;

    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
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
            SetDestination(puntoActual.position);
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
            sacarVida.LooseLife(_stats.Damage);

            // Detener el movimiento y entrar en estado de pausa.
            enPausaDespuesDeColision = true;
            tiempoPausa = 1.0f; // Restablecer el tiempo de pausa
        }
    }
   public void SetDestination(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
    }
    public void MoverHaciaPunto(Transform punto)
    {
        Vector3 direccion = (punto.position - transform.position).normalized;

        transform.position += direccion * _stats.MovementSpeed * Time.deltaTime;
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