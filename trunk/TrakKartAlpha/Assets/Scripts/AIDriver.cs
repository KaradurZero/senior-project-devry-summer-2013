using UnityEngine;
using System.Collections;
/*
 * Used in conjuntion with the Vehical script.
 * Tells the AI drivers how they should move.
 */ 
public class AIDriver: MonoBehaviour {
	CheckpointManagerLevel1 myCheckpoints;		//personal list of places to go
	CarStat myStats;							//accel, weight, luck, ect.
	public Vector3 myTargetPos;					//where the driver is headed
	public float mySpeed;						//how fast the vehicle will go
	
	public Vehicle player ;						//every AI driver needs to know the player
	private float m_maxDrag ;					//speed of natural decel
	bool isAlive, firstFrameDead;				//used for respawn
	Vector3 respawnPoint;						//where driver will respawn
	
	void Start () {
		isAlive = true;
		m_maxDrag = .5f ;
		myCheckpoints = gameObject.GetComponent<CheckpointManagerLevel1>();
		myStats = gameObject.GetComponent<CarStat>();
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
	void Update () {
		if(GameObject.Find("GameStateController").GetComponent<GameStateController>().gameState ==
			(int)GameStateController.gameStates.INGAMERUN) {
			//if driver is dead, do not update anything
			if(isAlive)
			{
				//if driver is frozen, do not update anything
				if( !player.isFrozen() ) {
					// Amount to Move
					float MoveRotate = player.stat.GetHandling() * Time.deltaTime;
					float horMovement;
					float vertMovement;
					float xDirection = myTargetPos.x - transform.position.x;
					float zDirection = myTargetPos.z - transform.position.z;
					
					horMovement = 0;
					vertMovement = 0;
					if(myTargetPos != null)
					{
						if(xDirection > 1)
							horMovement = 1f;
						else if(xDirection < -1)
							horMovement = -1f;
						else 
							horMovement = 0;
						if(zDirection > 1)
							vertMovement = 1f;
						else if(zDirection < -1)
							vertMovement = -1f;
						else
							vertMovement = 0;
					}
					Vector3 moveDirection= new Vector3 (horMovement, 0, vertMovement);
					player.SetDrag(Mathf.Lerp(m_maxDrag, 0, moveDirection.magnitude)) ;
					
					if( !player.stat.isOverheated() ) {
						if(Vector3.Distance(transform.position, myTargetPos) <= Random.Range(1f,5f))
						{
							if(myCheckpoints != null)
							{
								myTargetPos = myCheckpoints.GetCurrCheckpointPos();
							}
						}
						else 
						{
							if (moveDirection != Vector3.zero){
								Quaternion newRotation = Quaternion.LookRotation(moveDirection);
								transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, MoveRotate);
								player.AddForce(moveDirection, player.stat.GetCurrentSpeed());	
							}
							rigidbody.drag = 0.5f ;
						}
						//transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
						player.AddForce(moveDirection,  player.stat.GetAccel()) ;
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
					if(GetComponent<CheckpointManagerLevel1>() != null)
						respawnPoint = GetComponent<CheckpointManagerLevel1>().GetLastCheckpointPos();
				}
					transform.position = respawnPoint;
			}
		}
	}
}
