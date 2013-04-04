using UnityEngine;
using System.Collections;

public class basicMovementTesting : MonoBehaviour {
	
	public Vector3	m_prevVelocity;
	public Vector3 	m_currentVelocity;
	Vector3			m_oldPosition;
	// Use this for initialization
	void Start () {
		m_prevVelocity = Vector3.zero;
		m_currentVelocity = Vector3.zero;
	}
	
	float movementSpeed = 10.0f;
	// Update is called once per frame
	void Update () {
		m_prevVelocity = this.transform.position - m_oldPosition;
		Debug.Log("old position: " +m_oldPosition.ToString());
		m_oldPosition = this.transform.position;
		Debug.Log("current position: " + transform.position.ToString());
		if(Input.GetKey(KeyCode.W)) {
			transform.position += new Vector3( 0.0f, 0.0f, movementSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.S)) {
			transform.position += new Vector3( 0.0f, 0.0f, -movementSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.A)) {
			transform.position += new Vector3( -movementSpeed * Time.deltaTime, 0.0f, 0.0f);
		}
		if(Input.GetKey(KeyCode.D)) {
			transform.position += new Vector3( movementSpeed * Time.deltaTime, 0.0f, 0.0f);
		}
		//this works as current velocity much like old velocity because all movement updates
		//have happened this update.
		m_currentVelocity = this.transform.position - m_oldPosition;
	}
}
