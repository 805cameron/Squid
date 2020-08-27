using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoom_in_size;
    public float zoom_out_size;

    public float zoom_in_duration;
    public float zoom_out_duration;

    public float multiplier;

    bool panning;

    // Start is called before the first frame update
    void Start()
    {
        zoom_in_size = 9;
        zoom_out_size = 12;

        zoom_in_duration = 0.5f;
        zoom_out_duration = 1.5f;

        multiplier = 1f;
        panning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Player.is_launching == false)
        {
            panning = true;
            float timeStart = Time.time;

            if (panning)
            {
                float u = (Time.time - timeStart) / timeDuration;
            }
        }
        else
        { 
            if (GetComponent<Camera>().orthographicSize >= zoom_in_size)
            {
                GetComponent<Camera>().orthographicSize -= Time.deltaTime * 10;
            }
        }
    }
}
