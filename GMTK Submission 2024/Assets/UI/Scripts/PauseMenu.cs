using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    private PlayerControls playerControls;
    private InputAction menu;

    public Slider sensitivityXSlider;
    public Slider sensitivityYSlider;
    [SerializeField] CameraMovement cameraScript;

    [SerializeField] private GameObject pauseUI;
    public bool isPaused;

    void Awake() {
        playerControls = new PlayerControls();
    }

    void Start() {
        // levelChanger = GameObject.FindWithTag("LevelChanger");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pauseUI.SetActive(false);
        
        sensitivityXSlider.value = cameraScript.sensitivityX;
        sensitivityYSlider.value = cameraScript.sensitivityY;
        sensitivityXSlider.onValueChanged.AddListener(ChangeSensitivityX);
        sensitivityYSlider.onValueChanged.AddListener(ChangeSensitivityY);
    }

    void OnEnable() {
        menu = playerControls.Menu.Pause;

        menu.Enable();

        menu.performed += Pause;
    }

    void OnDisable() {
        menu.Disable();
    }

    void Pause(InputAction.CallbackContext context) {
        isPaused = !isPaused;

        if(isPaused) {
            ActivateMenu();
        } else {
            DeactivateMenu();
        }
    }

    void ActivateMenu() {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DeactivateMenu() {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ChangeSensitivityX(float value)
    {
        cameraScript.sensitivityX = value;
    }

    void ChangeSensitivityY(float value)
    {
        cameraScript.sensitivityY = value;
    }

}
