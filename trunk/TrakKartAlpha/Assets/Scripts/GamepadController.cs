using UnityEngine;
using System.Collections;

public class GamepadController : MonoBehaviour {
	
	public Vehicle player ;
	public Weapon myWeapon ;
	public GunShieldRotation myGunShieldRot;
	public vehicleItems myPowerup;
	private float m_maxDrag = 1.5f ;
	private bool m_swapButtonDown ;

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
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetJoystickNames()[0] != "Controller (Gamepad for Xbox 360)" ) {
			//is player alive?
			if( player.amAlive() ) {
				//lock controls when player is frozen/overheated
				if(!player.isFrozen()) {
					if( !player.stat.isOverheated() ) {
						//input poll
						float analogHorMovement = Input.GetAxisRaw("HorizontalJ");
						float analogVertMovement = Input.GetAxisRaw("VerticalJ");
						float weaponHoriz = Input.GetAxis("Weapon_Horizontal");
						float weaponVert = Input.GetAxis("Weapon_Vertical");
						Vector3 analogMoveDirection= new Vector3 (analogHorMovement, 0, analogVertMovement);
						Vector3 aimDirection= new Vector3 (weaponHoriz, 0, weaponVert);
						//Vector3 aimDirection= new Vector3 (-weaponVert, 0, -weaponHoriz);
						
						//set drag
						player.SetDrag(Mathf.Lerp(m_maxDrag, 0, analogMoveDirection.magnitude)) ;
					
						
							//rotate vehicle based on player direction
							if (analogMoveDirection != Vector3.zero){
								float MoveRotate = player.stat.GetHandling() * Time.deltaTime;
								Quaternion newRotation = Quaternion.LookRotation(analogMoveDirection);
								transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, MoveRotate);
								//add force for smoother turning
								player.AddForce(analogMoveDirection, player.stat.GetCurrentSpeed());	
							}
						
							//rotate gun/shield based on right thumbstick direction
							if (aimDirection != Vector3.zero) {
								myGunShieldRot.updateRotationToVec3(aimDirection) ;
							}
						
							//move vehicle
							player.AddForce(analogMoveDirection,  player.stat.GetAccel()) ;
							//player.weapon.Shoot(aimDirection) ;
						
						//*****************************************Player Input********************************************************//
							
							//fire weapon if thumbstick is moved
							if( (weaponHoriz != 0 || weaponVert != 0) && myGunShieldRot.isGunEnabled() && myWeapon.CanShoot()) {
								myWeapon.fireBullet() ;
								player.RaiseTemp( true ) ;
								
							}
							
							//brake
							if( Input.GetKey(KeyCode.Joystick1Button4) /*|| Input.GetKey(KeyCode.Joystick2Button4)*/) {
								player.SetDrag( 3f ) ;
								Debug.Log ("BRAKE");
							}
							
							//swap gun/shield
							if(Input.GetKeyDown(KeyCode.Joystick1Button0) && !m_swapButtonDown){
								m_swapButtonDown = true ;
								myGunShieldRot.swapGunShield() ;	
							}
							else if( m_swapButtonDown ) {
								m_swapButtonDown = false ;
							}
							
							//boost vehicle
							if( Input.GetKey(KeyCode.Joystick1Button5)) {
								player.BoostVehicle( 0.05f, false ) ;
							}
							else{
								player.TurnOffTempPerSecond( ) ;
							}
						
							//use powerup
							if( Input.GetKeyDown(KeyCode.Joystick1Button1)) {
							if( player.myPowerup.item != 0 )
								player.FirePowerUp() ;
						}
					}
					else
						player.SetDrag(m_maxDrag) ; //vehicle drags when player doesn't move or overheated
				}
				else {
					player.SetDrag(0f) ; //vehicle slips aroung when frozen
				}
			}
		}
	}
}
