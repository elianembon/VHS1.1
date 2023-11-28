using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    Player myplayer;
    void Start()
    {
        myplayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        myplayer.PlayerDead += ActivedScene;
    }

    public void ActivedScene(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
