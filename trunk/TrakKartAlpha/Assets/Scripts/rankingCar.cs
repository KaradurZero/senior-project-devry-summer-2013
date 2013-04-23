using UnityEngine;
using System.Collections;

public class rankingCar : MonoBehaviour {
	
	Vector3 targetPos;
	public void SetTargetPos(Vector3 a_target)
	{
		targetPos = a_target;
	}
	void Start () {
		
	}
	
	void Update () {
		if(Vector3.Distance(transform.position, targetPos) > .1f)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPos,Time.deltaTime);
		}
		else
			transform.position = targetPos;
	}
}
