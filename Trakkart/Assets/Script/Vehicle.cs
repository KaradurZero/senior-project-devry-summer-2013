using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {
	
	public CarStats stat ;
	public gun weapon ;
	
	private bool m_boosted ;
	private float m_boost_time ;
	private float m_boostPadTime ;
	
	private float m_slipTime ;
	private float m_slipCoeff ;
	
	private bool m_frozen ;
	private float m_freezeTime ;
	private float m_freezeDuration ;

	// Use this for initialization
	void Start () {
		m_boosted = m_frozen = false ;
		m_boost_time = 0f ;
		m_boostPadTime = 2f ;
		m_slipTime = 3f ;
		m_slipCoeff = 1f ;
		m_freezeTime = 0f ;
		m_freezeDuration = 3f ;
	}
	
	// Update is called once per frame
	void Update () {
		//weapon.transform.position = new Vector3(transform.position.x, 0.6f, transform.position.z) ;
		
		if( m_boosted && Time.time > m_boost_time ) {
			stat.ResetVelocity() ;
			stat.ResetAcceleration() ;
			m_boosted = false ;
		}
		
		//Debug.Log (rigidbody.drag);
		
		if( Time.time > m_slipTime ) {
			m_slipCoeff = 1f ;
			//Debug.Log("OFF") ;	
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
		
		if( Time.time <= m_freezeTime ) {
			rigidbody.velocity = rigidbody.velocity.normalized * stat.GetCurrentSpeed() ;
		}
		else
			m_frozen = false ;
		
		stat.SetCurrentSpeed(rigidbody.velocity.magnitude);
		
		//Debug.Log (stat.GetCurrentSpeed());
		//Debug.Log (stat.GetCurrTemp());
	}
	
	void OnTriggerEnter( Collider other ) {
		if( other.gameObject.tag == "Oil Slick" ){
			m_slipCoeff = 5f ;
			m_slipTime = Time.time + 3f ;
		}
		if( other.gameObject.tag == "Freeze" ) {
			m_freezeTime = Time.time + m_freezeDuration ;
			m_frozen = true ;
		}
			
	}
	
	void OnTriggerStay( Collider other ){
		if( other.gameObject.tag == "Slow" && !m_boosted ){
			rigidbody.drag = 10f ;
		}
		if( other.gameObject.tag == "Boost" ){
			BoostVehicle(m_boostPadTime);
		}
	}
	
	void OnTriggerExit( Collider other ) {
		
	}
	
	void OnCollisionStay( ){
		//rigidbody.drag = 10f ;
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
	
	public void SetDrag( float intensity ) {
		rigidbody.drag = intensity / m_slipCoeff ;
		Debug.Log (rigidbody.drag);
	}
	
	public void AddForce( Vector3 direction, float intensity ) {
			rigidbody.AddForce(direction * intensity/m_slipCoeff) ;
	}
	
	public bool isFrozen( ) {return m_frozen ;}
}
