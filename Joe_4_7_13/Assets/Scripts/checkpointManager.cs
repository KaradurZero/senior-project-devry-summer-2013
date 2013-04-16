using UnityEngine;
using System.Collections;

public class checkpointManager : MonoBehaviour {
	public GameObject playerVehical;
	public GameObject[] checkpoints;
	int focus;
	public GUIText LapText;
	int laps = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void Awake()
	{
		checkpoints = new GameObject[3];
		checkpoints[0] = GameObject.Find("cpt0");
		checkpoints[1] = GameObject.Find("cpt1");
		checkpoints[2] = GameObject.Find("cpt2");
		focus = 0;
		//checkpoints[0].renderer.material.color = Color.red;
		
		LapText = GameObject.Find("Lap_Count").GetComponent<GUIText>();
	}
	
	public int GetFocus()
	{
		return focus;
	}
	
	public GameObject GetCheckpoint()
	{
		return checkpoints[focus];
	}
	
	public bool NextCheckpoint()
	{
		//Will return false if it hasn't reached the end of the array
		//returning true implies that a new lap has been completed
		Debug.Log(checkpoints.Length);
		if(focus < checkpoints.Length - 1)
		{
			checkpoints[focus].renderer.material.color = Color.white;
			focus++;
			checkpoints[focus].renderer.material.color = Color.red;
			return false;
		}
		else
		{
			laps++;
			checkpoints[focus].renderer.material.color = Color.white;
			focus = 0;
			checkpoints[focus].renderer.material.color = Color.red;
			return true;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		LapText.text = laps.ToString();
	}
}
