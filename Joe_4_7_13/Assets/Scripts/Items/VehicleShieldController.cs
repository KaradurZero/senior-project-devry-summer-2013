using UnityEngine;
using System.Collections;

public class VehicleShieldController : MonoBehaviour {
	
	int m_shieldType;
	Vector3 m_shieldOriginalSize;
	Vector3 m_shieldEnlargedSize;//these affect the X scale only.
	
	bool m_isEnlarged;
	float m_maxEnlargedTime;
	float m_enlargeTimeLeft;
	bool m_isDeflecting;
	float m_maxDeflectorTime;
	float m_deflectorTimeLeft;
	
	enum shields { regular, deflector};
	// Use this for initialization
	void Start () {
		m_shieldType = (int)shields.regular;
		m_isEnlarged = false;
		m_isDeflecting = false;
		m_shieldOriginalSize = this.transform.localScale;
		m_shieldEnlargedSize = new Vector3(m_shieldOriginalSize.x * 2,
			m_shieldOriginalSize.y, m_shieldOriginalSize.z * 1.3f);
		m_maxEnlargedTime = 5.0f;
		m_enlargeTimeLeft = 0.0f;
		m_maxDeflectorTime = 5.0f;
		m_deflectorTimeLeft = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(m_isEnlarged) {
			updateEnlargeTimer();
		}
		if(m_isDeflecting) {
			
		}
	}
	
	void updateEnlargeTimer() {
		m_enlargeTimeLeft -= Time.deltaTime;
		if(m_enlargeTimeLeft <= 0.0f) {
			m_isEnlarged = false;
			m_enlargeTimeLeft = 0.0f;
			//then reset size of shield to original
			this.transform.localScale = m_shieldOriginalSize;
			//this.transform.collider.transform.localScale = m_shieldOriginalSize;
		}
	}
	
	void updateDeflectorTimer() {
		m_deflectorTimeLeft -= Time.deltaTime;
		if(m_deflectorTimeLeft <= 0.0f) {
			m_isDeflecting = false;
			m_deflectorTimeLeft = 0.0f;
			//then reset size of shield to original
			m_shieldType = (int)shields.regular;
			//this.transform.collider.transform.localScale = m_shieldOriginalSize;
		}
	}
	
	public void setDeflecting() {
		m_isDeflecting = true;
		m_deflectorTimeLeft = m_maxDeflectorTime;
	}
	
	public void enlargeShield() {
		m_isEnlarged = true;
		m_enlargeTimeLeft = m_maxEnlargedTime;
		//then set scale size of shield
		this.transform.localScale = m_shieldEnlargedSize;
		//this.transform.collider.transform.localScale = m_shieldEnlargedSize;
	}
	
	public bool isDeflector() {
		return m_shieldType == (int)shields.deflector;	
	}
}
