using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject editorPanel;

    public void OpenEditor()
    {
        editorPanel.SetActive(true);
    }

    public void SaveCard()
    {
        // Aquí puedes agregar el código para guardar la carta
        // Por ejemplo, leer el texto del InputField y crear una nueva carta
        string cardCode = editorPanel.GetComponentInChildren<InputField>().text;
        CreateCard(cardCode);
        editorPanel.SetActive(false);
    }

    private void CreateCard(string code)
    {
        // Implementa la lógica para crear una carta a partir del código
        Debug.Log("Nueva carta creada con el código: " + code);
    }
}
