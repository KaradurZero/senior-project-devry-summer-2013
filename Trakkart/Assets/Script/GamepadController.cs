using UnityEngine;
using System.Collections;

public class GamepadController : MonoBehaviour {
	
	public Vehicle player ;
	private float m_maxDrag = 1f ;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if( !player.isFrozen() ) {
			float analogHorMovement = Input.GetAxis("HorizontalJ");
			float analogVertMovement = Input.GetAxis("VerticalJ");
			float weaponHoriz = Input.GetAxis("Weapon_Horizontal");
			float weaponVert = Input.GetAxis("Weapon_Vertical");
			Vector3 analogMoveDirection= new Vector3 (analogHorMovement, 0, analogVertMovement);
			Vector3 aimDirection= new Vector3 (weaponHoriz, 0, weaponVert);
			
			float angle = Mathf.Atan2(weaponHoriz, weaponVert) * Mathf.Rad2Deg ;
			
			player.SetDrag(Mathf.Lerp(m_maxDrag, 0, analogMoveDirection.magnitude)) ;
			
			if (analogMoveDirection != Vector3.zero){
				float MoveRotate = player.stat.GetHandling() * Time.deltaTime;
				Quaternion newRotation = Quaternion.LookRotation(analogMoveDirection);
				transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, MoveRotate);
				player.AddForce(analogMoveDirection, player.stat.GetCurrentSpeed());	
			}
			
			if( !player.stat.isOverheated() ) {
				if (aimDirection != Vector3.zero){
					player.weapon.Rotate( aimDirection ) ;
				}
			
				player.AddForce(analogMoveDirection,  player.stat.GetAccel()) ;
				//player.weapon.Shoot(aimDirection) ;
				
				if( weaponHoriz != 0 || weaponVert != 0 ) {
					if( player.weapon.CanShoot() ) {
						player.weapon.Shoot( ) ;
						player.RaiseTemp(player.weapon.GetCost()) ;
					}
				}
				
				if( Input.GetKey(KeyCode.Joystick1Button4) ) {
					player.SetDrag( 2f ) ;
				}
				
				if( Input.GetKey(KeyCode.Joystick1Button5) ) {
					player.BoostVehicle( 0.05f ) ;
					player.RaiseTemperaturePerSecond( 20f ) ;
				}
				else{
					player.TurnOffTempPerSecond( ) ;
				}
			}
		}
		else
			player.SetDrag(0f) ;
	}
}
