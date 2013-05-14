using UnityEngine;
using System.Collections;

public class CarStat : MonoBehaviour {
	
	statsFromMenu menuStats;
	public float m_maxVelocity ;
	public float m_acceleration ;
	public float m_boostVelocity ;
	
	private int m_attack ;
	private int m_defense ;
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
	public int GetAttack( ) {return m_attack ;}
	public int GetDefense( ) {return m_defense ;}
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
		switch( m_attack ) {
		case 1:
			return 20f ;
			break ;
		case 2:
			return 18f ;
			break ;
		case 3:
			return 16f ;
			break ;
		case 4:
			return 14f ;
			break ;
		case 5:
			return 12f ;
			break ;
		default:
			return 10f ;
		}
	}
	
	public float FindDefenseTempCost( ) {
	//USE DEFENSE VALUE FOR RAISING TEMPERATURE
		switch( m_attack ) {
		case 1:
			return 25f ;
			break ;
		case 2:
			return 23f ;
			break ;
		case 3:
			return 21f ;
			break ;
		case 4:
			return 19f ;
			break ;
		case 5:
			return 17f ;
			break ;
		default:
			return 15f ;
		}
	}
	
	public void SetMaxVelocity( float a_speed ) {m_maxVelocity = a_speed ;}
	public void SetAccel( float a_acceleration ) {m_acceleration = a_acceleration ;}
	public void SetAttack( int a_attack ) {m_attack = a_attack ;}
	public void SetDefense( int a_defense ) {m_defense = a_defense ;}
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
		
		m_attack = 1 ;
		m_defense = 1 ;
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
		
		//if stats have been set in 
		if(gameObject.name == "Player")
		{
			menuStats = GameObject.Find("MenuStats").GetComponent<statsFromMenu>();
			if(menuStats != null)
			{
				m_maxVelocity 	= m_defaultMaxVelocity 	= menuStats.GetSpeed() + 10;
				m_acceleration 	= m_defaultAcceleration = menuStats.GetAccel() + 10;
				m_maxTemp 		= 100 + (menuStats.GetTemp() * 500);
				m_boostVelocity = 20 + menuStats.GetBoost() * 5;
				m_maxHealth 	= 100 + (menuStats.GetHealth() * 50);
				m_luck			= menuStats.GetLuck() * 10;
				m_attack		= menuStats.GetAttack() ;
				m_defense	 	= menuStats.GetDefense() ;
				
				//create a gameobject to send info back to menu
				GameObject	statsFromGame = new GameObject("statsFromGame");
				MenuStatsFromGame stats =  statsFromGame.AddComponent<MenuStatsFromGame>();
				stats.SetStats(
					menuStats.GetSpeed(),
					menuStats.GetAccel(),
					menuStats.GetTemp(),
					menuStats.GetBoost(),
					menuStats.GetLuck(),
					menuStats.GetHealth(),
					menuStats.GetAttack(),
					menuStats.GetDefense());
				
			}
		}
	}
}
