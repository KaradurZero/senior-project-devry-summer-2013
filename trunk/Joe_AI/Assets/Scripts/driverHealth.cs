using UnityEngine;
using System.Collections;

public class driverHealth : MonoBehaviour {

	public int initHealth;
	public int health;
	
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
		Destroy(gameObject);
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
//		if(hasDied)
//		{
//			if(Time.time - currRespawnTime > respawnTime)
//			{
//				currRespawnTime = Time.time;
//				hasDied = false;
//				gameObject.SetActive(true);
//				ReplenishHealthFull();
//			}
//		}
	}
}
