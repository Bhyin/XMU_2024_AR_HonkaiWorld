using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Events;

public partial class GameLogic : MonoBehaviour
{
    public enum GameProcess
    {
        SetAnchor, // ���õ�ͼê��
        SetMap, // ���õ�ͼ���ź���ת
        Gaming, // ��Ϸ������
        Pausing, // ��Ϸ��ͣ��
        PlacingCharacter // ���ý�ɫ��
    }

    public enum CharacterEnum
    {
        A,
        B,
        C
    }

    GameProcess process;
    

    // ARϵͳ���߼��
    List<ARRaycastHit> arRaycastHits;
    RaycastHit hit;

    [Header("Camera")]
    public Camera mainCamera;

    [Header("Anchor")]
    public AnchorBehaviour anchorBehaviour;

    [Header("Map")]
    public MapBehaviour mapBehaviour;

    [Header("UI Manager")]
    public GameUIManager uiManager;

    [Header("Interaction Manager")]
    public InteractionManager interactionManager;

    [Header("AR Manager")]
    public ARPlaneManager arPlaneManager;
    public ARRaycastManager aRRaycastManager;

    [Header("Character")]
    public GameObject characterPrefab;

    public GameObject activeChararacter;

    [Header("Enemy")]
    public GameObject emenyPrefab;
    public List<BaseEnemyBehaviour> enemies;

    public delegate void EventCallback();

    EventCallback activeOnPressCallback;
    EventCallback activeOnLongPressCallback;
    EventCallback activeOnClickCallback;
    EventCallback activeOnReleaseCallback;
    EventCallback activeUpdateCallback;

    Dictionary<GameProcess, EventCallback> onPressCallbacks;
    Dictionary<GameProcess, EventCallback> onLongPressCallbacks;
    Dictionary<GameProcess, EventCallback> onClickCallbacks;
    Dictionary<GameProcess, EventCallback> onReleaseCallbacks;
    Dictionary<GameProcess, EventCallback> updateCallbacks;

    void RegisterInteractionEventCallbacks()
    {
        // Press
        onPressCallbacks = new Dictionary<GameProcess, EventCallback>();
        //onPressCallbacks.Add(GameProcess.Gaming, GamingPressCallback);
        onPressCallbacks.Add(GameProcess.PlacingCharacter, PlacingCharPressCallback);

        // Long Press
        onLongPressCallbacks = new Dictionary<GameProcess, EventCallback>();
        onLongPressCallbacks.Add(GameProcess.SetAnchor, SetAnchorLongPressCallback);
        onLongPressCallbacks.Add(GameProcess.Gaming, GamingLongPressCallback);

        // Click
        onClickCallbacks = new Dictionary<GameProcess, EventCallback>();
        onClickCallbacks.Add(GameProcess.SetAnchor, SetAnchorClickCallback);
        //onClickCallbacks.Add(GameProcess.PlacingCharacter, PlacingCharClickCallback);

        // Release
        onReleaseCallbacks = new Dictionary<GameProcess, EventCallback>();
        onReleaseCallbacks.Add(GameProcess.Gaming, GamingReleaseCallback);
    }

    void RegisterUpdateCallbacks()
    {
        updateCallbacks = new Dictionary<GameProcess, EventCallback>();
        updateCallbacks.Add(GameProcess.SetAnchor, SetAnchorUpdate);
        updateCallbacks.Add(GameProcess.SetMap, SetMapUpdate);
        updateCallbacks.Add(GameProcess.Gaming, GameUpdate);
        updateCallbacks.Add(GameProcess.PlacingCharacter, PlacingCharUpdate);
    }

    private void Awake()
    {

        uiManager.SetGameLogic(this);

        process = GameProcess.SetAnchor;

        enemies = new List<BaseEnemyBehaviour>();
        arRaycastHits = new List<ARRaycastHit>();

        mapBehaviour.SetAnchor(anchorBehaviour);

        RegisterInteractionEventCallbacks();
        RegisterUpdateCallbacks();
        SetInteractionListeners();
    }

    public void SetInteractionListeners()
    {
        interactionManager.pressEvent += OnPressCallback;
        interactionManager.longPressEvent += OnLongPressCallback;
        interactionManager.clickEvent += OnClickCallback;
        interactionManager.releaseEvent += OnReleaseCallback;
    }

    public void OnPressCallback()
    {
        if (onPressCallbacks.TryGetValue(process, out activeOnPressCallback))
            activeOnPressCallback();
    }

    public void OnClickCallback()
    {
        if (onClickCallbacks.TryGetValue(process, out activeOnClickCallback))
            activeOnClickCallback();
    }

    public void OnLongPressCallback()
    {
        if (onLongPressCallbacks.TryGetValue(process, out activeOnLongPressCallback))
            activeOnLongPressCallback.Invoke();
    }

    public void OnReleaseCallback()
    {
        if (onReleaseCallbacks.TryGetValue(process, out activeOnReleaseCallback))
            activeOnReleaseCallback();
    }
}
