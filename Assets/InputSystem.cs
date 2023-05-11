//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/InputSystem.inputactions
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

public partial class @InputSystem : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputSystem"",
    ""maps"": [
        {
            ""name"": ""PlayerActionMap"",
            ""id"": ""02d412c2-be08-4b99-9d43-526fbb0d9a6b"",
            ""actions"": [
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""f4fddf6a-9d4f-401e-b70b-74683f243106"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""33d03e39-af1d-4f4d-b94b-e09faf492ad9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""806e012d-b353-4ce3-be42-b92367a7d513"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""6b125fbd-afb8-4c28-8d34-c775de4bd547"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f18f4d03-7bed-4dbe-b2b6-261ff1f89a79"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e36ba766-ace5-4fd0-bafc-4b977e485e36"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""74eccf65-3e05-4613-8c1f-8efba5479102"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2bb532c-ebfd-4ee6-8db8-b627e39b2441"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerActionMap
        m_PlayerActionMap = asset.FindActionMap("PlayerActionMap", throwIfNotFound: true);
        m_PlayerActionMap_Left = m_PlayerActionMap.FindAction("Left", throwIfNotFound: true);
        m_PlayerActionMap_Right = m_PlayerActionMap.FindAction("Right", throwIfNotFound: true);
        m_PlayerActionMap_Down = m_PlayerActionMap.FindAction("Down", throwIfNotFound: true);
        m_PlayerActionMap_Up = m_PlayerActionMap.FindAction("Up", throwIfNotFound: true);
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

    // PlayerActionMap
    private readonly InputActionMap m_PlayerActionMap;
    private IPlayerActionMapActions m_PlayerActionMapActionsCallbackInterface;
    private readonly InputAction m_PlayerActionMap_Left;
    private readonly InputAction m_PlayerActionMap_Right;
    private readonly InputAction m_PlayerActionMap_Down;
    private readonly InputAction m_PlayerActionMap_Up;
    public struct PlayerActionMapActions
    {
        private @InputSystem m_Wrapper;
        public PlayerActionMapActions(@InputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Left => m_Wrapper.m_PlayerActionMap_Left;
        public InputAction @Right => m_Wrapper.m_PlayerActionMap_Right;
        public InputAction @Down => m_Wrapper.m_PlayerActionMap_Down;
        public InputAction @Up => m_Wrapper.m_PlayerActionMap_Up;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionMapActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionMapActions instance)
        {
            if (m_Wrapper.m_PlayerActionMapActionsCallbackInterface != null)
            {
                @Left.started -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnRight;
                @Down.started -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnDown;
                @Up.started -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnUp;
            }
            m_Wrapper.m_PlayerActionMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
            }
        }
    }
    public PlayerActionMapActions @PlayerActionMap => new PlayerActionMapActions(this);
    public interface IPlayerActionMapActions
    {
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
    }
}
