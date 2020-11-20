namespace MySQLBackupNetCore.EventArgs
{
    public class GetTotalRowsArgs : System.EventArgs
    {
        int _totalTables = 0;
        int _curTable = 0;

        public GetTotalRowsArgs(int totalTables, int curTable)
        {
            _totalTables = totalTables;
            _curTable = curTable;
        }
    }
}
