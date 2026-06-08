using UnityEngine;
using UnityEngine.SceneManagement;
//Tells the computer to ad the input system to it reconzies keyboard inputs

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private bool grounded;
    
    // tells if we are within trigger of USB block to switch USB location
    public bool switchConfirm;
    // tells if we are going to start talking to the game manager to make the USB switch
    public bool switchAction;
    
    // If true, the USB is ready to go to the block
    // so if false the USB should come back to the player instead
    public bool USBtoBlock;

    [SerializeField] private GameObject gameManager;
    GameManager gmScript;

    Vector2 startPosition;

    private Rigidbody2D body;// "public" means youo can edit directly in Unity editor
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        gmScript = gameManager.GetComponent<GameManager>();
        switchAction = false;
        USBtoBlock = false;
        startPosition = transform.position;

    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        if (horizontalInput > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }
        
        if (Input.GetKey(KeyCode.J) && switchConfirm)
        {
            //
            if (switchAction != true)
            {
                switchAction = true;
            }
            else
            {
                if (gmScript.playerUSB)
                {
                    USBtoBlock = true;
                }
                else
                {
                    USBtoBlock = false;
                }
            }
            //
            /*
            switchAction = true;
            if (gmScript.playerUSB)
            {
                USBtoBlock = true;
            }
            else
            {
                USBtoBlock = false;
            }
            */
        }
        else
        {
            // usb switch was not within trigger distance while hitting J key or we don't hit J key while within trigger distance
            switchAction = false;
        }

    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    public void Die()
    {
        transform.position = startPosition;
        Application.LoadLevel(Application.loadedLevel);
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (trigger.gameObject.tag == "USB-Switch")
        {
            switchConfirm = true;
            Debug.Log(switchConfirm);
            if (Input.GetKey(KeyCode.J))
            {
                Debug.Log("Switch hit");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "USB-Switch")
        {
            switchConfirm = false;
            Debug.Log(switchConfirm);
            if (Input.GetKey(KeyCode.J))
            {
                Debug.Log("Switch miss");
            }
        }
    }

}
