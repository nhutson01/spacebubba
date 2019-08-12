using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;	
	
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip bossClip;
	public float defaultVolume;
	
	private AudioSource music;	
	
	void Awake () {
		Debug.Log ("Music player Awake " + GetInstanceID());
		
		// Destroys any duplicate music players.
		if (instance != null && instance != this){
			Destroy (gameObject);
			print ("Duplicate music player self-destructed.");
		} else {
			instance = this;		
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.volume = defaultVolume;	
			music.clip = startClip;
			music.loop = true;
			music.Play ();		
		} 
	}
	
	void OnLevelWasLoaded(int level){
		Debug.Log ("MusicPlayer: loaded level " + level);		
		
		if(level == 1  && music.clip != startClip){
			music.Stop ();		
			music.clip = startClip;
			music.Play ();
		}
		if(level == 3 && music.clip != gameClip){
			music.Stop ();
								
			music.clip = gameClip;
			music.Play ();
		}
		if(level == 4 && music.clip != startClip){
			music.Stop ();		
			music.clip = startClip;
			music.Play ();
		}
		if(level == 5 && music.clip != startClip){
			music.Stop ();		
			music.clip = startClip;
			music.Play ();
		}
		if(level == 7 && music.clip != gameClip){
			music.Stop ();		
			music.clip = gameClip;
			music.Play ();
		}
		if(level == 9 && music.clip != gameClip){
			music.Stop ();		
			music.clip = gameClip;
			music.Play ();
		}
				
		music.loop = true;
		
	}

	// Use this for initialization
	public void Start () {		
			Debug.Log ("Music player Start " + GetInstanceID()); 			
	}
	// Update is called once per frame
	void Update () {
		
		}
	}

