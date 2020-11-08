using System;
using System.Collections.Generic;
using UnityEngine;
using ChessEngine;
using System.Linq;

public class Board : MonoBehaviour
{

    private Dictionary<string, GameObject> cells;
    private Dictionary<string, GameObject> figures;
    private Dictionary<string, GameObject> transformations;
    private Chess chess;
    private DragAndDrop dd;
    private string onTransformationMove;

    public Board()
    {
        dd = new DragAndDrop(PickObject, DropObject);
        cells = new Dictionary<string, GameObject>();
        figures = new Dictionary<string, GameObject>();
        transformations = new Dictionary<string, GameObject>();
        chess = new Chess();
        onTransformationMove = string.Empty;
    }


    // Start is called before the first frame update
    void Start()
    {
        InitGameObjects();
        ShowFigures();
        MarkCellsFrom();
        ShowTransformationFigures();
    }

    private void ShowFigures()
    {
        for (int y = 0; y < ChessEngine.Constants.COUNT_SQUARES; y++)
            for (int x = 0; x < ChessEngine.Constants.COUNT_SQUARES; x++)
            {
                string key = "" + x + y;
                string figure = chess.GetFigureFromPosition(x, y).ToString();
                figures[key].transform.position = cells[key].transform.position;
                if (figures[key].name == figure)
                    continue;
                figures[key].GetComponent<SpriteRenderer>().sprite =
                    GameObject.Find(figure).GetComponent<SpriteRenderer>().sprite;
                if (figure != ".")
                    figures[key].GetComponent<SpriteRenderer>().sortingOrder = 1;
                else figures[key].GetComponent<SpriteRenderer>().sortingOrder = -2;
                figures[key].name = figure;
            }
    }

    private void InitGameObjects()
    {
        for (int y = 0; y < ChessEngine.Constants.COUNT_SQUARES; y++)
            for (int x = 0; x < ChessEngine.Constants.COUNT_SQUARES; x++)
            {
                string key = "" + x + y;
                string nameCell = (x + y) % 2 == 0 ? "BlackCell" : "WhiteCell";
                cells[key] = CreateGameObject(nameCell, x, y);
                figures[key] = CreateGameObject("P", x, y);
            }
        //magic. sorry
        transformations["Q"] = CreateGameObject("Q", Constants.COUNT_SQUARES / 2 + 1, Constants.COUNT_SQUARES + 6);
        transformations["R"] = CreateGameObject("R", Constants.COUNT_SQUARES / 2 + 2, Constants.COUNT_SQUARES + 6);
        transformations["B"] = CreateGameObject("B", Constants.COUNT_SQUARES / 2 + 3, Constants.COUNT_SQUARES + 6);
        transformations["N"] = CreateGameObject("N", Constants.COUNT_SQUARES / 2 + 4, Constants.COUNT_SQUARES + 6);
        transformations["q"] = CreateGameObject("q", Constants.COUNT_SQUARES / 2 + 1, -2);
        transformations["r"] = CreateGameObject("r", Constants.COUNT_SQUARES / 2 + 2, -2);
        transformations["b"] = CreateGameObject("b", Constants.COUNT_SQUARES / 2 + 3, -2);
        transformations["n"] = CreateGameObject("n", Constants.COUNT_SQUARES / 2 + 4, -2);
    }

    GameObject CreateGameObject(string pattern, int x, int y)
    {
        GameObject go = Instantiate(GameObject.Find(pattern));
        go.transform.position = new Vector2(x * go.transform.lossyScale.x * 2, y * go.transform.lossyScale.y * 2);
        go.name = pattern;
        return go;
    }
    // Update is called once per frame
    void Update()
    {
        dd.ActionHandler();
    }

    void ShowTransformationFigures(string pawn = "")
    {
        foreach (GameObject t in transformations.Values)
            SetSprite(t, ".");
        if (pawn == "P")
        {
            SetSprite(transformations["Q"], "Q");
            SetSprite(transformations["R"], "R");
            SetSprite(transformations["B"], "B");
            SetSprite(transformations["N"], "N");
        }
        else if (pawn == "p")
        {
            SetSprite(transformations["q"], "q");
            SetSprite(transformations["r"], "r");
            SetSprite(transformations["b"], "b");
            SetSprite(transformations["n"], "n");
        }
    }

    private void SetSprite(GameObject gameObj, string obj)
    {
        gameObj.GetComponent<SpriteRenderer>().sprite = GameObject.Find(obj).GetComponent<SpriteRenderer>().sprite;
    }

    void DropObject(Vector2 fromPosition, Vector2 toPosition)
    {
        string from = TransformVectorToCell(fromPosition);
        string to = TransformVectorToCell(toPosition);
        string figure = chess.GetFigureFromPosition(from).ToString();
        string move = figure + from + to;

        if (move.Length != 5)
            return;

        if (figure == "P" && to[1] == Convert.ToChar(Constants.COUNT_SQUARES) ||
        figure == "p" && to[1] == '1')
            if (chess.Move(move) != chess)
            {
                onTransformationMove = move;
                ShowTransformationFigures(figure);
                return;
            }
        chess = chess.Move(move);
        ShowFigures();
        MarkCellsFrom();
    }

    void PickObject(Vector2 fromPosition)
    {
        if (onTransformationMove != string.Empty)
        {
            int x = Convert.ToInt32(fromPosition.x / (cells.Values.ElementAt(0).transform.lossyScale.x * 2));
            if (onTransformationMove[0] == 'P')
            {
                if (x == 2) onTransformationMove += "Q";
                if (x == 3) onTransformationMove += "R";
                if (x == 4) onTransformationMove += "B";
                if (x == 5) onTransformationMove += "N";
            } else if (onTransformationMove[0] == 'p')
            {
                if (x == 2) onTransformationMove += "q";
                if (x == 3) onTransformationMove += "r";
                if (x == 4) onTransformationMove += "b";
                if (x == 5) onTransformationMove += "n";
            }
            chess = chess.Move(onTransformationMove);
            onTransformationMove = string.Empty;
            ShowTransformationFigures();
            return;
        }

        MarkCellsTo(TransformVectorToCell(fromPosition));
    }


    string TransformVectorToCell(Vector2 vector)
    {
        int x = Convert.ToInt32(vector.x / (cells.Values.ElementAt(0).transform.lossyScale.x * 2));
        int y = Convert.ToInt32(vector.y / (cells.Values.ElementAt(0).transform.lossyScale.y * 2));
        if (x >= 0 && x < Constants.COUNT_SQUARES && y >= 0 && y < Constants.COUNT_SQUARES)
            return ((char)('a' + x)).ToString() + (y + 1).ToString();
        return "";
    }

    void ShowCell(int x, int y, bool marked, bool validMoves = false)
    {
        string cell = (x + y) % 2 == 0 ? "BlackCell" : "WhiteCell";
        if (validMoves)
        {
            if (chess.GetFigureFromPosition(x, y) == '.')
                cells["" + x + y].GetComponent<SpriteRenderer>().sprite =
                        GameObject.Find("" + cell + "ValidMove").GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            if (marked) cell += "Marked";
            cells["" + x + y].GetComponent<SpriteRenderer>().sprite =
                          GameObject.Find(cell).GetComponent<SpriteRenderer>().sprite;
        }
    }

    void MarkCellsTo(string from)
    {
        UnMarkCells();
        foreach (string move in chess.YieldValidMoves())
            if (from == move.Substring(1, 2))
                ShowCell(move[3] - 'a', move[4] - '1', true, true);
    }

    void MarkCellsFrom()
    {
        UnMarkCells();

        foreach (string move in chess.YieldValidMoves())
            ShowCell(move[1] - 'a', move[2] - '1', true);
    }

    void UnMarkCells()
    {
        for (int y = 0; y < ChessEngine.Constants.COUNT_SQUARES; y++)
            for (int x = 0; x < ChessEngine.Constants.COUNT_SQUARES; x++)
            {
                ShowCell(x, y, false);
            }
    }
}
