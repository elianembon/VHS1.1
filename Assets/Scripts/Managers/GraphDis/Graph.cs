using System.Collections.Generic;

public class Graph<T>
{
    public Dictionary<T, List<T>> AdjacencyList { get; }

    public Graph()
    {
        AdjacencyList = new Dictionary<T, List<T>>();
    }

    public void AddVertex(T vertex)
    {
        if (!AdjacencyList.ContainsKey(vertex))
            AdjacencyList[vertex] = new List<T>();
    }

    public void AddEdge(T source, T destination)
    {
        if (AdjacencyList.ContainsKey(source) && AdjacencyList.ContainsKey(destination))
        {
            AdjacencyList[source].Add(destination);
            AdjacencyList[destination].Add(source); // Considerar la dirección en la que se puede viajar
        }
    }
}