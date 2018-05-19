using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour {

	public Transform[] points;
	private int destPoint;
	Rigidbody2D rb;
	[SerializeField]
	Transform currentTarget;

	public bool alerted;

	public float maxSpeed = 2f;
	public float rotSpeed = 90f;

	float pingTimer;

	float shotCooldown;
	float shotDelay = 1f;

	public Transform firePt;
	public GameObject laserPrefab;

	public int health = 3;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		MoveToNextPatrolPoint();	
	}

	void Update(){
		TurnToTarget();
		// MoveForward();

		float distance = Distance(currentTarget.position);
		if(distance < .5f){
			MoveToNextPatrolPoint();
		}
		pingTimer -= Time.deltaTime;
		if(pingTimer <= 0){
			RadarPing();
		}
		if(alerted){
			shotCooldown -= Time.deltaTime;
			if(shotCooldown <= 0 && currentTarget.tag == "Player" && Vector2.Distance(transform.position, currentTarget.position) < 10){
				Fire();
				shotCooldown = shotDelay;
			}
		}
	}

	void FixedUpdate(){
		float thrust = 10f;
		float speed = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y);
		rb.AddForce(transform.up * thrust);
		rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
	}
	void MoveToNextPatrolPoint(){
		if(points.Length == 0){
			return;
		}

		// agent.destination = points[destPoint].position;
		currentTarget = points[destPoint];
		destPoint = (destPoint + 1) % points.Length;
		Debug.Log("Moving to patrol point "+destPoint);
	}

	void MoveForward(){ // I don't like this
		Vector3 pos = transform.position;
		
		Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);
		
		pos += transform.rotation * velocity;

		transform.position = pos;
	}

	void TurnToTarget(){
		Vector3 dir = currentTarget.position - transform.position;
		dir.Normalize();

		float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

		Quaternion desiredRot = Quaternion.Euler( 0, 0,zAngle);

		transform.rotation = Quaternion.RotateTowards( transform.rotation, desiredRot, rotSpeed * Time.deltaTime);
	}

	float Distance(Vector2 target){
		float dist = Vector2.Distance(transform.position, target);
		return dist;
	}

	void RadarPing(){
		Debug.Log("PING");
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
				MoveToNextPatrolPoint();
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
		Instantiate(laserPrefab, firePt.position, firePt.rotation);
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

	void OnCollisionEnter2D(Collision2D col){
		if(col.transform.GetComponent<Patrol>() != null){
			Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), col.gameObject.GetComponent<Collider2D>());
		}
	}
}
