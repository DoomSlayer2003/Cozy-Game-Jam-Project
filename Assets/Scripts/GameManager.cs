using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private GameObject USBobject;
    [SerializeField] private GameObject USBplug;
    [SerializeField] private GameObject USBswitch;
    [SerializeField] private GameObject player;
    PlayerController pController;
    // If true, player has USB
    public bool playerUSB;
    public bool gmUSBtoPlug;
    public bool regButtonCheck;
    public bool usbButtonCheck;
    private CapsuleCollider2D USBcollider;
    public Transform usbObjOrient;
    public Transform usbPlugOrient;
    Vector3 usbEulerAngles;
    Quaternion usbRotation;
    Vector3 usbScale;
    Vector3 usbPos;

    Quaternion usbPlugRot;
    public bool usbUp;

    [SerializeField] private GameObject interactGrid;

    void Awake()
    {
        pController = player.GetComponent<PlayerController>();
        playerUSB = true;
        interactGrid.SetActive(false);
        USBcollider = USBobject.GetComponent<CapsuleCollider2D>();
        USBcollider.enabled = false;
        
        usbObjOrient = USBobject.transform;
        usbPos = usbObjOrient.localPosition;
        usbScale = usbObjOrient.localScale;
        
        //usbPlugRot = Quaternion.identity;
        usbPlugOrient = USBplug.transform;
        usbPlugRot = usbPlugOrient.rotation;
        //Debug.Log(usbPlugOrient.localRotation.z);
        //Debug.Log(usbPlugRot.z);
        //Debug.Log(usbPlugRot.eulerAngles.z);
        
        USBobject.transform.SetLocalPositionAndRotation(usbObjOrient.localPosition, usbObjOrient.localRotation);
        //USBobject.transform.localScale = Vector3.one;

        usbRotation = Quaternion.identity;
        Debug.Log(usbRotation.eulerAngles);
        //usbRotation.eulerAngles = new Vector3(0, 0, -90);
        
        //apply the Quaternion.eulerAngles change to the gameObject
        ////transform.rotation = usbRotation;

        //moving the value of the Vector3 into Quanternion.eulerAngle format
        //currentRotation.eulerAngles = currentEulerAngles;

        //(-0.62, 12.28, 0)
        //(0, , )
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(pController.switchAction);
        if (pController.plugAction)
        {
            if (pController.USBtoPlug)
            {
                gmUSBtoPlug = true;
                StartCoroutine(PlugParent());
            }
            else
            {
                gmUSBtoPlug = true;
                StartCoroutine(PlayerParent());
            }
        }

        if (pController.switchAction)
        {
            if (pController.USBtoSwitch)
            {
                gmUSBtoPlug = true;
                StartCoroutine(SwitchParent());
            }
            else
            {
                gmUSBtoPlug = true;
                StartCoroutine(PlayerParent());
            }
        }
/*
        if(playerUSB == false)
        {
            interactGrid.SetActive(true);
        }
        else
        {
            interactGrid.SetActive(false);
        }
*/
    }

    void USBUpApply()
    {
        if (usbUp)
        {
            //Debug.Log("USB up, collider off");
            USBcollider.enabled = false;
        }
        else
        {
            USBcollider.enabled = true;
            //Debug.Log("USB not up, collider on");
        }
    }

    // sets Player as parent to USB
    IEnumerator PlayerParent()
    {
        USBobject.transform.SetParent(player.transform, false);
        USBobject.transform.SetLocalPositionAndRotation(usbObjOrient.localPosition, usbObjOrient.localRotation);
        USBobject.transform.localPosition = new Vector3(usbPos.x, 0, 0);
        
        usbRotation.eulerAngles = new Vector3(0, 0, 0);
        USBobject.transform.rotation = usbRotation;
        Debug.Log(usbRotation.eulerAngles);
        
        USBobject.transform.localScale = new Vector3(usbScale.x, usbScale.y, usbScale.z);
        USBcollider.enabled = false;
        yield return new WaitForSeconds(0.1f);
        interactGrid.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        playerUSB = true;
        yield return null;
    }
    // sets USB Plug as parent to USB
    IEnumerator PlugParent()
    {
        USBobject.transform.SetParent(USBplug.transform, false);
        //USBobject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        USBobject.transform.localPosition = new Vector3 (0, 12, 0);

        Vector3 plugRot = USBplug.transform.localRotation.eulerAngles;
        //Debug.Log("plugRot " + plugRot);
        //Debug.Log("plugRot.z " + plugRot.z);
        //Debug.Log("USBplug " + USBplug.transform.eulerAngles.z);
        if (plugRot.z <= 0.01f && plugRot.z >= -0.01f)
        {
            usbUp = true;
        }
        else
        {
            usbUp = false;
        }
        Quaternion rotation = Quaternion.Euler(0, 0, 180);
        USBobject.transform.localRotation = rotation;
        
        USBobject.transform.localScale = Vector3.one;
        USBUpApply();
        yield return new WaitForSeconds(0.1f);
        interactGrid.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        playerUSB = false;
        yield return null;
    }
    // sets USB Switch as parent to USB
    IEnumerator SwitchParent()
    {
        USBobject.transform.SetParent(USBswitch.transform, false);
        USBcollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        interactGrid.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        playerUSB = false;
        yield return null;
    }

}
