using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour 
{

    private eventManager eventManager;
    public PlayerManager sacarVida;
    public Transform jugador;
    public float rangoPersecusion = 4f;
    public float distanciaMinima = 1.0f;

    [SerializeField] public EntityStats _stats;
    private bool enPausaDespuesDeColision;
    private bool presec = false;

    private float tiempoPausa = 0f; // 1 segundo de pausa
    private float rangoPersecusionInit;

    private Tree<Enemy> decisionTree;
    private eventManager _eventManager;
    private NavMeshAgent navMeshAgent;

    // Additional variables for Dijkstra
    public NodoManager nM;
    public int[] nodosRecorrido;
    public int indiceNodoActual = 0;
    public int _numOfAttacks = 0; 

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        rangoPersecusionInit = rangoPersecusion;
        enPausaDespuesDeColision = false;
        sacarVida = GameObject.FindObjectOfType<PlayerManager>();
        _eventManager = FindObjectOfType<eventManager>();

        var decisionTreeBuilder = new EnemyDecisionTreeBuilder();
        var rootNode = decisionTreeBuilder.BuildDecisionTree();
        decisionTree = new Tree<Enemy>(rootNode);

        // Dijkstra initialization
        AlgDijkstra.Dijkstra(nM.grafo, 0);
        nodosRecorrido = AlgDijkstra.ConvertirNodosAEnteros(AlgDijkstra.nodos);
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
            _numOfAttacks++;
            sacarVida.LooseLife(_stats.Damage);
            _eventManager.SendSanityDowngradeEvent(_numOfAttacks);
            // Detener el movimiento y entrar en estado de pausa.
            enPausaDespuesDeColision = true;
            tiempoPausa = 1.0f; // Restablecer el tiempo de pausa
        }
    }
    public void MoverHaciaNodo(int nodo)
    {
        Vector3 direccion = (nM.grafo.Nodos[nodo].transform.position - transform.position).normalized;
        transform.position += direccion * _stats.MovementSpeed * Time.deltaTime;

        // Update the NavMeshAgent destination
        SetDestination(nM.grafo.Nodos[nodo].transform.position);
    }

    public void SetDestination(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
    }
}
