using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public int time = 5;
    public float timefloat = 0;

    public static GameManager gm; 

    public GameObject blast;

    bool endgame = false;

    bool onetimeBossText = false;
    void Start()
    {
        if(gm == null) {
            gm = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(!endgame) {
                
            if(!onetimeBossText && time <=3) {
                CanvasManager.cm.showBossText("Boss Incoming", 3);
                onetimeBossText = true;
            }
        timefloat += Time.deltaTime;
        if(timefloat >= 1f) {
            timefloat = 0;
            time -=1;
            CanvasManager.cm.updateTimer(time);
        }

        if(time <= 0) {
            time = 15;
            CanvasManager.cm.ChangeTimeColor();
            EnemyManager.em.handleBoss();
            endgame = true;
        }

        } else {
             timefloat += Time.deltaTime;
            if(timefloat >= 1f) {
            timefloat = 0;
            time -=1;
            CanvasManager.cm.updateTimer(time);
            }
            if(time <= 0 && endgame == true)  {
                //endgame=false;
                blast.GetComponent<GameOverScript>().playBlast();
            }

        }
    }

    public void HandleGameEnd() {
        SceneManager.LoadScene("GameOver");
    }
}
