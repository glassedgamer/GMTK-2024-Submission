using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] CharacterController controller;

    [SerializeField] float speed = 30f;
    [SerializeField] float gravity = -30f;
    [SerializeField] float jumpHeight = 3.5f;

    bool isGrounded;
    bool jump;

    [SerializeField] LayerMask groundMask;
    
    Vector3 verticalVelocity = Vector3.zero;
    Vector3 horizontalInput;

    void FixedUpdate() {
        isGrounded = controller.isGrounded;
        if(isGrounded) {
            verticalVelocity.y = 0;
        }

        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);
        
        if(jump) {
            if(isGrounded) {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }
            jump = false;
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    public void OnJumpPressed() {
        jump = true;
    }

    public void ReceiveInput(Vector2 _horizontalInput) {
        horizontalInput = _horizontalInput;
    }
}
