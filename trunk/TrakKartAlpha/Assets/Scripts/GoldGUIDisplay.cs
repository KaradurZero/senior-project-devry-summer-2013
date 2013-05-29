using UnityEngine;
using System.Collections;

public class GoldGUIDisplay : MonoBehaviour {
	
	int targetGoldAmount, currentGoldAmount;
	
	CarStat stat;
	TextMesh goldDisplay;
	
	void Start () {
		currentGoldAmount = targetGoldAmount = 0;
		stat = GameObject.Find("Player").GetComponent<CarStat>();
		goldDisplay = GameObject.Find("GoldText").GetComponent<TextMesh>();
	}
	
	public void AddGoldAmount(int goldAdded)
	{
		targetGoldAmount += goldAdded;
	}
	
	void Update () {
		if(targetGoldAmount > currentGoldAmount)
		{
			currentGoldAmount++;
			goldDisplay.text = currentGoldAmount.ToString();
		}
	}
}
