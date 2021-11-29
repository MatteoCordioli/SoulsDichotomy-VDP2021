using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Image iconCharacter;
    [SerializeField] private Sprite playerIco;
    [SerializeField] private Sprite soulIco;

    [SerializeField]
    private Slider playerHealth;
    [SerializeField]
    private Slider soulHealth;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.changeCharacter += SwitchIcon;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
            
    }

    public void SetUpSlider()
    {
        playerHealth.value = 1f;
        soulHealth.value = 1f;
    }

    public void SliderPlayer(float value)
    {
        playerHealth.value = value;
    }
    
    public void SliderSoul(float value)
    {
        soulHealth.value = value;
    }

    private void SwitchIcon()
    {
        if (iconCharacter.sprite == playerIco)
        {
            iconCharacter.sprite = soulIco;
        }
        else
        {
            iconCharacter.sprite = playerIco;
        }
    }

    private void OnDestroy()
    {
        GameManager.changeCharacter -= SwitchIcon;
    }
}