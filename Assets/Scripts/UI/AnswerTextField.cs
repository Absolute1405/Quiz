using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class AnswerTextField : MonoBehaviour
{
    private TextMeshProUGUI _textField;

    private void Awake()
    {
        _textField = GetComponent<TextMeshProUGUI>();
    }

    public void RefreshAnswer(string answer)
    {
        _textField.text = $"Find {answer}";
    }
}
