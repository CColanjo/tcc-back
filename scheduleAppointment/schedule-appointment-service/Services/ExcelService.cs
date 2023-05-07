using ClosedXML.Excel;

public class ExcelService : IExcelService
{
    private IXLWorkbook _workbook = default!;
    
    public IEnumerable<IXLWorksheet> Initialize(params string[] sheetNames)
    {
        _workbook = new XLWorkbook();
        
        return sheetNames.Select(sheetName => _workbook.Worksheets.Add(sheetName));
    }

    public byte[] Generate()
    {
        using var stream = new MemoryStream();

        _workbook.SaveAs(stream);

        _workbook.Dispose();

        return stream.ToArray();
    }
}