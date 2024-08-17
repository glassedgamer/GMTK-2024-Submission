using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour {
    
    private PlayerControls playerControls;
    private InputAction interact;

    [SerializeField] Transform textLocation;

    [SerializeField] GameObject pickUpText;
    [SerializeField] GameObject noPickUpText;

    [SerializeField] float interactionRange = 2f;
    [SerializeField] LayerMask interactableLayer;

    private GameObject heldObj;
    private Rigidbody heldObjRB;

    [SerializeField] Transform holdArea;
    [SerializeField] float pickupForce = 150f;

    Camera mainCamera;

    void Awake() {
        playerControls = new PlayerControls();
    }

    void Start() {
        mainCamera = Camera.main; // Get the reference to the main camera
    }

    void OnEnable() {
        interact = playerControls.Movement.Interact;

        interact.Enable();

        interact.performed += Interact;
    }

    void OnDisable() {
        interact.Disable();
    }

    void Interact(InputAction.CallbackContext context) {
        GameObject canvas = GameObject.Find("Canvas");
        
        if(heldObj == null)
        {
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // Cast a ray from the center of the screen
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactionRange, interactableLayer))
            {
                if (hit.collider.gameObject.tag == "Shrinkable" && hit.collider.GetComponent<ShrinkableObject>().shrunk == true)
                {
                    // Picking up objects
                    PickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                // Nothing to pick up
                GameObject text = Instantiate(noPickUpText, textLocation.position, Quaternion.identity);
                text.transform.SetParent(canvas.transform);
            }
        } else {
            DropObject();
        }

        if (heldObj != null)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        if(Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }

    void PickupObject(GameObject pickObj)
    {
        if(pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = false;
            heldObjRB.drag = 10;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjRB.transform.parent = holdArea;
            heldObj = pickObj;
        }
    }

    public void DropObject()
    {
        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;

        heldObj.transform.parent = null;
        heldObj = null;
    }

}
