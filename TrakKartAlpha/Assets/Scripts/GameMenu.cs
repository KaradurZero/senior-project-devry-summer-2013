using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour {
	
	bool m_isTangible;//if player clicking on it can interact with it or not
	public GameObject m_pauseMenu;//menu or button system.
	GameStateController m_mainStateController;
	GameObject m_instantiatedMenu;//for destroying later on and keeping track of said object
	
	// Use this for initialization
	void Start () {
		m_isTangible = false;
		m_mainStateController = GameObject.Find("GameStateController").GetComponent<GameStateController>();
	}
	
	// Update is called once per frame
	void Update () {
		//check every frame the gameStateController's state. if it is ingamepause/ingamemenu
		//and isTangible is false then the switch has happened
		if(m_mainStateController.gameState == (int)GameStateController.gameStates.INGAMEMENU
			&& m_isTangible == false) {
			//instantiate new m_pauseMenu in front of camera
			m_isTangible = true;
			Vector3 a_pos = Camera.mainCamera.transform.position;
			a_pos.y -= 2.8f;//positions may need to be fudged a bit depending upon screen size and overall scaling
			a_pos.z += 0.0f;
			//position set, now instantiate menu at that position.
			m_instantiatedMenu = (GameObject)Instantiate(m_pauseMenu, a_pos, Quaternion.identity);
			//TODO set render to true and move object into position under camera for better performance
		}
		else if(m_mainStateController.gameState != (int)GameStateController.gameStates.INGAMEMENU
			&& m_isTangible == true) {
			//menu needs to be deleted and removed from the screen
			m_isTangible = false;
			Destroy(m_instantiatedMenu);//TODO: later make render = false and move object into a non race area to get better performance.
		}
		
		if( m_isTangible) {//if you can interact with it, check if a button was clicked.
			checkMenuClick();
		}
	}
	
	void checkMenuClick() {
		if(Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast(ray, out hit, float.MaxValue);
			
			Debug.Log("HIT: " + hit.transform.name);
			switch(hit.transform.name) {
			case "Button-01":
				//testing for grabbing gamestatecontroller and setting the gamestate
				m_mainStateController.setGameState( (int)GameStateController.gameStates.INGAMERUN);
				break;
			case "Main Menu":
				//close scene and return to main menu scene
				break;
			case "Help":
				//open up help menu for controls
				break;
			case "Garage":
				//change scene to the garage screen
				break;
			case "Resume":
				//returns the game state to running. ie unpausing
				m_mainStateController.setGameState( (int)GameStateController.gameStates.INGAMERUN);
				break;
			case "Arrow-up":
				break;
			case "Subtrack-Money":
				break;
			default:
				//has not hit any defined button names. do nothing
				break;
			}
		}
	}
}
