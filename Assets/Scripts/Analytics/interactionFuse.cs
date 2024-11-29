using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using System;

public class interactionFuse : Unity.Services.Analytics.Event
{
    public interactionFuse() : base("fuseObtained")
    {

    }

    public string gameObjectName { set { SetParameter("gameObjectName", value); } }


}
