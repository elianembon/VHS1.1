using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionDoor : Unity.Services.Analytics.Event
{
    public interactionDoor() : base("doorUsed")
    {

    }

    public string gameObjectName { set { SetParameter("gameObjectName", value); } }


}
