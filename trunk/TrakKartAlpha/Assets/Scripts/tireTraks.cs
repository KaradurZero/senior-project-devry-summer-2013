using UnityEngine;
using System.Collections;

public class tireTraks : MonoBehaviour {
	float lifeTime, currTime;
	
	void Start () {
		lifeTime = 5;
		currTime = Time.time;
	}
	
	void Update () {
	renderer.material.color = new Color(renderer.material.color.r,renderer.material.color.g, renderer.material.color.b, lifeTime - (Time.time - currTime));
	}
}
