using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    private Button _btnHello;
    private UIDocument _uiDocument;
    private Label _btnLabel;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _btnHello = _uiDocument.rootVisualElement.Q<Button>("btnHello");
        _btnLabel = _uiDocument.rootVisualElement.Q<Label>("btnLabel");
    }

    private void Start()
    {
        _btnHello.clicked += OnHelloClicked;
        _btnLabel.RegisterCallback<ClickEvent>(OnHelloLabelClicked);
    }

    private void OnHelloClicked() => Debug.Log("hello");

    private void OnHelloLabelClicked(ClickEvent e) => Debug.Log("hello label");
}
