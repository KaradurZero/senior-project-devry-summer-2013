using UnityEngine;
using System.Collections;

public class ranking : MonoBehaviour {
	CheckpointManagerLevel1[] carChecks;
	rankingCar[] carRanks;
	public GameObject[] ranks;//0 - fist, 5 - last
	
	// Use this for initialization
	void Start () {
		carChecks = new CheckpointManagerLevel1[6];
		carChecks[0] = GameObject.Find ("Player").GetComponent<CheckpointManagerLevel1>();
		carChecks[1] = GameObject.Find ("aiDriver_1").GetComponent<CheckpointManagerLevel1>();
		carChecks[2] = GameObject.Find ("aiDriver_2").GetComponent<CheckpointManagerLevel1>();
		carChecks[3] = GameObject.Find ("aiDriver_3").GetComponent<CheckpointManagerLevel1>();
		carChecks[4] = GameObject.Find ("aiDriver_4").GetComponent<CheckpointManagerLevel1>();
		carChecks[5] = GameObject.Find ("aiDriver_5").GetComponent<CheckpointManagerLevel1>();
		
		carRanks = new rankingCar[6];
		carRanks[0] = GameObject.Find ("rc1").GetComponent<rankingCar>();
		carRanks[1] = GameObject.Find ("rc2").GetComponent<rankingCar>();
		carRanks[2] = GameObject.Find ("rc3").GetComponent<rankingCar>();
		carRanks[3] = GameObject.Find ("rc4").GetComponent<rankingCar>();
		carRanks[4] = GameObject.Find ("rc5").GetComponent<rankingCar>();
		carRanks[5] = GameObject.Find ("rc6").GetComponent<rankingCar>();
		
		ranks = new GameObject[7]; //[6] is placeholder for sort
	
	}
	public void RankingUpdate()
	{
		
		ranks[0] = carChecks[0].transform.gameObject;
		ranks[1] = carChecks[1].transform.gameObject;
		ranks[2] = carChecks[2].transform.gameObject;
		ranks[3] = carChecks[3].transform.gameObject;
		ranks[4] = carChecks[4].transform.gameObject;
		ranks[5] = carChecks[5].transform.gameObject;
		int i = 0;
		int j = 0;
		while(i < 5)
		{
			if(carChecks[i].GetLap() < carChecks[i+1].GetLap() && carChecks[i].GetFocus() < carChecks[i+1].GetFocus())
			{
				ranks[i+1] = carChecks[i].gameObject;
				ranks[i] = carChecks[i+1].gameObject;
		//		i = 0;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	RankingUpdate();
	}
}
