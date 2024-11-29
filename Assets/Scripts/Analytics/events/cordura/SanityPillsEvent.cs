using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityPillsEvent :Unity.Services.Analytics.Event
{
    public SanityPillsEvent() : base("sanatyPills")
    {

    }

    public int Pills { set { SetParameter("Pills", value); } }
}
