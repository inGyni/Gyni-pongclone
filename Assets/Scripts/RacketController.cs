using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketController : MonoBehaviour
{
    public float speed;
    public string axis;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        float v = Input.GetAxisRaw(axis) * speed * Time.deltaTime;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, v);
    }
}
