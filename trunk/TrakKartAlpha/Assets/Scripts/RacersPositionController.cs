using UnityEngine;
using System.Collections;

struct sortingValues {
	public int m_vehiclePosInArray;
	public float m_raceSortedValue;
	//sorted value takes the lap * 100,000 + prevCheckpoint * 1,000 + prevCheckpointDistance * 1;
}

public class RacersPositionController : MonoBehaviour {
	//constant multipliers for creating the raceSortedValue. unable to #define these like in C++
	const int LAPMULTIPLIER 					= 100000;
	const int PREVCHECPOINTMULTIPLIER 			= 1000;
	const int PREVCHECKPOINTDISTANCEMULTIPLIER	= 1;
	
	/// <summary>
	/// for calculating the racers positions in race to see who is in first through last
	/// </summary>
	GameObject [] m_vehicles;
	int m_numOfVehicles;
	sortingValues [] m_sortedArray;
	int m_playerNumInArray;//the position in the array the player is in. for easy grabbing of info
	
	// Use this for initialization
	void Start () {
		m_numOfVehicles = 6;
		m_playerNumInArray = 0;
		
		m_sortedArray = new sortingValues[m_numOfVehicles];
		m_vehicles = GameObject.FindGameObjectsWithTag("Vehicle");
		for(int iter = 0; iter < m_numOfVehicles; ++iter) {
			if(m_vehicles[iter].name == "Player") {
				m_playerNumInArray = iter;
			}
			m_sortedArray[iter].m_raceSortedValue = 0;
			m_sortedArray[iter].m_vehiclePosInArray = iter;
		}//find actual player position in array. how to tell if player or not? object name is player?
		//go through sortingarray and set all m_vehicleposinarray values and m_raceSortedValue to zero;
	}
	
	// Update is called once per frame
	void Update () {
		calculatePositionalValues();//oh gawd the O(n) calls
		performInsertSort();
		//Debug.Log("Player position: " + getPlayerRank());
		
//		Debug.Log(
//			m_vehicles[m_sortedArray[0].m_vehiclePosInArray].name.ToString() + ", " +
//			m_vehicles[m_sortedArray[1].m_vehiclePosInArray].name.ToString() + ", " +
//			m_vehicles[m_sortedArray[2].m_vehiclePosInArray].name.ToString() + ", " +
//			m_vehicles[m_sortedArray[3].m_vehiclePosInArray].name.ToString() + ", " +
//			m_vehicles[m_sortedArray[4].m_vehiclePosInArray].name.ToString() + ", " +
//			m_vehicles[m_sortedArray[5].m_vehiclePosInArray].name.ToString()
//			);
	}
	
	public GameObject [] getSortedArray() { 
		GameObject [] a_sortedArray = new GameObject[m_numOfVehicles];
		for(int iter = 0; iter < m_numOfVehicles; ++iter) {
			a_sortedArray[iter] = m_vehicles[ m_sortedArray[iter].m_vehiclePosInArray];
		}
		return a_sortedArray;
	}
	
	public int getPlayerRank() {
		int playerRank = 0;
		//find player in sorted array by searching for the matching m_vehiclePosInArray and the m_playerNumInArray
		for(int iter = 0; iter < m_numOfVehicles; ++iter) {
			if( m_playerNumInArray == m_sortedArray[iter].m_vehiclePosInArray) {
				playerRank = iter+1;
			}
		}
		return playerRank;
	}
	
	/// <summary>
	/// sets the values that the sorting function will look at.
	/// </summary>
	void calculatePositionalValues() {
		//for each vehicle in sorted array, get vehicle pos in non sorted array then get
		//the lap, checkpoint, and distance from prev checkpoint to calculate
		//the raceSortedValue for sorted array. then set race sorted value from calculated information
		//CheckpointManagerLevel1
		for(int iter = 0; iter < m_numOfVehicles; ++iter) {
			GameObject workingVehicle = m_vehicles[ m_sortedArray[iter].m_vehiclePosInArray];
			float hashedPosition = workingVehicle.GetComponent<CheckpointManagerLevel1>().GetLap() * LAPMULTIPLIER;
			int workingFocus = workingVehicle.GetComponent<CheckpointManagerLevel1>().GetFocus();
//			if(workingFocus == 0) {
//				workingFocus = 17;//magic number represents level 1's number of checkpoints
//				//with 17 being considered the start/finish line
//				
//				//TODO get better way of finding focus(checkpoint). would be
//				//better if could call for prevFocus(checkpoint) number so no internal check 
//				//would be needed here.
//			}
			//prev segment doesn't work due to lap iteration happening when last checkpoint is hit(not when start/finish is run over)
			
			hashedPosition += workingFocus * PREVCHECPOINTMULTIPLIER;
			hashedPosition += workingVehicle.GetComponent<CheckpointManagerLevel1>().GetDistanceFromLastCheckpoint()
				* PREVCHECKPOINTDISTANCEMULTIPLIER;
			
			//position number created, set into array
			m_sortedArray[iter].m_raceSortedValue = hashedPosition;
		}
	}
	
	void performInsertSort() {
		
//		 for i ← 1 to i ← length(A)-1
//   {
//     //The values in A[ i ] are checked in-order, starting at the second one
//     // save A[i] to make a hole that will move as elements are shifted
//     // the value being checked will be inserted into the hole's final position
//     valueToInsert ← A[i]
//     holePos ← i
//     // keep moving the hole down until the value being checked is larger than 
//     // what's just below the hole <!-- until A[holePos - 1] is <= item -->
//     while holePos > 0 and valueToInsert < A[holePos - 1]
//       { //value to insert doesn't belong where the hole currently is, so shift 
//         A[holePos] ← A[holePos - 1] //shift the larger value up
//         holePos ← holePos - 1       //move the hole position down
//       }
//     // hole is in the right position, so put value being checked into the hole
//     A[holePos] ← valueToInsert 
//   }
		//changed to sort from largest in spot 0 and smallest in last spot
		sortingValues valueToInsert;
		int holePos;
		for(int iter = 1; iter < m_numOfVehicles; ++iter) {
			valueToInsert = m_sortedArray[iter];
			holePos = iter;
			while(holePos > 0 && valueToInsert.m_raceSortedValue //is greater than sorts largest values to left
							> m_sortedArray[holePos - 1].m_raceSortedValue) {
				m_sortedArray[holePos] = m_sortedArray[holePos - 1];//shifts larger value up
				holePos -= 1;//moves the hole position down
			}
			m_sortedArray[holePos] = valueToInsert;
		}//end for loop
	}//end function call
	
	
	//TODO create a function to
	//A: create a UI box and draw places of all racers into the box along with 
	//object name(can be changed to racer name later)
}
