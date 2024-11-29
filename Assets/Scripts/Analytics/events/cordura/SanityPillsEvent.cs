using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using System;

public class SanityPillsEvent :Unity.Services.Analytics.Event
{
    public SanityPillsEvent() : base("sanatyPills")
    {

    }

    public int Pills { set { SetParameter("Pills", value); } }
}
