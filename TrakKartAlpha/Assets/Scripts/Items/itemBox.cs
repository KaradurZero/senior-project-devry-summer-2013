using UnityEngine;
using System.Collections;

public class itemBox : MonoBehaviour {
	
	public GameObject crash;
	bool m_isTangible;//affects rendering
	float m_respawnMin, m_respawnMax, m_respawnSet;//min, max, and a set amount of time before item respawns from pickup
	float m_respawnTimer;
	string m_hTriggerSpecific_01;
		
	// Use this for initialization
	void Start () {
		isTangible 			= true;
		m_respawnTimer 		= 0;
		m_respawnMin 		= 3.0f;
		m_respawnMax		= 8.0f;
		m_respawnSet		= 5.0f;
		m_hTriggerSpecific_01 = "Vehicle";
	}
	
	// Update is called once per frame
	void Update () {
		if(m_isTangible) {
			transform.RotateAround(Vector3.up,.1f);
			//do something here? may leave to collision box update
		}
		else {
			if( m_respawnTimer > 0) {
				m_respawnTimer -= Time.deltaTime;
			}
			if( m_respawnTimer <= 0) {
				m_isTangible 		= true;
				m_respawnTimer 		= 0.0f;
				this.renderer.enabled = m_isTangible;
			}
		}
	}
	
	public float respawnMin
	{
		get {
			return m_respawnMin;
		}
		set {
			m_respawnMin = value;
		}
	}
	
	public float respawnMax
	{
		get {
			return m_respawnMax;
		}
		set {
			m_respawnMax = value;
		}
	}
	
	/// <summary>
	/// get or set the respawn after set time value
	/// </summary>
	public float respawnSetTime
	{
		get {
			return m_respawnSet;
		}
		set {
			m_respawnSet = value;
		}
	}
	
	public float respawnTimer
	{
		get {
			return m_respawnTimer;
		}
		set {
			m_respawnTimer = value;
		}
	}
	
	public bool isTangible
	{
		get {
			return m_isTangible;
		}
		set {
			m_isTangible = value;
		}
	}
	
	void OnTriggerEnter(Collider c) {
		if(m_isTangible) {
			if( c.tag == m_hTriggerSpecific_01) {//if a vehicle
				if( c.gameObject.GetComponent<vehicleItems>()) {//if has script
					if( c.gameObject.GetComponent<vehicleItems>().m_item == 0) {//if has no item
						c.gameObject.GetComponent<vehicleItems>().m_item = Random.Range(1,7);//Random.Range( 1, 7 );//give item
					if(GetComponent<AIState>() != null)
						GetComponent<AIState>().RecievePowerUp();
					}
					//c.gameObject.GetComponent<vehicleItems>().item = 3;//debugging
					itemPickedUp();
					
					GameObject sparks = (GameObject) Instantiate(crash, c.transform.position, Quaternion.identity);
					sparks.transform.LookAt(transform.position + new Vector3(0f,1f,0f));
					Destroy(sparks,2f);
					sparks.GetComponent<BreakBox>().isPlayer = false;
					if(c.name == "Player")
					{
						sparks.GetComponent<BreakBox>().isPlayer = true;
					}
				}
			}
		}
	}
	
	void itemPickedUp () {
		m_isTangible = false;
		this.renderer.enabled = m_isTangible;
		m_respawnTimer = m_respawnSet;
		//debug testing
		//Debug.Log( "Item picked up at: " + this.transform.position.ToString());
	}
}
