using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance; // статическая ссылка на экземпляр AudioManager

    public AudioSource audioSource; // ссылка на аудио источник

    private void Awake()
    {
        // Проверяем, существует ли уже экземпляр AudioManager
        if (instance == null)
        {
            // Если экземпляр не существует, сохраняем текущий экземпляр
            instance = this;
            DontDestroyOnLoad(gameObject); // сохраняем объект при загрузке новой сцены
        }
        else
        {
            // Если экземпляр уже существует, уничтожаем текущий объект AudioManager
            Destroy(gameObject);
        }
    }

    // Метод для воспроизведения аудио
    public void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
