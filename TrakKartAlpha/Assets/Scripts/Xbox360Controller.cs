using UnityEngine;
using System.Collections;

public class Xbox360Controller : MonoBehaviour {
	
	public Vehicle player ;
	public Weapon myWeapon ;
	public GunShieldRotation myGunShieldRot;
	public vehicleItems myPowerup;
	private float m_maxDrag = 0.5f ;
	private bool m_swapButtonDown ;

	// Use this for initialization
	void Start () {
		if(transform.GetComponentInChildren<Weapon>())
			myWeapon = transform.GetComponentInChildren<Weapon>();
		if(transform.GetComponentInChildren<GunShieldRotation>())
			myGunShieldRot = transform.GetComponentInChildren<GunShieldRotation>();
		if(transform.GetComponent<vehicleItems>())
			myPowerup = transform.GetComponent<vehicleItems>();
		
		m_swapButtonDown = false ;
	}
	
	// Update is called once per frame
	void Update () {
		if( player.amAlive() ) {
			if(!player.isFrozen()) {
				if( !player.stat.isOverheated() ) {
					float analogHorMovement = Input.GetAxis("HorizontalJ");
					float analogVertMovement = Input.GetAxis("VerticalJ");
					float weapon360_H = Input.GetAxis("Weapon360_H");
					float weapon360_V = Input.GetAxis("Weapon360_V");
					Vector3 analogMoveDirection= new Vector3 (analogHorMovement, 0, analogVertMovement);
					Vector3 aim360_dir = new Vector3 (weapon360_H, 0, weapon360_V) ;
					
					player.SetDrag(Mathf.Lerp(m_maxDrag, 0, analogMoveDirection.magnitude)) ;
				
					
						if (analogMoveDirection != Vector3.zero){
							float MoveRotate = player.stat.GetHandling() * Time.deltaTime;
							Quaternion newRotation = Quaternion.LookRotation(analogMoveDirection);
							transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, MoveRotate);
							player.AddForce(analogMoveDirection, player.stat.GetCurrentSpeed());	
						}
					
						if (aim360_dir != Vector3.zero){
							if(aim360_dir != Vector3.zero)
								myGunShieldRot.updateRotationToVec3(aim360_dir) ;
						}
					
						player.AddForce(analogMoveDirection,  player.stat.GetAccel()) ;
						//player.weapon.Shoot(aimDirection) ;
						
						if( (weapon360_H != 0 || weapon360_V != 0) && myGunShieldRot.isGunEnabled() && myWeapon.CanShoot()) {
							myWeapon.fireBullet() ;
							player.RaiseTemp( true ) ;
							
						}
						
						if( Input.GetKey(KeyCode.Joystick1Button4) /*|| Input.GetKey(KeyCode.Joystick2Button4)*/) {
							player.SetDrag( 2f ) ;
							Debug.Log ("BRAKE");
						}
						
						if(Input.GetKeyDown(KeyCode.Joystick1Button2) && !m_swapButtonDown){
							m_swapButtonDown = true ;
							myGunShieldRot.swapGunShield() ;	
						}
						else if( m_swapButtonDown ) {
							m_swapButtonDown = false ;
						}
						
						if( Input.GetKey(KeyCode.Joystick1Button5)) {
							player.BoostVehicle( 0.05f, false ) ;
						}
						else{
							player.TurnOffTempPerSecond( ) ;
						}
					
						if(Input.GetKey(KeyCode.Joystick1Button0)) {
						if( player.myPowerup.item != 0 )
							player.FirePowerUp() ;
					}
				}
				else
					player.SetDrag(m_maxDrag) ;
			}
			else {
				player.SetDrag(0f) ;
			}
		}
	}
}
