using UnityEngine;
using System.Collections;

public class SelectionManager : MonoBehaviour {
	public GameObject Level2;
	public GameObject Level3;	
	public GameObject NextLevel;
	public static int levelProgression = 1;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {		
		
		switch (levelProgression){
			case 1: Level2.SetActive(false);
					Level3.SetActive(false);					
					NextLevel.SetActive(true);
					break;
			case 2: Level2.SetActive(true);
					Level3.SetActive(false);
					NextLevel.SetActive(true);
					break;
			case 3: Level3.SetActive(true);
					NextLevel.SetActive(false);
					break;						
		}
	}
	
	public void UpLevelProg(){
		levelProgression++;
	}
	
	public static void ResetLevelProg(){
		levelProgression = 1;
	}
}
