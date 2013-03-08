using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour {
	
	float 	maxSpeed;
	float 	maxAcceleration;
	float 	maxDeceleration;
	Vector2	currVelocity;//speed and direction object is moving
	Vector2 steeringForce;
	Vector2 currPosition;//position of the current game object in 2D space
	Vector2 currDirection;//direction vehicle is pointing. steering force will effect this.
	
	void Init() {
		maxSpeed 			= 2.0f;
		maxAcceleration 	= 0.2f;
		maxDeceleration		= 0.2f;
		currDirection.y 	= 1.0f;
		currDirection.x 	= 0.0f;
		steeringForce.Normalize();
	}
	// Use this for initialization
	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.W)) {
			steeringForce = currDirection.normalized * maxAcceleration;
		}
		if(Input.GetKey(KeyCode.S)) {
			steeringForce = (-currDirection.normalized) * maxDeceleration;
		}
		if(!Input.anyKey) {//if no key has been pressed, be sure to reset steering force to 0,0
			steeringForce.Set(0,0);
		}
		currVelocity += steeringForce * Time.deltaTime;
		//if velocity is higher than maxSpeed forward or half max in reverse
		if(currVelocity.magnitude > maxSpeed || currVelocity.magnitude < -maxSpeed *0.5f) {
			currVelocity = currVelocity.normalized * maxSpeed;
		}
		
		//then change the object transform
		this.transform.position = new Vector3(this.transform.position.x + currVelocity.x,
			this.transform.position.y + currVelocity.y, this.transform.position.z);
	}
}
