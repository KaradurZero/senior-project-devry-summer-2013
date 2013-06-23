using UnityEngine;
using System.Collections;

public class LoadMainMenu : MonoBehaviour {
	void OnMouseDown()
	{
		if( GameObject.Find("MenuStats") )
			Destroy(GameObject.Find("MenuStats"));
		Application.LoadLevel(0);
	}
}
