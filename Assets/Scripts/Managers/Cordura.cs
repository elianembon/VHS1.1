using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cordura : MonoBehaviour
{
    private Slider _slider;
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    
    void Update()
    {
        
    }

    

    public void changeCurrentCordura (float CurrentCordura)
    {
        _slider.value = CurrentCordura;
    }

    public void InitBarraCordura (float QuantityCordura)
    {
        
        changeCurrentCordura(QuantityCordura);
    }
}
