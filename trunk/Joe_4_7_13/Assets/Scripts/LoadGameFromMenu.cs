using UnityEngine;
using System.Collections;

public class LoadGameFromMenu : MonoBehaviour {
	GameObject menuStats;
	public GUIText 
		t_speed,
		t_accel,
		t_brake,
		t_boost,
		t_weight,
		t_luck,
		t_attack,
		t_defense;
	void Start()
	{
		menuStats = GameObject.Find("statsFromMenu");
		t_speed = GameObject.Find("Text_Speed").GetComponent<GUIText>();
		t_accel = GameObject.Find("Text_Accel").GetComponent<GUIText>();
		t_brake = GameObject.Find("Text_Brake").GetComponent<GUIText>();
		t_boost = GameObject.Find("Text_Boost").GetComponent<GUIText>();
		t_weight = GameObject.Find("Text_Weight").GetComponent<GUIText>();
		t_luck= GameObject.Find("Text_Luck").GetComponent<GUIText>();
		t_attack= GameObject.Find("Text_Attack").GetComponent<GUIText>();
		t_defense= GameObject.Find("Text_Defense").GetComponent<GUIText>();
	}
	void OnMouseDown()
	{
		menuStats.GetComponent<statsFromMenu>().SetStats
			(	(int) t_speed.text,
				(int) t_accel.text,
				(int) t_brake.text,
				(int) t_boost.text,
				(int) t_weight.text,
				(int) t_luck.text,
				(int) t_attack.text,
				(int) t_defense.text
			);
			
		DontDestroyOnLoad(menuStats);
//		Application.LoadLevel(2);
	}
}
