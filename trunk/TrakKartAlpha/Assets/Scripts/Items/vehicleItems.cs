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
	/// mostly AI specific call where AI passes in value to use that item.
	/// </summary>
	/// <param name='a_item'>
	/// more AI specific item pass in.
	/// </param>
//	public void useItem(int a_item) {
////		if( m_item != 0 && Input.GetKeyDown(KeyCode.Space)) {
//			//use item
//			switch(a_item) {
//			case (int)items.oilSlick://use oil slick item
//				GameObject droppedItem = (GameObject) Instantiate(m_oilSlickObj, 
//					(transform.position - transform.forward * (this.transform.localScale.y * 1.2f/*scaled to be slightly larger than vehicle to drop slick behind vehicle*/)
//					/*vehicle size from front to back assuming car is moving forward by y*/),
//					Quaternion.identity);
//				//droppedItem.transform.position -= this.transform.parent.transform.forward * 2;
//				break;
//			case (int)items.homingMissle://use homing missle item
//				Debug.Log("used homingMissle");
//				float distance = Mathf.Infinity;
//				Vector3 position = transform.position;
//				GameObject targetEnemy = null;
//				foreach (GameObject go in m_otherVehicles) {
//					if(go.transform.position != this.transform.position) {
//						Vector3 diff = go.transform.position - position;
//						float curDistance = diff.sqrMagnitude;
//						if (curDistance < distance) {
//							targetEnemy = go;
//						    distance = curDistance;
//						}
//					}
//				}
//				GameObject missile =(GameObject)Instantiate(m_homingMissle, transform.position, Quaternion.identity);
//				missile.GetComponent<MissileMovement>().setIgnoreTarget(this.gameObject);
//				missile.GetComponent<MissileMovement>().setTarget(targetEnemy);
//				missile.GetComponent<MissileMovement>().setRotation(this.transform.rotation);
//				break;
//			case (int)items.freezeShot://use freeze shot item
//				Debug.Log("used freezeShot");
//				//instantiate freeze bullet and fire in direction that gun is facing.
//				//this will rely upon vehicle having the gun working correctly and pointing in a direction
//				//logic behind relying upon gun is so that only one script is needed for aiming directional projectiles.
//				Vector3 frontOfGun = transform.FindChild(m_hWeapon).transform.position + 
//					transform.FindChild(m_hWeapon).transform.forward * (this.transform.localScale.y * 1.3f);
//				GameObject freezeShot = (GameObject)Instantiate(m_freezeShot, frontOfGun, Quaternion.identity);
//				freezeShot.GetComponent<FreezeShotMovement>().lifespan 			= freezeLifespan;
//				freezeShot.GetComponent<FreezeShotMovement>().speed				= freezeSpeed;
//				freezeShot.GetComponent<FreezeShotMovement>().direction			= transform.FindChild(m_hWeapon).transform.eulerAngles.y;
//				freezeShot.GetComponent<FreezeShotMovement>().parentMomentum	= Vector3.zero;//or can give it the momentum of the parent as well.
//				break;
//			case (int)items.enlargeShield://use  enlarge shield item
//				Debug.Log("used enlargeShield");
//				//get script inside parent/child shield object and call enlarge shield function
//				this.transform.FindChild(m_hWeapon).FindChild(m_hShield).GetComponent<VehicleShieldController>().enlargeShield();
//				break;
//			case (int)items.deflectorShield://use deflector shield item
//				Debug.Log("used deflectorShield");
//				//get script inside shield object and call deflector shield function
//				this.transform.FindChild(m_hWeapon).FindChild(m_hShield).GetComponent<VehicleShieldController>().setDeflecting();
//				break;
//			case (int)items.itemSteal://use item steal item
//				Debug.Log("used itemSteal");
//				//todo: get nearest vehicle and get their item number. even if zero.
//				distance = Mathf.Infinity;
//				position = transform.position;
//				targetEnemy = null;
//				foreach (GameObject go in m_otherVehicles) {
//					//if object is not this object and object's script has m_item as non-zero
//					if(go.transform.position != this.transform.position && go.GetComponent<vehicleItems>().item != 0) {
//						Vector3 diff = go.transform.position - position;
//						float curDistance = diff.sqrMagnitude;
//						if (curDistance < distance) {
//							targetEnemy = go;
//						    distance = curDistance;
//						}
//					}
//				}
//				if(targetEnemy) {
//					m_item = targetEnemy.GetComponent<vehicleItems>().item;
//					targetEnemy.GetComponent<vehicleItems>().item = 0;
//					Debug.Log("item stolen: " + m_item);
//				}
//				else {
//					Debug.Log("no item stolen");
//				}
//				break;
//			default:
//				//do nothing
//				break;
//			}
//			//Debug.Log("Item Used");
//			m_item = 0;
////		}
//	}
	/// <summary>
	/// call that uses the m_item that is set within this script
	/// </summary>
	public void UseItem() {
		//Debug.Log(m_item);
		bool didSteal = false;
			switch(m_item) {
			case (int)items.oilSlick://use oil slick item
				GameObject droppedItem = (GameObject) Instantiate(m_oilSlickObj, 
					(transform.position - transform.forward * (transform.localScale.y * 2.5f)
					/*vehicle size from front to back assuming car is moving forward by y*/),
					Quaternion.identity);
				droppedItem.GetComponent<OilSlickBehavior>().setIgnoreTarget(this.gameObject) ;
				droppedItem.transform.position = new Vector3( droppedItem.transform.position.x, 0.1f, droppedItem.transform.position.z ) ;
				
				//droppedItem.transform.position -= this.transform.parent.transform.forward * 2;
				break;
			case (int)items.homingMissle://use homing missle item
				//Debug.Log("used homingMissle");
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
				break;
			case (int)items.freezeShot://use freeze shot item
				//Debug.Log("used freezeShot");
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
				break;
			case (int)items.enlargeShield://use  enlarge shield item
//				Debug.Log("used enlargeShield");
				//get script inside parent/child shield object and call enlarge shield function
				this.transform.FindChild(m_hWeapon).FindChild(m_hShield).GetComponent<VehicleShieldController>().enlargeShield();
				break;
			case (int)items.deflectorShield://use deflector shield item
//				Debug.Log("used deflectorShield");
				//get script inside shield object and call deflector shield function
				this.transform.FindChild(m_hWeapon).FindChild(m_hShield).GetComponent<VehicleShieldController>().setDeflecting();
				break;
			case (int)items.itemSteal:
				item = Random.Range(1, 6);
			break;
			//use item steal item
//				Debug.Log("used itemSteal");
				//todo: get nearest vehicle and get their item number. even if zero.
/*				distance = Mathf.Infinity;
				position = transform.position;
				targetEnemy = null;
				foreach (GameObject go in m_otherVehicles) {
					//if object is not this object and object's script has m_item as non-zero
					if(go.transform.position != this.transform.position) {
						if( go.GetComponent<vehicleItems>().item != 0) {
							didSteal = true;
							Vector3 diff = go.transform.position - position;
							float curDistance = diff.sqrMagnitude;
							if (curDistance < distance) {//set this target as closer
								targetEnemy = go;
						   	 	distance = curDistance;
							}
						}
					}
				}
*/				if(targetEnemy) {
					m_item = targetEnemy.GetComponent<vehicleItems>().item;
					targetEnemy.GetComponent<vehicleItems>().item = 0;
//					Debug.Log("item stolen: " + m_item);
				}
				else {
//					Debug.Log("no item stolen");
				}
				break;
			default:
				//do nothing
				break;
			}
			//Debug.Log("Item Used");
		if(gameObject.name == "Player")
			GameObject.Find("powerupDisplay").GetComponent<PowerUpDisplay>().DisplayPowerup(m_item);
	//	if(!didSteal)
	//	{
	//		m_item = 0;
	//		if(gameObject.name == "Player")
	//			GameObject.Find("powerupDisplay").GetComponent<PowerUpDisplay>().DisplayPowerup(0);
	//	}
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
		
	}
	

	
//	
//	void OnGUI() {
//		if(m_showHud) {
//			GUI.Box (new Rect (Screen.width - SmallWidth, 0, SmallWidth, SmallHeight), 
//					"Item Type: " + m_item);
//		}
//	}
}
