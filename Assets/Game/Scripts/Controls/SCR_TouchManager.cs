using UnityEngine;
using UnityEngine.InputSystem;

public class SCR_TouchManager : MonoBehaviour
{
    [SerializeField] SCR_MatchFinder m_finder;
    private IA_TouchControls _touchControls;
    Camera _cam;
    Vector2 _startTouchPos;
    Vector2 _endTouchPos;
    private void Awake()
    {
        _touchControls = new IA_TouchControls();
    }
    private void Start()
    {
        _cam = Camera.main;
        _touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        _touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }
    private void OnEnable()
    {
        _touchControls.Enable();
    }
    private void OnDisable()
    {
        _touchControls.Disable();
    }

    void StartTouch(InputAction.CallbackContext context)
    {
        _startTouchPos = _touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        Vector3 pos = new Vector3(_startTouchPos.x, _startTouchPos.y, _cam.nearClipPlane);
        Ray ray = _cam.ScreenPointToRay(pos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (SCR_GameState.IsGameStateMove())
            {
                SCR_TileBehaviour tile = hit.transform.GetComponent<SCR_TileBehaviour>();
                if (tile != null) 
                {
                    tile.SwitchSide();
                    SCR_GameState.SetCurrentGameState(GameState.wait);
                    SCR_GameStatus.DecreaseMovement(); 
                }
            }
        }
    }
    void EndTouch(InputAction.CallbackContext context)
    {
        _endTouchPos = _touchControls.Touch.TouchPosition.ReadValue<Vector2>();
    }
}
