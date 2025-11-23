using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    public Toggle controlToggle;
    private void Start()
    {
        controlToggle.isOn = SettingsManager.instance.showControl;
        controlToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    public void OnToggleChanged(bool value)
    {
        SettingsManager.instance.showControl = value;
    }
}