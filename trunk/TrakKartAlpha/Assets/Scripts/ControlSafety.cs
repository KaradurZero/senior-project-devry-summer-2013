using UnityEngine;
using System.Collections;

public class ControlSafety : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetJoystickNames().Length != 0) {
			Debug.Log (Input.GetJoystickNames()[0]);
			if( Input.GetJoystickNames()[0] == "Controller (Gamepad for Xbox 360)" ) {
					if( this.gameObject.GetComponent<Xbox360Controller>() == null ) {
					Destroy (this.GetComponent<KeyboardMouseController>()) ;
					this.gameObject.AddComponent<Xbox360Controller>() ;
				}
			}
			else if(this.gameObject.GetComponent<GamepadController>() == null) {
				Destroy (this.GetComponent<KeyboardMouseController>()) ;
				this.gameObject.AddComponent<GamepadController>() ;
			}
		}
		else {
			if(this.GetComponent<KeyboardMouseController>() == null) {
				
				if(this.gameObject.GetComponent<GamepadController>() == null) {
					Destroy (this.gameObject.GetComponent<GamepadController>()) ;
					Destroy (this.gameObject.GetComponent<Xbox360Controller>()) ;
					this.gameObject.AddComponent<KeyboardMouseController>() ;
				}
			}
		}
	}
}
