using UnityEngine;
using System.Collections;

public class LoadGameFromMenu : MonoBehaviour {

	GarageStatManager statManager;
	statsFromMenu menuStats;
	
	void Start()
	{
		statManager = GameObject.Find("GarageStatManager").GetComponent<GarageStatManager>();
		menuStats 	= GameObject.Find("MenuStats").GetComponent<statsFromMenu>();
	}
	void OnMouseDown()
	{
		menuStats.GetComponent<statsFromMenu>().SetStats
			(
				statManager.GetSpeedVal(),
				statManager.GetAccelVal(),
				statManager.GetTempVal(),
				statManager.GetBoostVal(),
				statManager.GetHealthVal(),
				statManager.GetLuckVal(),
				statManager.GetAttackVal(),
				statManager.GetDefenseVal()
			);
			
		DontDestroyOnLoad(menuStats);
		Application.LoadLevel(3);
	}
}
