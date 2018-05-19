using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : ShipComponent {

	void OnCollisionEnter2D(Collision2D col){
		if(col.transform.tag == "Player"){
			col.gameObject.GetComponent<PlayerShipController>().SwapShipComponent(this);
			GameObject.Destroy(gameObject);
		}
	}

}
