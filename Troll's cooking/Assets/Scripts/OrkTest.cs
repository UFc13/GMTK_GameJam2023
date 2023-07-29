using System;
using System.Linq.Expressions;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class OrkTest : MonoBehaviour
{
    public Transform firePoint;
    public string foodTag = "Food";
    public string playerTag = "Player";
    public float fireTime = 5f;
    public float followDistance = 10f;
    public float returnDistance = 15f;
    public bool countFood = false;
    public bool stop = false;

    private GameObject[] foodObjects;
    private GameObject targetFood;
    private GameObject targetPlayer;
    private NavMeshAgent agent;
    private float timer;
    private bool isFollowingPlayer;
    private GameObject poimal;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = fireTime;

        // ������� ��� ������� � ����� "Food" � ������ ����
        foodObjects = GameObject.FindGameObjectsWithTag(foodTag);
    }

    void OnCollisionEnter(Collision collision)
    {
        poimal = collision.gameObject;
        if (poimal.tag == "Player")
        {
            Destroy(poimal);
        }
    }

    private void Update()
    {
        if (stop == true)
        {
            agent.speed = 0;
        }

        // ���������, ����� �� ��������� ������
        if (!isFollowingPlayer && targetPlayer == null)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);
            foreach (GameObject player in players)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < followDistance)
                {
                    targetPlayer = player;
                    break;
                }
            }
        }

        // ���� ���� ���� �����, ���������� ���
        if (targetPlayer != null)
        {
            float playerDistance = Vector3.Distance(transform.position, targetPlayer.transform.position);

            // ���� ����� ��������� � ����������� ��������, ������� �� ���
            if (playerDistance < followDistance)
            {
                agent.SetDestination(targetPlayer.transform.position);
                isFollowingPlayer = true;
            }

            else
            {
                // ���� ����� ������� �� ������������ ����������, ��������� ���� ������� � ����� "Food"
                isFollowingPlayer = false;
                targetPlayer = null;
            }
        }

        else
        {
            if (countFood == true)
            {
                // ���� ��� �������� � ����� "Food", ����� � ����� "fire"
                agent.SetDestination(firePoint.position);
            }

            // ���� ��� ���� �����, �������� ��������� ������ � ����� "Food"
            else if(foodObjects.Length > 0)
            {
                if (targetFood == null)
                {
                    int randomIndex = UnityEngine.Random.Range(0, foodObjects.Length);
                    targetFood = foodObjects[randomIndex];
                }

                // ���������� ��������� ������ � ����� "Food"
                if (targetFood != null)
                {
                    agent.SetDestination(targetFood.transform.position);
                }
            }
        }

        // ���������, �������� �� ����� "Food" � ������ �� �����
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                // ����� ������ ����� "Food" � ������ ���������� �������
                if (targetFood != null)
                {
                    // ���������� ������ � ����� "Food"
                    Destroy(targetFood);
                    targetFood = null;

                    countFood = true;
                }

                timer = fireTime;

            }
        }
    }
}