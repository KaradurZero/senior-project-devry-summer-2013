using UnityEngine;
using System.Collections;

public class GarageStatManager : MonoBehaviour {
	
	public Material
		unfilled,
		filled;
	public GameObject[]
		speedObjs,
		accelObjs,
		brakeObjs,
		boostObjs,
		weightObjs,
		luckObjs,
		attackObjs,
		defenseObjs;
	int 
		speedVal,
		accelVal,
		brakeVal,
		boostVal,
		weightVal,
		luckVal,
		attackVal,
		defenseVal;
	int[] 
		speedCost,
		accelCost,
		brakeCost,
		boostCost,
		weightCost,
		luckCost,
		attackCost,
		defenseCost;
	
	void Start () 
	{
		speedObjs 	= new GameObject[5];
		accelObjs 	= new GameObject[5];
		brakeObjs 	= new GameObject[5];
		boostObjs 	= new GameObject[5];
		weightObjs 	= new GameObject[5];
		luckObjs 	= new GameObject[5];
		attackObjs 	= new GameObject[5];
		defenseObjs = new GameObject[5];
		speedCost	= new int[5];
		accelCost	= new int[5];
		brakeCost	= new int[5];
		boostCost	= new int[5];
		weightCost	= new int[5];
		luckCost	= new int[5];
		attackCost	= new int[5];
		defenseCost	= new int[5];
		speedVal = accelVal = brakeVal = boostVal = weightVal = luckVal = attackVal = defenseVal = 0;
		speedCost[0] = 100;		speedCost[1] = 350;		speedCost[2] = 700;		speedCost[3] = 1150;		speedCost[4] = 1700;	
		accelCost[0] = 100;		accelCost[1] = 350;		accelCost[2] = 700;		accelCost[3] = 1150;		accelCost[4] = 1700;		
		brakeCost[0] = 100;		brakeCost[1] = 350;		brakeCost[2] = 700;		brakeCost[3] = 1150;		brakeCost[4] = 1700;
		boostCost[0] = 100;		boostCost[1] = 350;		boostCost[2] = 700;		boostCost[3] = 1150;		boostCost[4] = 1700;
		weightCost[0] = 100;	weightCost[1] = 350;	weightCost[2] = 700;	weightCost[3] = 1150;		weightCost[4] = 1700;
		luckCost[0] = 100;		luckCost[1] = 350;		luckCost[2] = 700;		luckCost[3] = 1150;			luckCost[4] = 1700;
		attackCost[0] = 100;	attackCost[1] = 350;	attackCost[2] = 700;	attackCost[3] = 1150;		attackCost[4] = 1700;
		defenseCost[0] = 100;	defenseCost[1] = 350;	defenseCost[2] = 700;	defenseCost[3] = 1150;		defenseCost[4] = 1700;
		
		
		for(int i = 0; i < 5; i++)
		{
			speedObjs[i] 	= GameObject.Find("speed_" + (i + 1));
			accelObjs[i] 	= GameObject.Find("accel_" + (i + 1));
			brakeObjs[i] 	= GameObject.Find("brake_" + (i + 1));
			boostObjs[i] 	= GameObject.Find("boost_" + (i + 1));
			weightObjs[i] 	= GameObject.Find("weight_" + (i + 1));
			luckObjs[i] 	= GameObject.Find("luck_" + (i + 1));
			attackObjs[i] 	= GameObject.Find("attack_" + (i + 1));
			defenseObjs[i] 	= GameObject.Find("defense_" + (i + 1));
		}
	}
	public bool CanUpgradeStat(string statName)
	{
		switch(statName)
		{
		case "Speed":	return (speedVal < 5);		break;
		case "Accel":	return (accelVal < 5);		break;
		case "Brake":	return (brakeVal < 5);		break;
		case "Boost":	return (boostVal < 5);		break;
		case "Weight":	return (weightVal < 5);		break;
		case "Luck":	return (luckVal < 5);		break;
		case "Attack":	return (attackVal < 5);		break;
		case "Defense":	return (defenseVal < 5);	break;
		default:	
			Debug.Log("CanUpgradeStat out of range");
			return false;
			break;
		}
	}
	public void UpgradeStat(string statName)
	{
		switch(statName)
		{
		case "Speed":
			speedVal++;
			for(int i = 0; i < speedVal; i++)
				speedObjs[i].renderer.material = filled;
			break;
		case "Accel":
			accelVal++;
			for(int i = 0; i < accelVal; i++)
				accelObjs[i].renderer.material = filled;
			break;
		case "Brake":
			brakeVal++;
			for(int i = 0; i < brakeVal; i++)
				brakeObjs[i].renderer.material = filled;
			break;
		case "Boost":
			boostVal++;
			for(int i = 0; i < boostVal; i++)
				boostObjs[i].renderer.material = filled;
			break;
		case "Weight":
			weightVal++;
			for(int i = 0; i < weightVal; i++)
				weightObjs[i].renderer.material = filled;
			break;
		case "Luck":
			luckVal++;
			for(int i = 0; i < luckVal; i++)
				luckObjs[i].renderer.material = filled;
			break;
		case "Attack":
			attackVal++;
			for(int i = 0; i < attackVal; i++)
				attackObjs[i].renderer.material = filled;
			break;
		case "Defense":
			defenseVal++;
			for(int i = 0; i < defenseVal; i++)
				defenseObjs[i].renderer.material = filled;
			break;
		default:	
			Debug.Log("UpgradeStat out of range");
			break;
		}
	}
	public int GetStatCost(string statName)
	{
		switch(statName)
		{
		case "Speed":
			if(speedVal < 5)
				return speedCost[speedVal];
			else
				return 0;
			break;
		case "Accel":
			if(accelVal < 5)
				return accelCost[accelVal];
			else
				return 0;
			break;
		case "Brake":
			if(brakeVal < 5)
				return brakeCost[brakeVal];
			else
				return 0;
			break;
		case "Boost":
			if(boostVal < 5)
				return boostCost[boostVal];
			else
				return 0;
			break;
		case "Weight":
			if(weightVal < 5)
				return weightCost[weightVal];
			else
				return 0;
			break;
		case "Luck":
			if(luckVal < 5)
				return luckCost[luckVal];
			else
				return 0;
			break;
		case "Attack":
			if(attackVal < 5)
				return attackCost[attackVal];
			else
				return 0;
			break;
		case "Defense":
			if(defenseVal < 5)
				return defenseCost[defenseVal];
			else
				return 0;
			break;
		default:	
			Debug.Log("GetStatCost out of range");
			return 0;
			break;
		}
	}
}
