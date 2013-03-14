using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {
	
	public CarStats stat ;
	
	private bool m_boosted ;
	private float m_boost_time ;

	// Use this for initialization
	void Start () {
		m_boosted = false ;
		m_boost_time = 0f ;
		
	}
	
	// Update is called once per frame
	void Update () {
		if( m_boosted && Time.time >= m_boost_time ) {
			stat.ResetVelocity() ;
			stat.ResetAcceleration() ;
			m_boosted = false ;
		}
		
		//rigidbody.velocity = transform.forward * stat.GetAccel() * Time.deltaTime ;
		
		if( rigidbody.velocity.sqrMagnitude > (stat.GetMaxVelocity()*stat.GetMaxVelocity()) )
			rigidbody.velocity = rigidbody.velocity.normalized * stat.GetMaxVelocity() ;
		//else if( rigidbody.velocity.magnitude < -stat.GetMaxVelocity() / 4 )
			//rigidbody.velocity = transform.forward * -stat.GetMaxVelocity() / 4 * Time.deltaTime ;
		
		stat.SetCurrentSpeed(rigidbody.velocity.magnitude);
		
		//Debug.Log (stat.GetCurrentSpeed());
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
