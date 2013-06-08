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
		
		m_swapButtonDown = false ;
		m_mainStateController = GameObject.Find("GameStateController").GetComponent<GameStateController>();
	}
	
	// Update is called once per frame
	void Update () {
		//check first if game state is set to ingamerun
		if(m_mainStateController.gameState == (int)GameStateController.gameStates.INGAMERUN) {
			if( player.amAlive() ) {
				if(!player.isFrozen()) {
					if( !player.stat.isOverheated() ) {
					float horMovement = Input.GetAxis("Horizontal");
					float vertMovement = Input.GetAxis("Vertical");
					Vector3 moveDirection= new Vector3 (horMovement, 0, vertMovement);
					
					player.SetDrag(Mathf.Lerp(m_maxDrag, 0, moveDirection.magnitude)) ;
					
					
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
						
						if(Input.GetKeyDown(KeyCode.Space)) {
							if( player.myPowerup.item != 0 )
								myPowerup.UseItem() ;
							}
						
						if( Input.GetKey(KeyCode.Q) ) {
							player.SetDrag( 2f ) ;
							}
								
						if( Input.GetKey(KeyCode.Mouse1) ) {
							player.BoostVehicle( 0.05f, false ) ;
							}
						else{
							player.TurnOffTempPerSecond( ) ;
							}
						}
					else
						player.SetDrag(m_maxDrag) ;
					}
				else 
					player.SetDrag(0f) ;
			}
		}//end check if game is ingamerun state
	}
}