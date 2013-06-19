using UnityEngine;
using System.Collections;

public class EngineSound : MonoBehaviour {
	CarStat myStats;
	
	void Start () {
		myStats = transform.GetComponent<CarStat>();
	}
	
	void Update () {
		float percentage = myStats.GetCurrentSpeed() /  myStats.GetMaxVelocity();
		audio.pitch = 1 + percentage;
	
	}
}
