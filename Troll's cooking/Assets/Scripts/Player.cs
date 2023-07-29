using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E; // Клавиша для взаимодействия
    public float moveSpeed = 5f;  // Скорость передвижения игрока

    private Rigidbody rb;
    private GameObject interactableObject; // Ссылка на объект, с которым взаимодействует игрок
    private bool isInteracting = false; // Флаг, указывающий, идет ли взаимодействие в данный момент

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
                // Игрок нажимает клавишу взаимодействия и не находится в процессе взаимодействия
                interactableObject.transform.parent = transform; // Делаем объект дочерним игрока
                interactableObject.SetActive(false); // Скрываем объект
                isInteracting = true;
            }
            else if (isInteracting)
            {
                // Игрок нажимает клавишу взаимодействия во время взаимодействия
                interactableObject.transform.parent = null; // Возвращаем объект в исходное положение
                interactableObject.SetActive(true); // Включаем видимость объекта
                isInteracting = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isInteracting && other.CompareTag("Food"))
        {
            // Игрок входит в зону взаимодействия с объектом
            interactableObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == interactableObject)
        {
            // Игрок покидает зону взаимодействия с объектом
            interactableObject = null;
        }
    }

    void FixedUpdate()
    {
        // Получаем ввод от пользователя по горизонтали и вертикали
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // Создаем вектор направления на основе ввода пользователя
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        // Нормализуем вектор направления, чтобы скорость движения была одинаковой во всех направлениях
        movement = movement.normalized;

        // Устанавливаем скорость передвижения игрока
        rb.velocity = movement * moveSpeed;
    }
}