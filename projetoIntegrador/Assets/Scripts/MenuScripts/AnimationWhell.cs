using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationWhell : MonoBehaviour
{
    public float speedRotation;
    void Update()
    {
        Whell();
    }
    public void Whell()
    {
        transform.Rotate(new Vector3(0, 0, 3 * speedRotation * Time.deltaTime));
    }
}
