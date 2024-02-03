using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float RotateSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.forward * RotateSpeed * Time.deltaTime);
    }
}
