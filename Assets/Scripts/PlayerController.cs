using UnityEngine;
using UnityEngine.SceneManagement;
//Tells the computer to ad the input system to it reconzies keyboard inputs

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool grounded;

    private Rigidbody2D body;// "public" means youo can edit directly in Unity editor
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);


        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < 0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }



    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Goal")
        {
            Debug.Log("scene switch");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
