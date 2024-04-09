using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LinearProgressionAuthApacheTab : MonoBehaviour
{
    public InputField[] inputFields;
    public Color selectedColor; // Warna yang akan diterapkan saat terpilih

    private Color[] originalColors; // Array untuk menyimpan warna asli InputField

    void Start()
    {
        // Simpan warna asli setiap InputField
        originalColors = new Color[inputFields.Length];
        for (int i = 0; i < inputFields.Length; i++)
        {
            originalColors[i] = inputFields[i].image.color;

            // Tambahkan event listener untuk klik mouse pada setiap InputField
            AddListenerToInputField(inputFields[i], i);
        }
        inputFields[0].Select();
        inputFields[0].image.color = selectedColor;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeSelectedInputField();
        }
    }

    void AddListenerToInputField(InputField inputField, int index)
    {
        EventTrigger trigger = inputField.gameObject.AddComponent<EventTrigger>();

        // Buat entri event trigger untuk OnPointerClick
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { OnInputFieldClicked(index); });
        trigger.triggers.Add(entry);
    }

    void OnInputFieldClicked(int index)
    {
        // Kembalikan warna asli untuk semua InputField
        ResetInputFieldColors();

        // Pindahkan fokus ke InputField yang terklik
        inputFields[index].Select();

        // Ubah warna InputField yang terpilih
        inputFields[index].image.color = selectedColor;
    }

    void ChangeSelectedInputField()
    {
        // Temukan InputField yang sedang aktif
        InputField currentInputField = null;
        for (int i = 0; i < inputFields.Length; i++)
        {
            if (inputFields[i].isFocused)
            {
                currentInputField = inputFields[i];
                break;
            }
        }

        // Kembalikan warna asli untuk semua InputField
        ResetInputFieldColors();

        if (currentInputField != null)
        {
            // Temukan indeks InputField saat ini
            int currentIndex = System.Array.IndexOf(inputFields, currentInputField);

            // Pindahkan kursor ke InputField berikutnya dalam array
            int nextIndex = (currentIndex + 1) % inputFields.Length;
            inputFields[nextIndex].Select();

            // Ubah warna InputField yang terpilih
            inputFields[nextIndex].image.color = selectedColor;
        }
        else if (inputFields.Length > 0)
        {
            // Jika tidak ada yang sedang fokus, pilih InputField pertama
            inputFields[0].Select();
            inputFields[0].image.color = selectedColor;
        }
    }

    void ResetInputFieldColors()
    {
        // Kembalikan warna asli untuk semua InputField
        for (int i = 0; i < inputFields.Length; i++)
        {
            inputFields[i].image.color = originalColors[i];
        }
    }
}