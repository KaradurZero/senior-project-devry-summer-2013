using UnityEngine;
using System.Collections;

public class FollowSmoke : MonoBehaviour {
	public Transform gameObjectToFollow;

	void Start()
	{
		//transform.rotation = new Quaternion(-90f,0,0,0);
	}
	void Update () 
	{
		transform.position = gameObjectToFollow.position + (gameObjectToFollow.forward * 1f);
	}
}
