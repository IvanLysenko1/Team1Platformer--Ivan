using UnityEngine;
using UnityEngine.UI;

public class ClearSaveButton : MonoBehaviour
{
    public Button clearButton;

    void Start()
    {
        // ��������� ����� �� ������� ������� ������
        clearButton.onClick.AddListener(ClearSave);
    }

    void ClearSave()
    {
        // ������� ���������� (PlayerPrefs)
        PlayerPrefs.DeleteAll();
        Debug.Log("���������� �������!");
    }
}
