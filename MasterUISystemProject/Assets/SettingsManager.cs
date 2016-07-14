﻿using UnityEngine;
using System.IO;
using System.Collections;
using System.Xml;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public AvatarSettings a_Settings = new AvatarSettings();
    public KeyBindings kb_Settings = new KeyBindings();
    public WidgetControlSettings wc_Settings = new WidgetControlSettings();

    public TP_Motor tp_Motor_Ref;
    public TP_InputManager tp_InputManager_Ref;
    public MiniMapManager mm_Manager_Ref;
    public GameObject mm_GameObject;
    public GameObject sl_GameObject;
    public GameObject bm_GameObject;
    public GameObject ci_Gameobject;

    void OnEnable()
    {
        if (Instance == null)
            Instance = this;
    }
    /// <summary>
    /// There are only three settings files we need to load
    /// AvatarSettings, KeyBindings and WidgetControlSettings
    /// If those files are not found, save a copy of them with default settings.
    /// </summary>

    public void LoadSettingsFiles()
    {
        if (File.Exists(Application.dataPath + "/FullPackage/Settings/AvatarSettings.sets"))
        {
            a_Settings = XmlIO.Load(Application.dataPath + "/FullPackage/Settings/AvatarSettings.sets", typeof(AvatarSettings)) as AvatarSettings;
            ApplyAvatarSettings();
        }
        else
        {
            SaveAvatarSettings();
        }


        if (File.Exists(Application.dataPath + "/FullPackage/Settings/KeyBindings.sets"))
        {
            kb_Settings = XmlIO.Load(Application.dataPath + "/FullPackage/Settings/KeyBindings.sets", typeof(KeyBindings)) as KeyBindings;
            ApplyKeyBindings();
        }
        else
        {
            SaveKeyBindings();
        }

        if (File.Exists(Application.dataPath + "/FullPackage/Settings/WidgetControlSettings.sets"))
        {
            wc_Settings = XmlIO.Load(Application.dataPath + "/FullPackage/Settings/WidgetControlSettings.sets", typeof(WidgetControlSettings)) as WidgetControlSettings;
            ApplyWidgetControlSettings();
        }
        else
        {
            SaveWidgetControlSettings();
        }
    }

    /// <summary>
    /// Applies all of the AvatarSettings to the tp_Motor script.
    /// </summary>
	public void ApplyAvatarSettings()
    {
        tp_Motor_Ref.gravityOn = a_Settings.a_GravColl;
        tp_Motor_Ref.ForwardSpeed = a_Settings.a_ForwardSpeed;
        tp_Motor_Ref.BackwardSpeed = a_Settings.a_BackwardSpeed;
        tp_Motor_Ref.StrafingSpeed = a_Settings.a_StrafeSpeed;
        tp_Motor_Ref.VerticalSpeed = a_Settings.a_VerticalSpeed;
        tp_Motor_Ref.JumpSpeed = a_Settings.a_JumpSpeed;
    }

    /// <summary>
    /// This function fills in the AvatarSettingsMenu with all of the stored AvatarSettings values.
    /// </summary>
    public void FillAvatarSettingsMenu()
    {
        SettingsMenusRefs.Instance.trackingToggle.isOn = a_Settings.a_Tracking;
        SettingsMenusRefs.Instance.gravityToggle.isOn = a_Settings.a_GravColl;
        SettingsMenusRefs.Instance.forwardSpeedInput.text = a_Settings.a_ForwardSpeed.ToString();
        SettingsMenusRefs.Instance.backwardSpeedInput.text = a_Settings.a_BackwardSpeed.ToString();
        SettingsMenusRefs.Instance.strafeSpeedInput.text = a_Settings.a_StrafeSpeed.ToString();
        SettingsMenusRefs.Instance.jumpSpeedInput.text = a_Settings.a_JumpSpeed.ToString();
    }

    /// <summary>
    /// This updates all of the a_Settings values from the menu and then calls ApplyAvatarSettings.
    /// </summary>
    public void UpdateAvatarSettingsFromMenu()
    {
        a_Settings.a_Tracking = SettingsMenusRefs.Instance.trackingToggle.isOn;
        a_Settings.a_GravColl = SettingsMenusRefs.Instance.gravityToggle.isOn;

        if (SettingsMenusRefs.Instance.forwardSpeedInput.text != null && SettingsMenusRefs.Instance.forwardSpeedInput.text != "")
            a_Settings.a_ForwardSpeed = float.Parse(SettingsMenusRefs.Instance.forwardSpeedInput.text);

        if (SettingsMenusRefs.Instance.backwardSpeedInput.text != null && SettingsMenusRefs.Instance.backwardSpeedInput.text != "")
            a_Settings.a_BackwardSpeed = float.Parse(SettingsMenusRefs.Instance.backwardSpeedInput.text);

        if (SettingsMenusRefs.Instance.strafeSpeedInput.text != null && SettingsMenusRefs.Instance.strafeSpeedInput.text != "")
            a_Settings.a_StrafeSpeed = float.Parse(SettingsMenusRefs.Instance.strafeSpeedInput.text);

        if (SettingsMenusRefs.Instance.jumpSpeedInput.text != null && SettingsMenusRefs.Instance.jumpSpeedInput.text != "")
            a_Settings.a_JumpSpeed = float.Parse(SettingsMenusRefs.Instance.jumpSpeedInput.text);

        ApplyAvatarSettings();
    }

    /// <summary>
    /// This function saves the a_Settings object to the Settings folder.
    /// </summary>
    public void SaveAvatarSettings()
    {
        XmlIO.Save(a_Settings, Application.dataPath + "/FullPackage/Settings/AvatarSettings.sets");
    }

    /// <summary>
    /// Applies all of the KeyBindings to the tp_InputManager.
    /// </summary>
    public void ApplyKeyBindings()
    {
        tp_InputManager_Ref.forward = kb_Settings.kb_Forward;
        tp_InputManager_Ref.backward = kb_Settings.kb_Backward;
        tp_InputManager_Ref.leftward = kb_Settings.kb_Leftward;
        tp_InputManager_Ref.rightward = kb_Settings.kb_Rightward;
        tp_InputManager_Ref.elevate = kb_Settings.kb_Elevate;
        tp_InputManager_Ref.descend = kb_Settings.kb_Descend;
        tp_InputManager_Ref.gravity = kb_Settings.kb_Gravity;
        tp_InputManager_Ref.rotateLeft = kb_Settings.kb_RotateLeft;
        tp_InputManager_Ref.rotateRight = kb_Settings.kb_RotateRight;
        tp_InputManager_Ref.rotateKeySensitivity = kb_Settings.kb_RotateKeySensitivity;
        tp_InputManager_Ref.increaseSpeed = kb_Settings.kb_IncreaseSpeed;
        tp_InputManager_Ref.decreaseSpeed = kb_Settings.kb_DecreaseSpeed;
        tp_InputManager_Ref.toggleCamera = kb_Settings.kb_ToggleCamera;
    }

    /// <summary>
    /// This function fills in the KeybindingsMenu with all of the stored Keybindings values.
    /// </summary>
    public void FillKeybindingsMenu()
    {
        SettingsMenusRefs.Instance.forwardInput.text = kb_Settings.kb_Forward;
        SettingsMenusRefs.Instance.backwardInput.text = kb_Settings.kb_Backward;
        SettingsMenusRefs.Instance.leftInput.text = kb_Settings.kb_Leftward;
        SettingsMenusRefs.Instance.rightInput.text = kb_Settings.kb_Rightward;
        SettingsMenusRefs.Instance.elevateInput.text = kb_Settings.kb_Elevate;
        SettingsMenusRefs.Instance.descendInput.text = kb_Settings.kb_Descend;
        SettingsMenusRefs.Instance.gravityInput.text = kb_Settings.kb_Gravity;
        SettingsMenusRefs.Instance.rotateLeftInput.text = kb_Settings.kb_RotateLeft;
        SettingsMenusRefs.Instance.rotateRightInput.text = kb_Settings.kb_RotateRight;
        SettingsMenusRefs.Instance.rotateSensitivityInput.text = kb_Settings.kb_RotateKeySensitivity.ToString();
        SettingsMenusRefs.Instance.increaseSpeedInput.text = kb_Settings.kb_IncreaseSpeed;
        SettingsMenusRefs.Instance.decreaseSpeedInput.text = kb_Settings.kb_DecreaseSpeed;
        SettingsMenusRefs.Instance.toggleCameraInput.text = kb_Settings.kb_ToggleCamera;
    }

    /// <summary>
    /// This updates all of the kb_Settings values from the menu and then calls ApplyKeyBindings.
    /// </summary>
    public void UpdateKeyBindingsFromMenu()
    {
        if(SettingsMenusRefs.Instance.forwardInput.text != null && SettingsMenusRefs.Instance.forwardInput.text != "")
            kb_Settings.kb_Forward = SettingsMenusRefs.Instance.forwardInput.text;

        if (SettingsMenusRefs.Instance.backwardInput.text != null && SettingsMenusRefs.Instance.backwardInput.text != "")
            kb_Settings.kb_Backward = SettingsMenusRefs.Instance.backwardInput.text;

        if (SettingsMenusRefs.Instance.leftInput.text != null && SettingsMenusRefs.Instance.leftInput.text != "")
            kb_Settings.kb_Leftward = SettingsMenusRefs.Instance.leftInput.text;

        if (SettingsMenusRefs.Instance.rightInput.text != null && SettingsMenusRefs.Instance.rightInput.text != "")
            kb_Settings.kb_Rightward = SettingsMenusRefs.Instance.rightInput.text;

        if (SettingsMenusRefs.Instance.elevateInput.text != null && SettingsMenusRefs.Instance.elevateInput.text != "")
            kb_Settings.kb_Elevate = SettingsMenusRefs.Instance.elevateInput.text;

        if (SettingsMenusRefs.Instance.descendInput.text != null && SettingsMenusRefs.Instance.descendInput.text != "")
            kb_Settings.kb_Descend = SettingsMenusRefs.Instance.descendInput.text;

        if (SettingsMenusRefs.Instance.gravityInput.text != null && SettingsMenusRefs.Instance.gravityInput.text != "")
            kb_Settings.kb_Gravity = SettingsMenusRefs.Instance.gravityInput.text;

        if (SettingsMenusRefs.Instance.rotateLeftInput.text != null && SettingsMenusRefs.Instance.rotateLeftInput.text != "")
            kb_Settings.kb_RotateLeft = SettingsMenusRefs.Instance.rotateLeftInput.text;

        if (SettingsMenusRefs.Instance.rotateRightInput.text != null && SettingsMenusRefs.Instance.rotateRightInput.text != "")
            kb_Settings.kb_RotateRight = SettingsMenusRefs.Instance.rotateRightInput.text;

        if (SettingsMenusRefs.Instance.rotateSensitivityInput.text != null && SettingsMenusRefs.Instance.rotateSensitivityInput.text != "")
            kb_Settings.kb_RotateKeySensitivity = float.Parse(SettingsMenusRefs.Instance.rotateSensitivityInput.text);

        if (SettingsMenusRefs.Instance.increaseSpeedInput.text != null && SettingsMenusRefs.Instance.increaseSpeedInput.text != "")
            kb_Settings.kb_IncreaseSpeed = SettingsMenusRefs.Instance.increaseSpeedInput.text;

        if (SettingsMenusRefs.Instance.decreaseSpeedInput.text != null && SettingsMenusRefs.Instance.decreaseSpeedInput.text != "")
            kb_Settings.kb_DecreaseSpeed = SettingsMenusRefs.Instance.decreaseSpeedInput.text;

        if (SettingsMenusRefs.Instance.toggleCameraInput.text != null && SettingsMenusRefs.Instance.toggleCameraInput.text != "")
            kb_Settings.kb_ToggleCamera = SettingsMenusRefs.Instance.toggleCameraInput.text;

        ApplyKeyBindings();
    }

    /// <summary>
    /// This function saves the kb_Settings object to the Settings folder.
    /// </summary>
    public void SaveKeyBindings()
    {
        XmlIO.Save(kb_Settings, Application.dataPath + "/FullPackage/Settings/KeyBindings.sets");
    }

    /// <summary>
    /// Applies all of the WidgetControl settings to their respective objects/scripts.
    /// </summary>
    public void ApplyWidgetControlSettings()
    {
        bm_GameObject.GetComponent<RectTransform>().anchoredPosition = wc_Settings.bm_DefaultPosition;
        bm_GameObject.SetActive(wc_Settings.bm_Enabled);
        sl_GameObject.GetComponent<RectTransform>().anchoredPosition = wc_Settings.sl_DefaultPosition;
        sl_GameObject.SetActive(wc_Settings.sl_Enabled);
        mm_GameObject.GetComponent<RectTransform>().anchoredPosition = wc_Settings.mm_DefaultPosition;
        mm_GameObject.SetActive(wc_Settings.mm_Enabled);

        mm_Manager_Ref.mapProportionOfScreen = wc_Settings.mm_ScreenSize;
        mm_Manager_Ref.orthoCamRadiusFeet = wc_Settings.mm_ScopeRadius;
        mm_Manager_Ref.SetMiniMapCam();
    }

    /// <summary>
    /// This function fills in the WidgetControlMenu with all of the stored WidgetControl values.
    /// </summary>
    public void FillWidgetControlMenu()
    {
        SettingsMenusRefs.Instance.minimapToggle.isOn = wc_Settings.mm_Enabled;
        SettingsMenusRefs.Instance.bookmarkToggle.isOn = wc_Settings.bm_Enabled;
        SettingsMenusRefs.Instance.sunlightToggle.isOn = wc_Settings.sl_Enabled;
    }

    /// <summary>
    /// This updates all of the wc_Settings values from the menu and then calls ApplyWidgetControlSettings.
    /// </summary>
    public void UpdateWidgetControlSettingsFromMenu()
    {
        wc_Settings.mm_Enabled = SettingsMenusRefs.Instance.minimapToggle.isOn;
        wc_Settings.bm_Enabled = SettingsMenusRefs.Instance.bookmarkToggle.isOn;
        wc_Settings.sl_Enabled = SettingsMenusRefs.Instance.sunlightToggle.isOn;

        ApplyWidgetControlSettings();
    }

    /// <summary>
    /// This function saves the wc_Settings object to the Settings folder.
    /// </summary>
    public void SaveWidgetControlSettings()
    {
        XmlIO.Save(wc_Settings, Application.dataPath + "/FullPackage/Settings/WidgetControlSettings.sets");
    }

    /// <summary>
    /// This function can be called to set the screenSsize and scopeRadius values of the MiniMap stored in the wc_Settings object.
    /// The MiniMapManager script calls this function whenever it updates those values.
    /// </summary>
    /// <param name="screenSize"></param>
    /// <param name="scopeRadius"></param>
    public void SetMiniMapFields(float screenSize, float scopeRadius)
    {
        wc_Settings.mm_ScreenSize = screenSize;
        wc_Settings.mm_ScopeRadius = scopeRadius;

    }

    /// <summary>
    /// Whenever the application is closing, save the current settings.
    /// </summary>
    void OnApplicationQuit()
    {
        SaveAvatarSettings();
        SaveKeyBindings();
        SaveWidgetControlSettings();
    }
}

/// <summary>
/// Class to hold all of the settings for the avatar.
/// </summary>
public class AvatarSettings
{

    public bool a_Tracking = true;
    public bool a_GravColl = true;
    public float a_ForwardSpeed = 3;
    public float a_BackwardSpeed = 3;
    public float a_StrafeSpeed = 3;
    public float a_VerticalSpeed = 2;
    public float a_JumpSpeed = 6;

    public AvatarSettings()
    {
    }
}

/// <summary>
/// Class to hold general widget settings.
/// </summary>
public class WidgetControlSettings
{

    public bool bm_Enabled = true;
    public Vector2 bm_DefaultPosition = new Vector2(5, -5);
    public bool sl_Enabled = true;
    public Vector2 sl_DefaultPosition = new Vector2(155, -5);

    public bool mm_Enabled = true;
    public Vector2 mm_DefaultPosition = Vector2.zero;
    public float mm_ScreenSize = .2f;
    public float mm_ScopeRadius = 5f;

    public WidgetControlSettings()
    {
    }
}

/// <summary>
/// Class to hold keybindings.
/// </summary>
public class KeyBindings
{

    public string kb_Forward = "w";
    public string kb_Backward = "s";
    public string kb_Leftward = "q";
    public string kb_Rightward = "e";
    public string kb_Elevate = "up";
    public string kb_Descend = "down";
    public string kb_Gravity = "g";
    public string kb_RotateLeft = "a";
    public string kb_RotateRight = "d";
    public float kb_RotateKeySensitivity = .8f;
    public string kb_IncreaseSpeed = "=";
    public string kb_DecreaseSpeed = "-";
    public string kb_ToggleCamera = "c";

    public KeyBindings()
    {

    }
}