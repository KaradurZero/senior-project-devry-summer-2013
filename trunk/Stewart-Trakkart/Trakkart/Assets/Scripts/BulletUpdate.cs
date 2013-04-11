using UnityEngine;
using System.Collections;

public class BulletUpdate : MonoBehaviour {
	
	int 		bulletType;
	//Vector3 m_direction = Vector3.zero;
	float 		m_projectileSpeed;
	float 		m_direction;//the euler angle based upon the Y axis
	Vector3 	m_moveTo = Vector3.zero;//in case value does not get set properly
	float 		m_bulletLifespan;
	Vector3		m_parentMomentum = Vector3.zero;//in case value does not get set properly
		
	// Use this for initialization
	void Start () {
	
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
