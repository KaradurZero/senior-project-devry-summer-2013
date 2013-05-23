using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour {
	
	public enum gameStates {MAINMENU, INGAMERUN, INGAMEMENU, INGAMEPAUSE, RACEOVER, RACESTART};
	int m_mainGameState;
	
	
	// Use this for initialization
	void Start () {
		//m_mainGameState = (int)gameStates.MAINMENU;
		m_mainGameState = (int)gameStates.INGAMERUN;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			switch(m_mainGameState) {
			case (int)gameStates.INGAMERUN:
				//if game is running like normal
				m_mainGameState = (int)gameStates.INGAMEPAUSE;//or change later to pop up in game menu
				Time.timeScale = 0.0f;
				break;
			case (int)gameStates.INGAMEPAUSE:
				//if in main game but paused
				m_mainGameState = (int)gameStates.INGAMERUN;
				Time.timeScale = 1.0f;
				break;
			default:
				//error on game switch. should print out error message/log message then close game
				break;
			}
		}
	}
}
