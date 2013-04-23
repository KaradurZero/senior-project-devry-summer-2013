using UnityEngine;
using System.Collections;

public class OilSlickBehavior : MonoBehaviour {
	
	float m_lifetime;//how long this stays alive before being destroyed
	
	void Awake() {
		m_lifetime = 5.0f;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		m_lifetime -= Time.deltaTime;
		if(m_lifetime <= 0.0f) {
			destroySlick();
		}
	}
	
	void destroySlick() {
		Destroy(this.gameObject);
	}
	
	void OnTriggerEnter(Collider c) {
		if( c.tag == "Vehicle") {//if a vehicle
			Debug.Log("vehicle over/in oil slick");
//			if( c.gameObject.GetComponent</**/>()) {//if has movement script
//				//set variable/function call for oil slick
//			
//			}
		}
	}
}
