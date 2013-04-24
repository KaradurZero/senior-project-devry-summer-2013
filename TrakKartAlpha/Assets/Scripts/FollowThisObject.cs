using UnityEngine;
using System.Collections;

public class FollowThisObject : MonoBehaviour {
	public GameObject parentObject;
	public float deltaX, deltaY, deltaZ;
	
	float lastSpeed = 0f;
	
	void Update()
	{
		transform.position = parentObject.transform.position;
		transform.position += new Vector3(deltaX, deltaY, deltaZ);
		
		Camera.mainCamera.orthographicSize = 25 * (parentObject.GetComponent<CarStat>().GetCurrentSpeed() / parentObject.GetComponent<CarStat>().GetMaxVelocity()) ;
		
		if(Camera.mainCamera.orthographicSize < 10)
			Camera.mainCamera.orthographicSize = 10;
		
		if(parentObject.GetComponent<Vehicle>().isBoosted()) {
			if(Camera.mainCamera.orthographicSize > 25)
				Camera.mainCamera.orthographicSize = 25;
		}
		else if(Camera.mainCamera.orthographicSize > 20)
			Camera.mainCamera.orthographicSize = 20;
		
		lastSpeed = parentObject.GetComponent<CarStat>().GetCurrentSpeed() ;
	}
}
