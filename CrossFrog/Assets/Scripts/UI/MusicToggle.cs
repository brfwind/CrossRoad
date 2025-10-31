using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    public Toggle musicToggle;
    private void Start()
    {
        musicToggle.isOn = SettingsManager.instance.showMusic;
        AudioManager.instance.ToggleAudio(SettingsManager.instance.showMusic);
        musicToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    public void OnToggleChanged(bool value)
    {
        SettingsManager.instance.showMusic = value;
        AudioManager.instance.ToggleAudio(value);
    }
}
