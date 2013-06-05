using UnityEngine;
using System.Collections;

public class minimapItem : MonoBehaviour {
	public GameObject actualObject;
	public float localScaleValue;
	
	void Start()
	{
		transform.localScale = new Vector3( localScaleValue, localScaleValue, localScaleValue);
	}
	void Update () {
	
		transform.position = actualObject.transform.position + new Vector3(0f,999f,0f);
		transform.rotation = actualObject.transform.rotation;
	}
}
