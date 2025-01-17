using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    public static Vector3 direction;

    private Vector3 screen;

    private Vector3 MousePosition => Input.mousePosition - screen / 2;

    private Vector3 startPoint;
    private Vector3 updatePoint;

    public RectTransform joystickBG;
    public RectTransform joystickControl;
    public float magnitude;

    public GameObject joystickPanel;

    private bool active;

    // Start is called before the first frame update
    void Awake()
    {
        screen.x = Screen.width;
        screen.y = Screen.height;

        direction = Vector3.zero;

        joystickPanel.SetActive(false);
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            updatePoint = MousePosition;
            joystickControl.anchoredPosition = Vector3.ClampMagnitude((updatePoint - startPoint), magnitude) + startPoint;

            direction = (updatePoint - startPoint).normalized;
            direction.z = direction.y;
            direction.y = 0;

            direction.y = direction.x;
            direction.x = direction.z;
            direction.z = -direction.y;
            direction.y = 0;
        }
    }

    public void ButtonDown()
    {
        startPoint = MousePosition;
        joystickBG.anchoredPosition = startPoint;
        joystickPanel.SetActive(true);
        active = true;
    }

    public void ButtonUp()
    {
        joystickPanel.SetActive(false);
        direction = Vector3.zero;
        active = false;
    }


    private void OnDisable()
    {
        joystickPanel.SetActive(false);
        active = false;
    }
}
