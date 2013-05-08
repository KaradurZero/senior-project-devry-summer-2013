using UnityEngine;
using System.Collections;

public class KeyboardMouseController : MonoBehaviour {

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
			float horMovement = Input.GetAxis("Horizontal");
			float vertMovement = Input.GetAxis("Vertical");
			Vector3 moveDirection= new Vector3 (horMovement, 0, vertMovement);
			
			player.SetDrag(Mathf.Lerp(m_maxDrag, 0, moveDirection.magnitude)) ;
			
			if( !player.stat.isOverheated() ) {
				if (moveDirection != Vector3.zero){
					float MoveRotate = player.stat.GetHandling() * Time.deltaTime;
					Quaternion newRotation = Quaternion.LookRotation(moveDirection);
					transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, MoveRotate);
					player.AddForce(moveDirection, player.stat.GetCurrentSpeed());	
				}
				
				if(Input.GetKeyDown(KeyCode.Mouse2) && !m_swapButtonDown) { 
				m_swapButtonDown = true ;
				myGunShieldRot.swapGunShield() ;	
				}
				else if( m_swapButtonDown ) {
					m_swapButtonDown = false ;
				}
				
				myGunShieldRot.updateRotationToMouse();
				
				player.AddForce(moveDirection,  player.stat.GetAccel()) ;
				//player.weapon.Shoot(aimDirection) ;
				
				if(Input.GetKey(KeyCode.Mouse0) && myGunShieldRot.isGunEnabled() && myWeapon.CanShoot()) {
					myWeapon.fireBullet() ;
					player.RaiseTemp( true ) ;
					}
				
				if(Input.GetKey(KeyCode.Space)) {
					if( player.myPowerup.item != 0 )
						myPowerup.UseItem() ;
					}
				
				if( Input.GetKey(KeyCode.Q) ) {
					player.SetDrag( 2f ) ;
					}
						
				if( Input.GetKey(KeyCode.Mouse1) ) {
					player.BoostVehicle( 0.05f ) ;
					}
				else{
					player.TurnOffTempPerSecond( ) ;
					}
				}
			}
		else {
			player.SetDrag(0f) ;
		}
		
		//Debug.Log(player.stat.GetCurrTemp()) ;
	}
}
