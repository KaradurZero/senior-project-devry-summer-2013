using UnityEngine;
using System.Collections;

public class Sparks : MonoBehaviour {
	public AudioClip sparks;
	public bool isPlayers;
	void Start () {
		if(isPlayers)
			audio.PlayOneShot(sparks);
	}
}
