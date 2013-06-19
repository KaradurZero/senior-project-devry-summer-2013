using UnityEngine;
using System.Collections;

public class coinExplosion : MonoBehaviour {
	
	public AudioClip coinExplode;
	
	void Start () {
		audio.PlayOneShot(coinExplode);
	}
}
