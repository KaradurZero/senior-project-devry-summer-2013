using UnityEngine;
using System.Collections;

public class OilSlickBehavior : MonoBehaviour {
	
	float m_lifetime;//how long this stays alive before being destroyed
	GameObject m_ignoreTarget ;
	
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
		if( c.tag == "Vehicle" ) {//if a vehicle
			c.gameObject.GetComponent<Vehicle>().Slick () ;
//			if( c.gameObject.GetComponent</**/>()) {//if has movement script
//				//set variable/function call for oil slick
//			
//			}
		}
	}
	
	public void setIgnoreTarget(GameObject a_launcher) { 
		m_ignoreTarget = a_launcher;
		Physics.IgnoreCollision(this.collider, a_launcher.collider) ;
	}
}
