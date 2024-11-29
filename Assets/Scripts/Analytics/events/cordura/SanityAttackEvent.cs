using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityAttackEvent : Unity.Services.Analytics.Event
{
    public SanityAttackEvent() : base("sanityAttack")
    {

    }

    public int EnemyAttack { set { SetParameter("EnemyAttack", value); } }
}
