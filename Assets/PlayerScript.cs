using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public enum Action {ATTACK, DEFEND};


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) {
            Debug.Log("this is running");
            GetComponent<Animator>().PlayInFixedTime("swing");
            if(!EnemyManager.em.HandleAction(Action.ATTACK)) {
               
            }
        }

        if(Input.GetKeyDown(KeyCode.D)) {
            GetComponent<Animator>().PlayInFixedTime("block");
            if(!EnemyManager.em.HandleAction(Action.DEFEND)) {
               
            }
        }
    }

}
