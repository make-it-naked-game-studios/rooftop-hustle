 using UnityEngine;
 using System.Collections;
 
 public class Pulsate : MonoBehaviour {
     
	public float x = 0;
	public float y = 0;
	public float pulse;
	public float amplitude = 2.0f;
	public float frequency = 0.5f;
	private float _frequency;
	private float phase = 0.0f;
	private Vector3 initialPosition;
 
	void Start () {
		_frequency = frequency;    
		initialPosition = transform.position;
	}
     
	void Update () {
		if (frequency != _frequency) CalcNewFreq();

		pulse = Mathf.Sin (Time.time * _frequency + phase) * amplitude;
         
		Vector3 v3 = initialPosition;
		v3.x += x * pulse;
		v3.y += y * pulse;
		transform.position = v3;
	}
     
	void CalcNewFreq() {
		float curr = (Time.time * _frequency + phase) % (2.0f * Mathf.PI);
		float next = (Time.time * frequency) % (2.0f * Mathf.PI);
		phase = curr - next;
		_frequency = frequency;
	}
}