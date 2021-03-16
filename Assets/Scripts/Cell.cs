using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private List<Cell> _neighbors;
    public bool IsAlive { get; set; }
    private bool IsAliveNext { get; set; }

    private void Awake()
    {
        _neighbors = new List<Cell>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        IsAlive = false;
        _spriteRenderer.enabled = IsAlive;
    }

    public void NextLiveState(Rule rule)
    {
        int aliveCount = 0;
        foreach(Cell cell in _neighbors)
        {
            if (cell.IsAlive)
            {
                aliveCount++;
            }
        }
        if (IsAlive)
        {
            switch (rule) //Survive
            {
                case Rule.B3S23:
                    IsAliveNext = aliveCount == 2 || aliveCount == 3;
                    break;
                case Rule.B3S012345678:
                    IsAliveNext = aliveCount >= 0;
                    break;
                case Rule.B5678S45678:
                    IsAliveNext = aliveCount >= 4;
                    break;
                case Rule.B3678S34678:
                    IsAliveNext = aliveCount == 3 || aliveCount == 4 || aliveCount >= 6;
                    break;
                case Rule.B36S23:
                    IsAliveNext = aliveCount == 2 || aliveCount == 3;
                    break;
                case Rule.B2S:
                    IsAliveNext = false;
                    break;
            }
        }
        else
        {
            switch (rule) //Birth
            {
                case Rule.B3S23:
                    IsAliveNext = aliveCount == 3;
                    break;
                case Rule.B3S012345678:
                    IsAliveNext = aliveCount == 3;
                    break;
                case Rule.B5678S45678:
                    IsAliveNext = aliveCount >= 5;
                    break;
                case Rule.B3678S34678:
                    IsAliveNext = aliveCount == 3 || aliveCount >= 6;
                    break;
                case Rule.B36S23:
                    IsAliveNext = aliveCount == 3 || aliveCount == 6;
                    break;
                case Rule.B2S:
                    IsAliveNext = aliveCount == 2;
                    break;
            }
        }

    }

    public void AddNeighbor(Cell cell)
    {
        _neighbors.Add(cell);
    }

    public List<Cell> GetNeighbors()
    {
        return _neighbors;
    }

    public void CheckAlive()
    {
        IsAlive = IsAliveNext;
        _spriteRenderer.enabled = IsAlive;
    }

    public void SetAlive(bool value)
    {
        IsAlive = value;
        _spriteRenderer.enabled = value;
    }
}
