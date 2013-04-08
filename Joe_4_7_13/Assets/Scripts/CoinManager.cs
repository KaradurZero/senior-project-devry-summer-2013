using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour {
	
	GUIText cointText;
	
	int coinAmount;
	
	void Start () {
		coinAmount = 0;//initCoinAmount = 10;
		cointText = GameObject.Find("Text_Coins").GetComponent<GUIText>();
	}
	void OnGUI()
	{
		cointText.text = coinAmount.ToString();
	}
	public bool CanIncreaseStat()
	{
		return coinAmount > 0;
	}
	public void IncreaseStat()
	{
		coinAmount--;
	}
	public void DecreaseStat()
	{
		coinAmount++;
	}
}
