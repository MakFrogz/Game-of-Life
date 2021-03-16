using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputField _inputField;
    [SerializeField] private GameObject _UIpanel;
    private UIInputs _inputActions;
    private bool _hide;
    private void Awake()
    {
        _inputActions = new UIInputs();
        _inputActions.UI.Hide.performed += ctx => HideMenu();
    }

    private void Start()
    {
        _inputField.text = "50";
        _hide = false;
    }

    private void HideMenu()
    {
        _hide = !_hide;
        _UIpanel.SetActive(_hide);
    }

    public void StartSimulation()
    {
        Board.Instance.StartSimulation();
    }

    public void StopSimulation()
    {
        Board.Instance.StopSimulation();
    }

    public void ResetCells()
    {
        Board.Instance.ResetCells();
    }

    public void Randomize()
    {
        int liveDensity = int.Parse(_inputField.text);
        Board.Instance.Randomize(liveDensity);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        _inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        _inputActions.UI.Disable();
    }
}
