using UnityEngine;
using System.Collections;


public class NextSlide : MonoBehaviour {
	
	public GameObject bg ;
	
	void Start(){
		bg = GameObject.Find ("background") ;
	}
	
	void OnMouseDown()
	{
		bg.GetComponent<InstructionsScroll>().NextSlide() ;
	}
}
