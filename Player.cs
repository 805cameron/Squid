using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private GameObject squid;
    public GameObject ink;

    public float movement_speed; //public

    private float charge;
    public float min_charge; //public
    public float max_charge; //public
    public float launching_threshold; //public
    public float torpedo_multiplier; //public

    public float fire_rate; //public
    private float next_fire = 0.0f;

    private Vector2 previous_frame_position;
    private float speed;

    public static bool is_launching = false;

    private bool launch_canceled = false;




    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        squid = GameObject.Find("Squid Sprite");
        movement_speed = 5;
        min_charge = 0.4f;
        max_charge = 1;
        fire_rate = 0.5f;
        launching_threshold = 10;
        torpedo_multiplier = 40;
    }


    // Update is called once per frame
    public void Update()
    {
        // * * * * * * * *INPUTS* * * * * * * *
        if (is_launching == false)
        {
        // Make Player Face Mouse if M1 is held down
            if (Input.GetMouseButton(0))
            {
                squid.transform.up = GetDirection();

                if (charge < max_charge)
                {
                    charge += Time.deltaTime;
                }
            }


        // Torpedo when M1 is released
            if (Input.GetMouseButtonUp(0))
            {
                if (charge > min_charge)
                {
                    Torpedo();
                }
                charge = 0f;               
            }


        // Shoot Ink if M2 is pressed
            if (Input.GetMouseButton(1) && Time.time > next_fire)
            {
                next_fire = Time.time + fire_rate;
                ShootInk(GetZRotation());
            }
        }
    }
       

    void FixedUpdate()
    {
    // Movement Mechanics
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = (Input.GetMouseButton(0)) ? Vector2.zero : new Vector2(moveHorizontal * movement_speed, moveVertical * movement_speed);
        rb2d.drag = (Input.GetMouseButton(0)) ? 0.25f : 1;
        float velocity = (Input.GetMouseButtonDown(0)) ? 0.5f : 1;


    // Calculating Speed & if Player is Launching
        float movement_per_frame = Vector3.Distance(previous_frame_position, transform.position);
        speed = movement_per_frame / Time.deltaTime;
        previous_frame_position = transform.position;

        if (launch_canceled == false)
        {
            is_launching = (speed > launching_threshold) ? true : false;
        }
        else
        {
            is_launching = false;
        }


    // * * * * * * * *INPUTS* * * * * * * *
        if (is_launching == false)
        {
        // Applying movement
            rb2d.AddForce(movement);
            rb2d.velocity = rb2d.velocity * velocity;


            // Direction Facing
            

            if (Input.GetKey("s"))
            {
                squid.transform.eulerAngles = new Vector3(0, 0, 180);
            }
            else if (Input.GetKey("a"))
            {
                squid.transform.eulerAngles = new Vector3(0, 0, 90);
            }
            else if (Input.GetKey("d"))
            {
                squid.transform.eulerAngles = new Vector3(0, 0, 270);
            }
            else if (Input.GetKey("w"))
            {
                squid.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }


        // End the Launch when the spacebar is pressed
        if (Input.GetKeyDown("space"))
        {
            StartCoroutine("CancelLaunch");
        }
    }


    // Get Direction
    public Vector2 GetDirection()
    {
        Vector2 direction = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition) - squid.transform.position;
        return direction;
    }


    // Get Z Rotation
    public float GetZRotation()
    {
        float z_rotation = (Mathf.Atan2(GetDirection().y, GetDirection().x) * Mathf.Rad2Deg) / 2 - 45;
        return z_rotation;
    }


    // Torpedo
    void Torpedo()
    {
        is_launching = true;
        rb2d.velocity = (GetDirection().normalized * charge) * torpedo_multiplier;

    }


    // Cancel Launch
    public IEnumerator CancelLaunch()
    {
        launch_canceled = true;
        print("we in bois");

        if (speed > launching_threshold)
        {
            launch_canceled = true;
            is_launching = false;
            yield return new WaitUntil(() => speed < launching_threshold);
        }

        launch_canceled = false;
    }


    // Shoot ink pellets
    void ShootInk(float direction)
    {
        Instantiate(ink, transform.position, Quaternion.Euler(0f, 0f, direction));
    }


    //OnColliderEnter - determines what happens when another collider is touched
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (is_launching == true)
            {
                Destroy(collision.gameObject);
            }
        } 
    }

 
}
