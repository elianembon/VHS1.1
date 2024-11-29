using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityEvent : Unity.Services.Analytics.Event
{
    public SanityEvent() : base("sanity")
    {

    }

    public int LightTime { set { SetParameter("LightTime", value); } }
}
