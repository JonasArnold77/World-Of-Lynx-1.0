using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance;
    public List<AnimationClip> _FirstCombo = new List<AnimationClip>();

    public int ComboCounter;
    private void Awake()
    {
        Instance = this;
    }

    public void IncrementComboCounter()
    {
        if(ComboCounter < _FirstCombo.Count -1)
        {
            ComboCounter++;
        }
        else
        {
            ComboCounter = 0;
        }
    }

    public void ResetComboCounter()
    {
        ComboCounter = 0;
    }
}
