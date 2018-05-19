using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	public Transform gun1;
	public Transform gun2;
	public GameObject laserPrefab;

	float pingTimer;
	float fireTimer;
	public bool alerted;
	public Transform currentTarget;
	public float rotSpeed = 180f;

	public int health = 5;

	void Start () {
		fireTimer = 1f;
	}

	void Update(){
		pingTimer -= Time.deltaTime;
		if(pingTimer <= 0){
			PingRadar();
			pingTimer = 3f;
		}
		if(alerted){
			fireTimer -= Time.deltaTime;
			if(fireTimer <= 0){
				Fire();
				fireTimer = 1f;
			}
			TurnToTarget();
		}
	}
	
	void TurnToTarget(){
		Vector3 dir = currentTarget.position - transform.position;
		dir.Normalize();

		float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

		Quaternion desiredRot = Quaternion.Euler( 0, 0,zAngle);

		transform.rotation = Quaternion.RotateTowards( transform.rotation, desiredRot, rotSpeed * Time.deltaTime);
	}

	void PingRadar(){
		pingTimer = 3f;
		if(alerted){
			Vector2 origin = new Vector2(transform.position.x,transform.position.y);
			RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, 10f,Vector2.zero);
			bool inRange = false;
			for(int i = 0; i < hits.Length; i++){
				if(hits[i] == true && hits[i].transform.tag == "Player"){
					currentTarget = hits[i].transform;
					inRange = true;
					Debug.Log("Still in range");
				}
			}
			if(!inRange){
				alerted = false;
				currentTarget = null;
				Debug.Log("Player lost, returning to patrol");
			}
		} else {
			Vector2 origin = new Vector2(transform.position.x,transform.position.y);
			RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, 10f,Vector2.zero);
			for(int i = 0; i < hits.Length; i++){
				if(hits[i] == true && hits[i].transform.tag == "Player"){
					Debug.Log("METAL GEAR ALERT NOISE");
					currentTarget = hits[i].transform;
					alerted = true;
				}
			}
		}
	}

	void Fire(){
		Instantiate(laserPrefab, gun1.transform.position, gun1.transform.rotation);
		Instantiate(laserPrefab, gun2.transform.position, gun2.transform.rotation);
	}

	public void TakeDamage(int dmg){
		health -= dmg;
		if(health <= 0){
			Death();
		}
	}
	public void Death(){
		Destroy(gameObject);
	}
}
