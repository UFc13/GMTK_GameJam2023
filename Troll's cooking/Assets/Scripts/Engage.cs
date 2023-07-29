using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Engage : MonoBehaviour
{
    public int scorePlayer = 0;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            scorePlayer++;
        }
    }
}
