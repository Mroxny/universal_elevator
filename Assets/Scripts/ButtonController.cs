using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonController : MonoBehaviour
{
    public string defaultText;
    public AudioClip whenPressed;

    [SerializeField] private AudioSource audio;
    [SerializeField] private TextMesh textMesh;
    [SerializeField] private Button button;


    void Start()
    {
        SetText(defaultText);
    }

    public void SetText(string text) {
        textMesh.text = text;
    }

    public void PressButton()
    {
        audio.PlayOneShot(whenPressed);
        button.onClick.Invoke();
    }
}
