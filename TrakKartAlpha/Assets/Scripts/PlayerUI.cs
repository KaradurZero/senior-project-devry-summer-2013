using UnityEngine;
using System.Collections;

public class PlayerUI : MonoBehaviour {
	
	public GameObject player ;
	public float tempDisplay, healthDisplay; //current progress
	public Vector2 tempPos, healthPos, deathTextPos;
	public Vector2 tempSize, healthSize, deathTextSize;
	public Texture2D emptyTex;
	public Texture2D fullTex;
	
	GUIStyle style = new GUIStyle();

	// Use this for initialization
	void Start () {
		tempPos = new Vector2(10, Screen.height- 35);
		healthPos = new Vector2(10, Screen.height- 80);
		deathTextPos = new Vector2(Screen.width/2.5f, Screen.height/1.5f);
		tempSize = new Vector2(160,30);
		healthSize = new Vector2(160, 30 ) ;
		deathTextSize = new Vector2(500, 100 ) ;
		
		style.font = Resources.Load("arial") as Font ;
		style.fontSize = 50 ;
		style.normal.textColor = Color.red ;
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
		
		if( !this.GetComponent<Vehicle>().amAlive() )
			GUI.TextField(new Rect(deathTextPos.x, deathTextPos.y, deathTextSize.x, deathTextSize.y), "Respawning in: " + (this.GetComponent<driverHealth>().GetRespawnTime()+1), style ) ;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		tempDisplay = (player.GetComponent<CarStat>().GetCurrTemp()/player.GetComponent<CarStat>().GetMaxTemp());
		healthDisplay = (player.GetComponent<driverHealth>().GetHealth()/player.GetComponent<CarStat>().GetMaxHealth());
		//Debug.Log (Screen.currentResolution.height/2);
	}
}
