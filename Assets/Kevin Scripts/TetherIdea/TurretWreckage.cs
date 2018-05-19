using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretWreckage : MonoBehaviour {

	public GameObject guns;
	public float rotateSpeed;
	public GameObject splode;
	Rigidbody2D rb;
	void Update(){
		transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
	}

	public void Destroy(){
		Instantiate(splode, transform.position, transform.rotation);
		Instantiate(guns, transform.position, transform.rotation);
	}

	void OnCollisionEnter2D(Collision2D col){
		if (!rb)
            rb = GetComponent<Rigidbody2D>();
		if(col.gameObject.tag == "Player"){
			GameObject.FindObjectOfType<PlayerShipController>().TakeDamage(1);
            GameObject.FindObjectOfType<PlayerViewController>().UpdateSprite();
		}
	
		if (rb.mass < (col.gameObject.GetComponent<Rigidbody2D>().mass * 0.5f))
                return;

	}
}
