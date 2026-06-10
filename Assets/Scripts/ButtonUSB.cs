using UnityEngine;
using System.Collections;

public class ButtonUSB : MonoBehaviour
{

    [SerializeField] private GameObject gameManager;
    GameManager gmScript;
    [SerializeField] private GameObject objectToActivate;

    public bool usbfirstActive;
    public bool usbActive;
    
    private float doorCounter;
    string usbObjName;

    void Awake()
    {
        doorCounter = 0f;
        usbActive = false;
        usbfirstActive = false;
        gmScript = gameManager.GetComponent<GameManager>();
        usbObjName = objectToActivate.name;
        Debug.Log(usbDoorName);
    }

    void Update()
    {
        if (gmScript.gmUSBtoPlug) // AKA if button or whatever is active, the player doesn't have the USB anymore, therefore the USB is plugged into something
        {
            //Debug.Log("Active check");
            usbfirstActive = true;

            gmScript.usbButtonCheck = true;
            
            if (gmScript.noOverlapCheck)
            {
                if (objectToActivate.tag == "Door")
                {
                    //Debug.Log("Door activated");
                    DoorActivate();
                }                
            }
        }
        else if (!gmScript.gmUSBtoPlug && usbfirstActive)
        {
            gmScript.usbButtonCheck = false;
            if (objectToActivate.tag == "Door")
            {
                //Debug.Log("Door deactivated");
                DoorDeactivate();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "USB-Key") /*&& (gmScript.playerUSB == false)*/)
        {
            //Debug.Log("USB enter");
            usbActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "USB-Key") /*&& (gmScript.playerUSB == true)*/)
        {
            //Debug.Log("USB taken");
            usbActive = false;
        }
    }

    public void DoorActivate()
    {
        if (doorCounter >= 0.55f)
        {
            //Debug.Log("Door stopped");
            doorCounter = 0.55f;
        }
        else
        {
            doorCounter += 0.05f;
            Vector3 temp = objectToActivate.transform.position;
            temp.y += doorCounter;
            objectToActivate.transform.position = temp;
        }

    }

    public void DoorDeactivate()
    {
        if (doorCounter <= 0f)
        {
            //Debug.Log("Door returned");
            doorCounter = 0f;
        }
        else
        {
            Vector3 temp = objectToActivate.transform.position;
            temp.y -= doorCounter;
            doorCounter -= 0.05f;
            objectToActivate.transform.position = temp;
        }

    }
}
