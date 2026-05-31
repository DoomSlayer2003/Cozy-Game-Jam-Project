using UnityEngine;
using UnityEngine.InputSystem; //Tells the computer to ad the input system to it reconzies keyboard inputs

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction; // "public" means youo can edit directly in Unity editor
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = MoveAction.ReadValue<Vector2> ();

        Debug.Log(move);

        Vector2 position = (Vector2)transform.position + move * 0.01f;

        transform.position = position;

    }
}
