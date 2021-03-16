using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Board : MonoBehaviour
{
    [SerializeField] private Vector2Int _boardSize;
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private Camera _camera;

    public static Board Instance;
    private Cell[,] _cells;
    private UserInput _inputActions;
    private Rule _curentRule;
    private bool _isProcessing;
    private Vector2 _delta;
    private Vector2 _leftBottomPoint;

    public Vector2Int BoardSize { get { return _boardSize; }}
    public Vector2 LeftBottomPoint { get { return _leftBottomPoint; } }
    bool _hold;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        _isProcessing = false;
        _inputActions = new UserInput();
        _inputActions.Board.CellAction.performed += ctx => OnCellAction(ctx); 
        _inputActions.Board.CellAction.canceled += ctx => OnCellAction(ctx);
        _camera.GetComponent<CameraZoom>().InitInput(_inputActions);
    }

    void Start()
    {
        _cells = new Cell[_boardSize.x - 1, _boardSize.y - 1];
        Rect _spriteRect = _cellPrefab.GetComponent<SpriteRenderer>().sprite.rect;
        _leftBottomPoint = new Vector2(-Screen.width / (_spriteRect.width * 2), -Screen.height / (_spriteRect.height * 2));
        _delta = _leftBottomPoint + (_boardSize / 2);
        for (int x = 0; x < _cells.GetLength(0); x++)
        {
            for (int y = 0; y < _cells.GetLength(1); y++)
            {
                Cell cell = Instantiate(_cellPrefab, _leftBottomPoint + Vector2.one + new Vector2(x, y), Quaternion.identity);
                _cells[x, y] = cell;
            }
        }
        GetNeighbors();
    }


    public void OnCellAction(InputAction.CallbackContext ctx)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        bool value = ctx.ReadValue<float>() > 0 ? true : false; 
        if (ctx.performed)
        {
            _hold = true;
            StartCoroutine(Action(value));
        }
        else if (ctx.canceled)
        {
            _hold = false;
            StopCoroutine("Action");
        }
    }

    private IEnumerator Action(bool value)
    {
        while (_hold)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            int x = Mathf.RoundToInt(mousePosition.x + _cells.GetLength(0) / 2 + Mathf.Abs(_delta.x));
            int y = Mathf.RoundToInt(mousePosition.y + _cells.GetLength(1) / 2 + Mathf.Abs(_delta.y));
            if (x >= 0 && x < _cells.GetLength(0) && y >= 0 && y < _cells.GetLength(1))
            {
                _cells[x, y].SetAlive(value);
            }
            yield return null;
        }
    }

    private void GetNeighbors()
    {
        for (int x = 0; x < _cells.GetLength(0); x++)
        {
            for (int y = 0; y < _cells.GetLength(1); y++)
            {
                int xL = (x > 0) ? x - 1 : _cells.GetLength(0) - 1;
                int xR = (x < _cells.GetLength(0) - 1) ? x + 1 : 0;

                int yT = (y < _cells.GetLength(1) - 1) ? y + 1 : 0;
                int yB = (y > 0) ? y - 1 : _cells.GetLength(1) - 1;
                _cells[x, y].AddNeighbor(_cells[xL, y]);
                _cells[x, y].AddNeighbor(_cells[xR, y]);
                _cells[x, y].AddNeighbor(_cells[x, yT]);
                _cells[x, y].AddNeighbor(_cells[x, yB]);
                _cells[x, y].AddNeighbor(_cells[xL, yT]);
                _cells[x, y].AddNeighbor(_cells[xL, yB]);
                _cells[x, y].AddNeighbor(_cells[xR, yT]);
                _cells[x, y].AddNeighbor(_cells[xR, yB]);
            }
        }
    }

    public void StartSimulation()
    {
        if (!_isProcessing)
        {
            _isProcessing = true;
            StartCoroutine("SimulationCoroutine");
        }
    }
    public void StopSimulation()
    {
        if (_isProcessing)
        {
            _isProcessing = false;
            StopCoroutine("SimulationCoroutine");
        }
    }

    private IEnumerator SimulationCoroutine()
    {
        while (_isProcessing)
        {
            yield return new WaitForSeconds(0.1f);
            foreach (Cell cell in _cells)
            {
                cell.NextLiveState(_curentRule);
            }

            foreach (Cell cell in _cells)
            {
                cell.CheckAlive();
            }

        }
    }

    public void ResetCells()
    {
        foreach (Cell cell in _cells)
        {
            cell.SetAlive(false);
        }
    }

    public void Randomize(int liveDensity)
    {
        foreach (Cell cell in _cells)
        {
            cell.SetAlive(liveDensity > Random.Range(0, 100));
        }
    }

    public void SetRule(string ruleString)
    {
        switch (ruleString)
        {
            case nameof(Rule.B3S23):
                _curentRule = Rule.B3S23;
                break;
            case nameof(Rule.B3S012345678):
                _curentRule = Rule.B3S012345678;
                break;
            case nameof(Rule.B5678S45678):
                _curentRule = Rule.B5678S45678;
                break;
            case nameof(Rule.B3678S34678):
                _curentRule = Rule.B3678S34678;
                break;
            case nameof(Rule.B36S23):
                _curentRule = Rule.B36S23;
                break;
            case nameof(Rule.B2S):
                _curentRule = Rule.B2S;
                break;
        }
    }

    private void OnEnable()
    {
        _inputActions.Board.Enable();
        _inputActions.Camera.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Board.Disable();
        _inputActions.Camera.Disable();
    }
}
