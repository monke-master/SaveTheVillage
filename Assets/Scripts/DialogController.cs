using System;
using UnityEngine;

public class DialogController : MonoBehaviour
{

    private static GameObject _activeDialog;
    
    public static void ShowDialog(String name)
    {
        _activeDialog = GameObject.Find("Canvas").transform.Find(name).gameObject;
        _activeDialog.SetActive(true);
    }

    public static void HideDialog(String name)
    {
        GameObject.Find("Canvas").transform.Find(name).gameObject.SetActive(false);
    }

    public static void HideActiveDialog()
    {
        _activeDialog.SetActive(false);
    }
}
