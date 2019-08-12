using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {
	public static int moneyCount = 0;
	public Text text;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "$" + moneyCount;	
	}
	
	public static void Reset(){
		moneyCount = 0;
	}
	
	public static void Deduct(int i){
		moneyCount -= i;
	} 
}
