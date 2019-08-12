using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public static int textProgression = 1;
	public Text chat;
	public GameObject panel1;
	public GameObject ShopBox;
	public GameObject Page1;
	public GameObject Page2;
	public GameObject pg2button;
	public GameObject pg1button;
	public GameObject nextButton;
	public GameObject buyButton;
	public GameObject talkButton;
	public GameObject leaveButton;
	
	static bool hasLooped = false;	
	
	void Start(){
		ShopBox.SetActive(false);		
		panel1.SetActive(false);		
		leaveButton.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(hasLooped){
			leaveButton.SetActive(true);
		} else leaveButton.SetActive(false);
		
		switch (textProgression)
		{
		case 0:
			leaveButton.SetActive(true);
			break;
		case 1:
			if (!hasLooped){
				chat.text = "Name....'s..... Orrrrrrrdooo.\n Shiny shiny sell here!";
				nextButton.SetActive(true);
				buyButton.SetActive(false);
				leaveButton.SetActive(false);
			} else textProgression++;
			break;
		case 2:
			chat.text = "You buy shiny now.";
			buyButton.SetActive(true);
			nextButton.SetActive(false);
			leaveButton.SetActive(false);
			break;
		case 3: 
			panel1.SetActive (false);
			ShopBox.SetActive (true);
			Page1.SetActive(true);
			pg2button.SetActive(true);
			pg1button.SetActive(false);
			Page2.SetActive(false);
			leaveButton.SetActive(false);
			break;
		case 4:
			ShopBox.SetActive (false);
			talkButton.SetActive(true);			
			textProgression = 0;
			hasLooped = true;
			break;
		case 5:
			Page1.SetActive(false);
			Page2.SetActive(true);
			pg2button.SetActive(false);
			pg1button.SetActive(true);
			
			leaveButton.SetActive(false);			
			break;		
		}		
		
	}
	
	public void Text1(){
		textProgression = 1;
	}
	
	public void Text2(){
		textProgression = 2;
	}
	
	public void Text3(){
		textProgression = 3;
	}
	
	public void Text4(){
		textProgression = 4;
	}
	
	public void Text5(){
		textProgression = 5;
	}
	
	public void Talk(){
		if (!hasLooped){
			Text1 ();
		} else {
		Text3 ();
		}
		panel1.SetActive (true);		
		talkButton.SetActive(false);
	}
	
	public static void ResetChat(){
		hasLooped = false;
		textProgression = 1;
	}	
	
}
