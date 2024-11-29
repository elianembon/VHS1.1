using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;

public class eventManager : MonoBehaviour
{

   
    public void SendEnemydata(bool _enemyInArea)
    {
        enemyChaseEvent enemyChaseEvent = new enemyChaseEvent()
        {
            enemyInArea = _enemyInArea
        };

        AnalyticsService.Instance.RecordEvent(enemyChaseEvent);
    }
}