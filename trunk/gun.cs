using UnityEngine;
using System.Collections;

public class gun : MonoBehaviour {
	
	public GameObject m_bullet, m_bulletBig;
	float m_coolDownTime = 0f;
	public bool m_canShoot;
	Collider m_playerCollider;
	public enum FIRETYPE { SINGLE, SHOTGUN, MACHINE_GUN, BIG_GUN }
	public FIRETYPE m_gunState;
	
	private bool isFiring = false;
	private string[] clipNames = new string[] {"DG Chugs-13", "DGD Arpeggiated-03", "GDG Legato-03", "DGD Chugs-25"};
	
	void Start () {
		m_playerCollider = GameObject.Find("player").GetComponent<Collider>();
		m_gunState = FIRETYPE.MACHINE_GUN;
	}
	
	public int GetGunType()
	{
		switch(m_gunState)
		{
		case FIRETYPE.SINGLE:		return 1;
		case FIRETYPE.MACHINE_GUN:	return 2;
		case FIRETYPE.SHOTGUN:		return 3;
		case FIRETYPE.BIG_GUN:		return 4;
		default:	return 0;
		}
	}
	void Update () {
		if(m_coolDownTime > 0f){
			m_coolDownTime -= Time.deltaTime;
			m_canShoot = false;
		}
		else
			m_canShoot = true;
		if(Input.GetKey(KeyCode.Space))
		{
			if(m_canShoot)
				Shoot ();
		}
		if(Input.GetKey(KeyCode.Alpha1))
		{
			m_gunState = FIRETYPE.SINGLE;
		}
		if(Input.GetKey(KeyCode.Alpha2))
		{
			m_gunState = FIRETYPE.MACHINE_GUN;
		}
		if(Input.GetKey(KeyCode.Alpha3))
		{
			m_gunState = FIRETYPE.SHOTGUN;
		}
		if(Input.GetKey(KeyCode.Alpha4))
		{
			m_gunState = FIRETYPE.BIG_GUN;
		}
	}
	
	void Shoot()
	{
		switch(m_gunState)
		{
		case FIRETYPE.SINGLE:
			m_coolDownTime = 1f;
			GameObject shotFiredSingle = (GameObject) Instantiate(m_bullet,transform.position, Quaternion.identity);
			Physics.IgnoreCollision(shotFiredSingle.collider, m_playerCollider);
			//rBody = shotFiredSingle.AddComponent<Rigidbody>();
			//Physics.IgnoreCollision(
			shotFiredSingle.AddComponent<bullet>();
			shotFiredSingle.GetComponent<bullet>().SetDirection(.1f, .005f);
			break;
		case FIRETYPE.MACHINE_GUN:
			m_coolDownTime = .25f;
			GameObject shotFiredMachineGun = (GameObject) Instantiate(m_bullet,transform.position, Quaternion.identity);
			Physics.IgnoreCollision(shotFiredMachineGun.collider, m_playerCollider);
			shotFiredMachineGun.AddComponent<bullet>();
			shotFiredMachineGun.GetComponent<bullet>().SetDirection(.1f,.5f);
			break;
		case FIRETYPE.SHOTGUN:
			m_coolDownTime = 2f;
			for(int i = 0; i < 8; i++)
			{
				GameObject shotFiredShotgun = (GameObject) Instantiate(m_bullet,transform.position, Quaternion.identity);
				Physics.IgnoreCollision(shotFiredShotgun.collider, m_playerCollider);
				Physics.IgnoreCollision(shotFiredShotgun.collider, m_playerCollider);
				shotFiredShotgun.AddComponent<bullet>();
				shotFiredShotgun.GetComponent<bullet>().SetDirection(.1f, .01f * i);
				shotFiredShotgun.GetComponent<bullet>().SetDeathTime(2.5f);
			}
			break;
		case FIRETYPE.BIG_GUN:
			m_coolDownTime = 1f;
			GameObject shotFiredBigGun = (GameObject) Instantiate(m_bulletBig,transform.position, Quaternion.identity);
			Physics.IgnoreCollision(shotFiredBigGun.collider, m_playerCollider);
			shotFiredBigGun.AddComponent<bullet>();
			shotFiredBigGun.GetComponent<bullet>().SetDirection(.05f,.005f);
			break;
		}
		
		SoundPalette.PlaySound("Gunshot/" + clipNames[(int)m_gunState], 66f);
		
//		if(!isFiring)
//			StartCoroutine(waitForSound(SoundPalette.PlaySound("Gunshot/" + clipNames[(int)m_gunState])));
	}
	
	IEnumerator waitForSound(AudioSource playMe)
	{
		while(isFiring = playMe.isPlaying)
			yield return null;
	}
	
}
