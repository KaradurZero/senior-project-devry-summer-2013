using UnityEngine;
using System.Collections;

public class FollowThisObject : MonoBehaviour {
	public GameObject parentObject;
	private GameObject[] vehicleList ;
	public float deltaX, deltaY, deltaZ;
	float min_size = 20f ;
	float max_size = 30f ;
	float default_size = 10f ;
	public float switchTime = 5f ;
	int i = 0 ;
	Vector3 targetPos;
	
	void Start() {
		vehicleList = GameObject.FindGameObjectsWithTag("Vehicle") ;
	}
	
	void Update()
	{
		transform.position = parentObject.transform.position;
		transform.position += new Vector3(deltaX, deltaY, deltaZ);
		targetPos = transform.position;
		
		if( !parentObject.GetComponent<AIDriver>().enabled ) {
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
		
		
		if(Input.GetKeyDown(KeyCode.Alpha2)) {
			
			if( i > vehicleList.Length - 1 )
				i = 0 ;
			
			parentObject = vehicleList[i] ;
			++i ;
		}
		
		transform.position = Vector3.Lerp(transform.position,targetPos,Time.deltaTime * 0.019f);
		
	}
}
