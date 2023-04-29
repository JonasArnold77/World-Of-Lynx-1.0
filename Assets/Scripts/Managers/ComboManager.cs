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

    public List<SuperCombo> actualSuperCombosInRace;

    public SuperCombo ActualSuperCombo;
    
    public int ComboCounter;
    private void Awake()
    {
        Instance = this;
    }

    public void ResetComboCounter()
    {
        ComboCounter = 0;
    }

    public void CheckForSuperCombo(EControls control)
    {
        actualSuperCombosInRace = SuperCombos.Where(s => s.InputList[s.Counter] == control).ToList();
        actualSuperCombosInRace.ForEach(a => a.Counter++);
        actualSuperCombosInRace.Where(a => a.Counter == a.InputList.Count).ToList().ForEach(a2 => a2.Effect());
        actualSuperCombosInRace.Where(a => a.Counter == a.InputList.Count).ToList().ForEach(a2 => a2.Counter = 0);
    }

    public void ResetAllComboCountersInsteadOfSelected(EControls control)
    {
        var resetCombos = Combos.Where(c => c.InputType != control).ToList();
        resetCombos.ForEach(rc => rc.ResetCounter());
    }
}
