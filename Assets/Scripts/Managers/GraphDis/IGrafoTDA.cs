using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrafoTDA 
{
    void InicializarGrafo(int n);
    void AgregarVertice(int v, Nodo n);
    //void EliminarVertice(int v);
    //ConjuntoTDA Vertices();
    void AgregarArista( int v1, int v2, int peso);
    //void EliminarArista(int v1, int v2);
    //bool ExisteArista(int v1, int v2);
    //int PesoArista(int v1, int v2);
}
