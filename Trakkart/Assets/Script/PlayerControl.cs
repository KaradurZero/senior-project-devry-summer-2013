using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	public Vehicle player ;
	private float m_maxDrag ;

	// Use this for initialization
	void Start () {
		m_maxDrag = 1f ;
	}
	
	// Update is called once per frame
	void Update () {
		if( !player.isFrozen() ) {
			// Amount to Move
			float horMovement = Input.GetAxis("Horizontal");
			float vertMovement = Input.GetAxis("Vertical");
			Vector3 moveDirection= new Vector3 (horMovement, 0, vertMovement);
			
			player.SetDrag(Mathf.Lerp(m_maxDrag, 0, moveDirection.magnitude)) ;
			
			 
			// Move the player
			if (moveDirection != Vector3.zero){
				float MoveRotate = player.stat.GetHandling() * Time.deltaTime;
				Quaternion newRotation = Quaternion.LookRotation(moveDirection);
				transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, MoveRotate);
				player.AddForce(moveDirection, player.stat.GetCurrentSpeed());	
			}
			
			if( !player.stat.isOverheated() ) {
				//if(Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
				player.AddForce(moveDirection, player.stat.GetAccel()) ;
				//}
					
				if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Joystick1Button4)){
					player.SetDrag( 2f ) ;
					//Debug.Log ("SCREECH!");
				}
				
				if( Input.GetKey(KeyCode.Mouse1) ) {
					player.BoostVehicle( 0f ) ;
					player.RaiseTemperaturePerSecond( 20f ) ;
				}
				else {
					player.TurnOffTempPerSecond( ) ;	
				}
			}
		}
	}
}
