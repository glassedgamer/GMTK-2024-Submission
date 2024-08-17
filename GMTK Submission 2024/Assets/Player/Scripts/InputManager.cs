using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {

    [SerializeField] PlayerMovement movement;
    [SerializeField] CameraMovement cameraMovement;
    // [SerializeField] Gun gun;

    PlayerControls controls;
    PlayerControls.MovementActions groundMovement;

    Vector2 horizontalInput;
    Vector2 mouseInput;

    void Awake() {
        controls = new PlayerControls();
        groundMovement = controls.Movement;

        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        groundMovement.Jump.performed += _ => movement.OnJumpPressed();

        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        // groundMovement.Shoot.performed += _ => gun.Shoot();
    }

    void Update() {
        movement.ReceiveInput(horizontalInput);
        cameraMovement.ReceiveInput(mouseInput);
    }
   
    void OnEnable() {
        controls.Enable();
    }

    void OnDestroy() {
        controls.Disable();
    }
}
