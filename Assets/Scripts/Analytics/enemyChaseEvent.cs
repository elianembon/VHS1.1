using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyChaseEvent : Unity.Services.Analytics.Event
{
    public enemyChaseEvent() : base("enemyChase")
    {

    }
    public bool enemyInArea { set { SetParameter("enemyInArea", value); } }
}
