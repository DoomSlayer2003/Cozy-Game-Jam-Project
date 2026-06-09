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

    [SerializeField] private GameObject interactGrid;

    void Awake()
    {
        pController = player.GetComponent<PlayerController>();
        playerUSB = true;
        interactGrid.SetActive(false);
        USBcollider = USBobject.GetComponent<CapsuleCollider2D>();
        USBcollider.enabled = false;
        //USBobject.transform..SetparentworldPositionStays(false);
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
