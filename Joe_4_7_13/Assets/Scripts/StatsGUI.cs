using UnityEngine;
using System.Collections;

public class StatsGUI : MonoBehaviour {
		
	
	vehicleItems 	AiDriver1Item,
					AiDriver2Item,
					AiDriver3Item,
					AiDriver4Item,
					AiDriver5Item,
					PlayerItem;
	
	float 			AiDriver1,
					AiDriver2,
					AiDriver3,
					AiDriver4,
					AiDriver5;
	string			guiDisplayText;
	
	void Awake() {
		AiDriver1Item = GameObject.Find("aiDriver_1").GetComponent<vehicleItems>();
		AiDriver2Item = GameObject.Find("aiDriver_2").GetComponent<vehicleItems>();
		AiDriver3Item = GameObject.Find("aiDriver_3").GetComponent<vehicleItems>();
		AiDriver4Item = GameObject.Find("aiDriver_4").GetComponent<vehicleItems>();
		AiDriver5Item = GameObject.Find("aiDriver_5").GetComponent<vehicleItems>();
		
		PlayerItem = GameObject.Find("Player").GetComponent<vehicleItems>();

	}
    void OnGUI() {	
		//update current temp
		AiDriver1 = GameObject.Find("aiDriver_1").GetComponent<CarStat>().GetCurrTemp();
		AiDriver2 = GameObject.Find("aiDriver_2").GetComponent<CarStat>().GetCurrTemp();
		AiDriver3 = GameObject.Find("aiDriver_3").GetComponent<CarStat>().GetCurrTemp();
		AiDriver4 = GameObject.Find("aiDriver_4").GetComponent<CarStat>().GetCurrTemp();
		AiDriver5 = GameObject.Find("aiDriver_5").GetComponent<CarStat>().GetCurrTemp();
		
		guiDisplayText = 
			"Power-ups:" +
			"\nPLAYER : " + PlayerItem.item +
			"\ncar 1 : " + AiDriver1Item.item + 
				"\ntemp : " + (int)AiDriver1 + "%" +
			"\ncar 2 : " + AiDriver2Item.item +
				"\ntemp : " + (int)AiDriver2 + "%" +
			"\ncar 3 : " + AiDriver3Item.item + 
				"\ntemp : " + (int)AiDriver3 + "%" +
			"\ncar 4 : " + AiDriver4Item.item + 
				"\ntemp : " + (int)AiDriver4 + "%" +
			"\ncar 5 : " + AiDriver5Item.item +
				"\ntemp : " + (int)AiDriver5 + "%";
        guiDisplayText = GUI.TextArea(new Rect(10, 10, 200, 200), guiDisplayText, 200);
    }
}
