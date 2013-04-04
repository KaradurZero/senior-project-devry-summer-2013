using UnityEngine;
using System.Collections;

public class IncreaseThisStat : MonoBehaviour {
	
	public GUIText myStatGUI;
	CoinManager coinManager;
	
	void Start () {
		coinManager = GameObject.Find("CoinManager").GetComponent<CoinManager>();	
	}
	void OnMouseEnter()
	{
		transform.localScale += new Vector3(.3f,.3f,.3f);
	}
	void OnMouseExit()
	{
		transform.localScale -= new Vector3(.3f,.3f,.3f);
	}
	void OnMouseDown()
	{
		int statVal = int.Parse(myStatGUI.text);
		if(coinManager.CanIncreaseStat())
			
		{
			coinManager.IncreaseStat();
			statVal++;
			myStatGUI.text = statVal.ToString();
		}
	}
}
