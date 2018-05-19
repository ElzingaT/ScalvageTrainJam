using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestructibleObject : MonoBehaviour
{
    public int health = 10;

    public UnityEvent onDamageTaken;
    public UnityEvent onDestroyed;

    public AudioClip destroySfx;

    public void DealDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            if (onDestroyed != null)
                onDestroyed.Invoke();

            if (destroySfx != null)
                AudioSource.PlayClipAtPoint(destroySfx, transform.position);

            Destroy(gameObject);
        }
        else
        {
            if (onDamageTaken != null)
                onDamageTaken.Invoke();
        }
    }
}
