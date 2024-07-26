namespace CohortAnalysisChart
{

    public enum CellType
    {
        /// <summary>
        /// On very top row (Header) with dark blue background.
        /// </summary>
        RowHeader,
        /// <summary>
        /// Diagonal header with dark blue background.
        /// </summary>
        ColumnHeader,
        /// <summary>
        /// Value parth of matrix.
        /// </summary>
        MatrixValue,
        /// <summary>
        /// Top left parth of first row.
        /// </summary>
        Timer,
        /// <summary>
        /// first two column of matrix(right after time row). 
        /// </summary>
        FixingMonth,
        FixingMatrixValue,
        /// <summary>
        /// This cell is contains just a gray color.
        /// </summary>
        GrayBackGround
    }

}
