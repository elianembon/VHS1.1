using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Subject 
{
    void Suscribe(Observer observer);
    void Unsuscribe(Observer observer);
    void Notify();
}
