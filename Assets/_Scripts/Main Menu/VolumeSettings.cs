using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider _MasterVolumeSlider;
    private void Start()
    {
        _MasterVolumeSlider.onValueChanged.AddListener(val => AudioManager.Instance.ChangeMasterVolume(val));
    }
}