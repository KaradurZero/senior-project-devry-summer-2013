using UnityEngine;
using System.Collections;

public class FollowThisObject : MonoBehaviour {
	public GameObject parentObject;
	public float deltaX, deltaY, deltaZ;
	
	void Update()
	{
		transform.position = parentObject.transform.position;
		transform.position += new Vector3(deltaX, deltaY, deltaZ);
	}
}
