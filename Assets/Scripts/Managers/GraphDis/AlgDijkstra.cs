using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

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
            if (shortestPathTreeSet[v] == false && distance[v] <= min)
            {
                min = distance[v];
                minIndex = v;
            }
        }

        return minIndex;
    }

    public static void Dijkstra(GrafoMA grafo, int source)
    {
        int verticesCount = grafo.cantNodos;

        source = grafo.Vert2Indice(source);

        distance = new int[verticesCount];
        nodos = new string[verticesCount];

        bool[] shortestPathTreeSet = new bool[verticesCount];

        List<List<int>> nodosLista = new List<List<int>>(); // Mueve la inicialización aquí

        for (int i = 0; i < verticesCount; ++i)
        {
            distance[i] = int.MaxValue;
            shortestPathTreeSet[i] = false;
            nodosLista.Add(new List<int>());
            nodosLista[i].Add(grafo.Etiqs[i]);
        }

        for (int i = 0; i < verticesCount; ++i)
        {
            int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);

            shortestPathTreeSet[u] = true;

            for (int v = 0; v < verticesCount; ++v)
            {
                if (!shortestPathTreeSet[v] && grafo.MAdy[u, v] != 0 && distance[u] != int.MaxValue && distance[u] + grafo.MAdy[u, v] < distance[v])
                {
                    distance[v] = distance[u] + grafo.MAdy[u, v];

                    nodosLista[v] = new List<int>(nodosLista[u]);
                    nodosLista[v].Add(grafo.Etiqs[v]);
                }
            }
        }

        for (int i = 0; i < verticesCount; i++)
        {
            if (nodosLista[i].Count > 0)
            {
                nodos[i] = string.Join(",", nodosLista[i]);
            }
        }

        int[] nodos1 = new int[verticesCount];
        int[] nodos2 = new int[verticesCount];

        distance[source] = 0;
        nodos1[source] = nodos2[source] = grafo.Etiqs[source];

        UnityEngine.Debug.Log("Entrando en el bucle principal de Dijkstra.");
        for (int i = 0; i < verticesCount; i++)
        {
            UnityEngine.Debug.Log($"Iteración {i + 1}: nodos1[{i}] = {nodos1[i]}, nodos2[{i}] = {nodos2[i]}");
            if (nodos1[i] != -1)
            {
                List<int> l1 = new List<int>();
                l1.Add(nodos1[i]);
                l1.Add(nodos2[i]);
                while (l1[0] != source)
                {
                    for (int j = 0; j < verticesCount; j++)
                    {
                        if (l1[0] == nodos2[j])
                        {
                            l1.Insert(0, nodos1[j]);
                            break;
                        }
                    }
                }

                // Actualizar nodos1 y nodos2 según el resultado del camino más corto
                nodos1[i] = l1[0]; // Nodo inicial del camino más corto
                nodos2[i] = l1[l1.Count - 1]; // Nodo final del camino más corto

                nodos[i] = string.Join(",", l1); // Simplificación aquí
            }
        }

        // Agrega mensajes de depuración
        UnityEngine.Debug.Log("Dijkstra nodos: " + string.Join(", ", nodos));
    }

    public static int[] ConvertirNodosAEnteros(string[] nodos)
    {
        if (nodos != null)
        {
            int[] nodosEnteros = new int[nodos.Length];

            for (int i = 0; i < nodos.Length; i++)
            {
                string[] partes = nodos[i].Split(',');

                if (partes.Length > 0)
                {
                    if (int.TryParse(partes[0], out int nodoEntero))
                    {
                        nodosEnteros[i] = nodoEntero;
                    }
                    else
                    {
                        throw new InvalidOperationException($"No se pudo convertir el nodo {partes[0]} a un entero.");
                    }
                }
            }

            return nodosEnteros;
        }
        else
        {
            // Manejar el caso cuando nodos es nulo
            Console.WriteLine("El arreglo de nodos es nulo.");
            return new int[0]; // o maneja de acuerdo a tus requerimientos
        }
    }
}