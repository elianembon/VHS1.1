using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDecisionTreeBuilder : eventManager
{



    public TreeNode<Enemy> BuildDecisionTree()
    {
        var rootNode = new TreeNode<Enemy>(
            condition: ShouldChasePlayer,
            action: ChasePlayer,
            falseNode: new TreeNode<Enemy>(
                condition: _ => true,
                action: MoveAlongDijkstraPath
            )
        );

        return rootNode;
    }

    private bool hasSentData = false;

    private bool ShouldChasePlayer(Enemy enemy)
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.jugador.position);
        bool shouldChase = distance <= enemy.rangoPersecusion;

        if (shouldChase && !hasSentData)
        {
            SendEnemydata(true);
            hasSentData = true;  // Marcar como enviado
        }
        else if (!shouldChase)
        {
            hasSentData = false;  // Resetear cuando la condición ya no se cumple
        }

        return shouldChase;
    }

    private void ChasePlayer(Enemy enemy)
    {
        

        Vector3 direction = (enemy.jugador.position - enemy.transform.position).normalized;
        enemy.transform.position += direction * enemy._stats.MovementSpeed * Time.deltaTime;

        // Update the NavMeshAgent destination
        enemy.SetDestination(enemy.jugador.position);
    }
    private void MoveAlongDijkstraPath(Enemy enemy)
    {
        // Check if Dijkstra nodes are available
        if (enemy.nodosRecorrido != null && enemy.nodosRecorrido.Length > 0)
        {
            enemy.MoverHaciaNodo(enemy.nM.grafo.Etiqs[enemy.nodosRecorrido[enemy.indiceNodoActual]]);

            if (Vector3.Distance(enemy.transform.position, enemy.nM.grafo.Nodos[enemy.nodosRecorrido[enemy.indiceNodoActual]].transform.position) < 0.1f)
            {
                enemy.indiceNodoActual = (enemy.indiceNodoActual + 1) % enemy.nodosRecorrido.Length;
            }
        }
    }
}
