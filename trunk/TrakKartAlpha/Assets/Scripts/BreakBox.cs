using UnityEngine;
using System.Collections;

public class BreakBox : MonoBehaviour {

	public AudioClip boxExplode;
	public bool isPlayer;
	void Start () {
		if(isPlayer)
			audio.PlayOneShot(boxExplode);
	}
}
