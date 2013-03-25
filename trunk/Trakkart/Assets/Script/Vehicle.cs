using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {
	
	public CarStats stat ;
	
	private bool m_boosted ;
	private bool m_tempRising ;
	private float m_boost_time ;
	private float m_boostPadTime ;
	private float m_temperature ;

	// Use this for initialization
	void Start () {
		m_boosted = m_tempRising = false ;
		m_boost_time = 0f ;
		m_boostPadTime = 2f ;
		m_temperature = 0f ;
		
	}
	
	// Update is called once per frame
	void Update () {
		if( m_boosted && Time.time > m_boost_time ) {
			stat.ResetVelocity() ;
			stat.ResetAcceleration() ;
			m_boosted = false ;
		}
		
		if( !m_tempRising ){
			m_temperature -= stat.GetCooling() * Time.deltaTime ;
			
			if(m_temperature < 0f)
				m_temperature = 0f ;
			
		}
		
		if( rigidbody.velocity.sqrMagnitude > (stat.GetMaxVelocity()*stat.GetMaxVelocity()) )
			rigidbody.velocity = rigidbody.velocity.normalized * stat.GetMaxVelocity() ;
		
		stat.SetCurrentSpeed(rigidbody.velocity.magnitude);
		
		//Debug.Log (stat.GetCurrentSpeed());
		Debug.Log (m_temperature);
	}
	
	void OnTriggerEnter( Collider other ) {
		if( other.gameObject.tag == "Boost" ){
			BoostVehicle(m_boostPadTime);
		}
	}
	
	void OnTriggerStay( Collider other ){
		if( other.gameObject.tag == "Slow" && !m_boosted ){
			rigidbody.drag = 10f ;
		}
	}
	
	void OnTriggerExit( Collider other ) {
		if( other.gameObject.tag == "Boost" ){
			
		}
	}
	
	void OnCollisionStay( ){
		rigidbody.drag = 10f ;
	}
	
	public void BoostVehicle( float boostTime ) {
		stat.SetMaxVelocity(stat.GetBoostSpeed()) ;
		stat.SetAccel(stat.GetBoostSpeed()) ;	
		m_boosted = true ;
		m_boost_time = Time.time + boostTime ;
	}
	
	public void RaiseTemperaturePerSecond( float tempPerSecond ) {
		m_temperature += tempPerSecond * Time.deltaTime ;	
		m_tempRising = true ;
	}
	
	public void TurnOffTempPerSecond( ) {
		m_tempRising = false ;	
	}
}
