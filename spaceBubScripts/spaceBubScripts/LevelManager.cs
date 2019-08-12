using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public static bool transition1Done = false;
	public static bool level1returning = false;
	public static bool level2returning = false;
	public static bool level3returning = false;
	
	public static void ResetLevels(){
		transition1Done = false;
		level1returning = false;
		level2returning = false;
		level3returning = false;
	}
	
	public void LoadLevel(string name){		
		
		Debug.Log("Level load requested for: " +name);
		Application.LoadLevel (name);		
	}
	
	public void QuitRequest(){
		Debug.Log ("Quit requested.");
		Application.Quit ();
	}
	
	public void LoadNextLevel(){		
		Application.LoadLevel (Application.loadedLevel + 1);			
	}
	
	public void LoadTransition(){
		if(transition1Done == false){
			Application.LoadLevel ("Transition1");
			transition1Done = true;
		} else {
			Application.LoadLevel ("LevelChoose");
		}
	}
	
	public void LoadLevel1(){
		if(level1returning == false){
			Application.LoadLevel ("Level1");
			level1returning = true;
		} else {
			EnemyFormation.UpDiff1();
			Application.LoadLevel ("Level1");			
		}
	}
	
	public void LoadLevel2(){
		if(level2returning == false){
			Application.LoadLevel ("Level2");
			level2returning = true;
		} else {
			EnemyFormation.UpDiff2();
			Application.LoadLevel ("Level2");			
		}
	}
	
	public void LoadLevel3(){
		if(level3returning == false){
			Application.LoadLevel ("Level3");
			level3returning = true;
		} else {
			EnemyFormation.UpDiff3();
			Application.LoadLevel ("Level3");			
		}
	}
	
	public void LoadNextUncompleted(){
		if(level1returning == false){
			LoadLevel1();
		}
		else if (level2returning == false){
			LoadLevel2();
		}
		else if (level3returning == false){
			LoadLevel3();
		}
	}
	
	void Update(){
		if (Application.loadedLevelName == "Level1"){
			level1returning = true;
		}
		
		if (Application.loadedLevelName == "Level2"){
			level2returning = true;
		}
		
		if (Application.loadedLevelName == "Level3"){
			level3returning = true;
		}	
	}	
}
