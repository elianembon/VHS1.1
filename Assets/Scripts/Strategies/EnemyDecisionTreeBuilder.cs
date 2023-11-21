using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDecisionTreeBuilder
{


    public TreeNode<Enemy> BuildDecisionTree(GrafoMA grafo)
    {
        var rootNode = new TreeNode<Enemy>(
            condition: ShouldChasePlayer,
            action: ChasePlayer,
            falseNode: new TreeNode<Enemy>(
                condition: _ => true,  // Siempre verdadero para la patrulla
                action: enemy => PatrolWithGraph(grafo, enemy)
            )
        );

        return rootNode;
    }

    private bool ShouldChasePlayer(Enemy enemy)
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.jugador.position);
        bool shouldChase = distance <= enemy.rangoPersecusion;
        Debug.Log($"ShouldChasePlayer: {shouldChase}");
        return shouldChase;
    }

    private void ChasePlayer(Enemy enemy)
    {
        Debug.Log("Chasing Player");
        Vector3 direction = (enemy.jugador.position - enemy.transform.position).normalized;
        enemy.transform.position += direction * enemy.speed * Time.deltaTime;
    }

    private void PatrolWithGraph(GrafoMA grafo, Enemy enemy)
    {
        Debug.Log("Patrolling with Graph");

        int currentNode = grafo.Vert2Indice(enemy.puntoActual.GetInstanceID()); // Obtener el índice del nodo actual
        AlgDijkstra.Dijkstra(grafo, currentNode); // Aplicar el algoritmo de Dijkstra

        // Obtener el próximo nodo en el camino óptimo
        int nextNode = AlgDijkstra.nodos[currentNode]
            .Split(',')
            .Select(int.Parse)
            .FirstOrDefault();

        // Mover hacia el próximo nodo
        if (nextNode != 0)
        {
            Transform nextPoint = FindTransformById(grafo, nextNode);
            enemy.MoverHaciaPunto(nextPoint);
        }
    }

    private Transform FindTransformById(GrafoMA grafo, int nodeId)
    {
        // Obtén todos los vértices (nodos) del grafo
        var vertices = grafo.Vertices().OfType<int>();

        // Encuentra el índice del vértice con el ID deseado
        int index = vertices.Select(grafo.Vert2Indice)
                            .FirstOrDefault(i => grafo.Etiqs[i] == nodeId);

        // Si se encuentra el índice, obtén el Transform correspondiente
        return index != -1 ? grafo.ObtenerVertice(index) as Transform : null;
    }
}
