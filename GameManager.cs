using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject playerPb;
    public int currentHp;
    public bool dieOnce=true;
    public int score=0;
    public bool isVictory=false;
    public bool isAlive=true;
    public bool isDancing = false;
    private void Awake()
    {
        if (instance==null)
        {
          instance = this;

        }
       
    }

    private void Update()
    {
        if (currentHp == 0 && dieOnce)
        {
            dieOnce = false;
            GameOver();

        }

    }

    public void PlayerDmg()
    {
        currentHp -= 1;

        
    }
   

    void GameOver()
    {
        isAlive = false;
        //Destroy(playerPb);
    }

}
