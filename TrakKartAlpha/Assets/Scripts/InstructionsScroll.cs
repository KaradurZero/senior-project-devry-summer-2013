using UnityEngine;
using System.Collections;

public class InstructionsScroll : MonoBehaviour {
	
	public Material[] texs = new Material[4];
	int currslide = 0 ;

	// Use this for initialization
	void Start () {
		renderer.material = texs[0] ;
	}
	
	// Update is called once per frame
	void Update () {
		
			
	}
	
	public void NextSlide() {
		if( currslide < 3 ){
			Debug.Log (currslide);
			this.gameObject.renderer.material = texs[currslide+1] ;
			currslide++ ;
		}
		else {
			this.gameObject.renderer.material = texs[0] ;
			currslide = 0 ;
		}
	}
	
	public void LastSlide() {
		if( currslide > 0 ){
			Debug.Log (currslide);
			this.gameObject.renderer.material = texs[currslide-1] ;
			currslide-- ;
		}
		else {
			this.gameObject.renderer.material = texs[3] ;
			currslide = 3 ;
		}	
	}
}
