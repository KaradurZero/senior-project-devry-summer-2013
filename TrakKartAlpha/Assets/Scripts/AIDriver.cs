using UnityEngine;
using System.Collections;

public class AIDriver: MonoBehaviour {
	waypointManager myWaypoints;
	CheckpointManagerLevel1 myCheckpoints;
	CarStat myStats;
	public Vector3 myTargetPos;
	public float mySpeed;
	
	public Vehicle player ;
	private float m_maxDrag ;
	Vector3 pointHit;
	bool isAlive, firstFrameDead;
	Vector3 respawnPoint;
	
	void Start () {
		isAlive = true;
		m_maxDrag = .5f ;
		myWaypoints = gameObject.GetComponent<waypointManager>();
		myCheckpoints = gameObject.GetComponent<CheckpointManagerLevel1>();
		myStats = gameObject.GetComponent<CarStat>();
		if(myWaypoints != null)
			myTargetPos = myWaypoints.GetCurrWaypointPos();
		if(myCheckpoints != null)
			myTargetPos = myCheckpoints.GetCurrCheckpointPos();
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
			if( !player.isFrozen() ) {
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
				//rigidbody.drag = Mathf.Lerp(m_maxDrag, 0, moveDirection.magnitude);
				player.SetDrag(Mathf.Lerp(m_maxDrag, 0, moveDirection.magnitude)) ;
				
				if( !player.stat.isOverheated() ) {
					if(Vector3.Distance(transform.position, myTargetPos) <= Random.Range(1f,5f))
					{
						if(myWaypoints != null)
						{
							myWaypoints.NextWaypoint();
							myTargetPos = myWaypoints.GetCurrWaypointPos();
						}
						if(myCheckpoints != null)
						{
							//myCheckpoints.NextCheckpoint();
							myTargetPos = myCheckpoints.GetCurrCheckpointPos();
						}
					}
					else 
					{
						
						if (moveDirection != Vector3.zero){
							Quaternion newRotation = Quaternion.LookRotation(moveDirection);
							transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, MoveRotate);
							//rigidbody.AddForce(moveDirection * player.stat.GetMaxVelocity());//GetCurrentSpeed());
							player.AddForce(moveDirection, player.stat.GetCurrentSpeed());	
						}
						rigidbody.drag = 0.5f ;
					}
					//transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
					player.AddForce(moveDirection,  player.stat.GetAccel()) ;
//					if(Vector3.Distance(myWaypoints.GetCurrWaypointPos(), transform.position) >= 1f)
//					{
//						player.BoostVehicle( 0.5f ) ;
						//player.RaiseTemperaturePerSecond( 10f ) ;
//					}
				}
			}
			else
				player.SetDrag(0) ;
		}
		else
		{
			if(firstFrameDead)
			{
				firstFrameDead = false;
				if(GetComponent<waypointManager>() != null)
					respawnPoint = GetComponent<waypointManager>().GetLastWaypointPos();
				if(GetComponent<CheckpointManagerLevel1>() != null)
					respawnPoint = GetComponent<CheckpointManagerLevel1>().GetLastCheckpointPos();
			}
				transform.position = respawnPoint;
		}
	}
}
