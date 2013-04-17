using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {
	
	public CarStat stat ;
	public vehicleItems myPowerup;
	
	private bool m_boosted ;
	private float m_boost_time ;
	private float m_boostPadTime = 1.5f ;
	
	public Object tireTraksPrefab;
	//turn effect
	Vector3 lastFrameAngle;
	bool isAlive;
	
	private float m_slipTime ;
	private float m_slipCoeff ;
	
	private bool m_frozen ;
	private float m_freezeTime ;
	private float m_freezeDuration = 3f;
	
	private bool m_slowed ;
	private float m_slowDrag = 2f;
	
	// Use this for initialization
	void Start () {
		isAlive = true;
		lastFrameAngle = transform.forward;
		m_boosted = m_frozen = m_slowed = false ;
		m_boost_time = 0f ;
		m_slipTime = 0f ;
		m_slipCoeff = 1f ;
		m_freezeTime = 0f ;
		
		myPowerup = transform.GetComponent<vehicleItems>();
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
			
			if( m_frozen && m_freezeTime > 0 )
				m_freezeTime -= Time.deltaTime ;
			else
				m_frozen = false ;
			
			if(Vector3.Angle(lastFrameAngle, transform.forward) > 1f)
			{
				Object traks = Instantiate(tireTraksPrefab,transform.position + new Vector3(0f,-(transform.position.y),0f),transform.rotation);
				Destroy (traks, 5);
			}
				
			lastFrameAngle = transform.forward;
			//Debug.Log (Vector3.Angle(lastFrameAngle, transform.forward));
			
			//rigidbody.velocity = transform.forward * stat.GetAccel() * Time.deltaTime ;
			
			if( !stat.GetTempPerSec() || stat.isOverheated() ){
			stat.SetCurrTemp( stat.GetCurrTemp() - stat.GetCooling() * Time.deltaTime ) ;
			}
			
			if( rigidbody.velocity.sqrMagnitude > (stat.GetMaxVelocity()*stat.GetMaxVelocity()) )
				rigidbody.velocity = rigidbody.velocity.normalized * stat.GetMaxVelocity() ;
			//else if( rigidbody.velocity.magnitude < -stat.GetMaxVelocity() / 4 )
				//rigidbody.velocity = transform.forward * -stat.GetMaxVelocity() / 4 * Time.deltaTime ;
			
			stat.SetCurrentSpeed(rigidbody.velocity.magnitude);
			
			if(transform.position.y > 1f)
				transform.position = new Vector3( transform.position.x, 0.5f, transform.position.z ) ;
			
			//Debug.Log (stat.GetCurrentSpeed());
		}
		//else
			//Debug.Log ( "DEAD" );
	}
	
	/*void OnTriggerEnter( Collider other ) {
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
	}*/
	
	void OnCollisionEnter( Collision other ) {
		if( other.gameObject.tag == "Oil Slick" ){
			m_slipCoeff = 2f ;
			m_slipTime = Time.time + 3f ;
			Debug.Log ("Slick");
		}
		if( other.gameObject.tag == "Freeze" ) {
			m_freezeTime = m_freezeDuration ;
			m_frozen = true ;
			//Destroy (other.gameObject);
			Debug.Log ("freeze");
		}
		if( other.gameObject.tag == "Slow" ){
			SetDrag(m_slowDrag) ;
			m_slowed = true ;
			//Debug.Log ("Slow");
		}
			
	}
	
	void OnCollisionStay( Collision other ){
		if( other.gameObject.tag == "Slow" && !m_boosted ){
			SetDrag(m_slowDrag) ;
			m_slowed = true ;
		}
		if( other.gameObject.tag == "Boost" ){
			BoostVehicle(m_boostPadTime);
		}
	}
	
	void OnCollisionExit( Collision other ) {
		if( other.gameObject.tag == "Slow" ){
			m_slowed = false ;
		}
		
	}
	
	public void BoostVehicle( float boostTime ) {
		stat.SetMaxVelocity(stat.GetBoostSpeed()) ;
		stat.SetAccel(stat.GetBoostSpeed()) ;	
		m_boosted = true ;
		m_boost_time = Time.time + boostTime ;
	}
	
	public void RaiseTemperaturePerSecond( float tempPerSecond ) {
		if( !stat.GetTempPerSec() )
			stat.TempPerSecOn( ) ;
		
		stat.SetCurrTemp(stat.GetCurrTemp() + tempPerSecond * Time.deltaTime) ;
	}
	
	public void RaiseTemp( float a_amount ){
		stat.SetCurrTemp ( stat.GetCurrTemp() + a_amount ) ;
	}
	
	public void TurnOffTempPerSecond( ) {
		stat.TempPerSecOff( ) ;
	}
	
	public void SetDrag( float intensity ) {
		if( (m_slowed && intensity == m_slowDrag) || (!m_slowed && intensity <= m_slowDrag) || m_boosted)
			rigidbody.drag = intensity / m_slipCoeff ;
		//Debug.Log (rigidbody.drag);
	}
	
	public void AddForce( Vector3 direction, float intensity ) {
			rigidbody.AddForce(direction * intensity/m_slipCoeff) ;
	}
	
	public bool isFrozen( ) {return m_frozen ;}
	
	public void FirePowerUp()
	{
		myPowerup.UseItem();
	}
}
