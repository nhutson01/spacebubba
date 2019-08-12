using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Waves : MonoBehaviour {
	public Text text;
	public FormationController form;


	// Use this for initialization
	void Start () {
		form = GameObject.FindObjectOfType<FormationController>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Waves: " + form.waves;
	}
}
