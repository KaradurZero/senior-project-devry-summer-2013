using UnityEngine;
using System.Collections;

public class piggieBank : MonoBehaviour {
	
	public GameObject crash;
	
	void OnTriggerEnter(Collider c) {
		if( c.gameObject.name == "Player") {
			Destroy(transform.gameObject);
			GameObject sparks = (GameObject) Instantiate(crash, c.transform.position, Quaternion.identity);
			sparks.transform.LookAt(transform.position + new Vector3(0f,1f,0f));
			Destroy(sparks,2f);
			int goldRevieved;
			float luckImpact = GameObject.Find("Player").GetComponent<CarStat>().GetLuck();
			int diceRoll1 = Random.Range(1,6) * 100;
			int diceRoll2 = Random.Range(1, (int)luckImpact) * 100;
			goldRevieved = diceRoll1 + diceRoll2;
			GameObject.Find("GoldText").GetComponent<GoldGUIDisplay>().AddGoldAmount(goldRevieved);
		}
	}
}

