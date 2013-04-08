using UnityEngine;
using System.Collections;

public class driverHealth : MonoBehaviour {

	public int initHealth;
	public int health;
	public Object explosion;
	//respawn
	bool hasDied;
	float respawnTime, currRespawnTime;
	
	public void DealDamage(int damage)
	{
		health -= damage;
		if(health < 0)
			Die();
	}
	void Die()
	{
		hasDied = true;
		currRespawnTime = Time.time;
		if(transform.GetComponent<AIDriver>() != null)
			transform.GetComponent<AIDriver>().Die();
		if(transform.GetComponent<Vehicle>() != null)
			transform.GetComponent<Vehicle>().Die();
		if(transform.GetComponent<AIState>() != null)
			transform.GetComponent<AIState>().Die();
		if(transform.GetComponent<vehicleItems>() != null)
			transform.GetComponent<vehicleItems>().Die();
		GameObject splosion = (GameObject) Instantiate(explosion, transform.position,Quaternion.identity);
		splosion.transform.LookAt(Camera.main.transform.position);
	}
	void Revive()
	{
		if(transform.GetComponent<AIDriver>() != null)
			transform.GetComponent<AIDriver>().Revive();
		if(transform.GetComponent<Vehicle>() != null)
			transform.GetComponent<Vehicle>().Revive();
		if(transform.GetComponent<AIState>() != null)
			transform.GetComponent<AIState>().Revive();
	}
	void ReplenishHealth(int heal)
	{
		health += heal;
	}
	void ReplenishHealthFull()
	{
		health = initHealth;
	}
	
	void Start () {
		ReplenishHealthFull();
		hasDied = false;
		respawnTime = 5f;
		currRespawnTime = Time.time;
	}
	
	void Update()
	{
		if(hasDied)
		{
			if(Time.time - currRespawnTime > respawnTime)
			{
				hasDied = false;
				Revive();
				ReplenishHealthFull();
			}
		}
	}
}
