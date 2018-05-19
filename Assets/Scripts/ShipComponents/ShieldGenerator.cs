using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGenerator : ShipComponent {

	float shieldCooldown = 5f;
	public float shieldCooldownTimer;
	public float shields;
	public float maxShields = 5f ;

	void Update(){
		if(shieldCooldownTimer <= 0 && shields < maxShields){
			shields += Time.deltaTime;
			if(shields > maxShields){
				shields = maxShields;
			}
		} else {
			shieldCooldownTimer -= Time.deltaTime;
		}
	}

	public int GetShields(){
		return Mathf.RoundToInt(shields);
	}

	public int TakeDamage(int dmg){
		shieldCooldownTimer = shieldCooldown;
		shields = (dmg > shields) ? 0 : (shields - dmg);
		return Mathf.RoundToInt(shields);
	}


	void OnCollisionEnter2D(Collision2D col){
		if(col.transform.tag == "Player"){
			col.gameObject.GetComponent<PlayerShipController>().SwapShipComponent(this);
			GameObject.Destroy(gameObject);
		}
	}
}
