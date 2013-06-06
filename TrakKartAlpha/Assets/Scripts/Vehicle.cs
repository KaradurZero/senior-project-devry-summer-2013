using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {
	
	public CarStat stat ;
	public vehicleItems myPowerup;
	
	private bool m_boosted ;
	private float m_boost_time ;
	private float m_boostPadTime = 2f ;
	
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
		
		rigidbody.detectCollisions = false ;
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
		rigidbody.detectCollisions = true ;
		
		Renderer[] rend = GetComponentsInChildren<Renderer>();
		foreach(Renderer r in rend)
		{
			if(r.gameObject.name == "checkpointNav")
				r.enabled = true;
		}
		isAlive = true;
		stat.SetCurrTemp(0) ;
		gameObject.GetComponentInChildren<GunShieldRotation>().TurnOnGun() ;
	}
	public bool amAlive()
	{
		return isAlive;
	}
	// Update is called once per frame
	void Update () {
		if(isAlive)
		{
			//QUICK FIX: There is a bug where vehicle respawns under world. This should keep it from sinking.
			if( transform.position.y < 1f || transform.position.y > 1f )
				transform.position = new Vector3( transform.position.x, 1f, transform.position.z ) ;
				
			if( m_boosted && Time.time >= m_boost_time ) {
				stat.ResetVelocity() ;
				stat.ResetAcceleration() ;
				m_boosted = false ;
			}
			
			if( m_frozen && m_freezeTime > 0 )
				m_freezeTime -= Time.deltaTime ;
			else
				m_frozen = false ;
			
			if( m_slipTime > 0 )
				m_slipTime -= Time.deltaTime ;
			else
				m_slipCoeff = 1f ;
			
			//if(!m_boosted && m_slowed) {
			//	SetDrag(m_slowDrag) ;
			//}
			
			if(Vector3.Angle(lastFrameAngle, transform.forward) > 1f)
			{
				Object traks = Instantiate(tireTraksPrefab,transform.position + new Vector3(0f,-.5f,0f),transform.rotation);
				Destroy (traks, 5);
			}
				
			lastFrameAngle = transform.forward;
			//Debug.Log (Vector3.Angle(lastFrameAngle, transform.forward));
			
			//rigidbody.velocity = transform.forward * stat.GetAccel() * Time.deltaTime ;
			
			if( (!stat.GetTempPerSec() && this.gameObject.GetComponentInChildren<GunShieldRotation>().isGunEnabled()) || stat.isOverheated() ){
			stat.SetCurrTemp( stat.GetCurrTemp() - stat.GetCooling() * Time.deltaTime ) ;
			}
			
			if( rigidbody.velocity.sqrMagnitude > (stat.GetMaxVelocity()*stat.GetMaxVelocity()) )
				rigidbody.velocity = rigidbody.velocity.normalized * stat.GetMaxVelocity() ;
			//else if( rigidbody.velocity.magnitude < -stat.GetMaxVelocity() / 4 )
				//rigidbody.velocity = transform.forward * -stat.GetMaxVelocity() / 4 * Time.deltaTime ;
			
			if( rigidbody.velocity.magnitude >= 0 )
				stat.SetCurrentSpeed(rigidbody.velocity.magnitude);
			else
				stat.SetCurrentSpeed(0);
			
			if(transform.position.y > 1f)
				transform.position = new Vector3( transform.position.x, 0.5f, transform.position.z ) ;
			
			//Debug.Log (stat.GetCurrentSpeed());
		}
			
	
	}
	
	void OnTriggerEnter( Collider other ) {
		if( other.gameObject.tag == "Slow" ){
			SetDrag(m_slowDrag) ;
			m_slowed = true ;
			Debug.Log ("Slow");
		}
	}
	
	void OnTriggerStay( Collider other ) {
		if( other.gameObject.tag == "Slow" ){
			SetDrag(m_slowDrag) ;
			m_slowed = true ;
			Debug.Log ("Slow");
		}
		if( other.gameObject.tag == "Boost" ){
			BoostVehicle(m_boostPadTime);
		}
	}
	
	void OnTriggerExit( Collider other ) {
		if( other.gameObject.tag == "Slow" ){
			m_slowed = false ;
		}	
	}
	
	public void BoostVehicle( float boostTime ) {
		stat.SetMaxVelocity(stat.GetBoostSpeed()) ;
		stat.SetAccel(stat.GetBoostSpeed()) ;	
		RaiseTemperaturePerSecond( 10f ) ;
		m_boosted = true ;
		m_boost_time = Time.time + boostTime ;
	}
	
	public void RaiseTemperaturePerSecond( float tempPerSecond ) {
		if( !stat.GetTempPerSec() )
			stat.TempPerSecOn( ) ;
		
		stat.SetCurrTemp(stat.GetCurrTemp() + tempPerSecond * Time.deltaTime) ;
	}
	
	public void RaiseTemp( bool a_shooting ){
		if( a_shooting )
			stat.SetCurrTemp ( stat.GetCurrTemp() + stat.FindAttackTempCost() ) ;
		else
			stat.SetCurrTemp ( stat.GetCurrTemp() + stat.FindDefenseTempCost() ) ;
	}
	
	public void TurnOffTempPerSecond( ) {
		stat.TempPerSecOff( ) ;
	}
	
	public void SetDrag( float intensity ) {
		if( (m_slowed && intensity == m_slowDrag) || (!m_slowed && intensity <= m_slowDrag) || m_boosted)
			rigidbody.drag = intensity / m_slipCoeff ;
	}
	
	public void AddForce( Vector3 direction, float intensity ) {
			rigidbody.AddForce(direction * intensity/m_slipCoeff) ;
	}
	
	public bool isFrozen( ) {return m_frozen ;}
	public bool isBoosted( ) {return m_boosted ;}
	
	public void FirePowerUp()
	{
		myPowerup.UseItem();
	}
	
	public void Slick() {
		m_slipCoeff = 3f ;
		m_slipTime = 3f ;
	}
	
	public void Freeze() {
		m_freezeTime = m_freezeDuration ;
		m_frozen = true ;
	}
}
