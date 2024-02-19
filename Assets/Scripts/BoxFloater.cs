using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class BoxFloater : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] [Range(0,1)] float movementFactor;
    float movementImpact = 2f;
    Vector3 sizeVector;

    float period;

    Vector3 startPosition;
    Vector3 startSize;

    float randomSize;


    void Start()
    {
        period = UnityEngine.Random.Range(3,7);
        randomSize = UnityEngine.Random.Range(0f, 1f);
        movementImpact = UnityEngine.Random.Range(0.5f, 2f);;
        startPosition = transform.position;
        startSize = transform.localScale;
    }
    void Update()
    {

        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinwave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSinwave + 1f)/2f;
        sizeVector = new Vector3(randomSize, randomSize, randomSize);
        Vector3 offset = transform.right * movementImpact * movementFactor;
        transform.position = startPosition + offset;
        Vector3 sizeOffset = sizeVector * movementImpact * movementFactor;
        transform.localScale = startSize + sizeOffset;

    }
}
