using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Engine : ShipComponent {

	public abstract float EngineThrust();

	void OnTriggerEnter2D(Collider2D col){
		Debug.Log(col.gameObject.name,gameObject);
		if(col.gameObject.name == "TractorBeam"){
			col.gameObject.GetComponentInParent<PlayerShipController>().SwapShipComponent(this);
			GameObject.Destroy(gameObject);
		}
	}
}
