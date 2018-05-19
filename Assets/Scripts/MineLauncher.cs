using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineLauncher : MonoBehaviour
{
    public Transform target;
    public GameObject prefab;

    public float minLaunchVelocity;
    public float maxLaunchVelocity;

    public float launchDistance;

    public float minLaunchCooldown;
    public float maxLaunchCooldown;
    public float randCooldown;
    public float lastLaunchTime;

	void Start ()
    {
        randCooldown = Random.Range(minLaunchCooldown, maxLaunchCooldown);
        lastLaunchTime = Time.timeSinceLevelLoad;
    }
	
	void Update ()
    {
        if (Time.timeSinceLevelLoad - lastLaunchTime > randCooldown)
        {
            Launch();
        }
	}

    public void Launch()
    {
        Vector3 launchOrigin = target.transform.position + (Random.onUnitSphere * launchDistance);
        launchOrigin.z = 0;

        GameObject obj = Instantiate(prefab, launchOrigin, Quaternion.identity);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        float speed = Random.Range(minLaunchVelocity, maxLaunchVelocity);
        rb.velocity = ((target.position - obj.transform.position).normalized * speed);

        randCooldown = Random.Range(minLaunchCooldown, maxLaunchCooldown);
        lastLaunchTime = Time.timeSinceLevelLoad;
    }
}
