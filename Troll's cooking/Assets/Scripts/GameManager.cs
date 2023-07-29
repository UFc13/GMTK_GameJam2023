using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject deathScreen;
    public OrkTest ork;
    public Player player;
    public Engage pomoika;
    public KotelTrigger kotel;
    public int scoreForWin = 0;
    public int scoreForLose = 0;

    private bool win = false;
    private bool lose = false;


    // Update is called once per frame
    void Update()
    {
        if (pomoika.scorePlayer == 3)
        {
            win = true;
        }

        if (kotel.scoreOrk == 3 || player == null)
        {
            lose = true;
        }

        if (win == true)
        {
            player.moveSpeed = 0;
            ork.stop = true;
            SceneManager.LoadScene("Level 2");
        }

        if (lose == true)
        {
            deathScreen.SetActive(true);
            player.moveSpeed = 0;
            ork.stop = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Level 1");
            }
        }
    }
}
