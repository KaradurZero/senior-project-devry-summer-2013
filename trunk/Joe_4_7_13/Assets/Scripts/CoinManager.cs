using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour {
	
	GUIText cointText;
	
	int coinAmount, initCoinAmount;
	
	void Start () {
		coinAmount = initCoinAmount = 600;
		cointText = GameObject.Find("Text_Coins").GetComponent<GUIText>();
	}
	void OnGUI()
	{
		cointText.text = coinAmount.ToString();
	}
	public int GetCoinInitAmount()
	{
		return initCoinAmount;
	}
	public bool CanIncreaseStat()
	{
		return coinAmount > 0;
	}
	public bool CanIncreaseStatTen()
	{
		return coinAmount > 9;
	}
	public void IncreaseStat()
	{
		coinAmount--;
	}
	public void IncreaseStatTen()
	{
		coinAmount -= 10;
	}
	public void DecreaseStat()
	{
		coinAmount++;
	}
	public void DecreaseStatTen()
	{
		coinAmount += 10;
	}
}
