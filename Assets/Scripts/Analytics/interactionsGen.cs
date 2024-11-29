using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using System;

public class interactionsGen : Unity.Services.Analytics.Event
{
    public interactionsGen () : base ("genRepaired") 
    {
        
    }

    public string genName { set { SetParameter("genName", value); } }



}
