using UnityEngine;
using System.Collections;

public class saveTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        savedStatistics profile = new savedStatistics();
        saveSystem saver = new saveSystem(profile);
        //saver.SaveData();
        saver.LoadData();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
