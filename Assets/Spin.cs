 using UnityEngine;
 using System.Collections;
 
 public class Spin : MonoBehaviour {
     
	public float x = 0;
	public float y = 0;
	public float z = 0;
	public float spin;
	public float amplitude = 1.0f;
	public Vector3 nextRotation;

     
	void Update () {
		spin = (Time.time * amplitude * 0.01f) % 360;

		// Debug.Log(initialRotation.x + " + " + x * spin + " = " + initialRotation.x + x * spin);

		nextRotation.x = x * spin;// * Mathf.PerlinNoise(Time.time * x * spin, 0.0f);
		nextRotation.y = y * spin;// * Mathf.PerlinNoise(Time.time * y * spin, 0.0f);
		nextRotation.z = z * spin;// * Mathf.PerlinNoise(Time.time * z * spin, 0.0f);

		transform.Rotate(nextRotation, Space.Self);
	}
}