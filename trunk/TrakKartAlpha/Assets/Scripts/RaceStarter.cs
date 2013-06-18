using UnityEngine;
using System.Collections;

public class RaceStarter : MonoBehaviour {
	
	GameObject[] vehicleList ;
	float timer = 6f ;
	
	Vector2 textPos, textSize ;
	GUIStyle style = new GUIStyle();

	// Use this for initialization
	void Start () {
		vehicleList = GameObject.FindGameObjectsWithTag("Vehicle");
		foreach (GameObject racer in vehicleList) {
			if( racer.name == "Player" ) {
				racer.GetComponent<ControlSafety>().enabled = false ;
				
				if( racer.GetComponent<KeyboardMouseController>() != null )
					racer.GetComponent<KeyboardMouseController>().enabled = false ;
				else if( racer.GetComponent<GamepadController>() != null )
					racer.GetComponent<GamepadController>().enabled = false ;
				else if( racer.GetComponent<Xbox360Controller>() != null )
					racer.GetComponent<Xbox360Controller>().enabled = false ;	
			}
			else {
				racer.GetComponent<AIDriver>().enabled = false ;
				racer.GetComponent<AIState>().enabled = false ;
			}
		}
		
		textPos = new Vector2(Screen.width/3f, Screen.height/3f) ;
		textSize = new Vector2(300f, 300f) ;
		
		style.font = Resources.Load("arial") as Font ;
		style.fontSize = 50 ;
		style.normal.textColor = Color.blue ;
	}
	
	void OnGUI() {
		if( timer < 4f && timer > 3f ) {
			style.normal.textColor = Color.red ;
			GUI.TextField(new Rect(textPos.x, textPos.y, textSize.x, textSize.y), "" + (int)(timer), style ) ;	
		}
		else if( timer < 3f && timer > 1f ) {
			style.normal.textColor = Color.yellow ;
			GUI.TextField(new Rect(textPos.x, textPos.y, textSize.x, textSize.y), "" + (int)(timer), style ) ;	
		}
		else if( timer < 1f ) {
			style.normal.textColor = Color.green ;
			GUI.TextField(new Rect(textPos.x, textPos.y, textSize.x, textSize.y), "GO!", style ) ;
		}
		else {
			GUI.TextField(new Rect(textPos.x, textPos.y, textSize.x, textSize.y), "Get Ready!", style ) ;	
		}
	}
	
	// Update is called once per frame
	void Update () {
		if( timer > 0 ) {
			timer -= Time.deltaTime ;
		}
		else
			this.gameObject.GetComponent<RaceStarter>().enabled = false ;
		
		if( timer < 1 ) {
			foreach (GameObject racer in vehicleList) {
				if( racer.name == "Player" ) {
					racer.GetComponent<ControlSafety>().enabled = true ;
					
					if( racer.GetComponent<KeyboardMouseController>() != null )
						racer.GetComponent<KeyboardMouseController>().enabled = true ;
					else if( racer.GetComponent<GamepadController>() != null )
						racer.GetComponent<GamepadController>().enabled = true ;
					else if( racer.GetComponent<Xbox360Controller>() != null )
						racer.GetComponent<Xbox360Controller>().enabled = true ;	
				}
				else {
					racer.GetComponent<AIDriver>().enabled = true ;
					racer.GetComponent<AIState>().enabled = true ;
				}
			}	
		}
			
	}	
}
