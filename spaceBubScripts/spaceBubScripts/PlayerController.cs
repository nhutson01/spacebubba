using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float Speed;	
	public float padding;
	public float ypospadding;
	public float ynegpadding;	
	public float projectileSpeed;
	public float startFireRate;		
	public float frozenTime;
	
	public float volume;
	public float hitVolume;
	
	public GameObject projectile;
	public LevelManager levelManager;
	public Health healthBar;	
	
	public AudioClip fireSound;
	public AudioClip dieSound;
	public AudioClip hitSound;
	
	
	public Vector3 bubbaPos;
	
	float ymin, ymax;
	float xmin, xmax;
	float x = 0f, y =-5f;	
	
	float startDamage;
	public static float fireRate;	
	public static float health = 100f;
	public static float maxHealth = 0;
	public static float startMaxHealth;
	public static float killCount = 0;	
	
	private bool frozen = false;
	private bool allowFire = true;
	private bool notHealing = true;
	public static bool isDead = false;
	public static bool fireRateCapped = false;
	
	Color myColor;
	
	
	void OnEnable(){
		frozen = false;
		allowFire = true;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}	
	
	// Use this for initialization
	void Start () {
		myColor = gameObject.GetComponent<Renderer>().material.color;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		healthBar = GameObject.FindObjectOfType<Health>();					
		
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		Vector3 upmost = Camera.main.ViewportToWorldPoint(new Vector3(0,1,distance));
		Vector3 bottommost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
		ymin = bottommost.y + ypospadding;
		ymax = upmost.y- ynegpadding;
		
		// in case we want to change the max health later we'll use this.
		maxHealth = health;
		
		// used for resetting stats on lose
		startMaxHealth = health;
		
		fireRate = startFireRate;
		startDamage = Projectile.damage;
		
		isDead = false;														
	}
	
	IEnumerator Fire(){
		allowFire = false;
		GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0,projectileSpeed,0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position, volume);
		yield return new WaitForSeconds(fireRate);
		allowFire = true;	
	}
	
	// Update is called once per frame
	void Update () {
		 
		
		if (frozen == true){
			gameObject.GetComponent<Renderer>().material.color = Color.cyan;
		}
		
		Vector3 bubbaPos = new Vector3(x, y, 0f);
		this.transform.position = bubbaPos;
				
		// shoot lasers
		if (Input.GetKey(KeyCode.Space)&&(allowFire)&&notHealing&&!frozen){			
			StartCoroutine(Fire());	
		}
		
		// starts the healing process
		if (Input.GetKeyDown (KeyCode.Q)&&(ShopItems.purpBought)&&notHealing&&!frozen){
			StartCoroutine("BeginHealing");
		}
		
		// X movement
		if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))&&notHealing&&!frozen){
			x -= Speed * Time.deltaTime;
		}
		else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))&&notHealing&&!frozen){
			x += Speed * Time.deltaTime;		
		}
		// y movement
		if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))&&notHealing&&!frozen){
			y += Speed * Time.deltaTime;
		}
		else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))&&notHealing&&!frozen){
			y -= Speed * Time.deltaTime;		
		}
		
		if (Input.GetKeyDown (KeyCode.Alpha3)){
			Debug.Log("Kill count: " + killCount);
		}
		// restrict the player to the gamespace
		x = Mathf.Clamp (x, xmin, xmax);
		y = Mathf.Clamp (y, ymin, ymax);
		// restrict health so that burger purchases won't put us over
		// our max health
		health = Mathf.Clamp(health, 0, maxHealth);
		if (health > maxHealth){
			health = maxHealth;
		}				
	}
	
	 IEnumerator BeginHealing(){
		notHealing = false;		
		
		GetComponent<AudioSource>().Play ();
		
		for (int i = 0; i < 100; i++){
			gameObject.GetComponent<Renderer>().material.color = myColor;
			myColor.g -= 0.004f;			
			yield return new WaitForSeconds(0.04f);
		}
		
		health += 25;
		notHealing = true;
		gameObject.GetComponent<Renderer>().material.color = Color.white;
		myColor.g = 1;
		
	}	
	
	
	void OnTriggerEnter2D(Collider2D collider){
		EnemyProjectile missile = collider.gameObject.GetComponent<EnemyProjectile>();
		ColdProjectile freezer = collider.gameObject.GetComponent<ColdProjectile>();
		if (missile){
			if (ShopItems.purpBought){
				StopCoroutine("BeginHealing");
				notHealing = true;
				myColor.g = 1;
				GetComponent<AudioSource>().Stop();
			}
			health -= missile.GetDamage();
			StartCoroutine(HitFlash());
			AudioSource.PlayClipAtPoint(hitSound, transform.position, hitVolume);
			missile.Hit ();
			if (health <= 0){				
				KillBub ();
				levelManager.LoadLevel("Lose");							
			}
			Debug.Log ("Player hit for: " + missile.GetDamage());			
		} else if(freezer){
			frozenTime = freezer.GetFreeze();
			if (ShopItems.purpBought){
				StopCoroutine("BeginHealing");
				notHealing = true;
				myColor.g = 1;
				GetComponent<AudioSource>().Stop();
			}
			AudioSource.PlayClipAtPoint(hitSound, transform.position, hitVolume);
			freezer.Hit ();
			StartCoroutine(FreezeBub());
		}		
	}
	
	IEnumerator FreezeBub(){
		frozen = true;
		gameObject.GetComponent<Renderer>().material.color = Color.cyan;
		yield return new WaitForSeconds(frozenTime);
		
		frozen = false;
		gameObject.GetComponent<Renderer>().material.color = Color.white;
	}
	
	public void KillBub(){
		Destroy (healthBar);
		Destroy(gameObject);
		fireRate = startFireRate;
		killCount = 0;		
		Projectile.damage = startDamage;			
		MoneyManager.Reset();
		ShopItems.Reset();
		LevelManager.ResetLevels();
		SelectionManager.ResetLevelProg();
		EnemyFormation.ResetDifficulties();
		UIManager.ResetChat();		
		health = startMaxHealth;
		isDead = true;
	}
	
	IEnumerator HitFlash(){
		gameObject.GetComponent<Renderer>().material.color = Color.red;
		yield return new WaitForSeconds(0.07f);
		gameObject.GetComponent<Renderer>().material.color = Color.white;
		yield return new WaitForSeconds(0.07f);
		gameObject.GetComponent<Renderer>().material.color = Color.red;
		yield return new WaitForSeconds(0.07f);
		gameObject.GetComponent<Renderer>().material.color = Color.white;		
	}		
}
