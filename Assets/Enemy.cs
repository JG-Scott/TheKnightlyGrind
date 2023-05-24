using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType {NORMAL, BOSS}

    public List<PlayerScript.Action> AttackPattern;
    public int health = 1;

    public string attackname;

    public string blockname;

    public EnemyType Enemytype;

    public GameObject[] hitSprites;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void updateHitSprites(int currentAttackIndex) {
        for(int i = 0; i < hitSprites.Length; i++) {
            hitSprites[i].GetComponent<SpriteRenderer>().enabled = false;
        }

        for(int i = hitSprites.Length-1; i >=currentAttackIndex; i--) {
            hitSprites[i].GetComponent<SpriteRenderer>().enabled = true;
        }
    } 

    public void handleAttack(int currentAttackIndex) {
        if(AttackPattern[currentAttackIndex] == PlayerScript.Action.ATTACK) {
            GetComponentInChildren<Animator>().Play(blockname);
        }

        if(AttackPattern[currentAttackIndex] == PlayerScript.Action.DEFEND) {
            GetComponentInChildren<Animator>().PlayInFixedTime(attackname);
        }
    }

    public bool doesDieFromDamage(int damage, int currentAttackIndex) {
        if(Enemytype == EnemyType.NORMAL) {
            if (currentAttackIndex < AttackPattern.Count) {
                return false;
            }
            health -= damage;
            if(health <= 0) {
                return true;

            }
            return false;
        }

        else {
            health-=damage;
            if(health <= 0) {
                return true;

            }
            return false;
        }
    }

    public void die() {
        Destroy(gameObject);
    }
}
