using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectFollow : MonoBehaviour
{
    public Rigidbody2D player;
    public Vector3 initialPosition;
    public Vector3 initialDiff;
	public float threshold;
	public float currentDelta;
	public float lag;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialDiff = initialPosition - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nextPosition = player.transform.position + initialDiff;

		currentDelta = transform.position.x - player.transform.position.x - initialDiff.x;

       	if (Math.Abs(currentDelta) > threshold) {
			lag = ((currentDelta > 0) ? threshold : -threshold);
			transform.position = new Vector3(nextPosition.x + lag, initialPosition.y, initialPosition.z);
		};
    }

    void FixedUpdate() 
    {
        //Debug.Log(calculateDeltaY(player.transform.position.y, transform.position.y));
    }

    private float calculateDeltaY(GameObject a, GameObject b) {
        return Math.Abs(a.transform.position.y - b.transform.position.y);
    }
}
