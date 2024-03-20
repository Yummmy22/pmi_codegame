using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    // Variabel untuk menampung audio clips
    public AudioClip correctSound;
    public AudioClip wrongSound;

    private AudioSource audioSource;

    // Fungsi ini dipanggil saat script instance di-load
    void Awake()
    {
        // Mendapatkan komponen AudioSource dari game object
        audioSource = GetComponent<AudioSource>();
    }

    // Fungsi untuk memainkan correct sound
    public void PlayCorrectSound()
    {
        if (correctSound != null)
        {
            audioSource.PlayOneShot(correctSound);
        }
    }

    // Fungsi untuk memainkan wrong sound
    public void PlayWrongSound()
    {
        if (wrongSound != null)
        {
            audioSource.PlayOneShot(wrongSound);
        }
    }
}
