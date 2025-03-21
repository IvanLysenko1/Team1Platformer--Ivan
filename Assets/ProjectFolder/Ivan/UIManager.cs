using System;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void QuitApplication()
    {
#if UNITY_EDITOR
        // ���� ���� �������� � ��������� Unity, ���������� ���
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ���� ���� ������� � �������� �� ���������
        Application.Quit();
#endif
    }
}
