using UnityEngine;
using System.Collections;

public class BubbaHider : MonoBehaviour {
	static BubbaHider instance = null;
	
	public static bool isActive = true;
	public GameObject thisBubba;
	public PlayerController Bubba;
	
	
	void Awake(){
		if (instance != null && instance != this){
			Destroy (gameObject);
			print ("Duplicate bubba self-destructed.");
		} else {
			instance = this;		
			GameObject.DontDestroyOnLoad(gameObject);
		}
		
		BubbaHider.isActive = true;
	}
	
	// Use this for initialization
	void Start () {
		thisBubba = GameObject.Find("Bubba Ship");
	}
	
	// Update is called once per frame
	void Update () {
		if (thisBubba != null){
			if(!isActive){
				thisBubba.SetActive(false);
			} else if (isActive == true){
				thisBubba.SetActive(true);
			}
		}
		
		if (Application.loadedLevelName == "Win"){
			Bubba.KillBub();
		}
		
		if (Application.loadedLevelName == "LevelChoose"){
			isActive = false;
		}
		
		if (PlayerController.isDead == true){
			Destroy (gameObject);
		}
	}
}
