﻿
using UnityEngine;

[DisallowMultipleComponent]

public class Oscillator : MonoBehaviour {
    
    [SerializeField] Vector3 movementVector = new Vector3(10f,10f,10f);
    [SerializeField] float period = 2f;

    [Range(0,1)] float movementFactor;

    Vector3 startingPos;

	void Start () 
    {
        startingPos = transform.position;
	}
	

	void Update () 
    {
        if (period <= Mathf.Epsilon) { return; }

        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2f;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = (float)(rawSinWave / 2f + 0.5);

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
	}
}
