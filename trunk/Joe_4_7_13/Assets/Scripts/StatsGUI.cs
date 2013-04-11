using UnityEngine;
using System.Collections;

public class StatsGUI : MonoBehaviour {
		
	
	vehicleItems 	AiDriver1Item,
					AiDriver2Item,
					AiDriver3Item,
					AiDriver4Item,
					AiDriver5Item;
	string			guiDisplayText;
	
	void Awake() {
		AiDriver1Item = GameObject.Find("aiDriver_1").GetComponent<vehicleItems>();
		AiDriver2Item = GameObject.Find("aiDriver_2").GetComponent<vehicleItems>();
		AiDriver3Item = GameObject.Find("aiDriver_3").GetComponent<vehicleItems>();
		AiDriver4Item = GameObject.Find("aiDriver_4").GetComponent<vehicleItems>();
		AiDriver5Item = GameObject.Find("aiDriver_5").GetComponent<vehicleItems>();

	}
    void OnGUI() {		
		guiDisplayText = 
			"Power-ups:" +
			"\ncar 1 : " + AiDriver1Item.item + 
			"\ncar 2 : " + AiDriver2Item.item +
			"\ncar 3 : " + AiDriver3Item.item + 
			"\ncar 4 : " + AiDriver4Item.item + 
			"\ncar 5 : " + AiDriver5Item.item;
        guiDisplayText = GUI.TextArea(new Rect(10, 10, 100, 100), guiDisplayText, 200);
    }
}
