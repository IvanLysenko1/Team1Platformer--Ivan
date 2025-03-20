using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public static VolumeSettings Instance; // ��������

    public AudioMixer audioMixer; // ������ �� AudioMixer
    public Slider musicSlider;    // ������� ��� ������ 
    public Slider SFXSlider;    // ������� ��� ������ 

    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    private void Awake()
    {
        // ���������� ���������
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ���������� ��� �������� ����� �����
        }
        else
        {
            Destroy(gameObject); // ���������� ��������
            return;
        }

        // ��������� ����������� �������� ���������
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 0.75f);
        float savedSFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 0.75f);

        // ��������� ��������� ���������
        SetMusicVolume(savedMusicVolume);
        SetSFXVolume(savedSFXVolume);

        // ���� �������� ���� �� ������� ����� (��������� ����), ����������� ��
        if (musicSlider != null && SFXSlider != null)
        {
            musicSlider.value = savedMusicVolume;
            SFXSlider.value = savedSFXVolume;

            // ������������� �� ��������� ���������
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            SFXSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    public void SetMusicVolume(float volume)
    {
        // ����������� �������� �������� � ��������������� ����� (� ��������)
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        // ��������� �������� � PlayerPrefs
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
    }

    public void SetSFXVolume(float volume)
    {
        // ����������� �������� �������� � ��������������� ����� (� ��������)
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        // ��������� �������� � PlayerPrefs
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
    }
}
