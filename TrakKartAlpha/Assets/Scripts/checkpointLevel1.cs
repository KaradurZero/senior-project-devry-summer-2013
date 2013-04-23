using UnityEngine;
using System.Collections;

public class checkpointLevel1 : MonoBehaviour {

	void OnTriggerEnter(Collider col) 
	{
		if(col.gameObject.GetComponent<CheckpointManagerLevel1>() != null)
		{
			if(gameObject.GetInstanceID() == col.gameObject.GetComponent<CheckpointManagerLevel1>().GetCheckpoint().GetInstanceID())
			col.gameObject.GetComponent<CheckpointManagerLevel1>().NextCheckpoint();
		}
    }
}
