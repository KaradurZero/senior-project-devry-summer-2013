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
		
		// Amount to Move
		float horMovement = Input.GetAxis("Horizontal");
		float vertMovement = Input.GetAxis("Vertical");
		Vector3 moveDirection= new Vector3 (horMovement, 0, vertMovement);
		
		rigidbody.drag = Mathf.Lerp(m_maxDrag, 0, moveDirection.magnitude);
		 
		// Move the player
		if (moveDirection != Vector3.zero){
			playerRotation(moveDirection) ;
		}
		
		if(Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
			rigidbody.AddForce(moveDirection * player.stat.GetAccel()) ;
		}
			
		if(Input.GetKey(KeyCode.Q)){
			rigidbody.drag = 1.5f ;	
		}
	}
	
	public void playerRotation( Vector3 direction ) {
		float MoveRotate = player.stat.GetHandling() * Time.deltaTime;
		Quaternion newRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, MoveRotate);
		rigidbody.AddForce(direction * player.stat.GetCurrentSpeed() );	
	}
}
