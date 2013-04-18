using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter()
	{
		transform.localScale += new Vector3 (0.3f,0.3f,0.3f);
	}
	
	void OnMouseExit()
	{
		transform.localScale -= new Vector3 (0.3f,0.3f,0.3f);
	}
	
	void OnMouseOver()
	{
		if(Input.GetMouseButton(0))
		{
			transform.Rotate(Vector3.up, 1f);
		}
	}
	
	void OnMouseDown()
	{
		//renderer.material.color = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f);	
		transform.Rotate(Vector3.up, 1f);
	}
}
