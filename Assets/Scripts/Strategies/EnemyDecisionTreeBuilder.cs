using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDecisionTreeBuilder
{
    public TreeNode<Enemy> BuildDecisionTree()
    {
        var rootNode = new TreeNode<Enemy>(
            condition: ShouldChasePlayer,
            action: ChasePlayer,
            falseNode: new TreeNode<Enemy>(
                condition: _ => true,  // Siempre verdadero para la patrulla
                action: Patrol
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

    private void Patrol(Enemy enemy)
    {
        Debug.Log("Patrolling");
        if (enemy.puntosRecorrido.Counter() > 0)
        {
            enemy.MoverHaciaPunto(enemy.puntoActual);

            if (Vector3.Distance(enemy.transform.position, enemy.puntoActual.position) <= enemy.distanciaMinima)
            {
                enemy.puntoActual = enemy.puntosRecorrido.Dequeue();
                enemy.puntosRecorrido.Enqueue(enemy.puntoActual);
            }
        }
    }
}
