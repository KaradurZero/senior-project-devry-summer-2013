using UnityEngine;
using System.Collections;

public class GamepadController : MonoBehaviour {
	
	public PlayerControl controller ;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float analogHorMovement = Input.GetAxis("HorizontalJ");
		float analogVertMovement = Input.GetAxis("VerticalJ");
		Vector3 analogMoveDirection= new Vector3 (analogHorMovement, 0, analogVertMovement);
		
		if (analogMoveDirection != Vector3.zero){
			controller.playerRotation(analogMoveDirection) ;
		}
		
		rigidbody.AddForce(analogMoveDirection * controller.player.stat.GetAccel()) ;
		
		if( Input.GetKey (KeyCode.Joystick1Button4) ) {
			rigidbody.drag = 2f ;
		}
		
		//Debug.Log (Input.GetAxis("Fire1"));
	}
}
