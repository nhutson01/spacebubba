using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour {	
	public GameObject enemyPrefab;
	public float width = 10f, height = 6f;	
	public float speed = 5f;
	public float spawnDelay = 0.5f;
	private float xmax, xmin;
	public int waves;
	int startWaves;	
	
	private bool movingRight = false;	
	
	public LevelManager levelManager;
	public GameObject winPanel;
	
	
	// Use this for initialization
	void Start () {
		winPanel.SetActive(false);
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = (Camera.main.ViewportToWorldPoint(new Vector3(0,0, distanceToCamera)));
		Vector3 rightEdge = (Camera.main.ViewportToWorldPoint(new Vector3(1,0, distanceToCamera)));
		xmax = rightEdge.x;
		xmin = leftEdge.x;
		
		SpawnUntilFull();
		
		startWaves = waves;
	}
	
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3 (width, height, 0f));
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if(movingRight){
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		
		float rightEdgeOfFormation = transform.position.x + 0.5f*width;
		float leftEdgeOfFormation = transform.position.x - 0.5f*width;
		if(leftEdgeOfFormation < xmin){
			movingRight = true;
		}else if(rightEdgeOfFormation > xmax){
			movingRight = false;
		}
		
		if (AllMembersDead()){
			waves -= 1;
			waves = Mathf.Clamp(waves, 0, waves);
			if(waves > 0){
			Debug.Log ("Spawning Enemies...");
			SpawnUntilFull();
			}
		}
			
		if(waves <= 0){
			WavesCleared();
		} else {
		winPanel.SetActive (false);
		BubbaHider.isActive = true;		
		}
	}
	
	void WavesCleared(){
		BubbaHider.isActive = false;		
		winPanel.SetActive(true);		
	}
	
	public void KeepFighting(){
		BubbaHider.isActive = true;
		EnemyFormation.UpDifficulty();				
		waves = startWaves + 1;		
	}
		
	Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount == 0){
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	bool AllMembersDead(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount > 0){
				return false;
			}
		}
		return true;		
	}
	
	void SpawnEnemies(){
		foreach(Transform child in transform){
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition();
		
		if(freePosition){
		
				GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
				enemy.transform.parent = freePosition;
			
		}
		if(NextFreePosition()){
		Invoke ("SpawnUntilFull", spawnDelay);
		}
	}					
}
