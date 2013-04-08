using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {
	
	float m_velocity ;
	
	Vector3 m_direction;
	bool m_willDie = false, m_amIBigBullet = false;
	float m_deathClock;
	float m_bulletStrength = 1f ;
	void Start()
	{
		if(gameObject.name == "bulletBig")
		{
			m_amIBigBullet = true;
			m_bulletStrength = 100f;
		}
	}
	
	public void SetDirection( Vector3 a_direction, float a_velocity )
	{
		m_direction = a_direction ;
		//rigidbody.velocity = m_direction * (a_velocity+100f);
		
		rigidbody.velocity = transform.TransformDirection((Vector3.forward) * (a_velocity + 200f));
		//Debug.Log (m_direction);
	}
	
	public void SetDeathTime(float a_deathTimeCap)
	{
		m_willDie = true;
		m_deathClock = a_deathTimeCap - Random.Range(0f,1f);
	}
	void OnCollisionEnter(Collision other){
		
		if(other.transform.tag == "enemy")
		{
			other.gameObject.GetComponent<CarStats>().TakeDamage(m_bulletStrength);
			if(!m_amIBigBullet)
			{
				//Debug.Log("HIT HERE");
			}
		}
		
		Die();
	}
	void Update () {
		if(m_willDie)
		{
			if(m_deathClock <= 0f)
				Die();
			m_deathClock -= Time.deltaTime;
		}
		//transform.position += transform.forward * m_velocity ;
	}
	
	void Die(){
		Destroy(gameObject);
	}	
}
