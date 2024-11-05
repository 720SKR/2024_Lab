using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandVTest : MonoBehaviour
{
    public int RandV;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomVSet()
    {
        RandV = Random.Range(0, 2);
        Debug.Log(RandV);
    }
}
