using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private GameObject USBobject;
    [SerializeField] private GameObject USBblock;
    [SerializeField] private GameObject player;
    PlayerController pController;

    void Awake()
    {
        pController = player.GetComponent<PlayerController>();
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
            USBobject.transform.SetParent(USBblock.transform);
        }
    }
}
