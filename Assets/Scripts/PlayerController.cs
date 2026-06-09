using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
//Tells the computer to ad the input system to it reconzies keyboard inputs

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private bool grounded;
    
    // tells if we are within trigger of USB block/plug to switch USB location
    public bool plugConfirm;
    // tells if we are going to start talking to the game manager to make the USB switch on a plug
    public bool plugAction;

    // similar to plug confirm/action but instead for the USB-Switch objects, aka buttons
    public bool switchConfirm;
    public bool switchAction;
    
    // If true, the USB is ready to go to the block
    // so if false the USB should come back to the player instead
    public bool USBtoPlug; //Plug
    public bool USBtoSwitch; //button

    [SerializeField] private GameObject gameManager;
    GameManager gmScript;

    Vector2 startPosition;
    Vector3 startScale;

    private Rigidbody2D body;// "public" means youo can edit directly in Unity editor
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        gmScript = gameManager.GetComponent<GameManager>();
        switchAction = false;
        plugAction = false;
        USBtoPlug = false;
        USBtoSwitch = false;
        startPosition = transform.position;
        startScale = transform.localScale;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        if (horizontalInput > 0.1f)
        {
            transform.localScale = new Vector3(startScale.x, startScale.y, startScale.z);
        }
        else if (horizontalInput < -0.1f)
        {
            transform.localScale = new Vector3(-startScale.x, startScale.y, startScale.z);
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
        }
        
        // determine what will happen when player hits J near USB *switch*
        if (Input.GetKey(KeyCode.J) && switchConfirm)
        {
            if (switchAction != true)
            {
                switchAction = true;
            }
            else
            {
                if (gmScript.playerUSB)
                {
                    USBtoSwitch = true;
                }
                else
                {
                    USBtoSwitch = false;
                }
            }
        }
        else
        {
            // usb plug was not within trigger distance while hitting J key or we don't hit J key while within trigger distance
            switchAction = false;
        }

        // determine what will happen when player hits J near USB *plug*
        if (Input.GetKey(KeyCode.J) && plugConfirm)
        {
            if (plugAction != true)
            {
                plugAction = true;
            }
            else
            {
                if (gmScript.playerUSB)
                {
                    USBtoPlug = true;
                }
                else
                {
                    USBtoPlug = false;
                }
            }
        }
        else
        {
            // usb plug was not within trigger distance while hitting J key or we don't hit J key while within trigger distance
            plugAction = false;
        }

    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Ground") || (collision.gameObject.tag == "Box"))
        {
            grounded = true;
        }
    }

    public void Die()
    {
        transform.position = startPosition;
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        
        if (trigger.gameObject.tag == "USB-Plug")
        {
            plugConfirm = true;
            Debug.Log(plugConfirm);
            if (Input.GetKey(KeyCode.J))
            {
                Debug.Log("Plug hit");
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

        if (trigger.gameObject.tag == "USB-Plug")
        {
            plugConfirm = false;
            Debug.Log(plugConfirm);
            if (Input.GetKey(KeyCode.J))
            {
                Debug.Log("Plug miss");
            }
        }
    }

}
