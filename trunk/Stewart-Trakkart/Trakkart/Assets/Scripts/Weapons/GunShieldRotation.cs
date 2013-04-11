using UnityEngine;
using System.Collections;

public class GunShieldRotation : MonoBehaviour {

	private Transform 	gunAndShield;
	public bool 		isUsingGun;
	public GameObject	m_shield;
	public Vector3		m_aimTo;
	//shield should be a child object of this object so rotation update will work on it
	//without needing to do redundant code for it.
	
	// Use this for initialization
	void Start () {
		isUsingGun = true;//start out with gun
		this.renderer.enabled		= isUsingGun;
		m_shield.renderer.enabled	= !isUsingGun;
		gunAndShield 				= transform;//set's pointer to current object's transform.
		//makes code more elegant and easier to follow
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(2/*middle mouse*/)) { swapGunShield();}
		updateRotationToMouse();
	}
	
	void swapGunShield() {//swaps gun out for shield and vice versa
		isUsingGun = !isUsingGun;//makes it the oposite. only works with boolean value
		this.renderer.enabled 		= isUsingGun;
		m_shield.renderer.enabled 	= !isUsingGun;
	}
	void updateRotationToMouse() {
		 // Generate a plane that intersects the transform's position with an upwards normal.
		Plane playerPlane = new Plane(Vector3.up, transform.parent.position);
		 
		// Generate a ray from the cursor position
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		 
		// Determine the point where the cursor ray intersects the plane.
		// This will be the point that the object must look towards to be looking at the mouse.
		// Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
		// then find the point along that ray that meets that distance. This will be the point
		// to look at.
		float hitdist = 0.0f;
		// If the ray is parallel to the plane, Raycast will return false.
		if (playerPlane.Raycast (ray, out hitdist)) {
			// Get the point along the ray that hits the calculated distance.
			Vector3 targetPoint = ray.GetPoint(hitdist);
			// Determine the target rotation. This is the rotation if the transform looks at the target point.
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.parent.position);
			
//			Debug.Log("current Euler angle: " + transform.rotation.eulerAngles.ToString() + "target Euler angle: "
//				+ targetRotation.eulerAngles.ToString());
			float curAngle = transform.rotation.eulerAngles.y;//current rotation about the Y axis
			float tarAngle = targetRotation.eulerAngles.y;//target Y angle rotation relative to this object's position
			float toAngle = tarAngle - curAngle;//gets the difference of angular distance.

			// Smoothly rotate towards the target point.
			//transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime); // WITH SPEED
			//transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1); // WITHOUT SPEED!!!
			gunAndShield.RotateAround(transform.parent.position, Vector3.up, toAngle);//rotate around parent position and rotate around Y axis.
		}
	}
	
	void updateRotationToVec3() {
		
	}
}
