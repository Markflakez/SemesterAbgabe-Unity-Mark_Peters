using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryRotate : MonoBehaviour
{
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.eulerAngles += new Vector3(0, rotationSpeed * Time.deltaTime, 0);
    }
}
