using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using System;

public class OnDeadByEnemyEvent : Unity.Services.Analytics.Event
{
    public OnDeadByEnemyEvent() : base("onDeadByEnemy")
    {

    }

    public bool deathByEnemy { set { SetParameter("deathByEnemy", value); } }


}
