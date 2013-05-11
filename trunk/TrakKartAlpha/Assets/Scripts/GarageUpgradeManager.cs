using UnityEngine;
using System.Collections;

public class GarageUpgradeManager : MonoBehaviour {
	GarageStatManager garageStats;
	bool isActive;
	public Material
		upgradeOK,
		upgradeTooMuch,
		upgradeMaxed,
		speed,
		accel,
		temp,
		boost,
		health,
		luck,
		attack,
		defense;
	GameObject 
		buyButton,
		labelStat,
		costLabel,
		costText;
	TextMesh 
		goldCount,
		cost;
	string stat;
	void Start()
	{
		garageStats = GameObject.Find("GarageStatManager").GetComponent<GarageStatManager>();
		buyButton = GameObject.Find("Button_Upgrade");
		labelStat = GameObject.Find("Label_UpgradeStat");
		costLabel = GameObject.Find("Label_Cost");
		costText = GameObject.Find("Text_Cost");
		cost = GameObject.Find("Text_Cost").GetComponent<TextMesh>();
		goldCount = GameObject.Find("Text_Gold").GetComponent<TextMesh>();
		stat = "Speed";

	}
	public void DisplayStatInfo(string statName)
	{
		stat = statName;
		isActive = true;
		costLabel.renderer.enabled 	= true;
		costText.renderer.enabled 	= true;
		buyButton.renderer.enabled	= true;
		labelStat.renderer.enabled 	= true;
		switch(statName)
		{
		case "Speed":		labelStat.renderer.material = speed;		break;
		case "Accel":		labelStat.renderer.material = accel;		break;
		case "Temp":		labelStat.renderer.material = temp;			break;
		case "Boost":		labelStat.renderer.material = boost;		break;
		case "Health":		labelStat.renderer.material = health;		break;
		case "Luck":		labelStat.renderer.material = luck;			break;
		case "Attack":		labelStat.renderer.material = attack;		break;
		case "Defence":		labelStat.renderer.material = defense;		break;
		default:		Debug.Log("DisplayStatInfo out of range");		break;
		}
		
		cost.text = garageStats.GetStatCost(stat).ToString();
		buyButton.renderer.material = upgradeOK;
		
		if (int.Parse(cost.text) > int.Parse(goldCount.text))
		{
			buyButton.renderer.material = upgradeTooMuch;
		}
		if (!garageStats.CanUpgradeStat(statName))
		{
			buyButton.renderer.material = upgradeMaxed;
		}
	}
	public void RequestUpgradeButton()
	{		
		if (int.Parse(cost.text) <= int.Parse(goldCount.text) && garageStats.CanUpgradeStat(stat))
		{
			Upgrade();
		} 
	}
	public void Upgrade()
	{
		garageStats.UpgradeStat(stat);
		goldCount.text = (int.Parse(goldCount.text) - int.Parse(cost.text)).ToString();
		cost.text = (garageStats.GetStatCost(stat)).ToString();
		if(int.Parse(cost.text) > int.Parse(goldCount.text))
		{
			buyButton.renderer.material = upgradeTooMuch;
		}
		if (!garageStats.CanUpgradeStat(stat))
		{
			buyButton.renderer.material = upgradeMaxed;
		}
	}
	public bool IsActive()
	{
		return isActive;
	}
	public void HideStatInfo()
	{
		isActive = false;
		costLabel.renderer.enabled 	= false;
		costText.renderer.enabled 	= false;
		buyButton.renderer.enabled	= false;
		labelStat.renderer.enabled 	= false;
	}
	void Update () {
	
	}
}
