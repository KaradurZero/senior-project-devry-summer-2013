using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public AudioClip mouseOverSound;
	public AudioClip mouseDownSound;
	void OnMouseEnter()
	{
		audio.PlayOneShot(mouseOverSound);
		transform.localScale += new Vector3 (0.3f,0.3f,0.3f);
	}
	
	void OnMouseExit()
	{
		transform.localScale -= new Vector3 (0.3f,0.3f,0.3f);
	}
	
	void OnMouseDown()
	{
		audio.PlayOneShot(mouseDownSound);
	}
}
