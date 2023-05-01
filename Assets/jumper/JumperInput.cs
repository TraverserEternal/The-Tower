//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/jumper/JumperInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @JumperInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @JumperInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""JumperInput"",
    ""maps"": [
        {
            ""name"": ""base"",
            ""id"": ""ccbed62a-3bae-42b2-8f0a-e2025635e22f"",
            ""actions"": [
                {
                    ""name"": ""jump"",
                    ""type"": ""Button"",
                    ""id"": ""fd13bd49-9d10-475c-92cf-65b2da54104a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""move"",
                    ""type"": ""Value"",
                    ""id"": ""aa0815b2-7a36-4cea-9ef2-8dd3a2cbf9cf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7a4924f6-1e72-4ac4-9c88-9376c461c14b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""A/D"",
                    ""id"": ""18d9801e-58b5-4909-9ebd-d374c62ecba9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7d8f2b77-c9ee-42aa-9ede-9ef42faaa1cd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""fd075e57-2863-4ffd-b173-e003c7e4ac95"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left/Right"",
                    ""id"": ""b4752071-18d9-44bb-9744-6cc28761365c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""cf8a526a-1e28-499e-aa43-b9b1b5c2de53"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e8dbe816-369a-44a0-b50c-cff420b465b0"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // base
        m_base = asset.FindActionMap("base", throwIfNotFound: true);
        m_base_jump = m_base.FindAction("jump", throwIfNotFound: true);
        m_base_move = m_base.FindAction("move", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // base
    private readonly InputActionMap m_base;
    private List<IBaseActions> m_BaseActionsCallbackInterfaces = new List<IBaseActions>();
    private readonly InputAction m_base_jump;
    private readonly InputAction m_base_move;
    public struct BaseActions
    {
        private @JumperInput m_Wrapper;
        public BaseActions(@JumperInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @jump => m_Wrapper.m_base_jump;
        public InputAction @move => m_Wrapper.m_base_move;
        public InputActionMap Get() { return m_Wrapper.m_base; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BaseActions set) { return set.Get(); }
        public void AddCallbacks(IBaseActions instance)
        {
            if (instance == null || m_Wrapper.m_BaseActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BaseActionsCallbackInterfaces.Add(instance);
            @jump.started += instance.OnJump;
            @jump.performed += instance.OnJump;
            @jump.canceled += instance.OnJump;
            @move.started += instance.OnMove;
            @move.performed += instance.OnMove;
            @move.canceled += instance.OnMove;
        }

        private void UnregisterCallbacks(IBaseActions instance)
        {
            @jump.started -= instance.OnJump;
            @jump.performed -= instance.OnJump;
            @jump.canceled -= instance.OnJump;
            @move.started -= instance.OnMove;
            @move.performed -= instance.OnMove;
            @move.canceled -= instance.OnMove;
        }

        public void RemoveCallbacks(IBaseActions instance)
        {
            if (m_Wrapper.m_BaseActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBaseActions instance)
        {
            foreach (var item in m_Wrapper.m_BaseActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BaseActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BaseActions @base => new BaseActions(this);
    public interface IBaseActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
}