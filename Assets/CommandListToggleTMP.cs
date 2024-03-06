using UnityEngine;
using TMPro; // Include the TextMeshPro namespace
using UnityEngine.UI;

public class CommandListToggleTMP : MonoBehaviour
{
    public TMP_Text commandTextTMP; // Assign this in the inspector with your TMP Text for the command
    public Button commandListButton; // Assign this in the inspector with your Command List Button

    private bool isTextVisible = true;

    // Start is called before the first frame update
    void Start()
    {
        // Add a click listener to the button which will call the ToggleText function
        commandListButton.onClick.AddListener(ToggleText);
        commandTextTMP.gameObject.SetActive(!isTextVisible); 
        UpdateTextVisibility();
    }

    // This function toggles the visibility of the TMP text
    void ToggleText()
    {
        isTextVisible = !isTextVisible; // Toggle the state
        UpdateTextVisibility();
    }

    // This function updates the TMP text visibility based on isTextVisible state
    void UpdateTextVisibility()
    {
        commandTextTMP.gameObject.SetActive(isTextVisible); // Show or hide the TMP text
    }

    // Call this method to change the command text displayed by the TMP text element
    public void ChangeCommandText(string newCommand)
    {
        commandTextTMP.text = newCommand;
    }
}
