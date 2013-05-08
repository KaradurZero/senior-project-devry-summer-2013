using UnityEngine;
using System.Collections;

public class FollowThisObject : MonoBehaviour {
	public GameObject parentObject;
	public float deltaX, deltaY, deltaZ;
	float lastSpeed = 0f;
	float min_size = 10f ;
	float max_size = 20f ;
	float default_size = 10f ;
	
	void Update()
	{
		transform.position = parentObject.transform.position;
		transform.position += new Vector3(deltaX, deltaY, deltaZ);

		//Camera.mainCamera.orthographicSize = 10f + (10f * (parentObject.GetComponent<CarStat>().GetCurrentSpeed() / parentObject.GetComponent<CarStat>().GetMaxVelocity())) ;

		
		//Debug.Log(parentObject.GetComponent<Vehicle>().isBoosted()) ;
		
		if(!(parentObject.GetComponent<Vehicle>().isBoosted())) {
			Camera.mainCamera.orthographicSize = default_size + (min_size * (parentObject.GetComponent<CarStat>().GetCurrentSpeed() / parentObject.GetComponent<CarStat>().GetMaxVelocity())) ;
		}
		else 
			Camera.mainCamera.orthographicSize = default_size + (max_size * (parentObject.GetComponent<CarStat>().GetCurrentSpeed() / parentObject.GetComponent<CarStat>().GetMaxVelocity())) ;
		
		//lastSpeed = parentObject.GetComponent<CarStat>().GetCurrentSpeed() ;
	}
}
