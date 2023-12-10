using System.Collections;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [SerializeField]
    NodoManager nM;
    public float velocidad = 5.0f;

    private int[] nodosRecorrido;
    private int indiceNodoActual = 0;

    void Start()
    {
        nM.LoadNodos(); // Aseg�rate de cargar los nodos antes de cargar el grafo
        nM.ChargeGrafo(); // Aseg�rate de cargar el grafo correctamente

        AlgDijkstra.Dijkstra(nM.grafo, 0); // Cambi� el �ndice de inicio a 0
        nodosRecorrido = AlgDijkstra.ConvertirNodosAEnteros(AlgDijkstra.nodos);
    }

    void Update()
    {
       
            if (nodosRecorrido != null && nodosRecorrido.Length > 0)
            {
                MoverHaciaNodo(nM.grafo.Etiqs[nodosRecorrido[indiceNodoActual]]);

                if (Vector3.Distance(transform.position, nM.grafo.Nodos[nodosRecorrido[indiceNodoActual]].transform.position) < 0.1f)
                {
                    indiceNodoActual = (indiceNodoActual + 1) % nodosRecorrido.Length;
                }
            }
            
    }
    
    void MoverHaciaNodo(int nodo)
    {
        // Assuming that Nodo class has a Position property
        Vector3 direccion = (nM.grafo.Nodos[nodo].transform.position - transform.position).normalized;
        transform.position += direccion * velocidad * Time.deltaTime;
    }
}