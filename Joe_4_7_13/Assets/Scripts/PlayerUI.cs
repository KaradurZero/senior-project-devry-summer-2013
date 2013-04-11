using UnityEngine;
using System.Collections;

public class PlayerUI : MonoBehaviour {
	
	public GameObject player ;
	 public float barDisplay; //current progress
	public Vector2 pos;
	public Vector2 size;
	GUISkin skin ;
	public Texture2D emptyTex;
	public Texture2D fullTex;

	// Use this for initialization
	void Start () {
		pos = new Vector2(10,110);
		size = new Vector2(100,20);
	}
	
	 void OnGUI() {
		//draw the background:
		GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), emptyTex);
		 
		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0,0, size.x * Mathf.Clamp01(barDisplay), size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), fullTex);
		GUI.EndGroup();
		GUI.EndGroup();
	}
	
	// Update is called once per frame
	void Update () {
		barDisplay = player.GetComponent<CarStat>().GetCurrTemp()*0.01f;
	}
}
