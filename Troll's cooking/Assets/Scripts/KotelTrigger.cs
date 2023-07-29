using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KotelTrigger : MonoBehaviour
{
    public OrkTest ork;
    public int scoreOrk = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (ork.countFood == true)
        {
            ork.countFood = false;
            scoreOrk++;
        }
    }
}
