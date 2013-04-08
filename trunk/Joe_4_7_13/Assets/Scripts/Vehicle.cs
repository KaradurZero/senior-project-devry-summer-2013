using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {
	
	public CarStat stat ;
	
	private bool m_boosted ;
	private float m_boost_time ;
	public Object tireTraksPrefab;
	//turn effect
	Vector3 lastFrameAngle;
	bool isAlive;
	
	// Use this for initialization
	void Start () {
		isAlive = true;
		lastFrameAngle = transform.forward;
		m_boosted = false ;
		m_boost_time = 0f ;
		
	}
		
		
	public void Die()
	{
		renderer.enabled = false;
		Renderer[] rend = GetComponentsInChildren<Renderer>();
		foreach(Renderer r in rend)
		{
			r.enabled = false;
		}
		isAlive = false;
	}
	public void Revive()
	{
		renderer.enabled = true;
		Renderer[] rend = GetComponentsInChildren<Renderer>();
		foreach(Renderer r in rend)
		{
			r.enabled = true;
		}
		isAlive = true;
	}
	public bool amAlive()
	{
		return isAlive;
	}
	// Update is called once per frame
	void Update () {
		if(isAlive)
		{
			if( m_boosted && Time.time >= m_boost_time ) {
				stat.ResetVelocity() ;
				stat.ResetAcceleration() ;
				m_boosted = false ;
			}
			
			if(Vector3.Angle(lastFrameAngle, transform.forward) > 1f)
			{
				Object traks = Instantiate(tireTraksPrefab,transform.position + new Vector3(0f,-.5f,0f),transform.rotation);
				Destroy (traks, 5);
			}
				
			lastFrameAngle = transform.forward;
			
			//rigidbody.velocity = transform.forward * stat.GetAccel() * Time.deltaTime ;
			
			if( rigidbody.velocity.sqrMagnitude > (stat.GetMaxVelocity()*stat.GetMaxVelocity()) )
				rigidbody.velocity = rigidbody.velocity.normalized * stat.GetMaxVelocity() ;
			//else if( rigidbody.velocity.magnitude < -stat.GetMaxVelocity() / 4 )
				//rigidbody.velocity = transform.forward * -stat.GetMaxVelocity() / 4 * Time.deltaTime ;
			
			stat.SetCurrentSpeed(rigidbody.velocity.magnitude);
		}
	}
	
	void OnTriggerEnter( Collider other ) {
		if( other.gameObject.tag == "Slow" && !m_boosted ){
			stat.SetCurrentSpeed(stat.GetCurrentSpeed()/2f) ;
			stat.SetMaxVelocity(stat.GetMaxVelocity()/2f) ;
			stat.SetAccel(stat.GetAccel()/2f) ;
		}
		if( other.gameObject.tag == "Boost" ){
			stat.SetMaxVelocity(stat.GetBoostSpeed()) ;
			stat.SetAccel(stat.GetBoostSpeed()) ;
		}
	}
	
	void OnTriggerExit( Collider other ) {
		if( other.gameObject.tag == "Slow" && !m_boosted ){
			stat.ResetVelocity() ;
			stat.ResetAcceleration() ;
		}
		if( other.gameObject.tag == "Boost" ){
			m_boost_time = Time.time + 2f ;
			m_boosted = true ;
		}
	}
	
	void OnCollisionStay( ){
		rigidbody.drag = 10f ;
	}
}
