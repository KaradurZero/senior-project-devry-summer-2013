using UnityEngine;
using System.Collections;

public class enemyDriver : MonoBehaviour {
	waypointManager myWaypoints;
	CarStats myStats;
	Vector3 myTargetPos;
	public float mySpeed;
	
	void Start () {
		myWaypoints = transform.GetComponent<waypointManager>();
		myStats = transform.GetComponent<CarStats>();
		myTargetPos = myWaypoints.GetCurrWaypointPos();
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position, myTargetPos) < Random.Range(0,6))
		{
			myWaypoints.NextWaypoint();
			myTargetPos = myWaypoints.GetCurrWaypointPos();
		}
		transform.position = Vector3.MoveTowards(transform.position,myTargetPos,mySpeed * Time.smoothDeltaTime);
	}
}
