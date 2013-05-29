using UnityEngine;
using System.Collections;

public class rankDisplay : MonoBehaviour {
	public Material[] rankMats;
	RacersPositionController posCont;
	void Start()
	{
		posCont = GameObject.Find("RacersPositionController").GetComponent<RacersPositionController>();
	}
	// Update is called once per frame
	void Update () {
	renderer.material = rankMats[posCont.getPlayerRank() - 1];
	}
}
