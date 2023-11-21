using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Nodo : MonoBehaviour
{
    [Serializable]
    public struct NodoConnection
    {
        public int origin;
        public int destination;
        public int cost;
        public Nodo destinationNode;
    }

    public List<NodoConnection> connections = new List<NodoConnection>();
}
