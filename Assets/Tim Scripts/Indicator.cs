using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public Transform target;
    private Vector3 targetPoint;
    private Quaternion targetRotation;

    void Update()
    {
        if (target == null)
            return;

        Vector3 difference = target.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        float distance = Vector3.Distance(target.position, transform.position);
        float scale = Mathf.Clamp(distance / 10.0f, 0.0f, 1.0f);
        transform.localScale = new Vector3(scale, scale, 1.0f);
    }
}
