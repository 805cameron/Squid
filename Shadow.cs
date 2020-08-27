using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public GameObject [] lights;

    // Start is called before the first frame update
    void Awake()
    {
        lights = new GameObject[10];

        for (int i = 0; i < lights.Length; i++)
        {
            lights[i] = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        lights = GameObject.FindGameObjectsWithTag("Light");

        ClosestLight();
    }

    // Find the closest light to the shadow and return it as this GameObject
    public GameObject ClosestLight ()
    {
        GameObject closest_light = null;
        float closest_distance_sqr = Mathf.Infinity;
        Vector3 current_position = transform.position;

        foreach (GameObject light in lights)
        {
            Vector3 direction_to_target = light.GetComponent<Transform>().position - current_position;
            float direction_to_target_sqr = direction_to_target.sqrMagnitude;

            if (direction_to_target_sqr < closest_distance_sqr)
            {
                closest_distance_sqr = direction_to_target_sqr;
                closest_light = light;
            }
        }
        return closest_light;
    }
}
