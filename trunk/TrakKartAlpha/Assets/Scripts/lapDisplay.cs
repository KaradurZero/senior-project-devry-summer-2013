using UnityEngine;
using System.Collections;

public class lapDisplay : MonoBehaviour {
	public Material 
		lap1,
		lap2,
		lastLap,
		finished;
	int lapCount;
	void Start()
	{
		renderer.material = lap1;
		lapCount = 0;
	}
	public void StartingLap( int lapStarting)
	{
		switch (lapStarting)
		{
		case 1:		renderer.material = lap1;			break;
		case 2:		renderer.material = lap2;			break;
		case 3:		renderer.material = lastLap;		break;
		case 4:		renderer.material = finished;		break;
		}
	}
}
