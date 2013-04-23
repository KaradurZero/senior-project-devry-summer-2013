using UnityEngine;
using System.Collections;

public class MissileMovement : MonoBehaviour {
	
	GameObject m_targetEnemy;
	GameObject m_ignoreTarget;
	float m_maxMovementSpeed;
	float m_lifeSpan;
	float turnSpeed;//value between 0.0f and 1.0f if not using Time.deltaTime to lower value
	float m_closeDistance;
	string m_hTriggerSpecific_01;
	int m_damageThisDeals;
	
	// Use this for initialization
	void Start () {
		//if movement speed is increased then turnspeed will need to be increased
		m_maxMovementSpeed 		= 20.0f;
		turnSpeed 				= 2.0f;
		m_closeDistance 		= 10.0f;
		m_hTriggerSpecific_01 	= "Vehicle";
		m_lifeSpan 				= 10.0f;
		m_damageThisDeals 		= 200;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 offset = m_targetEnemy.transform.position - transform.position;
        float sqrLen = offset.sqrMagnitude;
        if (sqrLen < m_closeDistance * m_closeDistance) {//if missle is close, turn up it's turn speed
			turnSpeed = 6.0f;
		}
		else {//otherwise set turn speed to it's normal amount;
			turnSpeed = 2.0f;
		}
        	//print("The other transform is close to me!");
		// Determine the target rotation. This is the rotation if the transform looks at the target point.
		Quaternion targetRotation = Quaternion.LookRotation(m_targetEnemy.transform.position - this.transform.position);
		// Smoothly rotate towards the target point.
		
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime); // WITH SPEED
		//transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1); // WITHOUT SPEED!!!
		//then update movement towards enemy.
		this.transform.position += this.transform.forward * m_maxMovementSpeed * Time.deltaTime;
		
		m_lifeSpan -= Time.deltaTime;
		if(m_lifeSpan < 0.0f) {
			Destroy(this.gameObject);
		}
	}
	
	void OnTriggerEnter(Collider c) {
		if(c.transform.position != m_ignoreTarget.transform.position && c.tag == m_hTriggerSpecific_01) {
			//TODO: get enemy script and lower health/energy from missile hit
			c.GetComponent<driverHealth>().DealDamage(m_damageThisDeals);
			hasHitEnemy();
		}
	}
	
	void hasHitEnemy() {
		Destroy(this.gameObject);
	}
	
	public void setTarget(GameObject a_enemy) { m_targetEnemy = a_enemy;}
	public void setIgnoreTarget(GameObject a_launcher) { m_ignoreTarget = a_launcher;}
	public void setRotation(Quaternion a_rotation) {this.transform.rotation = a_rotation;}
}
