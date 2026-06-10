using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{

    [SerializeField] private GameObject gameManager;
    GameManager gmScript;
    ButtonChecker bcScript;
    [SerializeField] private GameObject objectToActivate;

    public bool firstActive;
    public bool buttonActive;

    private float doorCounter;
    public string regObjName;

    void Awake()
    {
        doorCounter = 0f;
        buttonActive = false;
        firstActive = false;
        gmScript = gameManager.GetComponent<GameManager>();
        bcScript = gameManager.GetComponent<ButtonChecker>();
        regObjName = objectToActivate.name;
        Debug.Log(regObjName);
    }

    void Update()
    {
        if (buttonActive)
        {
            //Debug.Log("Active check");
            firstActive = true;

            bcScript.regButtonCheck = true;
            //bcScript.ButtonCompare(1);
            if (bcScript.noOverlapCheck)
            {
                if (objectToActivate.tag == "Door")
                {
                    //Debug.Log("Door activated");
                    DoorActivate();
                }                
            }
        }
        else if (!buttonActive && firstActive)
        {
            bcScript.regButtonCheck = false;
            //bcScript.ButtonCompare(1);
            if (!bcScript.noOverlapCheck)
            {
                if (objectToActivate.tag == "Door")
                {
                    //Debug.Log("Door deactivated");
                    DoorDeactivate();
                }                
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            //Debug.Log("Box enter");
            buttonActive = true;
        }
        if ((collision.gameObject.tag == "USB-Key") /*&& (gmScript.playerUSB == false)*/)
        {
            //Debug.Log("USB enter");
            buttonActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            buttonActive = false;
        }
        if ((collision.gameObject.tag == "USB-Key") /*&& (gmScript.playerUSB == true)*/)
        {
            //Debug.Log("USB taken");
            buttonActive = false;
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
