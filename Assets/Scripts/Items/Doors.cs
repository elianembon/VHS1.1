using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : eventManager
{

    [SerializeField] private Transform destination;
    [SerializeField] string nameDoor;

    public Transform GetDestination()
    {
        SendinteractionDoor(nameDoor);
        return destination;
    }
}
