using UnityEngine;
using System.Collections;

public class CheckpointManagerLevel1 : MonoBehaviour {
	public GameObject[] checkpoints;
	int focus;
	int laps;
	
	void Awake()
	{
		laps = 1;
		checkpoints = new GameObject[17];
		checkpoints[0] = GameObject.Find("cpt0");
		checkpoints[1] = GameObject.Find("cpt1");
		checkpoints[2] = GameObject.Find("cpt2");
		checkpoints[3] = GameObject.Find("cpt3");
		checkpoints[4] = GameObject.Find("cpt4");
		checkpoints[5] = GameObject.Find("cpt5");
		checkpoints[6] = GameObject.Find("cpt6");
		checkpoints[7] = GameObject.Find("cpt7");
		checkpoints[8] = GameObject.Find("cpt8");
		checkpoints[9] = GameObject.Find("cpt9");
		checkpoints[10] = GameObject.Find("cpt10");
		checkpoints[11] = GameObject.Find("cpt11");
		checkpoints[12] = GameObject.Find("cpt12");
		checkpoints[13] = GameObject.Find("cpt13");
		checkpoints[14] = GameObject.Find("cpt14");
		checkpoints[15] = GameObject.Find("cpt15");
		checkpoints[16] = GameObject.Find("cpt16");
		focus = 0;
		checkpoints[0].renderer.material.color = Color.blue;
	}
	public GameObject GetCheckpoint()
	{
		return checkpoints[focus];
	}
	public int GetFocus()
	{
		return focus;
	}
	public int GetLap()
	{
		return laps;
	}
	public Vector3 GetLastCheckpointPos()
	{
		if(focus == 0)
			return checkpoints[16].transform.position;
		else
			return checkpoints[focus - 1].transform.position;
	}
	public Vector3 GetCurrCheckpointPos()
	{
		return checkpoints[focus].transform.position;
	}
	public void NextCheckpoint()
	{
		if(focus < checkpoints.Length - 1)
		{
			focus++;
			if(gameObject.name == "Player")
			{
				checkpoints[focus-1].renderer.material.color = Color.white;
				checkpoints[focus].renderer.material.color = Color.blue;
				
			}
		}
		else
		{
			checkpoints[checkpoints.Length - 1].renderer.material.color = Color.white;
			checkpoints[0].renderer.material.color = Color.blue;
			focus = 0;
			laps ++;
			if(gameObject.name == "Player")
			{
					GameObject.Find ("lapDisplay").GetComponent<lapDisplay>().StartingLap(laps);
			}
			if(laps == 4)
			{
				//This car finished
			}
		}
	}
	//returns the waypoint that is x amount of waypoints ahead of the driver
	public Transform GetFutureCheckpointTransform(int futureCount)
	{
		int t = futureCount + focus;
		while(t >= checkpoints.Length)
		{
			t -= checkpoints.Length;
		}
		return checkpoints[t].transform;
	}
}
