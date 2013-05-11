using UnityEngine;
using System.Collections;

public class statsFromMenu : MonoBehaviour {
	int 
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
		speed = accel = temp = boost = health = luck = attack = defense = 0;
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
	public int GetSpeed() 	{ return speed    + 1;		}
	public int GetAccel() 	{ return accel    + 1;		}
	public int GetTemp() 	{ return temp     + 1;		}
	public int GetBoost() 	{ return boost    + 1;		}
	public int GetHealth() 	{ return health   + 1;		}
	public int GetLuck() 	{ return luck	  + 1;		}
	public int GetAttack() 	{ return attack   + 1;		}
	public int GetDefense() { return defense  + 1;		}
}
