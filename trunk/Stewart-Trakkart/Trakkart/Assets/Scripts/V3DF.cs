using System;

namespace UnityEngine
{
	public struct V3DF
	{
		float m_x, m_y, m_z;
		
		/** access X */
		float getX(){return m_x;}
		/** access Y */
		float getY(){return m_y;}
		/** access Z */
		float getZ(){return m_z;}
		/** mutate X */
		void setX(float a_value){m_x=a_value;}
		/** mutate Y */
		void setY(float a_value){m_y=a_value;}
		/** mutate Z */
		void setZ(float a_value){m_z=a_value;}
		/** mutate X */
		void addX(float a_value){m_x+=a_value;}
		/** mutate Y */
		void addY(float a_value){m_y+=a_value;}
		/** mutate Z */
		void addZ(float a_value){m_z+=a_value;}
	}
}

