using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	public Vehicle player ;
	private float m_maxDrag ;

	// Use this for initialization
	void Start () {
		m_maxDrag = 0.5f ;
	}
	
	// Update is called once per frame
	void Update () {
		
		// Amount to Move
		float MoveRotate = player.stat.GetHandling() * Time.deltaTime;
		float horMovement = Input.GetAxisRaw("Horizontal");
		float vertMovement = Input.GetAxisRaw("Vertical");
		Vector3 moveDirection= new Vector3 (horMovement, 0, vertMovement);
		rigidbody.drag = Mathf.Lerp(m_maxDrag, 0, moveDirection.magnitude);
		 
		 
		// Move the player
		//transform.Translate(Vector3.forward * player.stat.GetSpeed() * Time.deltaTime);
		//transform.Rotate(Vector3.up * player.stat.GetHandling() * Time.deltaTime);
		
		if(Input.anyKey){	
			
			if (moveDirection != Vector3.zero){
				Quaternion newRotation = Quaternion.LookRotation(moveDirection);
				transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, MoveRotate);
				rigidbody.AddForce(moveDirection * player.stat.GetCurrentSpeed());
			}
			
			if(Input.GetKey(KeyCode.D)){
				rigidbody.AddForce(moveDirection * player.stat.GetAccel()) ;
			}
			if(Input.GetKey(KeyCode.A)){
				rigidbody.AddForce(moveDirection * player.stat.GetAccel()) ;
			}
			if(Input.GetKey(KeyCode.W)){
				rigidbody.AddForce(moveDirection * player.stat.GetAccel()) ;
			}
			if(Input.GetKey(KeyCode.S)){
				rigidbody.AddForce(moveDirection * player.stat.GetAccel()) ;
			}
			
			if(Input.GetKey (KeyCode.Q)){
				rigidbody.drag = 2f ;	
			}
			else{
				rigidbody.drag = 0.5f ;	
			}
			
			//if( m_orientationDiff <= 1f )
				
		}
	}
}
