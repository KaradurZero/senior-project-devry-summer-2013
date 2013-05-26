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
				m_mainGameState = (int)gameStates.INGAMEMENU;//or change later to pop up in game menu
				Time.timeScale = 0.0f;
				break;
			case (int)gameStates.INGAMEPAUSE:
				//if in main game but paused
				m_mainGameState = (int)gameStates.INGAMERUN;
				Time.timeScale = 1.0f;
				break;
			case (int)gameStates.INGAMEMENU:
				m_mainGameState = (int)gameStates.INGAMERUN;
				Time.timeScale = 1.0f;
				break;
			default:
				//error on game switch. should print out error message/log message then close game
				break;
			}
		}
	}//end update
	
	public int gameState {
		get {
			return m_mainGameState;
		}
		set {
			m_mainGameState = value;
		}
	}
	
	public void setGameState(int a_gameStatesEnum) {
		switch(a_gameStatesEnum) {
		case (int)gameStates.MAINMENU:
			m_mainGameState = (int)gameStates.MAINMENU;
			Time.timeScale = 0.0f;//set time to pause. or change if you want things updating in background
			break;
		case (int)gameStates.INGAMERUN:
			m_mainGameState = (int)gameStates.INGAMERUN;
			Time.timeScale = 1.0f;//set time to normal speed
			break;
		case (int)gameStates.INGAMEMENU:
			m_mainGameState = (int)gameStates.INGAMEMENU;
			Time.timeScale = 0.0f;
			break;
		case (int)gameStates.INGAMEPAUSE:
			m_mainGameState = (int)gameStates.INGAMEPAUSE;
			Time.timeScale = 0.0f;
			break;
		case (int)gameStates.RACEOVER:
			m_mainGameState = (int)gameStates.RACEOVER;
			Time.timeScale = 1.0f;
			break;
		case (int)gameStates.RACESTART:
			m_mainGameState = (int)gameStates.RACESTART;
			Time.timeScale = 1.0f;
			break;
		default:
			Debug.Log("error in GameStateController.cs: a_gameStatesEnum value is out of range with value: " + a_gameStatesEnum.ToString());
			break;
		}
	}
}
