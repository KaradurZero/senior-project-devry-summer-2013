using UnityEngine;
using System.Collections;

public class NavigationDisplay : MonoBehaviour {
	
	public GameObject player ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 CurrCheckpointPos = player.GetComponent<CheckpointManagerLevel1>().GetCurrCheckpointPos();
		Quaternion rotation = Quaternion.LookRotation(CurrCheckpointPos - player.transform.position);
		
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
	}
}