using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityDowngradeEvent : Unity.Services.Analytics.Event
{
    public SanityDowngradeEvent() : base("sanityDowngrade")
    {

    }

    public int ShadowTime { set { SetParameter("ShadowTime", value); } }
}
