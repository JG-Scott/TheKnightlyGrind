using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public GameObject blast;
    public GameObject regularhead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playBlast() {
        blast.SetActive(true);
        blast.GetComponent<Animator>().PlayInFixedTime("blast");
        StartCoroutine(showforasecond());
        regularhead.GetComponent<SpriteRenderer>().enabled = false;
    }
    public IEnumerator showforasecond() {
        yield return new WaitForSeconds(1.5f);
        GameManager.gm.HandleGameEnd();
    }

}
