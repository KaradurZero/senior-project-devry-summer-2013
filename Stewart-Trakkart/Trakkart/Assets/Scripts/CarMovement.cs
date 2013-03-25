using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour {
	
	float 	maxSpeed;
	float 	maxAcceleration;
	float 	maxDeceleration;
	float 	currSpeed;
	//float 	rotation;//used with currDirection
	Vector3	currVelocity;//speed and direction object is moving
	Vector3 steeringForce;
	//Vector3 currDirection;//direction vehicle is pointing. steering force will effect this.	
	
	void Init() {
		maxSpeed 			= 0.5f;
		maxAcceleration 	= 0.2f;
		maxDeceleration		= 0.2f;
		//currDirection.y 	= 1.0f;
		//currDirection.x 	= 0.0f;
		steeringForce.Normalize();
		currSpeed 			= 0.0f;
	}
	// Use this for initialization
	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.W)) {
			//steeringForce = this.transform.forward;// * maxAcceleration;//currDirection.normalized * maxAcceleration;
			//Debug.Log( "x: " + transform.forward.x +"y: " + transform.forward.y + "z: " + transform.forward.z);
			//Debug.Log("rotation: " + getRotationFromCurrDirection());
			currSpeed += maxAcceleration *Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.S)) {
			//steeringForce = (-currDirection.normalized) * maxDeceleration;
			//steeringForce = -this.transform.forward;// * maxAcceleration;
			currSpeed -= maxDeceleration *Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.A)) {
			transform.RotateAround( Vector3.down, 5 * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.D)) {
			transform.RotateAround( Vector3.down, -5 * Time.deltaTime);
		}
		if(!Input.anyKey) {//if no key has been pressed, slow down
			//steeringForce.Set(0,0,0);
			//currSpeed = 0.0f;
			if(currSpeed < 0.001f && currSpeed > -0.001f) {
				currSpeed = 0.0f;
			}
			else if(currSpeed > 0.0f) {
				currSpeed -= 0.1f * Time.deltaTime;
			}
			else {
				currSpeed += 0.1f * Time.deltaTime;
			}
		}
		
		if(currSpeed > maxSpeed) 		{ currSpeed = maxSpeed;}
		if(currSpeed < -maxSpeed *0.5f) { currSpeed = -maxSpeed * 0.5f;}
		//apply movement logic
		currVelocity = this.transform.forward * currSpeed;
		//if velocity is higher than maxSpeed forward or half max in reverse
		if( (currVelocity.magnitude > maxSpeed) && (currSpeed > 0.0f)) {
			currVelocity = currVelocity.normalized * maxSpeed;
		}
		else if ( (currVelocity.magnitude > maxSpeed *0.5f) && (currSpeed < 0.0f)) {
			currVelocity = currVelocity.normalized * (maxSpeed * 0.5f);
		}
		
		//then change the object transform
		this.transform.position = new Vector3(this.transform.position.x + currVelocity.x,
			this.transform.position.y /*+ currVelocity.y*/, this.transform.position.z + currVelocity.z);
	}
	
//	float getRotationFromCurrDirection() {
//		return (Mathf.Atan2( currDirection.y, currDirection.x) / Mathf.PI * 180.0f);
//	}
//	void setCurrDirectionFromRotation() {
//		currDirection.x = Mathf.Sin(rotation);
//		currDirection.y = Mathf.Cos(rotation);
//	}
}
