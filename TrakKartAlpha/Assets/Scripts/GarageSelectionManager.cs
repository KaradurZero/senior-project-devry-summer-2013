using UnityEngine;
using System.Collections;

public class GarageSelectionManager : MonoBehaviour {
	
	GarageStatManager garageStats;
	GarageUpgradeManager garageUpgrade;
	GameObject upgradeButtonObj;
	GameObject lastSelected;
	string selectionName;
	GameObject 
		highlightSelect, 
		highlightHover;
	Vector3 upgradeButtonOrigScale;
	
	public AudioClip statSelect, statHighlighted;
	
	void Start () 
	{
		garageStats = GameObject.Find("GarageStatManager").GetComponent<GarageStatManager>();
		garageUpgrade = GameObject.Find("GarageUpgradeManager").GetComponent<GarageUpgradeManager>();
		highlightHover = GameObject.Find("highlightHover");
		highlightSelect = GameObject.Find("highlightSelect");
		upgradeButtonObj = GameObject.Find("Button_Upgrade");
		upgradeButtonOrigScale = upgradeButtonObj.transform.localScale;
		lastSelected = GameObject.Find("Speed");
	
	}
	public void HighlightUpgrade()
	{
		upgradeButtonObj.transform.localScale = upgradeButtonOrigScale +  new Vector3(0.1F,0,.1f);
	}
	public void DehighlightUpgrade()
	{
		upgradeButtonObj.transform.localScale = upgradeButtonOrigScale;
	}
	public void RequestSelectUpgrade()
	{
		//if(upgradeButtonObj.renderer.material == garageUpgrade.upgradeOK)
			garageUpgrade.RequestUpgradeButton();
	}
	public void SelectRequest()
	{
		//play sound for selecting a stat
		audio.PlayOneShot(statSelect);
		
		highlightSelect.transform.position = highlightHover.transform.position;
		garageUpgrade.DisplayStatInfo(selectionName);	
	}
	public void HighlightThisItem(GameObject highlighted)
	{
		if(highlighted != lastSelected)
		{
			audio.PlayOneShot(statHighlighted);
			lastSelected = highlighted;
		}
		selectionName = highlighted.transform.name;
		highlightHover.transform.position = highlighted.transform.position + new Vector3(3.5f,0f,.35f);
	}
}
