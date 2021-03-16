using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DropDownHandler : MonoBehaviour
{
    [SerializeField] private List<string> _list;
    [SerializeField] private UnityEvent<string> _event;
    private Dropdown _dropDown;
    void Start()
    {
        _dropDown = GetComponent<Dropdown>();
        _dropDown.options.Clear();
        _dropDown.AddOptions(_list);
        _dropDown.onValueChanged.AddListener(delegate { DropdownModeSelected(); });
    }

    private void DropdownModeSelected()
    {
        _event?.Invoke(_list[_dropDown.value]);
    }

}
