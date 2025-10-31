using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualToggle : MonoBehaviour
{
    public GameObject manualPanel;
    private bool isActive = false;

    public void ManualPanel()
    {
        isActive = !isActive;
        manualPanel.SetActive(isActive);
    }

    
}
