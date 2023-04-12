using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using AdventureEngine.Engine;

public class CommandDispatcher : MonoBehaviour
{
    private TMP_InputField commandTMP;

    void Start()
    {
        commandTMP = GameObject.Find("Input Box").GetComponent(typeof(TMP_InputField)) as TMP_InputField;
    }

    private string GetCommandText()
    {
        return commandTMP.text;
    }

    private void ClearCommandTextAndFocus()
    {
        commandTMP.text = "";
        commandTMP.ActivateInputField();
    }

    public void ParseCommand()
    {
        string command = GetCommandText();
        if (command != "")
        {
            WindowHandler.AppendWindowText("Log", $"Command Dispatched: {command}\n");
        }
        ClearCommandTextAndFocus();
    }
}
