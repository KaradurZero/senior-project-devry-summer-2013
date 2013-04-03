using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {
	
	public CarStats stat ;
	public gun weapon ;
	
	private bool m_boosted ;
	private float m_boost_time ;
	private float m_boostPadTime ;

	// Use this for initialization
	void Start () {
		m_boosted = false ;
		m_boost_time = 0f ;
		m_boostPadTime = 2f ;
		
	}
	
	// Update is called once per frame
	void Update () {
		//weapon.transform.position = new Vector3(transform.position.x, 0.6f, transform.position.z) ;
		
		if( m_boosted && Time.time > m_boost_time ) {
			stat.ResetVelocity() ;
			stat.ResetAcceleration() ;
			m_boosted = false ;
		}
		
		if( !stat.GetTempPerSec() ){
			stat.SetCurrTemp( stat.GetCurrTemp() - stat.GetCooling() * Time.deltaTime ) ;
			
			if(stat.GetCurrTemp() < 0f)
				stat.SetCurrTemp( 0f ) ;
		}
		else{
			if( stat.GetCurrTemp() > 100f )
				stat.SetCurrTemp( 100f ) ;
		}
		
		if( rigidbody.velocity.sqrMagnitude > (stat.GetMaxVelocity()*stat.GetMaxVelocity()) )
			rigidbody.velocity = rigidbody.velocity.normalized * stat.GetMaxVelocity() ;
		
		stat.SetCurrentSpeed(rigidbody.velocity.magnitude);
		
		//Debug.Log (stat.GetCurrentSpeed());
		//Debug.Log (stat.GetCurrTemp());
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
		stat.SetCurrTemp(stat.GetCurrTemp() + tempPerSecond * Time.deltaTime) ;
		
		if( !stat.GetTempPerSec() )
			stat.TempPerSecOn( ) ;
	}
	
	public void TurnOffTempPerSecond( ) {
		stat.TempPerSecOff( ) ;
	}
}
