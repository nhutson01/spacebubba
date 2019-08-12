using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {

	public static float damage = 10f;
	
	public float GetDamage(){
		return damage;
	}
	
	public void Hit(){
		Destroy (gameObject);
	}
}
