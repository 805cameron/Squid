using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    public float intensity;

    public GameObject[] shadows;
    float max_distance;

    // Start is called before the first frame update
    void Start()
    {
        intensity = 1.5f;
        max_distance = GetComponent<Light>().range / 3f;
    }

    // Update is called once per frame
    void Update()
    {
        shadows = GameObject.FindGameObjectsWithTag("Shadow");

        UpdateShadowOpacity();

    }

    // Update opacity of shadows based on their distance to the closest light
    void UpdateShadowOpacity()
    {
        foreach (GameObject shadow in shadows)
        {
            Color shadow_color = shadow.GetComponent<SpriteRenderer>().color;
            float distance = Vector2.Distance(transform.position, shadow.transform.position);

            if (distance < max_distance && gameObject == shadow.GetComponent<Shadow>().ClosestLight())
            {
                shadow_color.a = 1 - Mathf.Sin((distance / max_distance) * (Mathf.PI / 2));
                shadow.GetComponent<SpriteRenderer>().color = shadow_color;
            }
        }
    }

 
    public IEnumerator pulse()
    {
        intensity = 1.5f - (0.5f * Mathf.Sin((Time.deltaTime) * (Mathf.PI / 2)));

        gameObject.GetComponent<Light>().intensity = intensity;



        yield return new WaitForEndOfFrame();
    }
    
}
