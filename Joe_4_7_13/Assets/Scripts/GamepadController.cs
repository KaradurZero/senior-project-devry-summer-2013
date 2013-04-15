using UnityEngine;
using System.Collections;

public class GamepadController : MonoBehaviour {
	
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
			if(!player.isFrozen()) {
				float analogHorMovement = Input.GetAxis("HorizontalJ");
				float analogVertMovement = Input.GetAxis("VerticalJ");
				float weaponHoriz = Input.GetAxis("Weapon_Horizontal");
				float weaponVert = Input.GetAxis("Weapon_Vertical");
				Vector3 analogMoveDirection= new Vector3 (analogHorMovement, 0, analogVertMovement);
				Vector3 aimDirection= new Vector3 (weaponHoriz, 0, weaponVert);
				
				float angle = Mathf.Atan2(weaponHoriz, weaponVert) * Mathf.Rad2Deg ;
				
				player.SetDrag(Mathf.Lerp(m_maxDrag, 0, analogMoveDirection.magnitude)) ;
			
				if( !player.stat.isOverheated() ) {
					if (analogMoveDirection != Vector3.zero){
						float MoveRotate = player.stat.GetHandling() * Time.deltaTime;
						Quaternion newRotation = Quaternion.LookRotation(analogMoveDirection);
						transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, MoveRotate);
						player.AddForce(analogMoveDirection, player.stat.GetCurrentSpeed());	
					}
				
					if (aimDirection != Vector3.zero){
						myGunShieldRot.updateRotationToVec3(aimDirection) ;
					}
					player.AddForce(analogMoveDirection,  player.stat.GetAccel()) ;
					//player.weapon.Shoot(aimDirection) ;
					
					if( (weaponHoriz != 0 || weaponVert != 0) && myGunShieldRot.isGunEnabled() ) {
						myWeapon.fireBullet() ;
					}
					
					if( Input.GetKey(KeyCode.Joystick1Button4) ) {
						player.SetDrag( 1f ) ;
					}
					
					if(Input.GetKeyDown(KeyCode.Joystick1Button0) && !m_swapButtonDown){
						m_swapButtonDown = true ;
						myGunShieldRot.swapGunShield() ;	
					}
					else if( m_swapButtonDown ) {
						m_swapButtonDown = false ;
					}
					
					if( Input.GetKey(KeyCode.Joystick1Button5) ) {
						player.BoostVehicle( 0.05f ) ;
						player.RaiseTemperaturePerSecond( 10f ) ;
					}
					else{
						player.TurnOffTempPerSecond( ) ;
					}
				
					if( Input.GetKey(KeyCode.Joystick1Button1) ) {
					if( player.myPowerup.item != 0 )
						player.FirePowerUp() ;
				}
			}
		}
		else {
			player.SetDrag(0f) ;
		}
	}
}
