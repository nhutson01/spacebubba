using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionText : MonoBehaviour {
	public Text text;	
	public LevelManager levelManager;
	public Text next;
	
	private float waitTime = 0.01f;
	
	float y = 223.5f;
	
	Vector2 textPos;
	
	string bubbaStory = "Having made short work of the " + 
						"evil space cow horde, Bubba continues " +
						"his cosmic quest for sustenance.\n\n\n\n " +
						"In the distance, he spots a lone man on " +
						"an asteroid.... ";
				
	
	private bool readyToStart = false;
	
	// Use this for initialization
	void Start () {		
		y = -450;
		StartCoroutine(scrollText());
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 textPos = new Vector2(298f, y);
		text.transform.position = textPos;		
		
		if (readyToStart == true){
			if (Input.GetKeyDown(KeyCode.Space)){
				levelManager.LoadNextLevel();
			}
		}
	}
	
	IEnumerator scrollText(){		
		
		for (int i = 0; i < 1000; i++){ 
			yield return new WaitForSeconds(waitTime);
			y += 1;
			
			if (i >= 1){				
				text.text = bubbaStory;			
			}
			if (i >= 150){				
				readyToStart = true;
				next.text = "Press Space to skip.";		
			}
			
			if (i >= 999){
				yield return new WaitForSeconds(3);
				levelManager.LoadNextLevel();			
			}			
		}	
		
	}
}
