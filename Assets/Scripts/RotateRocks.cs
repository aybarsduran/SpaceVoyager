using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRocks : MonoBehaviour
{
    private GameObject child;
    public float rotationSpeed = 10f;
    void Start()
    {
        child = this.gameObject.transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        // Z rotasyonunu deðiþtirerek objeyi döndür
        child.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
