using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentIntantiate : MonoBehaviour
{
    public List<GameObject> monuments;
    public GameObject monumentToDestroy;
    public Transform finalPoint;
    public Transform initialPont;
    public float vel;
    public int index;
    bool isDetroy;
  
  
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        isDetroy = true;
    }
    void FixedUpdate()
    {

        SpamMonument();
            DestroiMonumenst();
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
   



    void SpamMonument()
    {
        if(isDetroy)
        {
            Instantiate(monuments[index],initialPont);
            isDetroy=false;
        }
    }
    void DestroiMonumenst()
    {
        monumentToDestroy = initialPont.GetChild(0).gameObject;
        if (monumentToDestroy.transform.position.x < finalPoint.position.x )
        {
            
            Destroy(initialPont.GetChild(0).gameObject);
            isDetroy = true;
            index++;
            if(index == monuments.Count)
            {
                index=0;
            }
        }
    }

}
