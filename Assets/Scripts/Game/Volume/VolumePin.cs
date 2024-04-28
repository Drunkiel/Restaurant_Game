using UnityEngine;

public class VolumePin : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        VolumeController.instance.effectsVolumeSlider.onValueChanged.AddListener(delegate { UpdateVolume(); });
    }

    private void UpdateVolume()
    {
        audioSource.volume = VolumeController.instance.effectsVolumeSlider.value / 100;
    }
}
