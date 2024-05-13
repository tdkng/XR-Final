using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingRod : MonoBehaviour
{
    public GameObject bobberPrefab; // Prefab for the bobber
    public Transform castPoint; // Point from which the bobber is spawned
    public float castForce = 10f; // Force applied to the bobber when casting
    public LayerMask waterLayer; // Layer mask for the water surface
    public float reelSpeed = 5f; // Speed at which the bobber is reeled in

    private GameObject bobberInstance; // Reference to the spawned bobber
    private bool isCasting = false; // Flag to track if the rod is currently casting

    private void Start()
    {

    }

    private void Update()
    {
        if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            if (!isCasting)
            {
                Cast();
            }
            else
            {
                Reel();
            }
        }
    }

    private void Cast()
    {
        isCasting = true;
        bobberInstance = Instantiate(bobberPrefab, castPoint.position, Quaternion.identity);
        Rigidbody bobberRb = bobberInstance.GetComponent<Rigidbody>();
        bobberRb.AddForce(transform.forward * castForce, ForceMode.Impulse);
    }

    private void Reel()
    {
        if (bobberInstance != null)
        {
            Transform bobberTransform = bobberInstance.transform;
            bobberTransform.position = Vector3.MoveTowards(bobberTransform.position, transform.position, reelSpeed * Time.deltaTime);

            if (bobberTransform.position == transform.position)
            {
                Destroy(bobberInstance);
                isCasting = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == waterLayer)
        {
            // Pond script logic to spawn fish when bobber touches water
            // Assuming the Pond script has a method to handle fish spawning
            Pond pond = other.GetComponent<Pond>();
            if (pond != null)
            {
                pond.SpawnFish();
            }
        }
    }
}
