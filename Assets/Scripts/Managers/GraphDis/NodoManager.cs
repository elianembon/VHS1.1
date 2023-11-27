using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodoManager : MonoBehaviour
{

    private List<Nodo> nodos = new List<Nodo>();

    public GrafoMA grafo;

    private void Awake()
    {
        LoadNodos();
    }
    void Start()
    {

        ChargeGrafo();

    }

    public void LoadNodos()
    {
        foreach(Nodo n in GetComponentsInChildren<Nodo>())
        {
            nodos.Add(n);
        }
    }

    public void ChargeGrafo()
    {
        grafo = new GrafoMA();
        grafo.InicializarGrafo(nodos.Count);

        for (int i = 0; i < nodos.Count; i++)
        {
            grafo.AgregarVertice(i, nodos[i]);
        }

        for (int i = 0; i < nodos.Count; i++)
        {
            foreach (Nodo n in nodos)
            {
                foreach (Nodo.NodoConnection nods in n.connections)
                {
                    grafo.AgregarArista(nods.origin, nods.destination, nods.cost);
                }
            }
        }

    }
}
