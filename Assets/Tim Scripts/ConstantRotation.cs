using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    public float rotationSpeed = 10.0f;

    void Update()
    {
        transform.Rotate(0.0f, 0.0f, rotationSpeed * Time.deltaTime);
    }
}
