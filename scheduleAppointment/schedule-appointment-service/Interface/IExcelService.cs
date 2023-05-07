using ClosedXML.Excel;


public interface IExcelService
{
    IEnumerable<IXLWorksheet> Initialize(params string[] sheetNames);
    byte[] Generate();
}