using UnityEngine;
using System.Collections;

public class EnemyFormation : MonoBehaviour {
	public float health;
	public GameObject projectile;
	public GameObject projectile2;
	public float projectileSpeed;		
	public float shotsPerSeconds = 0.5f;
	public float volume;
	public float orbVolume;
	public float hitVolume;
	public float dieVolume;
	public float xDir = 5f;
	// used for level 1 enemies
	public static float difficultyMultiplier1 = 1.0f;
	// used for level 2 enemies
	public static float difficultyMultiplier2 = 1.0f;
	// used for level 3 enemies
	public static float difficultyMultiplier3 = 1.0f;	
	float currentDiff;
	
	public static int lvl1Diff = 1;
	public static int lvl2Diff = 1;
	public static int lvl3Diff = 1;	
	
	// used for modifying the projectile's damage when
	// the difficulty goes up, since the actual damage
	// variable is a static.
	public float tempDamage = 10f;
	public float tempDamage2 = 12f;
	public float tempDamage3 = 14f;	
	
	public Sprite[] hitSprites;
	int damageStatus = 0;	
	
	public int minMoney;
	public int maxMoney;	
	
	public GameObject blood;
	
	public AudioClip fireSound;
	public AudioClip fireSound2;
	public AudioClip hitSound;
	public AudioClip deathSound;
	public AudioClip arrivalSound;
	public AudioClip fireSound3;
	
	Color myColor = Color.red;
	float maxHealth;
	
	void Start(){
		AudioSource.PlayClipAtPoint(arrivalSound, transform.position);		
		
		maxHealth = health;
		
		if (Application.loadedLevelName == "Level1"){
			currentDiff = difficultyMultiplier1;
			projectileSpeed *= currentDiff;
			shotsPerSeconds *= currentDiff;
			health *= currentDiff;
			EnemyProjectile.damage = (tempDamage *= currentDiff);					
		}
		if (Application.loadedLevelName == "Level2"){
			currentDiff = difficultyMultiplier2;
			projectileSpeed *= currentDiff;
			shotsPerSeconds *= currentDiff;
			health *= currentDiff;
			EnemyProjectile.damage = (tempDamage2 *= currentDiff);			
		}
		if (Application.loadedLevelName == "Level3"){
			currentDiff = difficultyMultiplier3;
			projectileSpeed *= currentDiff;
			shotsPerSeconds *= currentDiff;
			health *= currentDiff;
			EnemyProjectile.damage = (tempDamage3 *= currentDiff);			
		}		
	}
		
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if (missile){
			StartCoroutine(HitFlash());			
			AudioSource.PlayClipAtPoint(hitSound, transform.position, hitVolume);					
			health -= missile.GetDamage();
			missile.Hit ();
			if (health <= 0){
				PlayerController.killCount += 1;
				Die();
			}
			if (health <= maxHealth/1.2f){
				damageStatus = 1;
			}
			if (health <= maxHealth/3.2f){
				damageStatus = 2;
			}
			LoadSprites(); 			
		}		
	}
	
	IEnumerator HitFlash(){
		gameObject.GetComponent<Renderer>().material.color = myColor;
		yield return new WaitForSeconds(0.07f);
		gameObject.GetComponent<Renderer>().material.color = Color.white;
		yield return new WaitForSeconds(0.07f);
		gameObject.GetComponent<Renderer>().material.color = myColor;
		yield return new WaitForSeconds(0.07f);
		gameObject.GetComponent<Renderer>().material.color = Color.white;		
	}
	
	void LoadSprites(){
		int spriteIndex = damageStatus;
		if (hitSprites[spriteIndex]){
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex]; 
		} else {
			Debug.LogError("Missing sprite");
		}
	}
	
	void Die(){
		AudioSource.PlayClipAtPoint(deathSound, transform.position, dieVolume);
		SplatBlood();				
		MoneyManager.moneyCount += Random.Range (minMoney, maxMoney);
		
		Destroy(gameObject);		
	}
	
	void SplatBlood(){
		GameObject bloodSplat = Instantiate (blood, transform.position, Quaternion.identity) as GameObject;
	}
	
	void Fire(){
		if (Application.loadedLevelName == "Level1")
		{
			Vector3 startPosition = transform.position + new Vector3(0, 0, 0);
			GameObject beam = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
			beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0,-projectileSpeed,0);
			AudioSource.PlayClipAtPoint(fireSound, transform.position, volume);
		} 		
		else if((Application.loadedLevelName == "Level2") && (Random.value > 0.7f)){
			if(Random.value < 0.5f){
				Vector3 startPosition = transform.position + new Vector3(0, 0, 0);
				GameObject orb = Instantiate(projectile2, startPosition, Quaternion.identity) as GameObject;
				orb.GetComponent<Rigidbody2D>().velocity = new Vector3 (-xDir,(-projectileSpeed/1.5f),0);
				AudioSource.PlayClipAtPoint(fireSound2, transform.position, orbVolume);
			} else{
				Vector3 startPosition = transform.position + new Vector3(0, 0, 0);
				GameObject orb = Instantiate(projectile2, startPosition, Quaternion.identity) as GameObject;
				orb.GetComponent<Rigidbody2D>().velocity = new Vector3 (xDir,(-projectileSpeed/2),0);
				AudioSource.PlayClipAtPoint(fireSound2, transform.position, orbVolume);
			}
		}
		else if(Application.loadedLevelName == "Level3"){ 
			if (Random.value < 0.8f){
				Vector3 startPosition = transform.position + new Vector3(0, 0, 0);
				GameObject beam1 = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
				beam1.GetComponent<Rigidbody2D>().velocity = new Vector3 (0,-projectileSpeed,0);
				AudioSource.PlayClipAtPoint(fireSound, transform.position, volume);
		} else {
			if(Random.value < 0.5f){
				Vector3 startPosition = transform.position + new Vector3(0, 0, 0);
				GameObject orb = Instantiate(projectile2, startPosition, Quaternion.identity) as GameObject;
				orb.GetComponent<Rigidbody2D>().velocity = new Vector3 (-xDir,(-projectileSpeed/1.5f),0);
				AudioSource.PlayClipAtPoint(fireSound3, transform.position, orbVolume);
			} else {
				Vector3 startPosition = transform.position + new Vector3(0, 0, 0);
				GameObject orb = Instantiate(projectile2, startPosition, Quaternion.identity) as GameObject;
				orb.GetComponent<Rigidbody2D>().velocity = new Vector3 (xDir,(-projectileSpeed/2),0);
				AudioSource.PlayClipAtPoint(fireSound3, transform.position, orbVolume);
				}
			}
	 	}
	}
	
	void Update(){
		float probability = Time.deltaTime * shotsPerSeconds;
		if(Random.value < probability){
		Fire ();		
		}	
		
		if (Application.loadedLevelName == "Lose" || Application.loadedLevelName == "Win"){
			difficultyMultiplier1 = 1.0f;
			difficultyMultiplier2 = 1.0f;
			difficultyMultiplier3 = 1.0f;			
		}	
		
		if (Input.GetKeyDown(KeyCode.Alpha2)){
			Debug.Log("Current Diff: " +currentDiff + " diff1: " + difficultyMultiplier1 + " diff2: " + difficultyMultiplier2 + " diff3: " + difficultyMultiplier3);
		}
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			Debug.Log ("Projectile speed: " + projectileSpeed + " SPS: " + shotsPerSeconds + " Health: " + health + " Damage: " + EnemyProjectile.damage);
		}
		
	}
	
	public static void UpDifficulty(){
		if (Application.loadedLevelName == "Level1"){				
			EnemyFormation.difficultyMultiplier1 += 0.1f;
			lvl1Diff++;
		}else if (Application.loadedLevelName == "Level2"){				
			EnemyFormation.difficultyMultiplier2 += 0.1f;
			lvl2Diff++;
		}else if (Application.loadedLevelName == "Level3"){				
			EnemyFormation.difficultyMultiplier3 += 0.1f;
			lvl3Diff++;
		}
			
	}
	
	public static void UpDiff1(){
		EnemyFormation.difficultyMultiplier1 += 0.1f;
		lvl1Diff++;
	}
	
	public static void UpDiff2(){
		EnemyFormation.difficultyMultiplier2 += 0.1f;
		lvl2Diff++;
	}
	
	public static void UpDiff3(){
		EnemyFormation.difficultyMultiplier3 += 0.1f;
		lvl3Diff++;
	}
	
	public static void ResetDifficulties(){
		EnemyFormation.difficultyMultiplier1 = 1.0f;
		EnemyFormation.difficultyMultiplier2 = 1.0f;
		EnemyFormation.difficultyMultiplier3 = 1.0f;
		lvl1Diff = 1;
		lvl2Diff = 1;
		lvl3Diff = 1;
	}
}
