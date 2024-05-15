using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    public Movement playerMovement;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall")
        {
            playerMovement.detecting = true;
            Debug.Log("Colidindo");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        playerMovement.detecting = false;
    }
}
