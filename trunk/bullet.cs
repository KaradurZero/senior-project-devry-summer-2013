using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {
	
	Vector3 m_direction = Vector3.zero;
	bool m_willDie = false, m_amIBigBullet = false;
	float m_deathClock;
	float m_bulletStrength = 1f;
	void Start()
	{
		if(gameObject.name == "bulletBig")
		{
			m_amIBigBullet = true;
			m_bulletStrength = 100f;
		}
	}
	public void SetDirection(float a_xSpeed, float a_randAmount)
	{
		float ySpeed = Random.Range(a_randAmount * -1f, a_randAmount * 1f);
		m_direction = new Vector3(a_xSpeed,ySpeed, 0f);
	}
	public void SetDeathTime(float a_deathTimeCap)
	{
		m_willDie = true;
		m_deathClock = a_deathTimeCap - Random.Range(0f,1f);
	}
	void OnCollisionEnter(Collision collision){
		
		if(collision.transform.tag == "enemy")
		{
			collision.gameObject.GetComponent<HitPoints>().TakeDamage(m_bulletStrength);
			if(!m_amIBigBullet)
			{
				Debug.Log("HIT HERE");
				Destroy(gameObject);
			}
		}
	}
	void Update () {
		if(m_willDie)
		{
			if(m_deathClock <= 0f)
				Die();
			m_deathClock -= Time.deltaTime;
		}
		transform.position += m_direction * Time.timeScale;
	}
	
	void OnBecameInvisible(){
		Die ();
	}
	
	void Die(){
		Destroy(gameObject);
	}	
}
