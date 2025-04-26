using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Eshop.Share.Helpers.Utilities.Utilities.Providers
{
    public class ExcelHelper
    {
        public async Task<DataTable> ConvertExcelToDataTable(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return new DataTable();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    try
                    {
                        var worksheet = workbook.Worksheet(1);
                        var dataTable = new DataTable();
                        bool firstRow = true;
                        foreach (var row in worksheet.RowsUsed())
                        {
                            if (firstRow)
                            {
                                foreach (var cell in row.Cells())
                                {
                                    dataTable.Columns.Add(cell.Value.ToString());
                                }
                                firstRow = false;
                            }
                            else
                            {
                                var newRow = dataTable.NewRow();
                                int i = 0;
                                foreach (var cell in row.Cells())
                                {
                                    newRow[i] = cell.Value.ToString();
                                    i++;
                                }
                                dataTable.Rows.Add(newRow);
                            }
                        }
                        return dataTable;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public List<T> ConvertExcelToModelList<T>(XLWorkbook workbook) where T : new()
        {
            try
            {
                var worksheet = workbook.Worksheet(1);
                var modelList = new List<T>();
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                var headers = worksheet.FirstRowUsed().Cells().Select(cell => cell.Value.ToString()).ToList();

                foreach (var row in worksheet.RowsUsed().Skip(1)) // Skip header row
                {
                    var model = new T();
                    for (int i = 0; i < headers.Count; i++)
                    {
                        var property = properties.FirstOrDefault(p => p.Name.Equals(headers[i], StringComparison.OrdinalIgnoreCase));
                        if (property != null && row.Cell(i + 1) != null)
                        {
                            var cellValue = row.Cell(i + 1).Value;
                            object value = null;

                            try
                            {
                                var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                                switch (Type.GetTypeCode(targetType))
                                {
                                    case TypeCode.Int32:
                                        value = Convert.ToInt32(cellValue);
                                        break;
                                    case TypeCode.Decimal:
                                        value = Convert.ToDecimal(cellValue);
                                        break;
                                    case TypeCode.Double:
                                        value = Convert.ToDouble(cellValue);
                                        break;
                                    case TypeCode.Single:
                                        value = float.Parse(cellValue.ToString());
                                        break;
                                    case TypeCode.DateTime:
                                        value = Convert.ToDateTime(cellValue);
                                        break;
                                    case TypeCode.Boolean:
                                        value = Convert.ToBoolean(cellValue);
                                        break;
                                    case TypeCode.String:
                                        value = cellValue.ToString();
                                        break;
                                    default:
                                        if (targetType.IsEnum)
                                        {
                                            value = System.Enum.Parse(targetType, cellValue.ToString());
                                        }
                                        else
                                        {
                                            value = Convert.ChangeType(cellValue, targetType);
                                        }
                                        break;
                                }
                            }
                            catch (Exception)
                            {
                                value = null;
                            }

                            property.SetValue(model, value);
                        }
                    }
                    modelList.Add(model);
                }

                return modelList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error processing Excel file.", ex);
            }
        }


        public void ExportToExcel<T>(List<T> data, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                try
                {
                    var worksheet = workbook.Worksheets.Add("Sheet1");

                    var properties = typeof(T).GetProperties();
                    for (int i = 0; i < properties.Length; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = properties[i].Name;
                    }

                    for (int i = 0; i < data.Count; i++)
                    {
                        for (int j = 0; j < properties.Length; j++)
                        {
                            var value = properties[j].GetValue(data[i]);
                            worksheet.Cell(i + 2, j + 1).Value = value?.ToString() ?? string.Empty;
                        }
                    }

                    workbook.SaveAs(filePath);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}
