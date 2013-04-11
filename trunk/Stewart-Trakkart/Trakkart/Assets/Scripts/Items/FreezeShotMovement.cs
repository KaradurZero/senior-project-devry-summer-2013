using UnityEngine;
using System.Collections;

public class FreezeShotMovement : MonoBehaviour {
	
	float 		m_projectileSpeed;
	float 		m_direction;//the euler angle based upon the Y axis
	Vector3 	m_moveTo = Vector3.zero;//in case value does not get set properly
	float 		m_bulletLifespan;
	Vector3		m_parentMomentum = Vector3.zero;//in case value does not get set properly
	
	string		m_hTriggerSpecific_01;
	string		m_hTriggerSpecific_02;
	// Use this for initialization
	void Start () {
		m_hTriggerSpecific_01 = "ItemBox";
		m_hTriggerSpecific_02 = "Item";
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
		this.transform.rotation = Quaternion.AngleAxis( m_direction, Vector3.up);
		m_moveTo = this.transform.forward * m_projectileSpeed;
		
		this.transform.position += (m_moveTo * Time.deltaTime) + m_parentMomentum;
	}
	
	void OnTriggerEnter(Collider c) {
		//apply freeze effect to other vehicle
		//TODO: get other vehicle game object then movement script and call freeze function.
		//if( c.tag == m_hTriggerSpecific_01) {//if a vehicle
		//c.GetComponent</*scriptName*/>().freezeMovement;//sudo code
		if(c.tag != m_hTriggerSpecific_01 && c.tag != m_hTriggerSpecific_02) {
			Debug.Log("freeze bullet collided with: " + c.tag);
			destroyBullet();
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
	
	public float direction
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
}
