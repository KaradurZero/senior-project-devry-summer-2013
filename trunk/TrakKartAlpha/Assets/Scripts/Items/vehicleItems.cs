using UnityEngine;
using System.Collections;

public class vehicleItems : MonoBehaviour {
	
	public int m_item;//NOTE: remove public when not debugging
	int SmallWidth;// = Screen.width/5;
	int SmallHeight;// = Screen.height/10;
	//public bool m_showHud;
	string m_hTriggerSpecific_01;
	string m_hWeapon;
	string m_hShield;
	GameObject[] m_otherVehicles;
	
	public GameObject m_oilSlickObj;
	public GameObject m_homingMissle;
	public GameObject m_freezeShot;
//	public GameObject m_enlargeShield;
//	public GameObject m_deflectorShield;
//	public GameObject m_itemSteal;
	
	float freezeLifespan;
	float freezeSpeed;
	
	// Use this for initialization
	enum items { oilSlick = 1, homingMissle , freezeShot, enlargeShield, deflectorShield, itemSteal};
	void Awake() {
		m_item 					= 0;
		SmallWidth 				= Screen.width/5;
		SmallHeight	 			= Screen.height/10;
		//m_showHud 				= true;
		
		m_hTriggerSpecific_01 	= "Vehicle";
		m_hWeapon 				= "AimCube";
		m_hShield 				= "vehicleShield";
		freezeLifespan 			= 5.0f;
		freezeSpeed				= 20.0f;
	}
	
	void Start () {
		m_otherVehicles = GameObject.FindGameObjectsWithTag(m_hTriggerSpecific_01);
	}
	/// <summary>
	/// call that uses the m_item that is set within this script
	/// </summary>
	public void UseItem() {
		Debug.Log(m_item);
			switch(m_item) {
			case (int)items.oilSlick://use oil slick item
				GameObject droppedItem = (GameObject) Instantiate(m_oilSlickObj, 
					(transform.position - transform.forward * (transform.localScale.y * 2.5f)
					/*vehicle size from front to back assuming car is moving forward by y*/),
					Quaternion.identity);
				droppedItem.GetComponent<OilSlickBehavior>().setIgnoreTarget(this.gameObject) ;
				droppedItem.transform.position = new Vector3( droppedItem.transform.position.x, 0.1f, droppedItem.transform.position.z ) ;
				m_item = 0 ;
				//droppedItem.transform.position -= this.transform.parent.transform.forward * 2;
				break;
			case (int)items.homingMissle://use homing missle item
				Debug.Log("used homingMissle");
				float distance = Mathf.Infinity;
				Vector3 position = transform.position;
				GameObject targetEnemy = null;
				foreach (GameObject go in m_otherVehicles) {
					if(go.transform.position != this.transform.position) {
						Vector3 diff = go.transform.position - position;
						float curDistance = diff.sqrMagnitude;
						if (curDistance < distance) {
							targetEnemy = go;
						    distance = curDistance;
						}
					}
				}
				Vector3 front = transform.position + 
					transform.forward * (this.transform.localScale.y * 1.5f);
				GameObject missile =(GameObject)Instantiate(m_homingMissle, front, Quaternion.identity);
				//missile.GetComponent<MissileMovement>().setIgnoreTarget(this.gameObject.transform.FindChild("vehicleShield").gameObject);
				missile.GetComponent<MissileMovement>().setIgnoreTarget(this.gameObject);
				missile.GetComponent<MissileMovement>().setTarget(targetEnemy);
				missile.GetComponent<MissileMovement>().setRotation(this.transform.rotation);
				m_item = 0 ;
				break;
			case (int)items.freezeShot://use freeze shot item
				Debug.Log("used freezeShot");
				//instantiate freeze bullet and fire in direction that gun is facing.
				//this will rely upon vehicle having the gun working correctly and pointing in a direction
				//logic behind relying upon gun is so that only one script is needed for aiming directional projectiles.
				Vector3 frontOfGun = transform.FindChild(m_hWeapon).transform.position + 
					transform.FindChild(m_hWeapon).transform.forward * (this.transform.localScale.y * 2.5f);
				GameObject freezeShot = (GameObject)Instantiate(m_freezeShot, frontOfGun, Quaternion.identity);
				freezeShot.GetComponent<FreezeShotMovement>().setIgnoreTarget(this.gameObject);
				freezeShot.GetComponent<FreezeShotMovement>().lifespan 			= freezeLifespan;
				freezeShot.GetComponent<FreezeShotMovement>().speed				= freezeSpeed;
				freezeShot.GetComponent<FreezeShotMovement>().direction			= transform.FindChild(m_hWeapon).transform.rotation;
				freezeShot.GetComponent<FreezeShotMovement>().parentMomentum	= Vector3.zero;//or can give it the momentum of the parent as well.
				m_item = 0 ;
				break;
			case (int)items.enlargeShield://use  enlarge shield item
				Debug.Log("used enlargeShield");
				//get script inside parent/child shield object and call enlarge shield function
				this.transform.FindChild(m_hWeapon).FindChild(m_hShield).GetComponent<VehicleShieldController>().enlargeShield();
				m_item = 0 ;
				break;
			case (int)items.deflectorShield://use deflector shield item
				Debug.Log("used deflectorShield");
				//get script inside shield object and call deflector shield function
				this.transform.FindChild(m_hWeapon).FindChild(m_hShield).GetComponent<VehicleShieldController>().setDeflecting();
				m_item = 0 ;
				break;
			case (int)items.itemSteal://use item steal item
				Debug.Log("used itemSteal");
				//todo: get nearest vehicle and get their item number. even if zero.
				distance = Mathf.Infinity;
				position = transform.position;
				targetEnemy = null;
				GameObject[] targetItems = new GameObject[m_otherVehicles.Length] ;
				int numTargets = 0 ;
				foreach (GameObject go in m_otherVehicles) {
					//if object is not this object and object's script has m_item as non-zero
					if(go != this.gameObject ) {
						if( go.GetComponent<vehicleItems>().item != 0) {
							targetItems[numTargets] = go ;
							++numTargets ;
						}
					}
				}
				if( numTargets == 0 )
					m_item = Random.Range(1, 6) ;
				else {
						targetEnemy = targetItems[Random.Range(0, numTargets)] ;
						m_item = targetEnemy.GetComponent<vehicleItems>().m_item ;
						targetEnemy.GetComponent<vehicleItems>().m_item = 0 ;
			}
				break;
			default:
				//do nothing
				break;
			}
			//Debug.Log("Item Used");
		if(gameObject.name == "Player")
			GameObject.Find("powerupDisplay").GetComponent<PowerUpDisplay>().DisplayPowerup(m_item);
	}
	
	public int item {
		get {
			return m_item;
		}
		set {
			m_item = value;
		}
	}
	
	public void Die()
	{
		m_item = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha9))
		{
			if(gameObject.name == "Player") {
				m_item = (int)items.itemSteal ;
				GameObject.Find("powerupDisplay").GetComponent<PowerUpDisplay>().DisplayPowerup(m_item);
			}
			
		}
	}
	

	
//	
//	void OnGUI() {
//		if(m_showHud) {
//			GUI.Box (new Rect (Screen.width - SmallWidth, 0, SmallWidth, SmallHeight), 
//					"Item Type: " + m_item);
//		}
//	}
}
