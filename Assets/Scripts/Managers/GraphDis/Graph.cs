using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Graph : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Programa Iniciado\n");

        GrafosMA grafoEst = new GrafosMA();
        grafoEst.InicializarGrafo();

        int[] vertices = { 1, 2, 3, 4, 5, 6 };
        for (int i = 0; i < vertices.Length; i++)
        {
            grafoEst.AgregarVertice(vertices[i]);
        }

        int[] aristas_origen = { 1, 2, 1, 3, 3, 5, 6, 4 };
        int[] aristas_destino = { 2, 1, 3, 5, 4, 6, 5, 6 };
        int[] aristas_pesos = { 12, 10, 21, 9, 32, 12, 87, 10 };

        for (int i = 0; i < aristas_pesos.Length; i++)
        {
            grafoEst.AgregarArista(aristas_origen[i], aristas_destino[i], aristas_pesos[i]);
        }

        Debug.Log("\nListado de Etiquetas de los nodos");
        for (int i = 0; i < grafoEst.Etiqs.Length; i++)
        {
            if (grafoEst.Etiqs[i] != 0)
            {
                Debug.Log("Nodo: " + grafoEst.Etiqs[i].ToString());
            }
        }

        Debug.Log("\nListado de Aristas (Inicio, Fin, Peso)");
        for (int i = 0; i < grafoEst.cantNodos; i++)
        {
            for (int j = 0; j < grafoEst.cantNodos; j++)
            {
                if (grafoEst.MAdy[i, j] != 0)
                {
                    int nodoIni = grafoEst.Etiqs[i];
                    int nodoFin = grafoEst.Etiqs[j];
                    Debug.Log(nodoIni.ToString() + ", " + nodoFin.ToString() + ", " + grafoEst.MAdy[i, j].ToString());
                }
            }
        }

        StartCoroutine(EjecutarAlgoritmoDijkstra(grafoEst, 3));
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator EjecutarAlgoritmoDijkstra(GrafosMA grafo, int origen)
    {
        yield return null; // Esperar un frame

        Debug.Log("\nAlgoritmo Dijkstra");
        AlgDijkstra.Dijkstra(grafo, origen);
        MuestroResultadosAlg(AlgDijkstra.distance, grafo.cantNodos, grafo.Etiqs, AlgDijkstra.nodos);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void MuestroResultadosAlg(int[] distance, int verticesCount, int[] Etiqs, string[] caminos)
    {
        string distancia = "";

        Debug.Log("Vertice    Distancia desde origen    Nodos");

        for (int i = 0; i < verticesCount; ++i)
        {
            if (distance[i] == int.MaxValue)
            {
                distancia = "---";
            }
            else
            {
                distancia = distance[i].ToString();
            }
            Debug.Log($"{Etiqs[i]}\t  {distancia}\t\t\t   {caminos[i]}");
        }
    }
}
/* ----------------------------------------------------------------------------------*/

    public int Vert2Indice(int etiqueta)
    {
        for (int i = 0; i < cantNodos; i++)
        {
            if (Etiqs[i] == etiqueta)
            {
                return i;
            }
        }
        return -1; // Devolver -1 si la etiqueta no se encuentra en el grafo.
    }
}



namespace _8_Grafos
{
    class AlgDijkstra
    {
        public static int[] distance;
        public static string[] nodos;

        private static int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
        {
            int min = int.MaxValue;
            int minIndex = 0;

            for (int v = 0; v < verticesCount; ++v)
            {
                // obtengo siempre el nodo con la menor distancia calculada
                // solo lo verifico en los nodos que no tienen seteado ya un camino (shortestPathTreeSet[v] == false)
                if (shortestPathTreeSet[v] == false && distance[v] <= min)
                {
                    min = distance[v];
                    minIndex = v;
                }
            }

            // devuelvo el nodo calculado
            return minIndex;
        }

        public static void Dijkstra(GrafosMA grafo, int source)
        {
            // obtengo la matriz de adyacencia del TDA_Grafo
            int[,] graph = grafo.MAdy;
            
            // obtengo la cantidad de nodos del TDA_Grafo
            int verticesCount = grafo.cantNodos;

            // obtengo el indice del nodo elegido como origen a partir de su valor
            source = grafo.Vert2Indice(source);

            // vector donde se van a guardar los resultados de las distancias entre 
            // el origen y cada vertice del grafo
            distance = new int[verticesCount];

            bool[] shortestPathTreeSet = new bool[verticesCount];

            int[] nodos1 = new int[verticesCount];
            int[] nodos2 = new int[verticesCount];

            for (int i = 0; i < verticesCount; ++i)
            {
                // asigno un valor maximo (inalcanzable) como distancia a cada nodo
                // cualquier camino que se encuentre va a ser menor a ese valor
                // si no se encuentra un camino, este valor maximo permanece y es el 
                // indica que no hay ningun camino entre el origen y ese nodo
                distance[i] = int.MaxValue;

                // seteo en falso al vector que guarda la booleana cuando se encuentra un camino
                shortestPathTreeSet[i] = false;

                nodos1[i] = nodos2[i] = -1;
            }

            // la distancia al nodo origen es 0
            distance[source] = 0;
            nodos1[source] = nodos2[source] = grafo.Etiqs[source];

            // recorro todos los nodos (vertices)
            for (int count = 0; count < verticesCount - 1; ++count)
            {
                int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
                shortestPathTreeSet[u] = true;

                // recorro todos los nodos (vertices)
                for (int v = 0; v < verticesCount; ++v)
                {
                    // comparo cada nodo (que aun no se haya calculado) contra el que se encontro que tiene la menor distancia al origen elegido
                    if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
                    {
                        // si encontré una distancia menor a la que tenia, la reasigno la nodo
                        distance[v] = distance[u] + graph[u, v];
                        // guardo los nodos para reconstruir el camino
                        nodos1[v] = grafo.Etiqs[u];
                        nodos2[v] = grafo.Etiqs[v];
                    }   
                }
            }

            // construyo camino de nodos
            nodos = new string[verticesCount];
            int nodOrig = grafo.Etiqs[source];
            for (int i = 0; i < verticesCount; i++)
            {
                if (nodos1[i] != -1)
                {
                    List<int> l1 = new List<int>();
                    l1.Add(nodos1[i]);
                    l1.Add(nodos2[i]);
                    while (l1[0] != nodOrig)
                    {
                        for (int j = 0; j < verticesCount; j++)
                        {
                            if (j != source && l1[0] == nodos2[j])
                            {
                                l1.Insert(0, nodos1[j]);
                                break;
                            }
                        }
                    }
                    for (int j = 0; j < l1.Count; j++)
                    {
                        if (j == 0)
                        {
                            nodos[i] = l1[j].ToString();
                        }
                        else
                        {
                            nodos[i] += "," + l1[j].ToString();
                        }
                    }
                }
            }
        }


    }
    
    
    
}
