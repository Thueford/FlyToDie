using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : StageButtonHandler
{
    public TMPro.TextMeshPro text;
    public bool repeat = true;
    private string[] s;
    private int iText, iChar;

    private void Start()
    {
        if (!text)
        {
            Destroy(this);
            return;
        }

        text.gameObject.SetActive(false);
        s = text.text.Trim().Replace("\n", "%\n").Split('%');
    }

    public override void Toggle(bool status, Collider c)
    {
        // start talking
        if (!text.IsActive())
        {
            if (!repeat && iText > 0) return;
            text.gameObject.SetActive(true);
            text.text = "";
            iText = iChar = 0;
            KeyHandler.enableMovement = false;
            InvokeRepeating(nameof(DisplayText), 0, 0.03f);
        }
        // skip talking
        else if (iChar < s[iText].Length)
        {
            CancelInvoke();
            while (iChar < s[iText].Length) DisplayText();
        }
        // stop talking
        else if (++iText == s.Length)
        {
            text.text = "";
            text.gameObject.SetActive(false);
            KeyHandler.enableMovement = true;
        }
        // continue talking
        else
        {
            iChar = 0;
            InvokeRepeating(nameof(DisplayText), 0, 0.03f);
        }
    }
    
    private void DisplayText()
    {
        // Events
        if (text.text == "" && s[iText][iChar] == '[')
            handleEvent(s[iText].Substring(iChar + 1, s[iText++].IndexOf(']') - iChar - 1));

        // mid pause
        if (iChar == s[iText].Length || s[iText][iChar] == '%') CancelInvoke();
        // next line
        else if (s[iText][iChar] == '\n') text.text = "";
        // delays
        else if (s[iText][iChar] != '_') text.text += s[iText][iChar];
        iChar++;
    }

    public void handleEvent(string name)
    {
        //ScriptedStage s = StageManager.curStage.GetComponent<ScriptedStage>();
        //if(s) s.Invoke("ev_" + name, 0);
    }
}
