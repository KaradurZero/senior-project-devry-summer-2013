using UnityEngine;
using System.Collections;

public class FreezeShotMovement : MonoBehaviour {
	
	float 		m_projectileSpeed;
	Quaternion 	m_direction;//the euler angle based upon the Y axis
	Vector3 	m_moveTo = Vector3.zero;//in case value does not get set properly
	float 		m_bulletLifespan;
	float		m_bulletSpeed = 2f ;
	Vector3		m_parentMomentum = Vector3.zero;//in case value does not get set properly
	
	string		m_hTriggerSpecific_01;
	string		m_hTriggerSpecific_02;
	// Use this for initialization
	void Start () {
		m_hTriggerSpecific_01 = "Vehicle";
		m_hTriggerSpecific_02 = "Wall";
		m_bulletLifespan = 0.5f ;
	}
	
	// Update is called once per frame
	void Update () {
		updateBulletMovement();
		
		m_bulletLifespan -= Time.deltaTime;
		if(m_bulletLifespan < 0.0f) {
			destroyBullet();
		}
	}
	
	void updateBulletMovement() {
		//will update by moving the bullet forward by it's speed * deltaTime
		this.transform.rotation = m_direction ; //Quaternion.AngleAxis( m_direction, Vector3.up);
		m_moveTo = this.transform.forward * m_projectileSpeed;
		
		this.transform.position += (m_moveTo * Time.deltaTime * m_bulletSpeed) + m_parentMomentum;
	}
	
	void OnTriggerEnter(Collider c) {
		//apply freeze effect to other vehicle
		//TODO: get other vehicle game object then movement script and call freeze function.
		//if( c.tag == m_hTriggerSpecific_01) {//if a vehicle
		//c.GetComponent</*scriptName*/>().freezeMovement;//sudo code
		if(c.gameObject.tag == "Vehicle" /*c.gameObject.tag != m_hTriggerSpecific_02*/) {
			c.gameObject.GetComponent<Vehicle>().Freeze() ;
		}
	}
	
	public float speed
	{
		get {
			return m_projectileSpeed;
		}
		set {
			this.m_projectileSpeed = value;
		}
	}
	
	public Quaternion direction
	{
		get {
			return m_direction;
		}
		set {
			this.m_direction = value;
		}
	}
	
	public float lifespan
	{
		get {
			return m_bulletLifespan;
		}
		set {
			m_bulletLifespan = value;
		}
	}
	
	public Vector3 parentMomentum
	{
		get {
			return m_parentMomentum;
		}
		set {
			m_parentMomentum = value;
		}
	}
	
	public void destroyBullet()
	{
		Destroy(this.gameObject);
	}
	
	public void setIgnoreTarget(GameObject a_launcher) {
		Physics.IgnoreCollision(this.collider, a_launcher.collider) ;
	}
}