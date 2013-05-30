using UnityEngine;
using System.Collections;

public class MenuStatsFromGame : MonoBehaviour {
	public int 
		gold,
		speed,
		accel,
		temp,
		boost,
		health,
		luck,
		attack,
		defense;
	
	void Start () 
	{
//		gold = speed = accel = temp = boost = health = luck = attack = defense = 0;
	}
	public void SetGold(int a_gold)
	{
		gold = a_gold;
	}
	public void SetStats(int a_speed, int a_accel, int a_temp, int a_boost, int a_health, int a_luck, int a_attack, int a_defense)
	{
		speed= a_speed;
		accel = a_accel;
		temp = a_temp;
		boost = a_boost;
		health = a_health;
		luck = a_luck;
		attack = a_attack;
		defense = a_defense;	
	}
	public int GetSpeed() 	{ return speed;		}
	public int GetAccel() 	{ return accel;		}
	public int GetTemp() 	{ return temp;		}
	public int GetBoost() 	{ return boost;		}
	public int GetHealth() 	{ return health;	}
	public int GetLuck() 	{ return luck;		}
	public int GetAttack() 	{ return attack;	}
	public int GetDefense() { return defense;	}
	public int GetGold() 	{ return gold;		}
}
