using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public AudioClip mouseOverSound;
	public AudioClip mouseDownSound;
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
}
