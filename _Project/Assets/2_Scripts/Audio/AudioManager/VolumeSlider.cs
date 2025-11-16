using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public enum VolumeType
    {
        Music,
        SFX
    }

    [SerializeField] private Slider slider;
    [SerializeField] private VolumeType volumeType = VolumeType.Music;

    private void Awake()
    {
        if (slider == null)
            slider = GetComponent<Slider>();

        // Cargar el volumen guardado y asignarlo al slider
        slider.value = PlayerPrefs.GetFloat($"{volumeType}Volume", 0.8f);

        // Asignar el evento para cambiar volumen en tiempo real
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        switch (volumeType)
        {
            case VolumeType.Music:
                AudioManager.Instance.SetMusicVolume(value);
                PlayerPrefs.SetFloat("MusicVolume", value);
                break;
            case VolumeType.SFX:
                AudioManager.Instance.SetSFXVolume(value);
                PlayerPrefs.SetFloat("SFXVolume", value);
                break;
        }
    }

    private void OnDestroy()
    {
        // Remover el listener para evitar memory leaks
        if (slider != null)
            slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}