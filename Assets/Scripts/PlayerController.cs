using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector2 MovementSpeed = new Vector2(100.0f, 100.0f); 
    [SerializeField] Rigidbody2D rigidbody2D; 

    private Vector2 inputVector = new Vector2(0.0f, 0.0f);

    void Update()
    {
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + (inputVector * MovementSpeed * Time.fixedDeltaTime));
    }
}