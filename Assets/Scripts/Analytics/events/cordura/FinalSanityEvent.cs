using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSanityEvent : Unity.Services.Analytics.Event
{
    public FinalSanityEvent() : base("finalSanity")
    {

    }

    public int FinalSanity { set { SetParameter("FinalSanity", value); } }
}
