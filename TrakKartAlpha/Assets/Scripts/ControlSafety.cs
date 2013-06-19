using UnityEngine;
using System.Collections;

public class ControlSafety : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetJoystickNames().Length != 0) {
			//if( Input.GetJoystickNames()[0] == null && Input.GetJoystickNames().Length > 1 ) {
			//	for( int i = 0; i < Input.GetJoystickNames().Length; ++i ) {
			//		Input.GetJoystickNames()[i] = Input.GetJoystickNames()[i] ;	
			//	}
			//}
			
			Debug.Log(Input.GetJoystickNames()[0]) ;
			
			if( Input.GetJoystickNames()[0] == "Controller (Gamepad for Xbox 360)" ) {
					if( this.gameObject.GetComponent<Xbox360Controller>() == null ) {
					Debug.Log ("CONTROLLER");
					Destroy (this.GetComponent<KeyboardMouseController>()) ;
					Destroy (this.GetComponent<GamepadController>()) ;
					this.gameObject.AddComponent<Xbox360Controller>() ;
				}
			}
			else {
					if(this.gameObject.GetComponent<GamepadController>() == null) {
					Debug.Log ("XBOX");
					Destroy (this.GetComponent<KeyboardMouseController>()) ;
					Destroy (this.GetComponent<Xbox360Controller>()) ;
					this.gameObject.AddComponent<GamepadController>() ;
				}
			}
		}
		else {
			if(this.gameObject.GetComponent<KeyboardMouseController>() == null) {
				Debug.Log ("KEYBOARD");
				Destroy (this.gameObject.GetComponent<GamepadController>()) ;
				Destroy (this.gameObject.GetComponent<Xbox360Controller>()) ;
				this.gameObject.AddComponent<KeyboardMouseController>() ;
			}
		}
	}
}
