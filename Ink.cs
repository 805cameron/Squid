using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ink : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb2d;
    private new Transform transform;
    public GameObject ink_particles;
    public float ink_speed;
    private Quaternion start_rotation;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        transform = GetComponent<Transform>();
        start_rotation = Quaternion.Euler(0, 0, (player.GetComponent<Player>().GetZRotation() / 2 - 45));
        ink_speed = 10f;
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * ink_speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Instantiate(ink_particles, transform.position, start_rotation);

            Destroy(gameObject);
        }
    }
}
