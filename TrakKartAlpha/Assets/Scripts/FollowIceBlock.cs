using UnityEngine;
using System.Collections;

public class FollowIceBlock : MonoBehaviour {
	
	public Transform gameObjectToFollow;
	
	//freeze lasts 3 seconds, destroy when car is free to move
	void Start()
	{
		Destroy(gameObject, 3f);
	}
	//Move with an object without rotating with that object
	void Update ()
	{
		transform.position = gameObjectToFollow.position;
	}
}
