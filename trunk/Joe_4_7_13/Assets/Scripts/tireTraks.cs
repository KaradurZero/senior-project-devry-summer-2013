using UnityEngine;
using System.Collections;

public class tireTraks : MonoBehaviour {
	float lifeTime, currTime;
	// Use this for initialization
	void Start () {
	lifeTime = 5;
		currTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	renderer.material.color = new Color(renderer.material.color.r,renderer.material.color.g, renderer.material.color.b, lifeTime - (Time.time - currTime));
	}
}
