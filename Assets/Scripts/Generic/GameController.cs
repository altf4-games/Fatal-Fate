using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum CharacterType
{
    Murderer,
    Victim,
}

public class GameController : MonoBehaviour
{
    public CharacterType character;

    private void Start()
    {
        SetCursor(false, true);
    }

    public static void SetCursor(bool visible, bool locked)
    {
        Cursor.visible = visible;
        Cursor.lockState = (locked == true) ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
