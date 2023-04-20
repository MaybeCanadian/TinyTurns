using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : UIObject
{
    public float percent;
    public Slider healthSlider = null;

    #region Init Functions
    public HealthBar(UIList UI) : base(UI)
    {
        percent = 1.0f;
    }
    #endregion

    #region Visuals
    public override void CreateVisuals(bool remake = false)
    {
        base.CreateVisuals(remake);

        if(objectOBJ == null)
        {
            return;
        }

        healthSlider = objectOBJ.GetComponent<Slider>();
    }
    public override void DestroyVisuals()
    {
        base.DestroyVisuals();

        healthSlider = null;
    }
    #endregion
}
