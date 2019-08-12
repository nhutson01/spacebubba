using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DifficultyText : MonoBehaviour {
	public Text longText;
	public Text shortText1;
	public Text shortText2;
	public Text shortText3;
	
	public Text kills;	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName == "Level1"){
			longText.text = "Current difficulty for\n this level: " + EnemyFormation.lvl1Diff;
		}else if (Application.loadedLevelName == "Level2"){
			longText.text = "Current difficulty for\n this level: " + EnemyFormation.lvl2Diff;
		}else if (Application.loadedLevelName == "Level3"){
			longText.text = "Current difficulty for\n this level: " + EnemyFormation.lvl3Diff;
		}
		
		if (Application.loadedLevelName == "LevelChoose"){
			shortText1.text = "Difficulty: " + EnemyFormation.lvl1Diff;
			shortText2.text = "Difficulty: " + EnemyFormation.lvl2Diff;
			shortText3.text = "Difficulty: " + EnemyFormation.lvl3Diff;
			kills.text = "Kill count: " + PlayerController.killCount;
		}  
	}
	
	
}
