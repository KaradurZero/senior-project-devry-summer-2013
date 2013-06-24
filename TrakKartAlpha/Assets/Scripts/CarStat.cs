using UnityEngine;
using System.Collections;

//********************************CAR STATS*********************************//
//Sets the statistics of all vehicles, both player and AI, based on skill points//

public class CarStat : MonoBehaviour {
	
	statsFromMenu menuStats;
	public float m_maxVelocity ;
	public float m_acceleration ;
	public float m_boostVelocity ;
	public GameObject m_overheatedGUI;
	public GameObject m_engineFlames;
	
	private int m_attack ;
	private int m_defense ;
	private float m_cooling ;
	private float m_handling ;
	private float m_luck ;
	
	private float m_maxTemp ;
	private float m_currTemp ;
	private bool m_tempPerSec ;
	private bool m_overheated ;
	
	public float m_currVelocity ;
	
	private float m_maxHealth ;
	
	private float m_defaultMaxVelocity ;
	private float m_defaultAcceleration ;
	private float m_defaultHandling ;
	
	//return values for other scripts to use
	public float GetMaxVelocity( ) {return m_maxVelocity ;}
	public float GetAccel( ) {return m_acceleration ;}
	public int GetAttack( ) {return m_attack ;}
	public int GetDefense( ) {return m_defense ;}
	public float GetCooling( ) {return m_cooling ;}
	public float GetHandling( ) {return m_handling;}
	public float GetLuck( ) {return m_luck;}
	public float GetMaxTemp( ) {return m_maxTemp;}
	public float GetCurrTemp( ) {return m_currTemp;}
	
	public bool GetTempPerSec( ) {return m_tempPerSec ;}
	public bool isOverheated( ) {return m_overheated ;}
	
	public float GetCurrentSpeed( ) {return m_currVelocity ;}
	public float GetBoostSpeed( ) {return m_boostVelocity ;}
	
	public float GetMaxHealth( ) {return m_maxHealth ;}
	
	//raises temperature based on attack skill
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
	
	//raises temperature based on defense skill
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
	
	//Set value functions for other scripts to use
	public void SetMaxVelocity( float a_speed ) {m_maxVelocity = a_speed ;}
	public void SetAccel( float a_acceleration ) {m_acceleration = a_acceleration ;}
	public void SetAttack( int a_attack ) {m_attack = a_attack ;}
	public void SetDefense( int a_defense ) {m_defense = a_defense ;}
	public void SetCooling( float a_cooling ) {m_cooling = a_cooling ;}
	public void SetHandling( float a_handling ) {m_handling = a_handling ;}
	public void SetLuck( float a_luck ) {m_luck = a_luck ;}
	
	public void SetMaxTemp( float a_maxTemp ) {m_maxTemp = a_maxTemp ;}
	
	//controls whether vehicle is overheated or not
	public void SetCurrTemp( float a_currTemp ) {
		m_currTemp = a_currTemp ;
		
		if( m_currTemp >= m_maxTemp ) {
			m_currTemp = m_maxTemp ;
			m_tempPerSec = false ;
			m_overheated = true ;
			GameObject flames = (GameObject) Instantiate(m_engineFlames,transform.position, m_engineFlames.transform.rotation) as GameObject;
			flames.GetComponent<FollowSmoke>().gameObjectToFollow = gameObject.transform;
			Destroy (flames, 5f);
			if(name == "Player")
			{
				GameObject gui = (GameObject) Instantiate(m_overheatedGUI) as GameObject;
				gui.GetComponent<DissolveImageGUI>().lifeTime = 5f;
				Destroy(gui, 5f);
				flames.GetComponent<BreakBox>().isPlayer = true;
			}
			else
				flames.GetComponent<BreakBox>().isPlayer = false;
		}
		if( m_currTemp <= 0 ) {
			m_currTemp = 0 ;
			m_overheated = false ;
		}
	
	}
	
	//turns auto-temp raising on/off
	public void TempPerSecOn( ) {m_tempPerSec = true ;}
	public void TempPerSecOff( ) {m_tempPerSec = false ;}
	
	//sets current velocity of object
	//keeps object from moving OVER maximum velocity
	public void SetCurrentSpeed( float a_currSpeed ) {m_currVelocity = a_currSpeed ;}
	
	public void ResetVelocity( ) {m_maxVelocity = m_defaultMaxVelocity;}
	public void ResetAcceleration( ) {m_acceleration = m_defaultAcceleration;}

	// Use this for initialization
	void Start () {
		
		//set manually for testing purposes
		m_currVelocity = 0f ;
		
		m_attack = 1 ;
		m_defense = 1 ;
		m_cooling = 20f ; //cooling stat has been gutten from the game, but used for normal temperature cooling rate
		m_handling = 5f ; //handling stat has been gutted from the game, but used for normal vehicle rotation
		m_luck = 1f ;
		
		m_maxTemp = 100f ;
		m_currTemp = 0f ;
		m_tempPerSec = m_overheated = false ;
		
		m_maxHealth = 100f ;
		
		m_defaultMaxVelocity = m_maxVelocity ;
		m_defaultAcceleration = m_acceleration ;
		m_defaultHandling = m_handling ;
		
		//create a gameobject to send info back to menu
		GameObject	statsFromGame = new GameObject("statsFromGame");
		MenuStatsFromGame stats =  statsFromGame.AddComponent<MenuStatsFromGame>();
		stats.SetStats(1,1,1,1,1,1,1,1);	//default values
		stats.SetGold(0);
		//if stats have been set in 
//		if(gameObject.name == "Player")
//		{
			menuStats = GameObject.Find("MenuStats").GetComponent<statsFromMenu>();
			if(menuStats != null)
			{
				m_maxVelocity 	= m_defaultMaxVelocity 	= menuStats.GetSpeed() + 15;
				m_acceleration 	= m_defaultAcceleration = menuStats.GetAccel() + 10;
				m_maxTemp 		= 100 + (menuStats.GetTemp() * 20);
				m_boostVelocity = m_maxVelocity + (menuStats.GetBoost() * 3);
				m_maxHealth 	= 100 + (menuStats.GetHealth() * 50);
				m_luck			= menuStats.GetLuck() * 10;
				m_attack		= menuStats.GetAttack() ;
				m_defense	 	= menuStats.GetDefense() ;
				m_cooling 		= m_maxTemp * .2f ; //temperature always cool down based on max temp per second
				
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
//		}
	}
}
