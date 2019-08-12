using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {
	public Text text;	
	public LevelManager levelManager;
	public Text next;
	
	private float waitTime = 0.01f;

	float y = 223.5f;

	Vector2 textPos;
	
	string bubbaStory = "After having depleted Planet " + 
						"Earth of it's consumable resources, " +
						"Bubba seeks to sate his hunger elsewhere. " +
						"Donning his trusty wingsuit, he takes " +
						"to the cosmos.\n\n\n\n\n\n\n He soon discovers that he " +
						"is not alone...";
						
	private bool hasStarted = false;
	private bool readyToStart = false;
	
	// Use this for initialization
	void Start () {		
		text.text = ("Press Space to begin.");
		
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		
				
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 textPos = new Vector2(298f, y);
		text.transform.position = textPos;
		
		if (!hasStarted){
			if (Input.GetKeyDown (KeyCode.Space)){
				y = -650;
				hasStarted = true;
								
				StartCoroutine(scrollText());
							
			}
		} 
		
		if (readyToStart == true){
			if (Input.GetKeyDown(KeyCode.Space)){
				levelManager.LoadLevel("Start");
			}
		}
	}
	
	IEnumerator scrollText(){		
		
		for (int i = 0; i < 1350; i++){ 
		yield return new WaitForSeconds(waitTime);
		y += 1;
		
			if (i >= 1){
				readyToStart = true;
				next.text = "Press Space to skip.";
				text.text = bubbaStory;			
			}
			
			if (i >= 1349){
				yield return new WaitForSeconds(3);
				levelManager.LoadLevel("Start");			
			}			
		}	
		
	}	
	
}