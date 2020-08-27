using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubbles : MonoBehaviour
{
    private ParticleSystem particles;

    public int bubble_amount;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        bubble_amount = 50;
    }

    // Update is called once per frame
    void Update()
    {
        var emission = particles.emission;
        if (Player.is_launching == true)
        {
            emission.rateOverTime = bubble_amount;
        }
        else
        {
            emission.rateOverTime = 0;
        }
    }
}
