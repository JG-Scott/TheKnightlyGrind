using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration;
    public AnimationCurve curve;
    Vector3 startPos;
    public 
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShakeCamera() {
        StartCoroutine(shake());
    }

    IEnumerator shake() {

        float timeshaking = 0f;

        while(timeshaking < duration) {
            timeshaking += Time.deltaTime;
            float Strength = curve.Evaluate(timeshaking/duration);
            transform.position = startPos + Random.insideUnitSphere * Strength;
            yield return null;
        }
        transform.position = startPos;
    }
}
