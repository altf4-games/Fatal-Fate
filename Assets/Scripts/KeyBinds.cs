using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBinds : MonoBehaviour
{
    [SerializeField] private KeyCode engineKey = KeyCode.F;
    [SerializeField] private KeyCode hornKey = KeyCode.H;
    [SerializeField] private KeyCode brakeKey = KeyCode.Space;
    [SerializeField] private KeyCode exitKey = KeyCode.Escape;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    public static KeyCode engine;
    public static KeyCode horn;
    public static KeyCode brake;
    public static KeyCode exit;
    public static KeyCode sprint;

    private void Awake()
    {
        engine = engineKey;
        horn = hornKey;
        brake = brakeKey;
        exit = exitKey;
        sprint = sprintKey;
    }
}
