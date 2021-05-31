namespace TwoDimensionalArray
{
    public static class TwoDimensionalArrayExtension
    {
        const int rowsIndex = 0;
        const int columnsIndex = 1;

        public static int GetRowsCount(this bool[,] array)
        {
            return array.GetLength(rowsIndex);
        }

        public static int GetColumnsCount(this bool[,] array)
        {
            return array.GetLength(columnsIndex);
        }
    }
}
