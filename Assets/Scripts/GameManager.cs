using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private GameObject USBobject;
    [SerializeField] private GameObject USBblock;
    [SerializeField] private GameObject player;
    PlayerController pController;
    // If true, player has USB
    public bool playerUSB;

    [SerializeField] private GameObject interactGrid;

    void Awake()
    {
        pController = player.GetComponent<PlayerController>();
        playerUSB = true;
        interactGrid.SetActive(false);
        //USBobject.transform..SetparentworldPositionStays(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(pController.switchAction);
        if (pController.switchAction)
        {
            if (pController.USBtoBlock)
            {
                StartCoroutine(BlockParent());
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

    // sets USB Block as parent to USB
    IEnumerator BlockParent()
    {
        USBobject.transform.SetParent(USBblock.transform, false);
        yield return new WaitForSeconds(0.1f);
        interactGrid.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        playerUSB = false;
        yield return null;
    }
    // sets Player as parent to USB
    IEnumerator PlayerParent()
    {
        USBobject.transform.SetParent(player.transform, false);
        yield return new WaitForSeconds(0.1f);
        interactGrid.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        playerUSB = true;
        yield return null;
    }
}
