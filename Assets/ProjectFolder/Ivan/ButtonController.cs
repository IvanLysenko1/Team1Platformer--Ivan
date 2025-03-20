using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonController : MonoBehaviour, IPointerEnterHandler
{
    public UnityEvent OnSoundFinished; // �������, ������� ����� ������� ����� ���������� �����
    private Button _button; //������ �������   

    MenuAudio MenuAudio;

    private void Awake()
    {
        MenuAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<MenuAudio>();
    }

    private void Start()
    {       
        _button = GetComponent<Button>();

        if (_button != null)
        {
            // ��������� ���������� ������� �� ������
            _button.onClick.AddListener(OnButtonClick);
        }
    }

    // �����, ���������� ��� ��������� ������� �� ������
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ���������, ������� �� ���� � �������� �� ���� ���������
        if (MenuAudio.SFXSource != null && MenuAudio.HoverSound != null)
        {
            MenuAudio.PlaySFX(MenuAudio.HoverSound);
        }


    }
    // �����, ���������� ��� ����� �� ������
    private void OnButtonClick()
    {
        if (MenuAudio.SFXSource != null && MenuAudio.ClickSound != null)
        {
            // ����������� ����
            MenuAudio.PlaySFX(MenuAudio.ClickSound);

            // ��������� ��������, ������� ���� ���������� �����
            StartCoroutine(WaitForSoundToFinish());
        }
        else
        {
            // ���� ���� �� �����, ����� �������� �������
            OnSoundFinished.Invoke();
        }
    }

    private System.Collections.IEnumerator WaitForSoundToFinish()
    {
        // ����, ���� ���� �� ����������
        while (MenuAudio.SFXSource.isPlaying)
        {
            yield return null;
        }

        // �������� ������� ����� ���������� �����
        OnSoundFinished.Invoke();
    }
}
