using UnityEngine;
using TMPro;
using System.Collections;

public class TextFadeOut : MonoBehaviour
{
    public TMP_Text textToDisplay;
    public float delay = 2f;

    public void ShowAndHideText()
    {
        StartCoroutine(ShowAndHideTextRoutine());
    }

    IEnumerator ShowAndHideTextRoutine()
    {
        // Aktifkan game object
        textToDisplay.gameObject.SetActive(true);

        // Tunggu selama delay
        yield return new WaitForSeconds(delay);

        // Nonaktifkan game object
        textToDisplay.gameObject.SetActive(false);
    }
}
