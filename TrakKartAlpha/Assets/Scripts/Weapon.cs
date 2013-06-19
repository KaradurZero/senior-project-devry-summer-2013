using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public AudioClip turret;
	
	public float m_coolDownTime;
	public GameObject m_bullet;
	public float a_bulletSpeed1;
	public float a_bullet1LifespanMin, a_bullet1LifespanMax;
	public bool isUsingMomentum;
	private bool m_canShoot ;
	
	void Awake() {
		a_bulletSpeed1 			= 20.0f;
		a_bullet1LifespanMin	= 3.0f;
		a_bullet1LifespanMax 	= 8.0f;
		isUsingMomentum 		= true;
		m_canShoot				= false ;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(m_coolDownTime > 0f){
			m_coolDownTime -= Time.deltaTime;
			m_canShoot = false;
		}
		else
			m_canShoot = true;
	}
	
	public void fireBullet() {
		if(m_canShoot)
		{
		
			audio.PlayOneShot(turret);
			
			m_coolDownTime = 0.5f ;
			GameObject projectile = (GameObject) Instantiate(m_bullet, transform.position + (transform.forward * 1.5f),new Quaternion(
				90f, m_bullet.transform.rotation.y, m_bullet.transform.rotation.z, m_bullet.transform.rotation.w));

			projectile.GetComponent<BulletUpdate>().DoNotCollideWith(this.transform.parent.gameObject);
			projectile.GetComponent<BulletUpdate>().speed 			= a_bulletSpeed1;
			projectile.GetComponent<BulletUpdate>().direction 		= this.transform.transform.eulerAngles.y;
			projectile.GetComponent<BulletUpdate>().lifespan 		= 
			Random.Range(a_bullet1LifespanMin, a_bullet1LifespanMax);
			if(transform.root.name == "Player")
				projectile.GetComponent<BulletUpdate>().isPlayers = true;
			else
				projectile.GetComponent<BulletUpdate>().isPlayers = false;
		}
	}
	
	public bool CanShoot() {return m_canShoot;}
}
