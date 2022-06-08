using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public void MakeMoveOnClick()
    {
        GameManager.Instance.NextTurn();
    }

    public void RedoOnClick()
    {
        GameManager.Instance.commandManager.Redo();
    }

    public void UndoOnClick()
    {
        GameManager.Instance.commandManager.Undo();
    }
}
