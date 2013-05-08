using UnityEngine;
using System.Collections;

public class CarStat : MonoBehaviour {
	
	public float m_maxVelocity ;
	public float m_acceleration ;
	public float m_boostVelocity ;
	
	private float m_attack ;
	private float m_defense ;
	private float m_cooling ;
	private float m_handling ;
	private float m_luck ;
	
	private float m_maxTemp ;
	private float m_currTemp ;
	private bool m_tempPerSec ;
	private bool m_overheated ;
	
	private float m_currVelocity ;
	
	private float m_maxHealth ;
	
	private float m_defaultMaxVelocity ;
	private float m_defaultAcceleration ;
	private float m_defaultHandling ;
	
	public float GetMaxVelocity( ) {return m_maxVelocity ;}
	public float GetAccel( ) {return m_acceleration ;}
	public float GetAttack( ) {return m_attack ;}
	public float GetDefense( ) {return m_defense ;}
	public float GetCooling( ) {return m_cooling ;}
	public float GetHandling( ) {return m_handling;}
	public float GetLuck( ) {return m_luck;}
	
	public float GetMaxTemp( ) {return m_maxTemp ;}
	public float GetCurrTemp( ) {return m_currTemp ;}
	public bool GetTempPerSec( ) {return m_tempPerSec ;}
	public bool isOverheated( ) {return m_overheated ;}
	
	public float GetCurrentSpeed( ) {return m_currVelocity ;}
	public float GetBoostSpeed( ) {return m_boostVelocity ;}
	
	public float GetMaxHealth( ) {return m_maxHealth ;}
	
	public float FindAttackTempCost( ) {
	//USE ATTACK VALUE FOR RAISING TEMPERATURE
		return 20f ;
	}
	
	public float FindDefenseTempCost( ) {
	//USE DEFENSE VALUE FOR RAISING TEMPERATURE
		return 25f ;
	}
	
	public void SetMaxVelocity( float a_speed ) {m_maxVelocity = a_speed ;}
	public void SetAccel( float a_acceleration ) {m_acceleration = a_acceleration ;}
	public void SetAttack( float a_attack ) {m_attack = a_attack ;}
	public void SetDefense( float a_defense ) {m_defense = a_defense ;}
	public void SetCooling( float a_cooling ) {m_cooling = a_cooling ;}
	public void SetHandling( float a_handling ) {m_handling = a_handling ;}
	public void SetLuck( float a_luck ) {m_luck = a_luck ;}
	
	public void SetMaxTemp( float a_maxTemp ) {m_maxTemp = a_maxTemp ;}
	public void SetCurrTemp( float a_currTemp ) {
		m_currTemp = a_currTemp ;
		
		if( m_currTemp >= m_maxTemp ) {
			m_currTemp = m_maxTemp ;
			m_tempPerSec = false ;
			m_overheated = true ;
		}
		if( m_currTemp <= 0 ) {
			m_currTemp = 0 ;
			m_overheated = false ;
		}
	
	}
	public void TempPerSecOn( ) {m_tempPerSec = true ;}
	public void TempPerSecOff( ) {m_tempPerSec = false ;}
	
	//sets current velocity of object
	//keeps object from moving OVER maximum velocity
	public void SetCurrentSpeed( float a_currSpeed ) {m_currVelocity = a_currSpeed ;}
	
	public void ResetVelocity( ) {m_maxVelocity = m_defaultMaxVelocity;}
	public void ResetAcceleration( ) {m_acceleration = m_defaultAcceleration;}
	public void ResetHandling( ) {m_handling = m_defaultHandling;}

	// Use this for initialization
	void Start () {
		//m_maxVelocity = 200f ;
		//m_boostVelocity = 400f ;
		//m_acceleration = 100f ;
		
		m_currVelocity = 0f ;
		
		m_attack = 10f ;
		m_defense = 10f ;
		m_cooling = 20f ;
		m_handling = 5f ;
		m_luck = 5f ;
		
		m_maxTemp = 100f ;
		m_currTemp = 0f ;
		m_tempPerSec = m_overheated = false ;
		
		m_maxHealth = 100f ;
		
		m_defaultMaxVelocity = m_maxVelocity ;
		m_defaultAcceleration = m_acceleration ;
		m_defaultHandling = m_handling ;
	}
	
	public void LoadStats( ) {
		
	}
}
