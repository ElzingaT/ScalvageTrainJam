using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacemine : MonoBehaviour {

	public GameObject splode;
	public AudioClip clip;

	void OnCollisionEnter2D(Collision2D col){
		DestructibleObject thing = col.gameObject.GetComponent<DestructibleObject>();
		if(thing != null){
			col.gameObject.GetComponent<DestructibleObject>().DealDamage(20);
			Instantiate(splode, transform.position, transform.rotation);
			Destroy(gameObject);
		} else if (col.gameObject.tag == "Player"){
			col.gameObject.GetComponent<PlayerShipController>().TakeDamage(1);
			AudioSource.PlayClipAtPoint(clip, transform.position);
			Instantiate(splode, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
