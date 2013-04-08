using UnityEngine;
using System.Collections;

public class vehicleItems : MonoBehaviour {
	
	int m_item;
	void Start() {
		m_item = 0;
	}
	public void Die()
	{
		m_item = 0;
	}
	public int GetItem()
	{
		return m_item;
	}
	public void UseItem()
	{
		m_item = 0;
	}
	public int item
	{
		get {
			return m_item;
		}
		set {
			m_item = value;
		}
	}
}
