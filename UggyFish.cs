using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UggyFish : MonoBehaviour
{
    private Animator animator;
    private Transform squid;

    public float speed = 2;
    public float follow_distance = 12;

    public Vector2 anchor;
    public Vector3 target;
    public float start_wait_time;
    private float wait_time;

    // Start is called before the first frame update
    void Awake()
    {
        squid = GameObject.Find("Squid Sprite").GetComponent<Transform>();
        animator = GetComponent<Animator>();

        anchor = gameObject.GetComponent<Transform>().position;

        target = new Vector2(Random.Range(anchor.x - 6, anchor.x + 6), Random.Range(anchor.y - 6, anchor.y + 6));

        start_wait_time = 3;

        wait_time = start_wait_time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, squid.position) < follow_distance)
        {
            target = squid.position;
            transform.position = Vector2.MoveTowards(transform.position, squid.position, speed * Time.deltaTime);
            anchor = gameObject.GetComponent<Transform>().position;
        }
        else
        {
            if (Vector2.Distance(target, squid.position) < 0.2f)
            {
                target = new Vector2(Random.Range(anchor.x - 6, anchor.x + 6), Random.Range(anchor.y - 6, anchor.y + 6));
            }

            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime / 2);

            if (Vector2.Distance(transform.position, target) < 0.2f)
            {
                if (wait_time <= 0)
                {
                    target = new Vector2(Random.Range(anchor.x - 6, anchor.x + 6), Random.Range(anchor.y - 6, anchor.y + 6));
                    wait_time = start_wait_time;
                }
                else
                {
                    wait_time -= Time.deltaTime;
                }
            }
        }



        if (animator.gameObject.activeSelf)
        {
            if (target.x > transform.position.x)
            {
                animator.SetBool("facing_left", false);
            }
            else
            {
                animator.SetBool("facing_left", true);
            }
        }


    }


}
