using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrafoTDA 
{
    void InicializarGrafo();
    void AgregarVertice(int v);
    void EliminarVertice(int v);
    ConjuntoTDA Vertices();
    void AgregarArista(int id, int v1, int v2, int peso);
    void EliminarArista(int v1, int v2);
    bool ExisteArista(int v1, int v2);
    int PesoArista(int v1, int v2);
    int Vert2Indice(int v);
    ConjuntoTDA VerticesAdyacentes(int v);
    object ObtenerVertice(int v);
}
