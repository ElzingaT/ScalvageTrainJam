using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherableDebris : MonoBehaviour {

	Rigidbody2D rb;

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
			Destroy(gameObject);
        }
        else if (collision.gameObject.GetComponent<Asteroid>())
        {
			if(collision.relativeVelocity.magnitude > 1.0f)
			collision.gameObject.GetComponent<DestructibleObject>().DealDamage(20);
			Destroy(gameObject);
            // // TODO: Check collision speed

            // // Only deal damage if larger than the other asteroid
            // if (rb.mass > collision.gameObject.GetComponent<Rigidbody2D>().mass)
            // {
            //     // TODO: Deal damage based on difference in mass?
            //     collision.gameObject.GetComponent<DestructibleObject>().DealDamage(Mathf.FloorToInt(collision.relativeVelocity.magnitude));
            // }
		}
    }
}
