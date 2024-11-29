using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAreaPersecutionEvent : Unity.Services.Analytics.Event
{
    public enemyAreaPersecutionEvent() : base("enemyAreaPersecutionEvent")
    {

    }

    public int ActualNodo { set { SetParameter("ActualNodo", value); } }
    public bool enemyInArea { set { SetParameter("enemyInArea", value); } }
}
