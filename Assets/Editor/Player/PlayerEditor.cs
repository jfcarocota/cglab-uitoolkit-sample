using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    private VisualElement _root;
    private Player _player;
    private VisualTreeAsset _treeAsset;
    private StyleSheet _styleSheet;
    private FloatField _health;
    private FloatField _maxHealth;
    private Label _healthPercent;
    private VisualElement _healthIndicator;
    private float _healthPercentF;

    private void OnEnable()
    {
        _root = new VisualElement();
        _treeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Player/Player.uxml");
        _styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/Player/Player.uss");

        _treeAsset.CloneTree(_root);
        _root.styleSheets.Add(_styleSheet);

        _player = (Player)target;

        _health = _root.Q<FloatField>("health");
        _maxHealth = _root.Q<FloatField>("maxHealth");
        _healthIndicator = _root.Q<VisualElement>("healthIndicator");
        _healthPercent = _root.Q<Label>("healthPercent");

        _health.BindProperty(serializedObject.FindProperty("_health"));
        _maxHealth.BindProperty(serializedObject.FindProperty("_maxHealth"));

        _health.RegisterValueChangedCallback(e =>
        {
            _player.Health = Mathf.Clamp(e.newValue, 0f, _player.MaxHealth);
            CalculateHealthBar();
            
        });

        _maxHealth.RegisterValueChangedCallback(e =>
        {
            _player.MaxHealth = Mathf.Clamp(e.newValue, 0.001f, _player.MaxHealth);
            _player.Health = Mathf.Clamp(e.newValue, 0f, _player.MaxHealth);
            CalculateHealthBar();
        });


    }


    private void CalculateHealthBar()
    {
        _healthPercentF = _player.Health * 100f / _player.MaxHealth;
        _healthPercent.text = $"{_healthPercentF}%";
        _healthIndicator.style.width = new StyleLength(new Length(_healthPercentF, LengthUnit.Percent));
    }


    public override VisualElement CreateInspectorGUI()
    {
        CalculateHealthBar();
        return _root;
    }
}
