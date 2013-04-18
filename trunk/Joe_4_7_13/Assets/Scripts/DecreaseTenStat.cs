using UnityEngine;
using System.Collections;

public class DecreaseTenStat : MonoBehaviour {

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
		if(statVal > 9)
			
		{
			coinManager.DecreaseStatTen();
			statVal -= 10;
			myStatGUI.text = statVal.ToString();
		}
	}
}
