using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinChecker : MonoBehaviour
{
    public RowChecker[] checkers;
    public UnityEvent onWin;

    public void CheckForWin()
    {
        foreach (var checker in checkers)
        {
            if (!checker.rightColor)
                return;
        }

        onWin?.Invoke();
        print("Win!");
    }
}
