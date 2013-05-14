using UnityEngine;
using System.Collections;

public class FollowThisObject : MonoBehaviour {
	public GameObject parentObject;
	public float deltaX, deltaY, deltaZ;
	float min_size = 20f ;
	float max_size = 30f ;
	float default_size = 10f ;
	Vector3 targetPos;
	void Update()
	{
		transform.position = parentObject.transform.position;
		transform.position += new Vector3(deltaX, deltaY, deltaZ);
		targetPos = transform.position;
		
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
		
		transform.position = Vector3.Lerp(transform.position,targetPos,Time.deltaTime * 0.019f);
		
	}
}
