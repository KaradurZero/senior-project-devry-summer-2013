using UnityEngine;
using System.Collections;

public class BulletUpdate : MonoBehaviour {
	public GameObject crash;
	bool hasCollider;
	Transform	parentTrans;
	int 		bulletType;
	float 		m_projectileSpeed;
	float 		m_direction;//the euler angle based upon the Y axis
	Vector3 	m_moveTo = Vector3.zero;//in case value does not get set properly
	float 		m_bulletLifespan;
	Vector3		m_parentMomentum = Vector3.zero;//in case value does not get set properly
	float		bulletSpeed;
		
	// Use this for initialization
	void Start () {
		bulletSpeed = 2;
		hasCollider = false;
	}
	
	// Update is called once per frame
	void Update () {
		updateBulletMovement();
		if(hasCollider == false && parentTrans != null && Vector3.Distance(parentTrans.position,transform.position) > 1f)
		{
			hasCollider = true;
			Collider collide = gameObject.AddComponent<SphereCollider>();
			collide.isTrigger = true;
		}
		m_bulletLifespan -= Time.deltaTime;
		if(m_bulletLifespan < 0.0f) {
			destroyBullet();
		}
	}
	
	void updateBulletMovement() {
		//will update by moving the bullet forward by it's speed * deltaTime
		this.transform.rotation = Quaternion.AngleAxis( m_direction, Vector3.up);
		m_moveTo = this.transform.forward * m_projectileSpeed;
		
		this.transform.position += (m_moveTo * Time.deltaTime * bulletSpeed) + m_parentMomentum;
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
	public void DoNotCollideWith(Transform other)
	{
		parentTrans = other;
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.renderer.enabled)
		{
		GameObject sparks = (GameObject) Instantiate(crash, other.transform.position, Quaternion.identity);
		sparks.transform.LookAt(transform.position);
		Destroy(sparks,.15f);
		driverHealth health = other.collider.gameObject.GetComponent<driverHealth>();
			
			if(other.gameObject.tag == "Shield"){
				other.gameObject.transform.parent.parent.GetComponent<Vehicle>().RaiseTemp(false) ;
				
				if( other.gameObject.GetComponent<VehicleShieldController>().isDeflector() )
					m_direction += 180f ;
				else
					destroyBullet() ;
				//Debug.Log ("SHIELD BLOCK");
			}
			else {
					if(health != null)
					{
						health.DealDamage(10);
					}
						destroyBullet();
			}
		}
	}
	void OnCollisionEnter(Collision other)
	{	
		if(other.gameObject.renderer.enabled)
		{
		GameObject sparks = (GameObject) Instantiate(crash, other.contacts[0].point, Quaternion.identity);
		Destroy(sparks,.15f);
		destroyBullet();
			
		}
	}
	public void destroyBullet()
	{
		Destroy(this.gameObject);
	}
}
