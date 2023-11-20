using UnityEngine;

public class DecisionTree
{
    private readonly Enemy enemy;

    public DecisionTree(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void ExecuteDecisionTree()
    {
        if (ShouldChasePlayer())
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private bool ShouldChasePlayer()
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.jugador.position);
        return distance <= enemy.rangoPersecusion;
    }

    private void ChasePlayer()
    {
        Vector3 direction = (enemy.jugador.position - enemy.transform.position).normalized;
        enemy.transform.position += direction * enemy.speed * Time.deltaTime;
    }

    private void Patrol()
    {
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

