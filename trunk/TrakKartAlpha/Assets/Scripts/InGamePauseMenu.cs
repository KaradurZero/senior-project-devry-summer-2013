using UnityEngine;
using System.Collections;

public class InGamePauseMenu : MonoBehaviour {
	
	bool m_isTangible;//if player clicking on it can interact with it or not
	public GameObject m_pauseMenu;//menu or button system.
	GameStateController m_mainStateController;
	
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
			Vector3 a_pos = Camera.mainCamera.transform.position;
			a_pos.y -= 2.0f;
			//position set, now instantiate menu at that position.
		}
	}
}
