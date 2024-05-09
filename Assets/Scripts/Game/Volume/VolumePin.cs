using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VolumePin : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        VolumeController.instance.effectsVolumeSlider.onValueChanged.AddListener(delegate { UpdateVolume(); });
        UpdateVolume();
        if (TryGetComponent(out Button _button))
            _button.onClick.AddListener(audioSource.Play);
    }

    private void UpdateVolume()
    {
        audioSource.volume = VolumeController.instance.effectsVolumeSlider.value / 100;
    }
}
