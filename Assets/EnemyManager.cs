using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public BackgrounManager bm;
    public List<GameObject> Enemies;
    public bool LastThingDied = false;

    public GameObject currentEnemy;
    public Slider slider;
     public Slider bossHealthSlider;
    public int currentAttackIndex = 0;

    public int damage = 1;
    public int xp;

    public int xpToLevelUp = 50;

    public GameObject[] EnemyPrefabs;

    public Transform[] enemyLocations;

    public GameObject ground1;

    public GameObject ground2;

    public Transform endDistance;

    public Transform beginDistance;

    public Transform OffscreenPos;

    public CameraShake cam;


    public AudioSource[] hit; 
    public AudioSource block;
    public AudioSource death; 
    public AudioSource levelup;

    public AudioSource explosion ;

    public GameObject bloodsplatter;

    public GameObject Boss;

    public GameObject BigSplatter;

    public Animator BossAnimator;

    public AudioSource BossHitSound;



    public static EnemyManager em;
    // Start is called before the first frame update
    void Start()
    {
        if(em == null) {
            em = this;
        }

        for(int i = 0; i < 5; i++) {
            AddNewEnemy();
        }

        currentEnemy = Enemies[0];
        currentEnemy.GetComponent<Enemy>().updateHitSprites(currentAttackIndex);
        SetEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HandleAction(PlayerScript.Action action) {
        if(currentEnemy.GetComponent<Enemy>().Enemytype == Enemy.EnemyType.BOSS) {
        cam.ShakeCamera();
        Debug.Log("this is playing");
        if(action == PlayerScript.Action.DEFEND) {
               Debug.Log("defend");
               block.Play();
            return true;
        }
            if(!BossHitSound.isPlaying) {
                BossHitSound.Play();
                Instantiate(bloodsplatter);
            }
            bossHealthSlider.value = bossHealthSlider.value - damage;
                playHit();
            BossAnimator.PlayInFixedTime("hit");
        if(Boss.GetComponent<Enemy>().doesDieFromDamage(damage, currentAttackIndex)) {
            cam.ShakeCamera();
            Instantiate(BigSplatter);
            explosion.Play();
            Destroy(currentEnemy);
            StartCoroutine(loadGameWin());
            return true;
        }
        return true;
        } else {

        
        Debug.Log("PlayerActionRunning");
        if(LastThingDied) {
            LastThingDied = false;
        }
        currentEnemy.GetComponent<Enemy>().handleAttack(currentAttackIndex);


        if(action != currentEnemy.GetComponent<Enemy>().AttackPattern[currentAttackIndex]) {
            Debug.Log("wrong");
            LoseXP();
            death.Play();
            return false;
        }

        currentAttackIndex++;
        currentEnemy.GetComponent<Enemy>().updateHitSprites(currentAttackIndex);
        if(action == PlayerScript.Action.DEFEND) {
               Debug.Log("defend");
               block.Play();
            return true;
        }

        playHit();

        if(currentEnemy.GetComponent<Enemy>().doesDieFromDamage(damage, currentAttackIndex)) {
            cam.ShakeCamera();
            Instantiate(bloodsplatter);
            explosion.Play();
               Debug.Log("attack");
            GameObject Temp = Enemies[0];
            Enemies.RemoveAt(0);
            Destroy(Temp);
            currentEnemy = Enemies[0];
            AddNewEnemy();
            HandleXP();
            currentAttackIndex=0;
            LastThingDied = true;
            moveGround();
        }
        SetEnemies();
        return true;
        }
    }

    public void AddNewEnemy(){

       GameObject temp = Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)]);
       temp.transform.position = new Vector3(100,100,0);

       Enemies.Add(temp);
    }

    public void HandleXP() {
        xp += 10;
        slider.value = slider.value+0.1f;
        if(xp>=xpToLevelUp) {
            xpToLevelUp+=50;
            levelup.Play();
            slider.value = 0;
            damage+=1;
            CanvasManager.cm.showLevelUp(damage);
        }
    }

    public void LoseXP() {
        xp-=15;
        if(xp <=0) {
            xp=0;
        }
        slider.value = slider.value-0.2f; 
        if(xp < xpToLevelUp-50) {
            xpToLevelUp-=50;
            slider.value = 1;
            damage-=1;
            CanvasManager.cm.handleLevelDown(damage);
        }
    }

    public void SetEnemies() {
        currentEnemy.GetComponent<Enemy>().updateHitSprites(currentAttackIndex);
        for(int i = 0; i < Enemies.Count; i++) {
            Enemies[i].transform.position = enemyLocations[i].position;
        }
    }

    public void moveGround() {
        bm.moveBackground();
        ground1.transform.position += Vector3.left * 0.5f;
        ground2.transform.position += Vector3.left * 0.5f;

        if(ground1.transform.position.x <= endDistance.position.x) {
            ground1.transform.position = beginDistance.position;
        }
        if(ground2.transform.position.x <= endDistance.position.x) {
            ground2.transform.position = beginDistance.position;
        }
    }

    public void playHit() {
        hit[Random.Range(0, hit.Length)].Play();
    }


  public void handleBoss() {
    foreach(GameObject e in Enemies) {
        e.SetActive(false);
    }
    foreach(Transform t in enemyLocations) {
        Instantiate(bloodsplatter, t.position, Quaternion.identity);
    }
    bossHealthSlider.gameObject.SetActive(true);
    explosion.Play();
    Boss.SetActive(true);
    Boss.GetComponentInChildren<Animator>().Play("DropIn");
    currentEnemy = Boss;
    CanvasManager.cm.showBossText("Attack Him", 2);
  }
    public IEnumerator loadGameWin() {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("WinScreen");
    }
}
