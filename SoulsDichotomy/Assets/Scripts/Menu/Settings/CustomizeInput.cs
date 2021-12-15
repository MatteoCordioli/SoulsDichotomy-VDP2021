using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CustomizeInput : MonoBehaviour
{
    public delegate void OnChangeInput(KeyCode up, KeyCode down, KeyCode right, KeyCode left, KeyCode interact, KeyCode switchChar);
    public static OnChangeInput changeInput;

    //non credo serva ma per ora lo lascio qua che ci ho sudato il sangue
    public static OnChangeInput ChangeInput { 
        set { changeInput += value;
            changeInput(map["upKey"], map["downKey"], map["rightKey"], map["leftKey"], map["interactKey"], map["switchKey"]);
        }
        get { return changeInput; }
    }

    public TextMeshProUGUI upText;
    public TextMeshProUGUI downText;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI switchText;

    public GameObject pressAnyKey;

    private KeyCode up;
    private KeyCode down;
    private KeyCode right;
    private KeyCode left;
    private KeyCode interact;
    private KeyCode switchChar;

    private string selected;
    private bool editMode;
  
    public static Dictionary<String, KeyCode> map;
    // Start is called before the first frame update
    void Start()
    {
        map = new Dictionary<string, KeyCode>();
        map.Add("upKey", up);
        map.Add("downKey", down);
        map.Add("rightKey", right);
        map.Add("leftKey", left);
        map.Add("interactKey", interact);
        map.Add("switchKey", switchChar);
        SetDefault();
        editMode = false;
        FillKeyCode();
    }
    
    private void SetDefault()
    {
        map["upKey"] = KeyCode.UpArrow;
        map["downKey"] = KeyCode.DownArrow;
        map["rightKey"] = KeyCode.LeftArrow;
        map["leftKey"] = KeyCode.RightArrow;
        map["interactKey"] = KeyCode.E;
        map["switchKey"] = KeyCode.Space;
    }

    private void FillKeyCode()
    {
        IfExistFillOrInit(ref up, "upKey");
        IfExistFillOrInit(ref down, "downKey");
        IfExistFillOrInit(ref right, "rightKey");
        IfExistFillOrInit(ref left, "leftKey");
        IfExistFillOrInit(ref interact, "interactKey");
        IfExistFillOrInit(ref switchChar, "switchKey");
    }

    public void SetText()
    {
        upText.text = map["upKey"].ToString();
        downText.text = map["downKey"].ToString();
        rightText.text = map["rightKey"].ToString();
        leftText.text = map["leftKey"].ToString();
        interactText.text = map["interactKey"].ToString();
        switchText.text = map["switchKey"].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (editMode)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(keyCode) && keyCode!=KeyCode.Escape)
                    {
                        ChangeKey(keyCode, selected);
                        editMode = false;
                        break;
                    }
                }
            }
        }



    }

    public void SwitchEditMod(string name)
    {
        ToggleTextPressAnyKey();
        editMode = true;
        switch (name)
        {
            case "up":
                selected = "upKey";
                break;
            case "down":
                selected = "downKey";
                break;
            case "left":
                selected = "leftKey";
                break;
            case "right":
                selected = "rightKey";
                break;
            case "interact":
                selected = "interactKey";
                break;
            case "switch":
                selected = "switchKey";
                break;
        }
    }

    private void IfExistFillOrInit(ref KeyCode key, string name)
    {
        if (PlayerPrefs.HasKey(name))
        {
            if((KeyCode)PlayerPrefs.GetInt(name) == KeyCode.None)
            {
                PlayerPrefs.SetInt(name, (int)map[name]);
            }
            else
            {
                map[name] = (KeyCode)PlayerPrefs.GetInt(name);
            }
        }
        else
        {
            PlayerPrefs.SetInt(name, (int)map[name]);
        }
    }

    private void ChangeKey(KeyCode newKey, string name)
    {
        if (PlayerPrefs.HasKey(name))
        {
            
            PlayerPrefs.SetInt(name, (int)newKey);
            map[name] = newKey;
            SetText();
            ToggleTextPressAnyKey();
            if (changeInput != null)
            {
                changeInput(map["upKey"], map["downKey"], map["rightKey"], map["leftKey"], map["interactKey"], map["switchKey"]);
            }
        }
    }

    private void ToggleTextPressAnyKey()
    {
        pressAnyKey.SetActive(!pressAnyKey.activeInHierarchy);
    }
    
    public KeyCode GetSwitchkey()
    {
        return map["switchKey"];
    }


}