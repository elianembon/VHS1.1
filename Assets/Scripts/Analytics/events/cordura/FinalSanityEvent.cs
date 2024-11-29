using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSanityEvent : Unity.Services.Analytics.Event
{
    public FinalSanityEvent() : base("finalSanity")
    {

    }

    public float currentSanity { set { SetParameter("currentSanity", value); } }
}
