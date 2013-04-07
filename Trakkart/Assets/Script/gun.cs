using UnityEngine;
using System.Collections;

public class gun : MonoBehaviour {
	
	public GameObject m_bullet, m_bulletBig;
	Vector3 m_direction ;
	float m_coolDownTime = 0f;
	public bool m_canShoot;
	Collider m_playerCollider;
	public enum FIRETYPE { SINGLE, SHOTGUN, MACHINE_GUN, BIG_GUN }
	public FIRETYPE m_gunState;
	
	private bool isFiring = false;
	private string[] clipNames = new string[] {"DG Chugs-13", "DGD Arpeggiated-03", "GDG Legato-03", "DGD Chugs-25"};
	
	void Start () {
		m_playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider>();
		m_gunState = FIRETYPE.SINGLE;
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
		//transform.position = new Vector3(m_playerCollider.transform.position.x, 0.6f, m_playerCollider.transform.position.z) ;
		
		//Debug.Log (m_playerCollider.rigidbody.velocity.magnitude);
		//Debug.Log (m_direction);
		
		if(m_coolDownTime > 0f){
			m_coolDownTime -= Time.deltaTime;
			m_canShoot = false;
		}
		else
			m_canShoot = true;
		
		//if(Input.GetKey(KeyCode.Space))
		//{
			//if(m_canShoot)
				//Shoot();
		//}
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
	
	public void Rotate( Vector3 a_direction )
	{
		if (a_direction != Vector3.zero){
			float MoveRotate = 5f * Time.deltaTime;
			Quaternion newRotation = Quaternion.LookRotation(a_direction);
			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, MoveRotate);
			m_direction = a_direction ;
		}	
	}
	
	public void Shoot( )
	{	
		if( m_canShoot ) {
			switch(m_gunState)
			{
			case FIRETYPE.SINGLE:
				m_coolDownTime = 1f;
				GameObject shotFiredSingle = (GameObject) Instantiate(m_bullet,transform.position, transform.rotation);
				//Physics.IgnoreCollision(shotFiredSingle.collider, m_playerCollider);
				//rBody = shotFiredSingle.AddComponent<Rigidbody>();
				//Physics.IgnoreCollision(
				
				//shotFiredSingle.rigidbody.velocity = (rigidbody.velocity * 100f) ;
				//Debug.Log (rigidbody.velocity);
				
				//shotFiredSingle.AddComponent<bullet>();
				shotFiredSingle.GetComponent<bullet>().SetDirection(m_direction, m_playerCollider.rigidbody.velocity.magnitude);
				shotFiredSingle.GetComponent<bullet>().SetDeathTime(2.5f);
				break;
			case FIRETYPE.MACHINE_GUN:
				m_coolDownTime = .3f;
				GameObject shotFiredMachineGun = (GameObject) Instantiate(m_bullet,transform.position, transform.rotation);
				Physics.IgnoreCollision(shotFiredMachineGun.collider, m_playerCollider);
				
				//shotFiredMachineGun.AddComponent<bullet>();
				shotFiredMachineGun.GetComponent<bullet>().SetDirection(m_direction, m_playerCollider.rigidbody.velocity.magnitude);
				shotFiredMachineGun.GetComponent<bullet>().SetDeathTime(2.5f);
				break;
			case FIRETYPE.SHOTGUN:
				m_coolDownTime = 2f;
				for(int i = 0; i < 8; i++)
				{
					GameObject shotFiredShotgun =(GameObject) Instantiate(m_bullet,transform.position, transform.rotation);
					Physics.IgnoreCollision(shotFiredShotgun.collider, m_playerCollider);
					
					//shotFiredShotgun.AddComponent<bullet>();
					shotFiredShotgun.GetComponent<bullet>().SetDirection(m_direction, m_playerCollider.rigidbody.velocity.magnitude);
					shotFiredShotgun.GetComponent<bullet>().SetDeathTime(1f);
				}
				break;
			case FIRETYPE.BIG_GUN:
				m_coolDownTime = 1f;
				GameObject shotFiredBigGun = (GameObject) Instantiate(m_bulletBig,transform.position, transform.rotation);
				Physics.IgnoreCollision(shotFiredBigGun.collider, m_playerCollider);
				
				//shotFiredBigGun.AddComponent<bullet>();
				shotFiredBigGun.GetComponent<bullet>().SetDirection(m_direction, m_playerCollider.rigidbody.velocity.magnitude);
				shotFiredBigGun.GetComponent<bullet>().SetDeathTime(2f);
				break;
			}
		}
		
		//SoundPalette.PlaySound("Gunshot/" + clipNames[(int)m_gunState], 66f);
		
//		if(!isFiring)
//			StartCoroutine(waitForSound(SoundPalette.PlaySound("Gunshot/" + clipNames[(int)m_gunState])));
	}
	
	IEnumerator waitForSound(AudioSource playMe)
	{
		while(isFiring = playMe.isPlaying)
			yield return null;
	}
	
}
