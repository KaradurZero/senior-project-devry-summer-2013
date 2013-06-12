using UnityEngine;
using System.Collections;

//************************************FOLLOW THIS OBJECT****************************************//
//camera behavior which follows the target object while maintaining a top-view on the game screen

public class FollowThisObject : MonoBehaviour {
	public GameObject parentObject;
	private GameObject[] vehicleList ;
	public float deltaX, deltaY, deltaZ;
	float min_size = 5f ;
	float max_size = 10f ;
	float default_size = 15f ;
	public float switchTime = 5f ;
	int i = 0 ;
	Vector3 targetPos;
	
	void Start() {
		//find list of vehicles
		vehicleList = GameObject.FindGameObjectsWithTag("Vehicle") ;
	}
	
	void Update()
	{
		//move camera based on target object's position
		transform.position = parentObject.transform.position;
		transform.position += new Vector3(deltaX, deltaY, deltaZ);
		targetPos = transform.position;
		
		//if AIDriver is not enabled or doesn't exist, always follow player
		if( parentObject.GetComponent<AIDriver>() == null ) {
			
			//camera zooms based on player's speed
			if(!(parentObject.GetComponent<Vehicle>().isBoosted())) {
					transform.position = new Vector3(transform.position.x,
						default_size + (min_size * (parentObject.GetComponent<CarStat>().GetCurrentSpeed() / 
						parentObject.GetComponent<CarStat>().GetMaxVelocity())), transform.position.z) ;
			}
			else  {
					transform.position = new Vector3(transform.position.x,
						default_size + (max_size * (parentObject.GetComponent<CarStat>().GetCurrentSpeed() / 
						parentObject.GetComponent<CarStat>().GetMaxVelocity())),transform.position.z) ;
			}
		}
		else {
			//camera switches view between drivers overtime
			if( switchTime > 0 ) {
				switchTime -= Time.deltaTime ;
			}
			else {
				parentObject = vehicleList[i] ;
				++i ;
				if( i > vehicleList.Length - 1 )
					i = 0 ;
				switchTime = 5f ;
			}
			transform.position = new Vector3(transform.position.x, default_size, transform.position.z) ;
			
		}
		
		//camera zooms in/out
		transform.position = Vector3.Lerp(transform.position,targetPos,Time.deltaTime * 0.019f);
		
	}
}
