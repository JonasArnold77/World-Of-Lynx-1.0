using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance;
    public List<AnimationClip> _FirstCombo = new List<AnimationClip>();

    public List<Combo> Combos;
    public List<SuperCombo> SuperCombos;

    public SuperCombo ActualSuperCombo;
    
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

    public void ResetAllComboCountersInsteadOfSelected(EControls control)
    {
        var resetCombos = Combos.Where(c => c.InputType != control).ToList();
        resetCombos.ForEach(rc => rc.ResetCounter());
    }
}
