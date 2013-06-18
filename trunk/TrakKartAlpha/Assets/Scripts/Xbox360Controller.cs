using UnityEngine;
using System.Collections;

public class Xbox360Controller : MonoBehaviour {
	
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
		
		//is vehicle alive?
		if( player.amAlive() ) {
			//lock controls when vehicle is frozen/overheated
			if(!player.isFrozen()) {
				if( !player.stat.isOverheated() ) {
					
						//input poll
						float analogHorMovement = Input.GetAxis("HorizontalJ");
						float analogVertMovement = Input.GetAxis("VerticalJ");
						float weapon360_H = Input.GetAxis("Weapon360_H");
						float weapon360_V = Input.GetAxis("Weapon360_V");
						Vector3 analogMoveDirection= new Vector3 (analogHorMovement, 0, analogVertMovement);
						Vector3 aim360_dir = new Vector3 (weapon360_H, 0, weapon360_V) ;
						
						//set vehicle drag
						player.SetDrag(Mathf.Lerp(m_maxDrag, 0, analogMoveDirection.magnitude)) ;
				
						//rotate vehicle based on player direction
						if (analogMoveDirection != Vector3.zero){
							float MoveRotate = player.stat.GetHandling() * Time.deltaTime;
							Quaternion newRotation = Quaternion.LookRotation(analogMoveDirection);
							transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, MoveRotate);
							//add force for smoother turning
							player.AddForce(analogMoveDirection, player.stat.GetCurrentSpeed());	
						}
						
						//rotate gun/shield whenever right thumbstick is used
						if (aim360_dir != Vector3.zero){
							if(aim360_dir != Vector3.zero)
								myGunShieldRot.updateRotationToVec3(aim360_dir) ;
						}
						
						//move vehicle
						player.AddForce(analogMoveDirection,  player.stat.GetAccel()) ;
						//player.weapon.Shoot(aimDirection) ;
						
					//******************************Player Input*****************************************************************//
					
						//fire weapon whenever right thumbstick is pushed
						if( (weapon360_H != 0 || weapon360_V != 0) && myGunShieldRot.isGunEnabled() && myWeapon.CanShoot()) {
							myWeapon.fireBullet() ;
							player.RaiseTemp( true ) ;
							
						}
						
						//brake
						if( Input.GetKey(KeyCode.Joystick1Button4) /*|| Input.GetKey(KeyCode.Joystick2Button4)*/) {
							player.SetDrag( 3f ) ;
							Debug.Log ("BRAKE");
						}
						
						//swap gun/shield
						if(Input.GetKeyDown(KeyCode.Joystick1Button2) && !m_swapButtonDown){
							m_swapButtonDown = true ;
							myGunShieldRot.swapGunShield() ;	
						}
						else if( m_swapButtonDown ) {
							m_swapButtonDown = false ;
						}
						
						//boost
						if( Input.GetKey(KeyCode.Joystick1Button5)) {
							player.BoostVehicle( 0.05f, false ) ;
						}
						else{
							player.TurnOffTempPerSecond( ) ;
						}
					
						//use powerup
						if(Input.GetKeyDown(KeyCode.Joystick1Button0)) {
						if( player.myPowerup.item != 0 )
							player.FirePowerUp() ;
					}
				}
				else
					player.SetDrag(m_maxDrag) ; //set drag when player is not moving controls or overheated
			}
			else {
				player.SetDrag(0f) ; //vehicle slips around when frozen
			}
		}
	}
}
