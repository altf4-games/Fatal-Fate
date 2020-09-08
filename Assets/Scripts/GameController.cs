using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static void SetCursor(bool visible, bool locked)
    {
        Cursor.visible = visible;
        Cursor.lockState = (locked == true) ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
