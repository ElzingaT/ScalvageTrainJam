using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour {

	public int hull = 10;
	public int shields = 0;

	Rigidbody2D rb;
	public float rotationFactor = 360f;
	public float thrustSpeed = 2f;
	float maxVelocity = 5f;

	public GameObject laserPrefab;
	public Transform laserSpawn;

	public TractorBeam beam;

	public Engine engine;
	public ShieldGenerator shieldGenerator;
	public Guns guns;
	public Hyperdrive hyperdrive;

    public bool usingThrusters = false;
	public bool canShoot;
	public float shootCooldown = 1.0f;
	public float shootTimer;
    public AudioClip impactSfx;
	public AudioClip deathSound;
	public AudioClip laserShootFx;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		beam = GetComponentInChildren<TractorBeam>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetAxis("Horizontal") < 0 ){
			float desiredRot = transform.eulerAngles.z + rotationFactor * Time.deltaTime * 30f;
			Quaternion quat = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, desiredRot);
			transform.rotation = Quaternion.Lerp(transform.rotation, quat, Time.deltaTime * 100f);
		} else if(Input.GetAxis("Horizontal") > 0 ){
			float desiredRot = transform.eulerAngles.z - rotationFactor * Time.deltaTime * 30f;
			Quaternion quat = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, desiredRot);
			transform.rotation = Quaternion.Lerp(transform.rotation, quat, Time.deltaTime * 100f) ;
		}

        if (Input.GetButton("TractorBeam"))
        {
            beam.ActivateBeam();
        }
        else
        {
            beam.tractorActive = false;
        }

        if (shieldGenerator != null)
        {
            shields = shieldGenerator.GetShields();
        }
 
        canShoot = (guns != null && (Time.timeSinceLevelLoad - shootTimer > shootCooldown));
        if (canShoot && Input.GetButton("Shoot"))
        {
            AudioSource.PlayClipAtPoint(laserShootFx, transform.position);
            Instantiate(laserPrefab, laserSpawn.position, laserSpawn.rotation);
            shootTimer = Time.timeSinceLevelLoad;
        }
		
		bool canJump = CheckWinCondition();
	}

	void FixedUpdate(){
        usingThrusters = Input.GetAxis("Thruster") == -1;
        if (usingThrusters) { // come back and add incremental thrust
			float thrust = engine.EngineThrust();
			float speed = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y);
			rb.AddForce(transform.up * thrust);
			rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
		}
	}

	public void SwapShipComponent(ShipComponent newComponent){
		if(newComponent is Engine){
			if(newComponent is DamagedEngine){
				engine = gameObject.AddComponent<DamagedEngine>();
			}
			else if(newComponent is FullEngine){
				engine = gameObject.AddComponent<FullEngine>();
			}
			Debug.Log("This is in fact, an engine: " +engine);
		} else if(newComponent is ShieldGenerator){
			shieldGenerator = gameObject.AddComponent<ShieldGenerator>();
			Debug.Log("Shields Up Capn");
		} else if(newComponent is Hyperdrive){
			hyperdrive = gameObject.AddComponent<Hyperdrive>();
			Debug.Log("Hyperdrive engaged");
		} else if (newComponent is Guns){
			guns = gameObject.AddComponent<Guns>();
			canShoot = true;
			Debug.Log("GUNS");
		}
	}

	public void TakeDamage(int dmg){
		if(shields > 0){ //If shields, cannot take hull damage on this instance
			shields = shieldGenerator.TakeDamage(dmg);
			if(shields <= 0){
				shields = 0;
			}
		} else {
			hull -= dmg;
		}
		if(hull <= 0){
			Death();
		}
	}

	void Death(){
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
        // GetComponent<PlayerViewController>().OnPlayerDeath();
        LevelManager lm = GameObject.FindObjectOfType<LevelManager>();
        if (lm)
            lm.GameLost();

        this.enabled = false;
    }

	bool CheckWinCondition(){
		if(shieldGenerator == null){
			return false;
		} else if(engine == null || engine is FullEngine){
			return false;
		} else if(hyperdrive == null){
			return false;
		} 
		//all parts recovered
		return true;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 1.0f)
        {
            AudioSource.PlayClipAtPoint(impactSfx, transform.position);
        }
    }
}
