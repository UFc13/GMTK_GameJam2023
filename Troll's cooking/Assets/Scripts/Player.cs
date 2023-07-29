using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E; // ������� ��� ��������������
    public float moveSpeed = 5f;  // �������� ������������ ������

    private Rigidbody rb;
    private GameObject interactableObject; // ������ �� ������, � ������� ��������������� �����
    private bool isInteracting = false; // ����, �����������, ���� �� �������������� � ������ ������

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            if (!isInteracting && interactableObject != null)
            {
                // ����� �������� ������� �������������� � �� ��������� � �������� ��������������
                interactableObject.transform.parent = transform; // ������ ������ �������� ������
                interactableObject.SetActive(false); // �������� ������
                isInteracting = true;
            }
            else if (isInteracting)
            {
                // ����� �������� ������� �������������� �� ����� ��������������
                interactableObject.transform.parent = null; // ���������� ������ � �������� ���������
                interactableObject.SetActive(true); // �������� ��������� �������
                isInteracting = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isInteracting && other.CompareTag("Food"))
        {
            // ����� ������ � ���� �������������� � ��������
            interactableObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == interactableObject)
        {
            // ����� �������� ���� �������������� � ��������
            interactableObject = null;
        }
    }

    void FixedUpdate()
    {
        // �������� ���� �� ������������ �� ����������� � ���������
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // ������� ������ ����������� �� ������ ����� ������������
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        // ����������� ������ �����������, ����� �������� �������� ���� ���������� �� ���� ������������
        movement = movement.normalized;

        // ������������� �������� ������������ ������
        rb.velocity = movement * moveSpeed;
    }
}