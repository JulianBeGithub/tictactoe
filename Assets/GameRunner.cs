using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameRunner : MonoBehaviour
{
    [SerializeField]
    public GameObject X, O, playerWins, computerWins, tie;
    int[] spaces = new int[9];
    List<GameObject> moves = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int FindBestMove()
    {
        int bestMove = -1;
        int bestScore = int.MaxValue;
        for (int i = 0; i < 9; i++)
        {
            if (spaces[i] == 0)
            {
                spaces[i] = 2;
                int score = Minimax(0, true, int.MinValue, int.MaxValue, 1);
                spaces[i] = 0;
                if (score < bestScore)
                {
                    bestScore = score;
                    bestMove = i;
                }
            }
        }
        //Debug.Log(bestMove);
        return bestMove;
    }

    private int Minimax(int depth, bool isMaximizing, int alpha, int beta, int currentPlayer)
    {
        //Debug.Log(depth);
        if (win(1)) {
            return 1;
        }
        if (win(2)) {
            return -1;
        }
        if (boardFull()) {
            return 0;
        }

        if (isMaximizing)
        {
            int maxEval = int.MinValue;

            for (int i = 0; i < 9; i++)
            {
                if (spaces[i] == 0)
                {
                    spaces[i] = 1;
                    int eval = Minimax(depth + 1, false, alpha, beta, 2);
                    spaces[i] = 0;
                    maxEval = Mathf.Max(maxEval, eval);
                    alpha = Mathf.Max(alpha, eval);
                    if (beta <= alpha)
                        break;
                }
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            for (int i = 0; i < 9; i++)
            {
                if (spaces[i] == 0)
                {
                    spaces[i] = 2;
                    int eval = Minimax(depth + 1, true, alpha, beta, 1);
                    spaces[i] = 0;

                    minEval = Mathf.Min(minEval, eval);
                    beta = Mathf.Min(beta, eval);

                    if (beta <= alpha)
                        break;
                }
            }

            return minEval;
        }
    }

    public void opponentTurn() {
        if (boardFull()) {
            moves.Add(Instantiate(tie, new Vector3(0, 0, 0), Quaternion.identity));
            Debug.Log("hi there");
            return;
        }
                int number = FindBestMove();
                spaces[number] = 2;
                switch (number) {
                    case 0:
                        moves.Add(Instantiate(O, new Vector3(-4, 3.5f, 0), Quaternion.identity));
                        break;
                    case 1:
                        moves.Add(Instantiate(O, new Vector3(-0.5f, 3.5f, 0), Quaternion.identity));
                        break;
                    case 2:
                        moves.Add(Instantiate(O, new Vector3(2.5f, 3.5f, 0), Quaternion.identity));
                        break;
                    case 3:
                        moves.Add(Instantiate(O, new Vector3(-4, 0.5f, 0), Quaternion.identity));
                        break;
                    case 4:
                        moves.Add(Instantiate(O, new Vector3(-0.5f, 0.5f, 0), Quaternion.identity));
                        break;
                    case 5:
                        moves.Add(Instantiate(O, new Vector3(2.5f, 0.5f, 0), Quaternion.identity));
                        break;
                    case 6:
                        moves.Add(Instantiate(O, new Vector3(-4, -3, 0), Quaternion.identity));
                        break;
                    case 7:
                        moves.Add(Instantiate(O, new Vector3(-0.5f, -3, 0), Quaternion.identity));
                        break;
                    case 8:
                        moves.Add(Instantiate(O, new Vector3(2.5f, -3, 0), Quaternion.identity));
                        break;
                }
        if (win(2)) {
            moves.Add(Instantiate(computerWins, new Vector3(409, 197, 0), Quaternion.identity));
            spaces = new int[] {2, 2, 2, 2, 2, 2, 2, 2, 2};
        } else if (boardFull()) {
            moves.Add(Instantiate(tie, new Vector3(30, 3, 0), Quaternion.identity));
            Debug.Log("hi there");
        }
    }

    public bool win(int num) {
        return (spaces[0] == num && spaces[1] == num && spaces[2] == num) || 
        (spaces[3] == num && spaces[4] == num && spaces[5] == num) ||
        (spaces[6] == num && spaces[7] == num && spaces[8] == num) ||
        (spaces[0] == num && spaces[3] == num && spaces[6] == num) ||
        (spaces[1] == num && spaces[4] == num && spaces[7] == num) ||
        (spaces[2] == num && spaces[5] == num && spaces[8] == num) ||
        (spaces[0] == num && spaces[4] == num && spaces[8] == num) ||
        (spaces[2] == num && spaces[4] == num && spaces[6] == num);
    }

    public bool boardFull() {
        foreach (int n in spaces) {
            if (n == 0) {
                return false;
            }
        }
        return true;
    }

    public void OnRestartP1Click() {
        foreach (GameObject m in moves) {
            Destroy(m);
        }
        spaces = new int[9];
    }

    public void OnRestartP2Click() {
        foreach (GameObject m in moves) {
            Destroy(m);
        }
        spaces = new int[9];
        opponentTurn();
    }

    public void OnButton1Click()
    {
        if (spaces[0] == 0) 
        {
            moves.Add(Instantiate(X, new Vector3(0.5f, 4, 0), Quaternion.identity));
            spaces[0] = 1;
            if (!win(1)) {
                opponentTurn();
            } else {
                moves.Add(Instantiate(playerWins, new Vector3(408, 197, 0), Quaternion.identity));
                spaces = new int[] {2, 2, 2, 2, 2, 2, 2, 2, 2};
            }
        }
    }

    public void OnButton2Click()
    {
        if (spaces[1] == 0) 
        {
            moves.Add(Instantiate(X, new Vector3(4, 4, 0), Quaternion.identity));
            spaces[1] = 1;
            if (!win(1)) {
                opponentTurn();
            } else {
                moves.Add(Instantiate(playerWins, new Vector3(408, 197, 0), Quaternion.identity));
                spaces = new int[] {2, 2, 2, 2, 2, 2, 2, 2, 2};
            }
        }
    }

    public void OnButton3Click()
    {
        if (spaces[2] == 0) 
        {
            moves.Add(Instantiate(X, new Vector3(7, 4, 0), Quaternion.identity));
            spaces[2] = 1;
            if (!win(1)) {
                opponentTurn();
            } else {
                moves.Add(Instantiate(playerWins, new Vector3(408, 197, 0), Quaternion.identity));
                spaces = new int[] {2, 2, 2, 2, 2, 2, 2, 2, 2};
            }
        }
    }

    public void OnButton4Click()
    {
        if (spaces[3] == 0) 
        {
            moves.Add(Instantiate(X, new Vector3(0.5f, 1, 0), Quaternion.identity));
            spaces[3] = 1;
            if (!win(1)) {
                opponentTurn();
            } else {
                moves.Add(Instantiate(playerWins, new Vector3(408, 197, 0), Quaternion.identity));
                spaces = new int[] {2, 2, 2, 2, 2, 2, 2, 2, 2};
            }
        }
    }

    public void OnButton5Click()
    {
        if (spaces[4] == 0) 
        {
            moves.Add(Instantiate(X, new Vector3(4, 1, 0), Quaternion.identity));
            spaces[4] = 1;
            if (!win(1)) {
                opponentTurn();
            } else {
                moves.Add(Instantiate(playerWins, new Vector3(408, 197, 0), Quaternion.identity));
                spaces = new int[] {2, 2, 2, 2, 2, 2, 2, 2, 2};
            }
        }
    }

    public void OnButton6Click()
    {
        if (spaces[5] == 0) 
        {
            moves.Add(Instantiate(X, new Vector3(7, 1, 0), Quaternion.identity));
            spaces[5] = 1;
            if (!win(1)) {
                opponentTurn();
            } else {
                moves.Add(Instantiate(playerWins, new Vector3(408, 197, 0), Quaternion.identity));
                spaces = new int[] {2, 2, 2, 2, 2, 2, 2, 2, 2};
            }
        }
    }

    public void OnButton7Click()
    {
        if (spaces[6] == 0) 
        {
            moves.Add(Instantiate(X, new Vector3(0.5f, -2.5f, 0), Quaternion.identity));
            spaces[6] = 1;
            if (!win(1)) {
                opponentTurn();
            } else {
                moves.Add(Instantiate(playerWins, new Vector3(408, 197, 0), Quaternion.identity));
                spaces = new int[] {2, 2, 2, 2, 2, 2, 2, 2, 2};
            }
        }
    }

    public void OnButton8Click()
    {
        if (spaces[7] == 0) 
        {
            moves.Add(Instantiate(X, new Vector3(4, -2.5f, 0), Quaternion.identity));
            spaces[7] = 1;
            if (!win(1)) {
                opponentTurn();
            } else {
                moves.Add(Instantiate(playerWins, new Vector3(408, 197, 0), Quaternion.identity));
                spaces = new int[] {2, 2, 2, 2, 2, 2, 2, 2, 2};
            }
        }
    }

    public void OnButton9Click()
    {
        if (spaces[8] == 0) 
        {
            moves.Add(Instantiate(X, new Vector3(7, -2.5f, 0), Quaternion.identity));
            spaces[8] = 1;
            if (!win(1)) {
                opponentTurn();
            } else {
                moves.Add(Instantiate(playerWins, new Vector3(408, 197, 0), Quaternion.identity));
                spaces = new int[] {2, 2, 2, 2, 2, 2, 2, 2, 2};
            }
        }
    }
}
