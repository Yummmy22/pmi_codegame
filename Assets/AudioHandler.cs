using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    // Variabel untuk menampung audio clips
    public AudioSource correctSound;
    public AudioSource wrongSound;

    // Fungsi untuk memainkan correct sound
    public void PlayCorrectSound()
    {
        if (correctSound != null && !correctSound.isPlaying)
        {
            correctSound.Play();
        }
    }

    // Fungsi untuk memainkan wrong sound
    public void PlayWrongSound()
    {
        if (wrongSound != null && !wrongSound.isPlaying)
        {
            wrongSound.Play();
        }
    }
}
