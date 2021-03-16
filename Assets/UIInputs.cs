// GENERATED AUTOMATICALLY FROM 'Assets/UIInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @UIInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @UIInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""UIInputs"",
    ""maps"": [
        {
            ""name"": ""UI"",
            ""id"": ""efac2eef-090a-4175-a962-73992d0cc2b2"",
            ""actions"": [
                {
                    ""name"": ""Hide"",
                    ""type"": ""Button"",
                    ""id"": ""769263c7-29be-4592-b220-65eb908d28d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4b730a62-bf3c-4343-9228-cd08e0b4094d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Hide = m_UI.FindAction("Hide", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Hide;
    public struct UIActions
    {
        private @UIInputs m_Wrapper;
        public UIActions(@UIInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Hide => m_Wrapper.m_UI_Hide;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Hide.started -= m_Wrapper.m_UIActionsCallbackInterface.OnHide;
                @Hide.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnHide;
                @Hide.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnHide;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Hide.started += instance.OnHide;
                @Hide.performed += instance.OnHide;
                @Hide.canceled += instance.OnHide;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IUIActions
    {
        void OnHide(InputAction.CallbackContext context);
    }
}
