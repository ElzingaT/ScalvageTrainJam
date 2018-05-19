using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	public float moveSpeed;
	public float lifespan;
	float lifeTime;
	// Update is called once per frame
	public bool isPlayerLaser;
	public AudioClip laserHit;
	void Awake(){
		lifeTime = lifespan;
	}
	void Update () {
		lifeTime -= Time.deltaTime;
		if(lifeTime > 0){
			transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
		} else {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		// col.gameObject.GetComponent<DestructibleObject>().DealDamage(1);
		DestructibleObject destObj = col.gameObject.GetComponent<DestructibleObject>();
		if(destObj != null) {
			AudioSource.PlayClipAtPoint(laserHit, transform.position);
			destObj.DealDamage(1);
		}
		Destroy(gameObject);
	}
}
