using System;

namespace UnityEngine
{
	public struct V2DF
	{
		float m_x, m_y;
		
		/** access X */
		float getX(){return m_x;}
		/** access Y */
		float getY(){return m_y;}
		/** mutate X */
		void setX(float a_value){m_x=a_value;}
		/** mutate Y */
		void setY(float a_value){m_y=a_value;}
		/** mutate X */
		void addX(float a_value){m_x+=a_value;}
		/** mutate Y */
		void addY(float a_value){m_y+=a_value;}
	}
}

