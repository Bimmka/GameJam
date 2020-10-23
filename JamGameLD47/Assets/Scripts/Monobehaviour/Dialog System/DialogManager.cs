using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("Continue Button")]
    [SerializeField] private GameObject continueButton;

    [Header("Поле для вывода текста")]
    [SerializeField] private Text outPutTextField;

    [Header("Поле за вывода текста")]
    [SerializeField] private GameObject outPutPanel;

    private string[] speech;

    private int sentenseIndex = 0;

    public static DialogManager instance;

    public static Action<bool> DialogStart;

    private bool flag = false;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Dialog Manager not alone");
            return;
        }
        instance = this;
    }

    IEnumerator WriteMessage(string message)
    {
        outPutTextField.text = "";
        foreach (var letter in message.ToCharArray())
        {
            outPutTextField.text += letter;
            yield return new WaitForSeconds (0.05f);
        }
        continueButton.SetActive(true);
    }

    public void IncIndex()
    {
        sentenseIndex++;
        if (sentenseIndex < speech.Length) StartCoroutine(WriteMessage(speech[sentenseIndex]));
        else
        {
            if (DialogStart != null) DialogStart(false);
            flag = false;
            outPutTextField.text = "";
            outPutPanel.SetActive(false);
            sentenseIndex = 0;
        }
        continueButton.SetActive(false);
    }

    public void SetText (string[] text)
    {
        speech = text;
        outPutPanel.SetActive(true);
        flag = true;
        if (DialogStart != null) DialogStart(true);
        StartCoroutine(WriteMessage(speech[sentenseIndex])); 
    }

    public bool GetDialogSrart()
    {
        return flag;
    }

    private void OnDisable()
    {
        instance = null;
    }



}
