using UnityEngine;
using System.Collections;
/*
 * Uses the mouse to shoot a ray and determine which object is being selected.
 */
public class GarageInputMouseKeyboard : MonoBehaviour {
	
	GarageSelectionManager garageSelect;
	void Start () 
	{
		garageSelect = GameObject.Find("GarageSelectionManager").GetComponent<GarageSelectionManager>();
	}
	
	void Update () 
	{
		//shoots ray to the mouse
		bool hitUpgrade = false;
		Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll(r);
		foreach(RaycastHit rch in hits)
		{
			//did I hit a stat?
			if(rch.transform.tag == "GarageItem")
			{
				garageSelect.HighlightThisItem(rch.transform.gameObject);
				if(Input.GetMouseButtonDown(0))
					garageSelect.SelectRequest();
			}
			//did I hit the upgrade buton?
			if(rch.transform.name == "Button_Upgrade")
			{
				hitUpgrade = true;
				garageSelect.HighlightUpgrade();
				if(Input.GetMouseButtonDown(0))
					garageSelect.RequestSelectUpgrade();
			}
		}
		if(!hitUpgrade)
			garageSelect.DehighlightUpgrade();
	}
	
}
