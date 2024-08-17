using UnityEngine;

public class CameraMovement : MonoBehaviour {

    GameObject gameManager;
    GameObject pauseManager;

    [SerializeField] float xClamp = 85f;
    public float sensitivityX = 8f;
    public float sensitivityY = 0.5f;
    float mouseX, mouseY;
    float xRotation = 0f;

    [SerializeField] Transform playerCamera;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //pauseManager = GameObject.FindWithTag("PauseManager");
    }

    void Update() {
        //if(pauseManager.GetComponent<PauseMenu>().isPaused == false) {
            transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
            Vector3 targetRotation = transform.eulerAngles;
            targetRotation.x = xRotation;
            playerCamera.eulerAngles = targetRotation;
        //}
    }

    public void ReceiveInput(Vector2 mouseInput) {
        mouseY = mouseInput.y * sensitivityY;
        mouseX = mouseInput.x * sensitivityX;
    }

}
