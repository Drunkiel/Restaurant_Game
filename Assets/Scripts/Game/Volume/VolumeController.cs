using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public static VolumeController instance;

    public Slider backgroundVolumeSlider;
    [SerializeField] private AudioSource backgroundAudio;
    [SerializeField] private AudioClip[] backgroundClips;

    public Slider effectsVolumeSlider;

    private void Awake()
    {
        instance = this;
        backgroundAudio.clip = GetRandomClip();
        backgroundAudio.Play();
    }

    public void UpdateBackgroundVolume()
    {
        backgroundAudio.volume = backgroundVolumeSlider.value / 100;
    }

    public AudioClip GetRandomClip()
    {
        return backgroundClips[Random.Range(0, backgroundClips.Length)];
    }
}
