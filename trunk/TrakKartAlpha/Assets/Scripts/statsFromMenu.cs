using UnityEngine;
using System.Collections;

public class statsFromMenu : MonoBehaviour {
	int 
		speed,
		accel,
		brake,
		boost,
		weight,
		luck,
		attack,
		defense;
	
	void Start () 
	{
		speed = accel = brake = boost = weight = luck = attack = defense = 0;
	}
	public void SetStats(int a_speed, int a_accel, int a_brake, int a_boost, int a_weight, int a_luck, int a_attack, int a_defense)
	{
		speed= a_speed;
		accel = a_accel;
		brake = a_brake;
		boost = a_boost;
		weight = a_weight;
		luck = a_luck;
		attack = a_attack;
		defense = a_defense;	
	}
	public int GetSpeed() 	{ return speed;		}
	public int GetAccel() 	{ return accel;		}
	public int GetBrake() 	{ return brake;		}
	public int GetBoost() 	{ return boost;		}
	public int GetWeight() 	{ return weight;	}
	public int GetLuck() 	{ return luck;		}
	public int GetAttack() 	{ return attack;	}
	public int GetDefese() 	{ return defense;	}
}
