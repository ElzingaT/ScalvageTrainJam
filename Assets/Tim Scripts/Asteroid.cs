using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Rigidbody2D rb;

    public float minScale;
    public float maxScale;

    void Start()
    {
        float scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, 1.0f);

        rb = GetComponent<Rigidbody2D>();
        if (Random.Range(0, 10) == 0)
            rb.velocity = new Vector2(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f));
        rb.rotation = 0.0f;
        rb.AddTorque(Random.Range(0.0f, scale * 5.0f));

        // Base health on mass
        GetComponent<DestructibleObject>().health = Mathf.FloorToInt(rb.mass);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, transform.parent.position);
        if (distance < 100.0f)
            return;

        Vector3 force = ((transform.parent.position - gameObject.transform.position) * 0.01f);
        rb.AddForce(force);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!rb)
            rb = GetComponent<Rigidbody2D>();

        if (collision.relativeVelocity.magnitude < 1.0f)
            return;

        if (collision.gameObject.GetComponent<PlayerShipController>())
        {
            // Don't deal damage if smaller than half the player's mass
            if (rb.mass < (collision.gameObject.GetComponent<Rigidbody2D>().mass * 0.5f))
                return;

            GameObject.FindObjectOfType<PlayerShipController>().TakeDamage(1);
            GameObject.FindObjectOfType<PlayerViewController>().UpdateSprite();
        }
        else if (collision.gameObject.GetComponent<Asteroid>())
        {
            // TODO: Check collision speed

            // Only deal damage if larger than the other asteroid
            if (rb.mass > collision.gameObject.GetComponent<Rigidbody2D>().mass)
            {
                // TODO: Deal damage based on difference in mass?
                collision.gameObject.GetComponent<DestructibleObject>().DealDamage(1);
            }
        }

    }
}
