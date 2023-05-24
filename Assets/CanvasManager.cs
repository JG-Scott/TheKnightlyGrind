using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI timerText;


    public TextMeshProUGUI levelDisplayText;

    public TextMeshProUGUI LevelUptext;
    public static CanvasManager cm;

    public TextMeshProUGUI bossText;
    void Start()
    {
        if(cm == null) {
            cm = this;
        }
    }
    public void updateTimer(int time) {
        timerText.text = time.ToString();
    }

    public void showLevelUp(int damage) {
        LevelUptext.text = "LEVEL UP";
        levelDisplayText.text = "LVL: " + damage.ToString();
        StartCoroutine(showforasecond());
    }

    public void handleLevelDown(int damage) {
        levelDisplayText.text = "LVL: " + damage.ToString();
    }

    public void ChangeTimeColor() {
        timerText.color = Color.red;
    }

    public void showBossText(string s, int time) {
        bossText.text = s;
        StartCoroutine(showbossforasec(time));
    }

    public IEnumerator showforasecond() {
        yield return new WaitForSeconds(1.5f);
        LevelUptext.text = "";
    }

    public IEnumerator showbossforasec(int time) {
        yield return new WaitForSeconds(time);
        LevelUptext.text = "";
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry() {
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

}
