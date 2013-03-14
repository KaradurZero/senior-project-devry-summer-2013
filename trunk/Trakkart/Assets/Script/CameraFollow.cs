using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public Vehicle player ;
	
	public float m_default_distance ;
	public float m_default_rotation ;

	// Use this for initialization
	void Start () {
		m_default_distance = 100f ;
		m_default_rotation = 90f ;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3( player.transform.position.x, (player.transform.position.y + m_default_distance), player.transform.position.z ) ;
	}
}
