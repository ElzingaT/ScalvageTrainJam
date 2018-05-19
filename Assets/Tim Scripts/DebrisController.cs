using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisController : MonoBehaviour
{
    public Debris[] debris;
    public float pushStrength = 10.0f;

    public void Spawn()
    {
        Vector3 parentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        Vector3 parentPosition = gameObject.transform.position;

        for (int i = 0; i < debris.Length; i++)
        {
            Vector3 pos = new Vector3(parentPosition.x + debris[i].offset.x,
                                          parentPosition.y + debris[i].offset.y,
                                          parentPosition.z + debris[i].offset.z);

            GameObject obj = Instantiate(debris[i].prefab, transform.parent);
            obj.transform.position = pos;
            obj.transform.localScale = gameObject.transform.localScale;
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb)
            {
                rb.useAutoMass = true;
                rb.velocity = parentVelocity;
                Vector3 force = ((parentPosition - obj.transform.position) * -pushStrength);
                rb.AddForce(force);
            }
        }

        // temp
        Destroy(gameObject);
    }
}
