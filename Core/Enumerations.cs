using System;

namespace Core.Utilities.Enumerations
{
    public enum OperationType
    {
        Add,
        Edit,
        Delete,
        Read,
        Order
    }

    public enum DataFormat
    {
        XML,
        JSON
    }

    public enum GridExportTypes
    {
        CSV,
        XLSX,
        PDF
    }
}
