using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public float cooldownTime;
	float currTime;
	public GameObject m_bullet;
	public float a_bulletSpeed1;
	public float a_bullet1LifespanMin, a_bullet1LifespanMax;
	public bool isUsingMomentum;
	
	void Awake() {
		a_bulletSpeed1 			= 20.0f;
		a_bullet1LifespanMin	= 3.0f;
		a_bullet1LifespanMax 	= 8.0f;
		isUsingMomentum 		= true;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
/*		if(Input.GetMouseButtonDown(0)) {
			if(transform.GetComponent<GunShieldRotation>().isUsingGun) {
				fireBullet();
			}
		}
	*/
//		if(m_bullet) {
//			Debug.Log( m_bullet.transform.rotation.ToString());
//		}
	}
	
	public void fireBullet() {
		if(Time.time - currTime > cooldownTime)
		{
			currTime = Time.time;
			GameObject projectile = (GameObject) Instantiate(m_bullet, transform.position, Quaternion.identity);
	//		Physics.IgnoreCollision(projectile.collider, transform.root.collider);
	//		Debug.Log(transform.root.transform.name);
			//Physics.IgnoreCollision(transform.collider,projectile.collider);
			projectile.GetComponent<BulletUpdate>().DoNotCollideWith(transform.parent.transform);
			projectile.GetComponent<BulletUpdate>().speed 			= a_bulletSpeed1;
			projectile.GetComponent<BulletUpdate>().direction 		= this.transform.transform.eulerAngles.y;
			projectile.GetComponent<BulletUpdate>().lifespan 		= 
			Random.Range(a_bullet1LifespanMin, a_bullet1LifespanMax);
		//	projectile.GetComponent<BulletUpdate>().parentMomentum	=
		//		transform.parent.GetComponent<basicMovementTesting>().m_currentVelocity;
		/** basicMovementTesting is just the movement script. grab the current velocity of the parent
		 * by looking into that script and getting the value stored there (must be public)*/
		}
	}
}
