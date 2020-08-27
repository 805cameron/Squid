using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coral : MonoBehaviour
{
    public GameObject coral_light;
    public GameObject coral_particles;


    // Start is called before the first frame update
    void Start()
    {
        coral_light = GameObject.Find("Pink Point Light");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
