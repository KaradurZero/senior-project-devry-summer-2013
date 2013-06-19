using UnityEngine;
using System.Collections;

public class DissolveImageGUI : MonoBehaviour {
	
	//set lifetime in inspector, used for smooth disolve animation
	public float lifeTime;
	float currTime;
	
	void Start () 
	{
		transform.parent = Camera.main.transform;
		
		//this fits the image perfectly on the camera
		transform.localPosition = new Vector3(0f, -3.321338e-08f, 7.721388f);
		
		currTime = Time.time;
		Destroy(gameObject, lifeTime);
	}
	
	//Fade away smoothly over time
	void Update () 
	{
		renderer.material.color = new Color(renderer.material.color.r,renderer.material.color.g,
			renderer.material.color.b,
			((Mathf.Sin(Time.time - currTime) + 1f) / 2));
	}
}
