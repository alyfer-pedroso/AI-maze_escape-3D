using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Transform cameraTransform;
    [SerializeField] private int moveSpace = 5;
    private int rotMultiplier = 1;
    public bool isRandom = false;
    public float randomDelay = .3f;
    private bool firstTime = true;

    [Header("Detect")]
    [SerializeField] private float distanceDetect = 0.5f;
    public bool detecting = false;




    void Start()
    {
        if (isRandom) InvokeRepeating(nameof(Randomizer), 0f, randomDelay);
    }


    void Update()
    {
        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, distanceDetect))
        {
            if (hit.collider.gameObject.CompareTag("Wall") || hit.collider.gameObject.CompareTag("Marked") && hit.collider.gameObject.GetComponent<MeshRenderer>().enabled)
            {
                // Debug.Log("O objeto está colidindo na direção da frente! " + hit.collider.gameObject.tag);
                detecting = true;
                Rotate();
                detecting = false;
            }
        }
        if (!detecting) Move();

        cameraTransform.position = new Vector3(transform.position.x, cameraTransform.transform.position.y, transform.position.z);
        // cameraTransform.LookAt(transform);
    }

    void Rotate()
    {
        transform.rotation = Quaternion.Euler(0, 90 * rotMultiplier, 0);
        if (rotMultiplier >= 4) rotMultiplier = 1; else rotMultiplier++;
    }

    void Move()
    {
        transform.Translate(moveSpace * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Marked") && !other.gameObject.GetComponent<MeshRenderer>().enabled && (!isRandom || firstTime))
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
            firstTime = false;
        }
    }

    void Randomizer()
    {
        int random = UnityEngine.Random.Range(1, 4);
        rotMultiplier = random;
        Rotate();
    }
}