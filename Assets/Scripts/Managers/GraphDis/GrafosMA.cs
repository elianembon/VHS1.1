using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrafoMA: MonoBehaviour, IGrafoTDA
{
    public int[,] MAdy;
    public int[] Etiqs;
    public int cantNodos;
    public Nodo[] Nodos;

    public void InicializarGrafo(int maxNodos)
    {
        MAdy = new int[maxNodos, maxNodos];
        Etiqs = new int[maxNodos];
        cantNodos = 0;
        Nodos = new Nodo[maxNodos];
    }
    public void AgregarVertice(int v, Nodo n)
    {
        Etiqs[cantNodos] = v;
        Nodos[cantNodos] = n;
        for (int i = 0; i <= cantNodos; i++)
        {
            MAdy[cantNodos, i] = 0;
            MAdy[i, cantNodos] = 0;
        }
        cantNodos++;
    }

    public void AgregarArista(int v1, int v2, int peso)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        MAdy[o, d] = peso;
    }

    public List<int> ObtenerVecinos(int v)
    {
        List<int> vecinos = new List<int>();
        int indice = Vert2Indice(v);

        for (int i = 0; i < cantNodos; i++)
        {
            if (MAdy[indice, i] != 0)
            {
                vecinos.Add(Etiqs[i]);
            }
        }

        return vecinos;
    }

    public int Vert2Indice(int v)
    {
        int i = cantNodos - 1;
        while (i >= 0 && Etiqs[i] != v)
        {
            i--;
        }

        return i;
    }
}
