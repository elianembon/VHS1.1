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

    public void SendenemyAreaPersecutionEvent(bool _enemyInArea, int _ActualNodo)
    {
        enemyAreaPersecutionEvent enemyAreaPersecutionEvent = new enemyAreaPersecutionEvent()
        {
            enemyInArea = _enemyInArea,
            ActualNodo = _ActualNodo
        };

        AnalyticsService.Instance.RecordEvent(enemyAreaPersecutionEvent);
    }

        //lina
    public void SendinteractionFuse(string _gameObjectName)
    {
        interactionFuse interactionFuse = new interactionFuse()
        {
            gameObjectName  = _gameObjectName,
        };

        AnalyticsService.Instance.RecordEvent(interactionFuse);
    }

    public void SendinteractionDoor(string _gameObjectName)
    {
        interactionDoor interactionDoor = new interactionDoor()
        {
            gameObjectName = _gameObjectName,
        };

        AnalyticsService.Instance.RecordEvent(interactionDoor);
    }

    public void SendinteractionsGen(string _genName)
    {
        interactionsGen interactionsGen = new interactionsGen()
        {
            genName = _genName,
        };

        AnalyticsService.Instance.RecordEvent(interactionsGen);
    }


}