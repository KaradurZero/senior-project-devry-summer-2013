
using UnityEngine;
 
public class FillScreen:MonoBehaviour
{
	Camera cam;
	float pos;
	
	void Start()
	{
		cam = Camera.main;
	}
	
    void Update()
    {
 
        pos = (cam.nearClipPlane + 0.1f);
 
        transform.position = cam.transform.position + cam.transform.forward * pos;
 
        float h = Mathf.Tan(cam.fov*Mathf.Deg2Rad*0.5f)*pos*2f;
 
        transform.localScale = new Vector3(h*cam.aspect,h,0f);
    }
}