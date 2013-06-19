using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {
	
	public AudioClip hitWall, hitCar;
	public AudioClip boost;
	
	public CarStat stat ;
	public vehicleItems myPowerup;
	public GameObject FreezeGUI;
	public GameObject IceBlock;
	
	private bool m_boosted ;
	private float m_boost_time ;
	private float m_boostPadTime = 2f ;
	private float m_spinAngle = 0f ;
	
	public Object tireTraksPrefab;
	
	//turn effect
	Vector3 lastFrameAngle;
	bool isAlive;
	
	private float m_slipTime ;
	private float m_slipCoeff ;
	
	private bool m_frozen ;
	private float m_freezeTime ;
	private float m_freezeDuration = 3f;
	
	private bool m_invulnerable ;
	private float m_invunerableTime ;
	private float m_invulnerableDuration = 2f ;
	
	private bool m_slowed ;
	private float m_slowDrag = 5f;
	
	Color transparency ;
	
	// Use this for initialization
	void Start () {
		isAlive = true;
		lastFrameAngle = transform.forward;
		m_boosted = m_frozen = m_slowed = false ;
		m_boost_time = 0f ;
		m_slipTime = 0f ;
		m_slipCoeff = 1f ;
		m_freezeTime = 0f ;
		m_invulnerable = false ;
		m_invunerableTime = m_invulnerableDuration ;
		
		myPowerup = transform.GetComponent<vehicleItems>();
		
		transparency = renderer.material.color ;
	}
		
	//vehicle dies	
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
	
	//vehicle is respawning
	public void Revive()
	{
		renderer.enabled = true;
		rigidbody.detectCollisions = true ;
		m_invulnerable = true ;
		
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
	
	// Update is called once per frame
	void Update () {
		if(isAlive)
		{
			Debug.Log (transparency.b);
			//QUICK FIX: There is a bug where vehicle respawns under world. This should keep it from sinking.
			if( transform.position.y < 1f || transform.position.y > 1f )
				transform.position = new Vector3( transform.position.x, 1f, transform.position.z ) ;
				
			if( m_boosted && Time.time >= m_boost_time ) {
				stat.ResetVelocity() ;
				stat.ResetAcceleration() ;
				m_boosted = false ;
			}
			
			//when invulnerable, collider of vehicle is turned off, unable to react to any triggers
			if( m_invulnerable ) {
				transparency.a = 0.5f ;
				this.gameObject.collider.enabled = false ;
				if( m_invunerableTime > 0 )
					m_invunerableTime -= Time.deltaTime ;
				else {
					m_invulnerable = false ;
					m_invunerableTime = m_invulnerableDuration ;
					this.gameObject.collider.enabled = true ;
					transparency.a = 1f ;
				}
				
				renderer.material.color = transparency ;
			}
			
			//throttle code for when vehicle is frozen
			if( m_frozen && m_freezeTime > 0 ) {
				m_freezeTime -= Time.deltaTime ;
				
				transparency.b = 255f ;
				renderer.material.color = transparency ;
				
			}
			else {
				m_frozen = false ;
				transparency.b = 1f ;
				renderer.material.color = transparency ;
			}
			
			//throttle code for when vehicle is slipping on oil
			if( m_slipTime > 0 ) {
				m_slipTime -= Time.deltaTime ;
				
				//vehicle will spin 360 degrees before regaining control
				if( m_spinAngle < 360f ) {
					transform.RotateAround(Vector3.up, stat.GetHandling() * Time.deltaTime);
					m_spinAngle += (stat.GetHandling()) ;
					
					//player controls and AI controls are locked while spinning
					if( this.gameObject.name == "Player" ) {
						if( this.GetComponent<KeyboardMouseController>() != null )
							this.GetComponent<KeyboardMouseController>().enabled = false ;
						else if( this.GetComponent<GamepadController>() != null )
							this.GetComponent<GamepadController>().enabled = false ;
						else if( this.GetComponent<Xbox360Controller>() != null )
							this.GetComponent<Xbox360Controller>().enabled = false ;
						else
							this.GetComponent<AIDriver>().enabled = false ;
					}
					else {
						this.GetComponent<AIDriver>().enabled = false ;
					}
				}
				else {
					//player and AI regain control
					if( this.gameObject.name == "Player" ) {
						if( this.GetComponent<KeyboardMouseController>() != null )
							this.GetComponent<KeyboardMouseController>().enabled = true ;
						else if( this.GetComponent<GamepadController>() != null ) {
							this.GetComponent<GamepadController>().enabled = true ;
						}
						else if( this.GetComponent<Xbox360Controller>() != null ) {
							this.GetComponent<Xbox360Controller>().enabled = true ;
						}
						else
							this.GetComponent<AIDriver>().enabled = true ;
					}	
					else {
						this.GetComponent<AIDriver>().enabled = true ;
					}
				}
			}
			else {
				//slip coefficient is reset
				m_slipCoeff = 1f ;
				m_spinAngle = 0f ;
				
				}
			
			//if(!m_boosted && m_slowed) {
			//	SetDrag(m_slowDrag) ;
			//}
			
			//tire traks will be created when vehicle is making hard turns
			if(Vector3.Angle(lastFrameAngle, transform.forward) > 1f)
			{
				//drag is set when vehicle is turning to maintain control
				SetDrag(1f) ;
				Object traks = Instantiate(tireTraksPrefab,transform.position + new Vector3(0f,-.5f,0f),transform.rotation);
				Destroy (traks, 5);
				
				//play sound effect for skidding
		//		audio.PlayOneShot(skid);
			}
				
			lastFrameAngle = transform.forward;
			//Debug.Log (Vector3.Angle(lastFrameAngle, transform.forward));
			
			//rigidbody.velocity = transform.forward * stat.GetAccel() * Time.deltaTime ;
			
			//vehicle will cool down temperature
			if( (!stat.GetTempPerSec() && this.gameObject.GetComponentInChildren<GunShieldRotation>().isGunEnabled()) || stat.isOverheated() )
				stat.SetCurrTemp( stat.GetCurrTemp() - stat.GetCooling() * Time.deltaTime ) ;
		
			//vehicle will not have shield up when frozen or overheated and when frozen, temperature is reset for them
			if( stat.isOverheated() || isFrozen() ) {
				gameObject.GetComponentInChildren<GunShieldRotation>().TurnOnGun() ;
				
				if( isFrozen() ) {
					stat.SetCurrTemp(0f) ;
				}
				else if( stat.isOverheated() ) {
					transparency.r = 255f ;
					renderer.material.color = transparency ;
				}
			}
			else {
				transparency.r = 1f ;
				renderer.material.color = transparency ;
			}
			
			//vehicle will not move faster than the maximum velocity
			if( rigidbody.velocity.sqrMagnitude > (stat.GetMaxVelocity()*stat.GetMaxVelocity()) )
				rigidbody.velocity = rigidbody.velocity.normalized * stat.GetMaxVelocity() ;
			
			//vehicle will not have any negative velocity
			if( rigidbody.velocity.magnitude >= 0 )
				stat.SetCurrentSpeed(rigidbody.velocity.magnitude);
			else
				stat.SetCurrentSpeed(0);
			
			//redundant code to maintain vehicle's vertical position
			if(transform.position.y > 1f || transform.position.y < 1f)
				transform.position = new Vector3( transform.position.x, 1f, transform.position.z ) ;
		}
		else
			rigidbody.velocity = new Vector3(0,0,0); //when vehicle dies, vehicle will no longer move
	}
	
	//**************************Trigger Events********************************//
	
	//when vehicle is entering an object's collider
	void OnTriggerEnter( Collider other ) {
		if( other.gameObject.tag == "Slow" ){
			SetDrag(m_slowDrag) ;
			m_slowed = true ;
			Debug.Log ("Slow");
		}
		if( other.gameObject.tag == "Boost" ){
			audio.PlayOneShot(boost);
		}
		
		if( other.gameObject.tag == "Wall"){
			audio.PlayOneShot(hitWall);
		}
	}
	
	//while vehicle is within an object's collider
	void OnTriggerStay( Collider other ) {
		if( other.gameObject.tag == "Slow" ){
			SetDrag(m_slowDrag) ;
			m_slowed = true ;
			Debug.Log ("Slow");
		}
		if( other.gameObject.tag == "Boost" ){
			BoostVehicle(m_boostPadTime, true);
		}
	}
	
	//when vehicle exits an object's collider
	void OnTriggerExit( Collider other ) {
		if( other.gameObject.tag == "Slow" ){
			m_slowed = false ;
		}	
	}
	
	void OnCollisionEnter(Collision collision) {
		if( collision.collider.transform.gameObject.tag == "Vehicle"){
			audio.PlayOneShot(hitCar);
		}
	}
	//*************************Vehicle Behaviors**************************//
	
	
	//vehicle's speed and acceleration will match boost velocity
	public void BoostVehicle( float boostTime, bool boostPad ) {
		stat.SetMaxVelocity(stat.GetBoostSpeed()) ;
		stat.SetAccel(stat.GetBoostSpeed()) ;
		
		//if vehicle is boosted by player, temperature rises
		if( !boostPad )
			RaiseTemperaturePerSecond( 10f ) ;
		
		m_boosted = true ;
		m_boost_time = Time.time + boostTime ;
	}
	
	//Temperature that is raised per second
	public void RaiseTemperaturePerSecond( float tempPerSecond ) {
		if( !stat.GetTempPerSec() )
			stat.TempPerSecOn( ) ;
		
		stat.SetCurrTemp(stat.GetCurrTemp() + tempPerSecond * Time.deltaTime) ;
	}
	
	//Temperature that is raised by a fixed amount
	public void RaiseTemp( bool a_shooting ){
		
		//Vehicle temp is raised based on whether it is attacking or defending with shield
		if( a_shooting )
			stat.SetCurrTemp ( stat.GetCurrTemp() + stat.FindAttackTempCost() ) ;
		else
			stat.SetCurrTemp ( stat.GetCurrTemp() + stat.FindDefenseTempCost() ) ;
	}
	
	//Turns off auto temp rising
	public void TurnOffTempPerSecond( ) {
		stat.TempPerSecOff( ) ;
	}
	
	//sets the vehicle's friction
	public void SetDrag( float intensity ) {
		if( (m_slowed && intensity == m_slowDrag) || (!m_slowed && intensity <= m_slowDrag) || m_boosted)
			rigidbody.drag = intensity / m_slipCoeff ;
	}
	
	//Moves the vehicle using Unity's physics
	public void AddForce( Vector3 direction, float intensity ) {
			rigidbody.AddForce(direction * intensity/m_slipCoeff) ;
	}
	
	//boolean functions
	public bool isFrozen( ) {return m_frozen ;}
	public bool isBoosted( ) {return m_boosted ;}
	public bool amAlive( ) {return isAlive ;}
	
	//Uses item vehicle owns
	public void FirePowerUp()
	{
		myPowerup.UseItem();
	}
	
	//sets slip coefficient and duration when vehicle hits an oil slick
	public void Slick() {
		m_slipCoeff = 3f ;
		m_slipTime = 4f ;
	}
	
	//sets the freeze duration when vehicle is frozen and creates freeze visuals
	public void Freeze() {
		GameObject ice = (GameObject) Instantiate(IceBlock,transform.position,Quaternion.identity) as GameObject;
		ice.GetComponent<FollowIceBlock>().gameObjectToFollow = gameObject.transform;
		if(gameObject.name == "Player")
		{
			GameObject freeze = (GameObject) Instantiate(FreezeGUI) as GameObject;
			ice.GetComponent<BreakBox>().isPlayer = true;
		}
		else
			ice.GetComponent<BreakBox>().isPlayer = false;
		m_freezeTime = m_freezeDuration ;
		m_frozen = true ;
	}
}
