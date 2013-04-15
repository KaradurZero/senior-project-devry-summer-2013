using UnityEngine;
using System.Collections;

public class LapCheck : MonoBehaviour {
	checkpointManager mycheckpoints;
	
	// Use this for initialization
	void Start () 
	{
		mycheckpoints = GameObject.Find("CheckPointMngr").GetComponent<checkpointManager>();
		
	}
	
	void OnTriggerEnter(Collider col) 
	{
    	if(col.gameObject.tag == "Player" && gameObject.GetInstanceID() == mycheckpoints.GetCheckpoint().GetInstanceID())
		{
			bool worked = mycheckpoints.NextCheckpoint();
		}
    }
	
	// Update is called once per frame
	void Update ()
	{
	}
}
