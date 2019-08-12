using UnityEngine;
using System.Collections;

public class ColdProjectile : MonoBehaviour {

	public float freezeTime = 0.1f;
	
	public float GetFreeze(){
		return freezeTime;
	}
	
	public void Hit(){
		Destroy (gameObject);
	}
}
