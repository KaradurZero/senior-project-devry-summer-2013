using UnityEngine;
using System.Collections;

public class DecreaseThisStat : MonoBehaviour {
	
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
		if(statVal > 0)
			
		{
			coinManager.DecreaseStat();
			statVal--;
			myStatGUI.text = statVal.ToString();
		}
	}
}
