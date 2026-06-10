using UnityEngine;
using System.Collections;

public class ButtonChecker : MonoBehaviour
{
    [SerializeField] private GameObject regButton;
    [SerializeField] private GameObject usbButton;
    Button regScript;
    ButtonUSB usbScript;

    public bool regButtonCheck;
    public bool usbButtonCheck;
    public bool noOverlapCheck;


    void Start()
    {
        regScript = regButton.GetComponent<Button>();
        usbScript = usbButton.GetComponent<ButtonUSB>();
        noOverlapCheck = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonCompare(int identityChecker) // 1=button, 2=usb
    {
        // if button is on and USB is on, and if their objects share the same name, there is overlap
        
        if (identityChecker == 1)
        {
            //if usb is on, and shares object name, there's overlap and you can't run
            if(usbButtonCheck && (regScript.regObjName == usbScript.usbObjName))
            {
                noOverlapCheck = false; //FALSE: there *is* overlap, so the USB is on and the names are the same
            }
            else
            {
                noOverlapCheck = true; //TRUE: there is no overlap
            }

        }
        else if (identityChecker == 2)
        {
            //if button is on, and shares object name, there's overlap and you can't run
            if(regButtonCheck && (regScript.regObjName == usbScript.usbObjName))
            {
                noOverlapCheck = false;
            }
            else
            {
                noOverlapCheck = true;
            }
        }
        
    }
}
