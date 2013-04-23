using UnityEngine;
using System.Collections;

public class PowerUpDisplay : MonoBehaviour {
	
	public Material
		oilSlickMat,
		homingMat,
		freezeMat,
		enlargeMat,
		deflectorMat,
		itemStealMat,
		noneMat;
	void Start()
	{
		renderer.material = noneMat;
	}
	public void DisplayPowerup(int powerupValue)
	{
		switch(powerupValue)
		{
		case 0:		renderer.material = noneMat;				break;
		case 1:		renderer.material = oilSlickMat;			break;
		case 2:		renderer.material = homingMat;				break;
		case 3:		renderer.material = freezeMat;				break;
		case 4:		renderer.material = enlargeMat;				break;
		case 5:		renderer.material = deflectorMat;			break;
		case 6:		renderer.material = itemStealMat;			break;
		default:	Debug.Log ("PowerupValue is out of range");	break;
		}
	}
}
