namespace TwoDimensionalArray
{
    public static class TwoDimensionalArrayExtension
    {
        const int rowsIndex = 0;
        const int columnsIndex = 1;

        public static int GetRowsCount<T>(this T[,] array)
        {
            return array.GetLength(rowsIndex);
        }

        public static int GetColumnsCount<T>(this T[,] array)
        {
            return array.GetLength(columnsIndex);
        }
    }
}
