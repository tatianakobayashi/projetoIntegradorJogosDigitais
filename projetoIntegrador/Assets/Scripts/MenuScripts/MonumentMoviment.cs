using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentMoviment : MonoBehaviour
{
    public float vel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        MoveMonument();
    }
    void MoveMonument()
    {
       this.transform.position-=new Vector3(vel*Time.deltaTime,0,0);   
    }
}
