using UnityEngine;
using System.Collections;

public class PlayerUI : MonoBehaviour {
	
	public GameObject player ;
	public float tempDisplay, healthDisplay; //current progress
	public Vector2 tempPos, healthPos;
	public Vector2 tempSize, healthSize;
	public Texture2D emptyTex;
	public Texture2D fullTex;

	// Use this for initialization
	void Start () {
		tempPos = new Vector2(10, Screen.height- 35);
		healthPos = new Vector2(10, Screen.height- 80);
		tempSize = new Vector2(160,30);
		healthSize = new Vector2(160, 30 ) ;
		
		//Debug.Log (Screen.currentResolution.height/2);
	}
	
	 void OnGUI() {
		//draw the background:
		GUI.DrawTexture(new Rect(tempPos.x, tempPos.y, tempSize.x, tempSize.y), emptyTex, ScaleMode.StretchToFill);		
		
		//draw the filled-in part:
		GUI.BeginGroup(new Rect(tempPos.x, tempPos.y, tempSize.x * Mathf.Clamp01(tempDisplay), tempSize.y));
		GUI.DrawTexture(new Rect(0, 0, tempSize.x * Mathf.Clamp01(tempDisplay), tempSize.y), fullTex, ScaleMode.StretchToFill);		
		GUI.EndGroup();
		
		//GUI.BeginGroup(new Rect(healthPos.x, healthPos.y, healthSize.x, healthSize.y));
		GUI.DrawTexture(new Rect(healthPos.x, healthPos.y, healthSize.x, healthSize.y), fullTex, ScaleMode.StretchToFill);
			
		GUI.BeginGroup(new Rect(healthPos.x, healthPos.y, healthSize.x * Mathf.Clamp01(healthDisplay), healthSize.y));
		GUI.DrawTexture(new Rect(0, 0, healthSize.x * Mathf.Clamp01(healthDisplay), healthSize.y), emptyTex, ScaleMode.StretchToFill);
		
		GUI.EndGroup();
		
	}
	
	// Update is called once per frame
	void Update () {
		tempDisplay = (player.GetComponent<CarStat>().GetCurrTemp()/player.GetComponent<CarStat>().GetMaxTemp());
		healthDisplay = (player.GetComponent<driverHealth>().GetHealth()/player.GetComponent<CarStat>().GetMaxHealth());
		//Debug.Log (Screen.currentResolution.height/2);
	}
}
