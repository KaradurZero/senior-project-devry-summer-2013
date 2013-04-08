using UnityEngine;
using System.Collections;

public class waypointManager: MonoBehaviour {
	public GameObject[] waypoints;
	int focus;
	
	void Awake()
	{
		waypoints = new GameObject[15];
		waypoints[0] = GameObject.Find("w0");
		waypoints[1] = GameObject.Find("w1");
		waypoints[2] = GameObject.Find("w2");
		waypoints[3] = GameObject.Find("w3");
		waypoints[4] = GameObject.Find("w4");
		waypoints[5] = GameObject.Find("w5");
		waypoints[6] = GameObject.Find("w6");
		waypoints[7] = GameObject.Find("w7");
		waypoints[8] = GameObject.Find("w8");
		waypoints[9] = GameObject.Find("w9");
		waypoints[10] = GameObject.Find("w10");
		waypoints[11] = GameObject.Find("w11");
		waypoints[12] = GameObject.Find("w12");
		waypoints[13] = GameObject.Find("w13");
		waypoints[14] = GameObject.Find("w14");
		focus = 0;
	}
	public int GetFocus()
	{
		return focus;
	}
	public Vector3 GetCurrWaypointPos()
	{
		return waypoints[focus].transform.position;
	}
	public void NextWaypoint()
	{
		if(focus < waypoints.Length - 1)
			focus++;
		else
			focus = 0;
	}
	//returns the waypoint that is x amount of waypoints ahead of the driver
	public Transform GetFutureWaypointTransform(int futureCount)
	{
		int t = futureCount + focus;
		while(t >= waypoints.Length)
		{
			t -= waypoints.Length;
		}
		return waypoints[t].transform;
	}

}
