using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    public Dictionary<Transform, Dictionary<Transform, float>> AdjacencyList { get; private set; }

    public Graph()
    {
        AdjacencyList = new Dictionary<Transform, Dictionary<Transform, float>>();
    }

    public void AddNode(Transform node)
    {
        AdjacencyList[node] = new Dictionary<Transform, float>();
    }

    public void AddEdge(Transform node1, Transform node2, float weight)
    {
        AdjacencyList[node1][node2] = weight;
        AdjacencyList[node2][node1] = weight; // Grafo no dirigido
    }
}
