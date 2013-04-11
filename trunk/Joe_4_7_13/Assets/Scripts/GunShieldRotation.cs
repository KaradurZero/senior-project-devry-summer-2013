using UnityEngine;
using System.Collections;

public class GunShieldRotation : MonoBehaviour {
	
	private Transform 	gunAndShield;
	public bool 		isUsingGun;
	public GameObject	m_shield;
	public Vector3		m_aimTo;
	AIState 			aiState;
	
	// Use this for initialization
	void Start () {
		aiState = null;
		if(transform.parent.GetComponent<AIState>())
		{
			aiState = transform.parent.GetComponent<AIState>();
		}
		isUsingGun = true;//start out with gun
		this.renderer.enabled		= isUsingGun;
		m_shield.renderer.enabled	= !isUsingGun;
		gunAndShield 				= transform;//set's pointer to current object's transform.
		//makes code more elegant and easier to follow
	}
	
	// Update is called once per frame
	void Update () {
		if(aiState != null)
			updateRotationToVec3(aiState.GetTargetPos());
		else
		{
			//if(Input.GetMouseButtonDown(2/*middle mouse*/)) { swapGunShield();}
			//updateRotationToMouse();
		}
	}
	public void TurnOnShield()
	{
		isUsingGun = false;
		this.renderer.enabled = false;
		m_shield.collider.enabled = true;
		m_shield.renderer.enabled = true;
		
	}
	public void TurnOnGun()
	{
		isUsingGun = true;
		this.renderer.enabled = true;
		m_shield.collider.enabled = false;
		m_shield.renderer.enabled = false;
		
	}
	public void swapGunShield() {//swaps gun out for shield and vice versa
		isUsingGun = !isUsingGun;//makes it the oposite. only works with boolean value
		this.renderer.enabled 		= isUsingGun;
		m_shield.renderer.enabled 	= !isUsingGun;
	}
	
	public bool isGunEnabled() {return isUsingGun ;}
	
	public void updateRotationToMouse() {
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
	
	public void updateRotationToVec3(Vector3 targetPoint) {
		
		Quaternion targetRotation ;
		
		if( aiState == null ) {
			targetRotation = Quaternion.LookRotation(targetPoint);
		}
		else {
			targetRotation = Quaternion.LookRotation(targetPoint - transform.parent.position);
		}
			
		float curAngle = transform.rotation.eulerAngles.y;
		float tarAngle = targetRotation.eulerAngles.y;
		float toAngle = tarAngle - curAngle;
		gunAndShield.RotateAround(transform.parent.position, transform.parent.transform.up, toAngle * (Time.smoothDeltaTime*10));
		
		//Debug.Log (toAngle);
			
			
	}
}
