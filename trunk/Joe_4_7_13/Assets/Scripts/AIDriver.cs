using UnityEngine;
using System.Collections;

public class AIDriver: MonoBehaviour {
	waypointManager myWaypoints;
	CarStat myStats;
	Vector3 myTargetPos;
	public float mySpeed;
	
	public Vehicle player ;
	private float m_maxDrag ;
	Vector3 pointHit;
	bool isAlive, firstFrameDead;
	Vector3 respawnPoint;
	
	// Use this for initialization
	void Start () {
		isAlive = true;
		m_maxDrag = 0.5f ;
		myWaypoints = gameObject.GetComponent<waypointManager>();
		myStats = gameObject.GetComponent<CarStat>();
		myTargetPos = myWaypoints.GetCurrWaypointPos();
	}
	
	public void Die()
	{
		firstFrameDead = true;
		isAlive = false;
	}
	public void Revive()
	{
		isAlive = true;
	}
	// Update is called once per frame
	void Update () {
		if(isAlive)
		{
			// Amount to Move
			float MoveRotate = player.stat.GetHandling() * Time.deltaTime;
			float horMovement;// = Input.GetAxisRaw("Horizontal");
			float vertMovement;// = Input.GetAxisRaw("Vertical");
			float xDirection = myTargetPos.x - transform.position.x;
			float zDirection = myTargetPos.z - transform.position.z;
			
			horMovement = 0;
			vertMovement = 0;
			if(myTargetPos != null)
			{
				if(xDirection > 0)
					horMovement = 1f;
				else if(xDirection < 0)
					horMovement = -1f;
				else 
					horMovement = 0;
				if(zDirection > 0)
					vertMovement = 1f;
				else if(zDirection < 0)
					vertMovement = -1f;
				else
					vertMovement = 0;
			}
			Vector3 moveDirection= new Vector3 (horMovement, 0, vertMovement);
			rigidbody.drag = Mathf.Lerp(m_maxDrag, 0, moveDirection.magnitude);
			
			if(Vector3.Distance(transform.position, myTargetPos) < Random.Range(0,6))
			{
				myWaypoints.NextWaypoint();
				myTargetPos = myWaypoints.GetCurrWaypointPos();
			}
			else 
			{
				
				if (moveDirection != Vector3.zero){
					Quaternion newRotation = Quaternion.LookRotation(moveDirection);
					transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, MoveRotate);
					rigidbody.AddForce(moveDirection * player.stat.GetMaxVelocity());//GetCurrentSpeed());
				}
				rigidbody.drag = 0.5f ;
			}
			transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
	
		}
		else
		{
			if(firstFrameDead)
			{
				firstFrameDead = false;
				if(GetComponent<waypointManager>() != null)
					respawnPoint = GetComponent<waypointManager>().GetCurrWaypointPos();
			}	
			transform.position = respawnPoint;
		}
	}
}
