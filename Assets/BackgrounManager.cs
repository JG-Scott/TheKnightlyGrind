using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgrounManager : MonoBehaviour
{

    public GameObject background1A;
    public GameObject background1B;

    public Transform Reset1;
    public GameObject background2A;
    public GameObject background2B;
    public Transform Reset2;
    public GameObject background3A;
    public GameObject background3B;
    public Transform Reset3;
    public GameObject background4A;
    public GameObject background4B;
    public Transform Reset4;

    public void moveBackground() {
        background1A.transform.position += Vector3.left;
        background1B.transform.position += Vector3.left;

        if(background1A.transform.position.x < -25) {
            background1A.transform.position = Reset1.position;
        }
        if(background1B.transform.position.x < -25) {
            background1B.transform.position = Reset1.position;
        }
        background2A.transform.position += Vector3.left*0.5f;
        background2B.transform.position += Vector3.left*0.5f;
        
        if(background2A.transform.position.x < -25) {
            background2A.transform.position = Reset2.position;
        }
        if(background2B.transform.position.x < -25) {
            background2B.transform.position = Reset2.position;
        }
        background3A.transform.position += Vector3.left*0.25f;
        background3B.transform.position += Vector3.left*0.25f;
        if(background3A.transform.position.x < -25) {
            background3A.transform.position = Reset3.position;
        }
        if(background3B.transform.position.x < -25) {
            background3B.transform.position = Reset3.position;
        }
        background4A.transform.position += Vector3.left*0.1f;
        background4B.transform.position += Vector3.left*0.1f;

        if(background4A.transform.position.x < -25) {
            background4A.transform.position = Reset4.position;
        }
        if(background4B.transform.position.x < -25) {
            background4B.transform.position = Reset4.position;
        }
        
    }
}
