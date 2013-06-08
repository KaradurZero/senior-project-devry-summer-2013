using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIState : MonoBehaviour {
	
	//think times
	float timeShotAt;
	public float timeUntilRevengeShot;
	float updateStateTime;
	float PowerupUseStallTime;
	float tempPercentage, initialTemp, currentTemp;
	
	//periodicals
	float currentStateTime, deltaStateTime;
	float waitTime, currWaitTime;
	float currPowerupTime;
	
	//triggers
	bool IAmBeingShotAt;
	bool firstFrameShield;
	bool isAlive;
	
	//possessions
	Weapon myWeapon;
	GunShieldRotation myGunShieldRot;
	Transform enemyTrans;
	vehicleItems myPowerup;
	Vehicle myVehicle;
	CarStat myCarStat;
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
		isAlive				= true;
		PowerupUseStallTime = Random.Range(1f,2f);
		currentStateTime 	= currPowerupTime = Time.time;
		enemyTrans			= FindClosestDriver();
		
		if(transform.GetComponentInChildren<Weapon>())
			myWeapon = transform.GetComponentInChildren<Weapon>();
		if(transform.GetComponentInChildren<GunShieldRotation>())
			myGunShieldRot = transform.GetComponentInChildren<GunShieldRotation>();
		if(transform.GetComponent<vehicleItems>())
			myPowerup = transform.GetComponent<vehicleItems>();
		if(transform.GetComponent<Vehicle>())
			myVehicle = transform.GetComponentInChildren<Vehicle>();
		if(transform.GetComponent<CarStat>())
			myCarStat = transform.GetComponentInChildren<CarStat>();
		
		initialTemp = myCarStat.GetMaxTemp();
		
		StallTime(3f);		
	}
	public void Die()
	{
		isAlive = false;
	}
	public void Revive()
	{
		isAlive = true;
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
		float closestDistance = 999f;
		Transform closestTrans = transform;
		foreach(GameObject go in otherDrivers)
		{
			//go = null when closest enemy is dead
			if(go != null && go.GetComponent<Vehicle>().amAlive())
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
	void FirePowerUp()
	{
		myPowerup.UseItem();
	}
	public void RecievePowerUp()
	{
		currPowerupTime = Time.time;
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
				myState = AI_STATE.SHIELD;
				myGunShieldRot.TurnOnShield();
			}
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
			//attack
			currentTemp = myCarStat.GetCurrTemp();
			tempPercentage = currentTemp / initialTemp;
			
	}
	
	void Update () 
	{
		if(isAlive)
		{
			if( !GetComponent<Vehicle>().isFrozen() ) {
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
				
				//powerups
				if(myPowerup.item != 0 && Time.time - currPowerupTime > PowerupUseStallTime)
				{
					currPowerupTime = Time.time;
					FirePowerUp();			
				}
			}
		}
		else{
			myState = AI_STATE.NONE;
		}
		
		if( !GetComponent<Vehicle>().isFrozen() && !this.gameObject.GetComponent<CarStat>().isOverheated()) {
			switch(myState)
			{
			case AI_STATE.SHOOT:
				if(myWeapon.CanShoot())
				{
					if( this.gameObject.GetComponent<CarStat>().GetCurrTemp() + this.gameObject.GetComponent<CarStat>().FindAttackTempCost() < this.gameObject.GetComponent<CarStat>().GetMaxTemp() )
					{
						myWeapon.fireBullet();
						this.gameObject.GetComponent<Vehicle>().RaiseTemp(true) ;
					}
				}
				break;
			case AI_STATE.SHIELD:
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
		//else
			//myGunShieldRot.TurnOnGun();
	}
}
