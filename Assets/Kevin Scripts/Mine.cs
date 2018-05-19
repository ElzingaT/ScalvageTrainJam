using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

	public float pushForce = 1000f;

	void OnCollisionEnter2D(Collision2D col){
		if(col.transform.tag == "Player"){
			col.gameObject.GetComponent<PlayerShipController>().TakeDamage(2);
			Death();
		}
	}

	void Death(){
		Vector2 origin = new Vector2(transform.position.x,transform.position.y);
		RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, 10f,Vector2.zero);
		for(int i = 0; i < hits.Length; i++){
			Rigidbody2D rb = hits[i].transform.GetComponent<Rigidbody2D>();

			Vector3 heading = transform.position - hits[i].transform.position;
			float dist = heading.magnitude;
			Vector2 direct = heading.normalized;
			//Figure out how to polish this speed
			hits[i].transform.GetComponent<Rigidbody2D>().velocity -= direct * (pushForce * Time.deltaTime);
		}
		GameObject.Destroy(gameObject);
	}

}
