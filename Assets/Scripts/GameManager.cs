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
    private CapsuleCollider2D USBcollider;
    
    public Transform usbObjOrient;
    public Transform usbPlugOrient;
    Vector3 usbEulerAngles;
    Quaternion usbRotation;
    Vector3 usbScale;
    Vector3 usbPos;

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
        //usbPlugOrient = USBplug.transform;

        USBobject.transform.SetLocalPositionAndRotation(usbObjOrient.localPosition, usbObjOrient.localRotation);
        //USBobject.transform.localScale = Vector3.one;

        usbRotation = Quaternion.identity;
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
                StartCoroutine(PlugParent());
            }
            else
            {
                StartCoroutine(PlayerParent());
            }
        }

        if (pController.switchAction)
        {
            if (pController.USBtoSwitch)
            {
                StartCoroutine(SwitchParent());
            }
            else
            {
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

    // sets Player as parent to USB
    IEnumerator PlayerParent()
    {
        USBobject.transform.SetParent(player.transform, false);
        USBobject.transform.SetLocalPositionAndRotation(usbObjOrient.localPosition, usbObjOrient.localRotation);
        USBobject.transform.localPosition = new Vector3(usbPos.x, 0, 0);
        usbRotation.eulerAngles = new Vector3(0, 0, 0);
        USBobject.transform.rotation = usbRotation;
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
        usbRotation.eulerAngles = new Vector3(0, 0, -90);
        USBobject.transform.rotation = usbRotation;
        USBobject.transform.localScale = Vector3.one;
        //USBobject.transform.localScale = usbPlugOrient.lossyScale;
        USBcollider.enabled = true;
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
