using System;
namespace ChessEngine
{
    struct Cell
    {
        public static Cell none = new Cell(-1, -1);

        public int x { get; private set; }
        public int y { get; private set; }

        public Cell(string nameCell)
        {
            //check coordinate for validity
            if (nameCell.Length == 2 &&
            nameCell[0] >= 'a' && nameCell[0] <= 'h' &&
                nameCell[1] >= '1' && nameCell[1] <= '8')
            {
                x = nameCell[0] - 'a';
                y = nameCell[1] - '1';
            }
            else this = none;
        }

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public string Name
        {
            get
            {
                return ((char)('a' + x)).ToString() + (y + 1).ToString();
            }
        }

        public bool CheckOnBoard()
        {
            return (x >= 0 && x < Constants.COUNT_SQUARES) && (y >= 0 && y < Constants.COUNT_SQUARES);
        }
    }
}
