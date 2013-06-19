using UnityEngine;
using System.Collections;

public class KeyboardMouseController : MonoBehaviour {

	public Vehicle player ;
	public Weapon myWeapon ;
	public GunShieldRotation myGunShieldRot;
	public vehicleItems myPowerup;
	private float m_maxDrag = 1.5f ;
	private bool m_swapButtonDown ;
	
	GameStateController m_mainStateController;

	// Use this for initialization
	void Start () {
		if(transform.GetComponentInChildren<Weapon>())
			myWeapon = transform.GetComponentInChildren<Weapon>();
		if(transform.GetComponentInChildren<GunShieldRotation>())
			myGunShieldRot = transform.GetComponentInChildren<GunShieldRotation>();
		if(transform.GetComponent<vehicleItems>())
			myPowerup = transform.GetComponent<vehicleItems>();
		
		player = this.gameObject.GetComponent<Vehicle>() ;
		
		m_swapButtonDown = false ;
		m_mainStateController = GameObject.Find("GameStateController").GetComponent<GameStateController>();
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (this.GetComponent<GamepadController>());
		//check first if game state is set to ingamerun
		if(m_mainStateController.gameState == (int)GameStateController.gameStates.INGAMERUN) {
			//if player is alive
			if( player.amAlive() ) {
				
				//lock controls when player is frozen/overheated
				if(!player.isFrozen()) {
					if( !player.stat.isOverheated() ) {
						
						//input poll
						float horMovement = Input.GetAxis("Horizontal");
						float vertMovement = Input.GetAxis("Vertical");
						Vector3 moveDirection= new Vector3 (horMovement, 0, vertMovement);
					
						//Rotate vehicle based on direction
						if (moveDirection != Vector3.zero){
							float MoveRotate = player.stat.GetHandling() * Time.deltaTime;
							Quaternion newRotation = Quaternion.LookRotation(moveDirection);
							transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, MoveRotate);
							
							//force is added for smoother turning
							player.AddForce(moveDirection, player.stat.GetCurrentSpeed());	
						}
						
						//set Drag
						player.SetDrag(Mathf.Lerp(m_maxDrag, 0, moveDirection.magnitude)) ;
						
						//************************Player Input************************************************//
						//swap gun/shield
						if(Input.GetKeyDown(KeyCode.Mouse2) && !m_swapButtonDown) { 
						m_swapButtonDown = true ;
						myGunShieldRot.swapGunShield() ;	
						}
						else if( m_swapButtonDown ) {
							m_swapButtonDown = false ;
						}
					
						//mouse rotation
						myGunShieldRot.updateRotationToMouse();
						
						//move vehicle
						player.AddForce(moveDirection,  player.stat.GetAccel()) ;
						
						//fire weapon
						if(Input.GetKey(KeyCode.Mouse0) && myGunShieldRot.isGunEnabled() && myWeapon.CanShoot()) {
							myWeapon.fireBullet() ;
							player.RaiseTemp( true ) ;
							}
						
						//use powerup
						if(Input.GetKeyDown(KeyCode.Space)) {
							if( player.myPowerup.item != 0 )
								myPowerup.UseItem() ;
							}
						
						//brakes
						if( Input.GetKey(KeyCode.Q) ) {
							player.SetDrag( 3f ) ;
							}
							
						//boost
						if( Input.GetKey(KeyCode.Mouse1) ) {
							player.BoostVehicle( 0.05f, false ) ;
							}
						else{
							player.TurnOffTempPerSecond( ) ;
							}
						}
					else
						player.SetDrag(m_maxDrag) ; //vehicle is slowing down when there is no input or vehicle is overheated
					}
				else 
					player.SetDrag(0f) ; //vehicle is slipping about when vehicle is frozen
			}
		}//end check if game is ingamerun state
	}
}