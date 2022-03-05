using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPage : UI_Base
{
    [SerializeField] Transform ui;
    [SerializeField] Transform field;

    [SerializeField]private Main_UI m_UI;
    [SerializeField]private Main_Field m_Field;

    public override void StartUI()
    {
        ui = transform.GetChild(1);
        field = transform.GetChild(0);
        m_Field = ui.GetComponent<Main_Field>();
        m_UI = field.GetComponent<Main_UI>();

        m_Field.Set_Field();
        m_UI.Set_UI();
    }

    public override void EndUI()
    {
    }


}
