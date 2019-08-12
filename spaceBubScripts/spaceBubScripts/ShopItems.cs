using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopItems : MonoBehaviour {
	public Text burger;
	public Text burgSub;
	public int burgRestore;
	public int burgValue;
	
	public Text blueShiny;
	public Text blueSub;
	public int blueIncrease;
	public int blueValue;
	
	public Text purpleShiny;
	public Text purpleSub;
	public static bool purpBought = false;
	public int purpleValue;
	
	
	public Text greenShiny;
	public Text greenSub;
	public float greenIncrease;
	public int greenValue;	
	
	public Text redShiny;
	public Text redSub;
	public float redIncrease;
	public int redValue;
	
	public AudioClip buySound;
	public AudioClip rejectSound;

	public AudioSource sound1;

	public AudioSource sound2;
	
	public float volume;	
	
	// Use this for initialization
	void Start () {		
		burger.text = "Burger $" + burgValue;						
		blueShiny.text = "Blue Shiny $" + blueValue;
		purpleShiny.text = "Purple Shiny $" + purpleValue;	
		greenShiny.text = "Green Shiny $" + greenValue;
		redShiny.text = "Red Shiny $" + redValue;

		sound1 = GetComponent<AudioSource>();
			sound1.volume = volume;
			sound1.clip = buySound;
			sound1.loop = false;

		
			sound2.volume = volume;
			sound2.clip = rejectSound;
			sound2.loop = false;
					
	}
	
	// Update is called once per frame
	void Update () {
		burgSub.text = "Restores " + burgRestore + " health.\n(Player has " + PlayerController.health +
						"/" + PlayerController.maxHealth + ")\nAlso makes Bubba very happy!";
			
		blueSub.text = "Increases max health by " + blueIncrease + ".\nCurrent max health is: " +
						PlayerController.maxHealth;
		
		purpleSub.text = "Allows you to use a healing ability mid-battle. " +
						 "Press Q to activate, but be\n" + 
						 "warned, you cannot do " +
						 "anything while\nhealing.";
						 
		greenSub.text = string.Format("Increases fire rate by a small amount. Current fire rate is:\n{0:#.000} shots per second", (1/PlayerController.fireRate));  
		
		redSub.text = string.Format ("Increases projectile damage by {0:#}\n Current damage is: {1:#}", redIncrease, Projectile.damage);
		
		PlayerController.fireRate = Mathf.Clamp (PlayerController.fireRate, 0.1f, 0.4f);
		
		if (PlayerController.fireRate <= 0.1f){
			PlayerController.fireRate = 0.1f;
			PlayerController.fireRateCapped = true;
		}else PlayerController.fireRateCapped = false;							
	}
	
	public static void Reset(){
		purpBought = false;
	}
	
	public void BurgerBuy(){
		if(PlayerController.health < PlayerController.maxHealth && MoneyManager.moneyCount >= burgValue){
			sound1.Play();			
			PlayerController.health += burgRestore;
			MoneyManager.Deduct(burgValue);
			if (PlayerController.health > PlayerController.maxHealth){
				PlayerController.health = PlayerController.maxHealth;
			}
		}
		else sound2.Play();
	}
	
	public void BlueBuy(){
		if(MoneyManager.moneyCount >= blueValue){
			sound1.Play();	
			PlayerController.maxHealth += blueIncrease;
			MoneyManager.Deduct(blueValue);
		}
		else sound2.Play();
	}
	
	public void PurpleBuy(){
		if((MoneyManager.moneyCount >= purpleValue)&&(!purpBought)){
			sound1.Play();	
			purpBought = true;
			MoneyManager.Deduct(purpleValue);
		}
		else sound2.Play();
	}
	
	public void GreenBuy(){
		if(MoneyManager.moneyCount >= greenValue && PlayerController.fireRateCapped == false){
			sound1.Play();	
			PlayerController.fireRate -= greenIncrease;
			Debug.Log("Capped: " + PlayerController.fireRateCapped + " " + PlayerController.fireRate); 
			MoneyManager.Deduct(greenValue);
			greenValue = (int)(greenValue * 1.4);
			greenShiny.text = "Green Shiny $" + greenValue;
		}
		else sound2.Play();
	}
	
	public void RedBuy(){
		if(MoneyManager.moneyCount >= redValue){
			sound1.Play();	
			Projectile.damage += redIncrease;
			MoneyManager.Deduct(redValue);
		}
		else sound2.Play();
	}
}
