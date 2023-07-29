using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance; // ����������� ������ �� ��������� AudioManager

    public AudioSource audioSource; // ������ �� ����� ��������

    private void Awake()
    {
        // ���������, ���������� �� ��� ��������� AudioManager
        if (instance == null)
        {
            // ���� ��������� �� ����������, ��������� ������� ���������
            instance = this;
            DontDestroyOnLoad(gameObject); // ��������� ������ ��� �������� ����� �����
        }
        else
        {
            // ���� ��������� ��� ����������, ���������� ������� ������ AudioManager
            Destroy(gameObject);
        }
    }

    // ����� ��� ��������������� �����
    public void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
