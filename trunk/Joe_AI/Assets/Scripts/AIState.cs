using UnityEngine;
using System.Collections;

public class AIState : MonoBehaviour {
	
	//think times
	float timeShotAt;
	public float timeUntilRevengeShot;
	float updateStateTime;
	
	//periodicals
	float currentStateTime, deltaStateTime;
	float waitTime, currWaitTime;
	
	//triggers
	bool IAmBeingShotAt;
	bool firstFrameShield, firstFrameGun;
	
	//possessions
	Weapon myWeapon;
	GunShieldRotation myGunShieldRot;
	Transform enemyTrans;
	
	//competition - set in inspector
	public GameObject[] otherDrivers;
	
	//states
	enum AI_STATE{
		SHOOT,
		SHIELD,
		NONE};
	AI_STATE myState;
	
	void Start () {
		timeShotAt			= 0f;
		updateStateTime		= .1f;
		myWeapon 			= null;
		myGunShieldRot 		= null;
		IAmBeingShotAt 		= false;
		currentStateTime 	= currWaitTime = Time.time;
		enemyTrans			= FindClosestDriver();
		
		if(transform.GetComponentInChildren<Weapon>())
			myWeapon = transform.GetComponentInChildren<Weapon>();
		if(transform.GetComponentInChildren<GunShieldRotation>())
			myGunShieldRot = transform.GetComponentInChildren<GunShieldRotation>();
			
		StallTime(3f);
	}
	public Vector3 GetTargetPos()
	{
		if(enemyTrans != null)
			return enemyTrans.position;
		else
			return FindClosestDriver().position;
	}
	Transform FindClosestDriver()
	{
		//compare all drivers distance
		float closestDistance = 999.9f;
		Transform closestTrans = transform;
		foreach(GameObject go in otherDrivers)
		{
			//go = null when closest enemy is dead
			if(go != null)
			{
				float thisDistance = Vector3.Distance(transform.position, go.transform.position);
				if(thisDistance < closestDistance)
				{
					closestDistance = thisDistance;
					closestTrans = go.transform;
				}
			}
		}
		
		//if there is no enemies left to shoot at
		if(closestTrans == transform)
		{
			myState = AI_STATE.NONE;
		}
		return closestTrans;
	}
	
	void SetState(AI_STATE state)
	{
		myState = state;
	}
	
	public void BeingShotAtBy(Transform shooterTrans)
	{
		IAmBeingShotAt = true;
		enemyTrans = shooterTrans;
	}
	
	public void NoLongerBeingShotAt()
	{
		IAmBeingShotAt = false;
		enemyTrans = FindClosestDriver();
	}
	
	void StallTime(float t)
	{
       waitTime = t;
	}
	
	void StateChange()
	{
		//defend
		if(IAmBeingShotAt)
		{
			if(firstFrameShield)
			{
				timeShotAt = Time.time;
				firstFrameShield = false;
			}
			firstFrameGun = true;
			myState = AI_STATE.SHIELD;
			myGunShieldRot.TurnOnShield();
		}
		//attack
		else if(Vector3.Distance(transform.position, enemyTrans.position) < 20)
		{
			firstFrameShield = true;
			enemyTrans = FindClosestDriver();//.GetComponent<waypointManager>().GetFutureWaypointTransform(1);
			myState = AI_STATE.SHOOT;
			myGunShieldRot.TurnOnGun();
			if(enemyTrans.GetComponent<AIState>())
				enemyTrans.GetComponent<AIState>().BeingShotAtBy(transform);
		}
		//nothing
		else
			myState = AI_STATE.NONE;
	}
	
	void Update () {
		//do nothing if waitTime is set to anything
		if(waitTime > 0)
		{
			myState = AI_STATE.NONE;
		}
		//update state
		if(Time.time - currentStateTime > updateStateTime)
		{
			currentStateTime = Time.time;
			StateChange();
		}
		switch(myState)
		{
		case AI_STATE.SHOOT:
			myWeapon.fireBullet();
			break;
		case AI_STATE.SHIELD:
			float t = Time.time - timeShotAt;
			if(Time.time - timeShotAt > timeUntilRevengeShot)
				NoLongerBeingShotAt();
			break;
		case AI_STATE.NONE:
			if(Time.time - currWaitTime > waitTime)
			{
				waitTime = 0;
				currWaitTime = Time.time;
			}
			break;
		}
	}
}
