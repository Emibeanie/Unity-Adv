using System;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    public static WinUI Instance;

    private void Awake()
    {
        if (Instance is not null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Ok();
    }

    public void WinGame()
    {
        gameObject.SetActive(true);
    }

    public void Ok()
    {
        gameObject.SetActive(false);
    }
}