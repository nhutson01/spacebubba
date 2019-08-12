using UnityEngine;
using System.Collections;

public class BlorgPosition : MonoBehaviour {

	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(transform.position, 1);
	}
}
