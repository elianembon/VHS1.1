using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Programa Iniciado\n");

        GrafoMA grafoEst = new GrafoMA();
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

    IEnumerator EjecutarAlgoritmoDijkstra(GrafoMA grafo, int origen)
    {
        yield return null; // Esperar un frame

        Debug.Log("\nAlgoritmo Dijkstra");
        AlgDijkstra.Dijkstra(grafo, origen);
        MuestroResultadosAlg(AlgDijkstra.distance, grafo.cantNodos, grafo.Etiqs, AlgDijkstra.nodos);
    }

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
