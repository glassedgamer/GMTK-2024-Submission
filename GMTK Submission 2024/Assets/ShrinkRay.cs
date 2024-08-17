using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShrinkRay : MonoBehaviour
{

    Transform cam;

    private PlayerControls playerControls;
    private InputAction shoot;

    [Header("General")]
    [SerializeField] float range = 50f;
    [SerializeField] float reloadTime = 1f;

    [Header("Ammo")]
    [SerializeField] int maxAmmo = 1;
    [SerializeField] int currentAmmo = 1;
    
    private bool isReloading;

    private void Awake()
    {
        cam = Camera.main.transform;
        playerControls = new PlayerControls();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
    }

    void OnEnable()
    {
        shoot = playerControls.Movement.Shoot;

        shoot.Enable();

        shoot.performed += Shoot;
    }

    void OnDisable()
    {
        shoot.Disable();
    }

    void Shoot(InputAction.CallbackContext context)
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;

            RaycastHit hit;
            if (Physics.Raycast(cam.position, cam.forward, out hit, range))
            {
                if (hit.collider.gameObject.tag == "Shrinkable" && hit.collider.GetComponent<ShrinkableObject>() != null)
                {
                    if(hit.collider.GetComponent<ShrinkableObject>().shrunk == false)
                        hit.collider.GetComponent<ShrinkableObject>().ShrinkGunShrinksTheThing();
                }
            }
        } else
        {
            return;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
}

