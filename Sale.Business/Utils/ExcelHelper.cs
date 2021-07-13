using System;
using System.Collections;
using log4net;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
/* To work eith EPPlus library */
using OfficeOpenXml;
/* For I/O purpose */
using System.IO;
using System.Reflection;
using System.Xml;
using Sale.Data;


namespace Sale.Business
{
    public class ExcelHelper
    {
        public ExcelHelper()
        {
        }

        #region Public Method

        /// <summary>
        /// Load data from excel file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputPath"></param>
        /// <param name="fromRow"></param>
        /// <param name="fromColumn"></param>
        /// <returns>List data with T type</returns>
        public static List<T> LoadDataFromFile<T>(string inputPath, int fromRow, int fromColumn) where T : class
        {
            var returnList = new List<T>();
            string name = null;
            try
            {
                using (var package = new ExcelPackage(new FileInfo(inputPath)))
                {
                    if (package.Workbook.Worksheets.Count == 0)
                    {
                        throw new Exception("Your Excel file does not contain any work sheets");
                    }
                    //Retrieve first Worksheet
                    ExcelWorksheet workSheet = package.Workbook.Worksheets.First();

                    //create a instance of T
                    T objT;
                    //Get all the properties associated with T 
                    PropertyInfo[] objProp = typeof(T).GetProperties();
                    var toColumn = objProp.Count();

                    #region Check column validation
                    //Get ColumnName List on template
                    List<string> columnNames = new List<string>();
                    foreach (var cell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
                    {
                        columnNames.Add(cell.Value.ToString());
                    }
                    //Check column is exist
                    ArrayList colInvalid = new ArrayList();
                    foreach (var propertyInfo in objProp)
                    {
                        if (!columnNames.Contains(propertyInfo.Name))
                        {
                            //col not exist
                            colInvalid.Add(propertyInfo.Name);
                        }
                    }
                    //if (colInvalid.Count > 0)
                    //{
                    //    throw new Exception("Your Excel file does not contain any column name: " + string.Join(",", colInvalid.ToArray()));
                    //}
                    #endregion

                    //Loop through the rows of the excel sheet
                    for (var rowIndex = fromRow; rowIndex <= workSheet.Dimension.End.Row; rowIndex++)
                    {
                        //reset a instance of T
                        objT = Activator.CreateInstance<T>();

                        //Get row
                        var row = workSheet.Cells[rowIndex, fromColumn, rowIndex, toColumn];

                        foreach (var propertyInfo in objProp)
                        {
                            if (columnNames.Contains(propertyInfo.Name))
                            {
                                int position = columnNames.IndexOf(propertyInfo.Name);
                                name = propertyInfo.Name;
                                object value = row[rowIndex, position + 1].Value;

                                //To prevent an exception cast the value to the type of the property.
                                //propertyInfo.SetValue(objT, Convert.ChangeType(row[rowIndex, position + 1].Value, propertyInfo.PropertyType));

                                if ((propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(int?)))
                                {
                                    value = value ?? 0;
                                    value = Convert.ToInt32(value);
                                    propertyInfo.SetValue(objT, value);
                                    continue;
                                }

                                if ((propertyInfo.PropertyType == typeof(decimal) || propertyInfo.PropertyType == typeof(decimal?)))
                                {
                                    value = value ?? 0;
                                    value = Convert.ToDecimal(value);
                                    propertyInfo.SetValue(objT, value);
                                    continue;
                                }
                                if ((propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?)))
                                {
                                    value = (value == null || string.IsNullOrEmpty(value.ToString())) ? new DateTime(1970, 1, 1, 0, 0, 0) : Convert.ToDateTime(value);
                                    //value = Convert.ToDateTime(value);
                                    propertyInfo.SetValue(objT, value);
                                    continue;
                                }
                                if ((propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(bool?)) && !string.IsNullOrEmpty(value.ToString()))
                                {
                                    value = Convert.ToBoolean(value);
                                    propertyInfo.SetValue(objT, value);
                                    continue;
                                }
                                if ((propertyInfo.PropertyType == typeof(Guid) || propertyInfo.PropertyType == typeof(Guid?)) && !string.IsNullOrEmpty(value.ToString()))
                                {
                                    value = Guid.Parse(value.ToString());
                                    propertyInfo.SetValue(objT, value);
                                    continue;
                                }
                                else
                                {
                                    
                                    propertyInfo.SetValue(objT, value == null ? string.Empty : value.ToString());
                                    continue;
                                }
                            }
                        }
                        returnList.Add(objT);
                    }
                }
                return returnList;
            }
            catch (Exception ex)
            {
                Logger.Error("column:"+ name + "; ExcelHelper>LoadDataFromFile: " + ex);
                //Logger.Error(GetType(), ex);
                return null;
            }
        }

        /// <summary>
        /// Export excel without template
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="xmlConfigPath"></param>
        /// <param name="outputPath"></param>
        /// <param name="sheetName"></param>
        /// <param name="header"></param>
        /// <param name="footer"></param>
        /// <returns></returns>
        public static bool SaveDataToFile<T>(List<T> data, string xmlConfigPath, string outputPath, string sheetName = "Sheet1", Dictionary<string, object> header = null, Dictionary<string, object> footer = null) where T : class
        {
            try
            {
                if (data == null)
                {
                    throw new Exception("Data is null. Can not save to file");
                }

                sheetName = sheetName ?? "Sheet1";

                #region Build column header & name for data range
                var columnNames = new List<string>();
                var columnHeaders = new List<string>();

                //Read xml config for report
                XmlDocument xmldocConfig = new XmlDocument();
                xmldocConfig.Load(xmlConfigPath);
                XmlNode nColumnName = xmldocConfig.SelectSingleNode("/XmlConfig/ColumnName");
                XmlNode nColumnHeader = xmldocConfig.SelectSingleNode("/XmlConfig/ColumnHeader");

                //Column Name
                columnNames = nColumnName?.InnerText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                for (int i = 0; i < columnNames.Count; i++)
                    columnNames[i] = columnNames[i].Trim();

                //Column Header
                columnHeaders = nColumnHeader?.InnerText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                for (int i = 0; i < columnHeaders.Count; i++)
                    columnHeaders[i] = columnHeaders[i].Trim();

                //SortOrder column
                XmlNode nColumnSortOrder = xmldocConfig.SelectSingleNode("/XmlConfig/ColumnSortOrder");
                string colSortOrderName = nColumnSortOrder?.InnerText;

                //Check valid column name & header
                if (columnNames.Count != columnHeaders.Count)
                {
                    throw new Exception("Xml Config Error: ColumnName and ColumnHeader not map. file:" + xmlConfigPath);
                }
                #endregion

                using (var package = new ExcelPackage(new FileInfo(outputPath)))
                {
                    // add a new worksheet to the empty workbook
                    ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(sheetName);
                    workSheet.View.ShowGridLines = true;

                    // Total Column On Report
                    int columnNumber = columnNames.Count;
                    // Start row for content data
                    int rowContentStart = header != null ? header.Count + 2 : 1;

                    #region Content

                    #region Adding the column header
                    for (int i = 0; i < columnNumber; i++)
                    {
                        workSheet.Cells[rowContentStart, i + 1].Value = columnHeaders[i];
                    }
                    workSheet.Row(rowContentStart).Style.Font.Bold = true;
                    #endregion

                    #region Add Content report
                    int rowNumber = rowContentStart + 1;
                    // Loop through all data and add to worksheet
                    for (int i = 0; i < data.Count(); i++)
                    {
                        T objT = data[i];
                        for (int j = 0; j < columnNumber; j++)
                        {
                            if (colSortOrderName != null && colSortOrderName.Equals(columnNames[j]))
                            {
                                //Nếu là cột Số thứ thự
                                workSheet.Cells[rowNumber, j + 1].Value = i + 1;
                            }
                            else
                            {
                                SetValueCell(workSheet.Cells[rowNumber, j + 1], objT, columnNames[j]);
                            }

                        }
                        rowNumber++;
                    }
                    #endregion

                    #region Format cells for Date & Number & HorizontalAlignment
                    XmlNodeList nColumnFormat = xmldocConfig.SelectNodes("/XmlConfig/Format/Column");
                    if (nColumnFormat != null)
                    {
                        foreach (XmlNode node in nColumnFormat)
                        {
                            string colName = node["ColumnName"]?.InnerText;
                            string format = node["Format"]?.InnerText;
                            string align = node["Align"]?.InnerText;

                            if (!string.IsNullOrEmpty(colName) && columnNames.Contains(colName))
                            {
                                int position = columnNames.IndexOf(colName);

                                //Format
                                if (!string.IsNullOrEmpty(format))
                                {
                                    workSheet.Column(position + 1).Style.Numberformat.Format = format;
                                }

                                //HorizontalAlignment
                                if (string.IsNullOrEmpty(align)) continue;
                                switch (align.ToLower())
                                {
                                    case "center":
                                        workSheet.Column(position + 1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                        break;
                                    case "right":
                                        workSheet.Column(position + 1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                        break;
                                    default:
                                        workSheet.Column(position + 1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                        break;
                                }
                            }
                        }
                    }
                    #endregion

                    #region Add sum row
                    XmlNode nSumRow = xmldocConfig.SelectSingleNode("/XmlConfig/SumRow");
                    if (nSumRow != null)
                    {
                        XmlNode nSumRowCaption = nSumRow.SelectSingleNode("./Caption");
                        string captionText = nSumRowCaption?.InnerText;
                        string captionAlign = nSumRowCaption?.Attributes["Align"]?.Value;
                        string captionMergeFrom = nSumRowCaption?.Attributes["MergeFrom"]?.Value;
                        string captionMergeTo = nSumRowCaption?.Attributes["MergeTo"]?.Value;

                        //Merge Cells
                        //Get address excel range
                        string mergeAddress = workSheet.Cells[data.Count + rowContentStart + 1, Convert.ToInt32(captionMergeFrom)].Address + ":" + workSheet.Cells[data.Count + rowContentStart + 1, Convert.ToInt32(captionMergeTo)].Address;
                        using (ExcelRange rng = workSheet.Cells[mergeAddress])
                        {
                            rng.Merge = true;
                            rng.Value = captionText;
                            if (!string.IsNullOrEmpty(captionAlign))
                            {
                                switch (captionAlign.ToLower())
                                {
                                    case "center":
                                        rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                        break;
                                    case "right":
                                        rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                        break;
                                    default:
                                        rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                        break;
                                }
                            }
                        }

                        XmlNodeList nSumRowColumn = nSumRow.SelectNodes("./Column");
                        if (nSumRowColumn != null)
                        {
                            foreach (XmlNode node in nSumRowColumn)
                            {
                                string colName = node["ColumnName"]?.InnerText;
                                string format = node["Format"]?.InnerText;
                                string align = node["Align"]?.InnerText;

                                if (!string.IsNullOrEmpty(colName) && columnNames.Contains(colName))
                                {
                                    int position = columnNames.IndexOf(colName);
                                    workSheet.Cells[data.Count + rowContentStart + 1, position + 1].Formula = "=SUM(" + workSheet.Cells[rowContentStart + 1, position + 1].Address + ":" + workSheet.Cells[data.Count + rowContentStart, position + 1].Address + ")";

                                    //Format
                                    if (!string.IsNullOrEmpty(format))
                                    {
                                        workSheet.Column(position + 1).Style.Numberformat.Format = format;
                                    }

                                    //HorizontalAlignment
                                    if (string.IsNullOrEmpty(align)) continue;
                                    switch (align.ToLower())
                                    {
                                        case "center":
                                            workSheet.Column(position + 1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                            break;
                                        case "right":
                                            workSheet.Column(position + 1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                            break;
                                        default:
                                            workSheet.Column(position + 1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                            break;
                                    }
                                }
                            }
                        }
                        workSheet.Row(data.Count + rowContentStart + 1).Style.Font.Bold = true;
                    }
                    #endregion

                    // Fit the columns according to its content
                    for (int i = 1; i < columnNumber; i++)
                    {
                        workSheet.Column(i + 1).AutoFit();
                    }

                    #region Cells border style
                    string borderRange = workSheet.Cells[rowContentStart, 1].Address + ":" + workSheet.Cells[data.Count + (nSumRow != null ? rowContentStart + 1 : rowContentStart), columnNumber].Address;
                    using (ExcelRange rng = workSheet.Cells[borderRange])
                    {
                        rng.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        rng.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        rng.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        rng.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    }
                    #endregion

                    #region HorizontalAlignment Header
                    string headerAlign = nColumnHeader?.Attributes["Align"]?.InnerText;
                    if (!string.IsNullOrEmpty(headerAlign))
                    {
                        string headerRange = workSheet.Cells[rowContentStart, 1].Address + ":" + workSheet.Cells[rowContentStart, columnNumber].Address;
                        using (ExcelRange rng = workSheet.Cells[headerRange])
                        {
                            switch (headerAlign.ToLower())
                            {
                                case "center":
                                    rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                    break;
                                case "right":
                                    rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                    break;
                                default:
                                    rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                    break;
                            }
                        }
                    }
                    #endregion

                    #endregion

                    #region Header
                    //Header
                    if (header != null)
                    {
                        //Add header
                        int rowHeaderStart = 1;

                        if (header.ContainsKey(Constant.ReportHeader.STORE))
                        {
                            workSheet.Cells[rowHeaderStart, 1].Value = header[Constant.ReportHeader.STORE]?.ToString() ?? "";
                            workSheet.Cells[rowHeaderStart, 1].Style.Font.Size = 12;
                            workSheet.Cells[rowHeaderStart, 1].Style.Font.Bold = true;
                            workSheet.Cells[rowHeaderStart, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                            rowHeaderStart += 1;
                        }

                        if (header.ContainsKey(Constant.ReportHeader.REPORT_NAME))
                        {
                            //Merge Cells
                            string mergeAddress = workSheet.Cells[rowHeaderStart, 1].Address + ":" + workSheet.Cells[rowHeaderStart, columnNumber].Address;
                            using (ExcelRange rng = workSheet.Cells[mergeAddress])
                            {
                                rng.Merge = true;
                                rng.Value = header[Constant.ReportHeader.REPORT_NAME]?.ToString() ?? "";
                                rng.Style.Font.Size = 16;
                                rng.Style.Font.Bold = true;
                                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            }
                            rowHeaderStart += 1;
                        }
                        if (header.ContainsKey(Constant.ReportHeader.REPORT_DATE))
                        {
                            string mergeAddress = workSheet.Cells[rowHeaderStart, 1].Address + ":" + workSheet.Cells[rowHeaderStart, columnNumber].Address;
                            using (ExcelRange rng = workSheet.Cells[mergeAddress])
                            {
                                rng.Merge = true;
                                rng.Value = header[Constant.ReportHeader.REPORT_DATE]?.ToString() ?? "";
                                rng.Style.Font.Size = 12;
                                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            }
                        }
                    }

                    #endregion

                    // save our new workbook and we are done!
                    package.Save();
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("ExcelHelper>SaveDataToFile: " + ex);
                return false;
            }
        }

        /// <summary>
        /// Export excel from template file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="xmlConfigPath"></param>
        /// <param name="outputPath"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static bool ExportFromTemplate<T>(List<T> data, string xmlConfigPath, string outputPath, Dictionary<string, object> customData = null, List<string> columnDynamic = null, List<object> columnHeaderDynamic = null) where T : class
        {
            Logger.Error("**************ExportFromTemplate: START ***************");
            try
            {
                #region Check validation
                if (data == null || !data.Any())
                {
                    throw new Exception("Data is null or empty. Can not save to file");
                }
                #endregion

                //Read xml config
                XmlDocument xmldocConfig = new XmlDocument();
                xmldocConfig.Load(xmlConfigPath);

                #region Get Output File
                XmlNode nXmlConfig = xmldocConfig.SelectSingleNode("/XmlConfig");
                if (nXmlConfig == null)
                {
                    throw new Exception("XMLNode XmlConfig not exist in config file.");
                }
                //Output Template File
                var outputTemplatePath = System.Web.Hosting.HostingEnvironment.MapPath(nXmlConfig.Attributes?["OutputTemplatePath"]?.InnerText);
                if (!System.IO.File.Exists(outputTemplatePath))
                {
                    throw new Exception("Output template file is not exist!");
                }
                #endregion

                using (var package = new ExcelPackage(new FileInfo(outputTemplatePath)))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets.First();

                    #region Generate Content
                    int startRow, contentRows;
                    XmlNode nContent = xmldocConfig.SelectSingleNode("/XmlConfig/Content");
                    //Dòng bắt đầu nội dung output
                    string sStartRow = nContent?.Attributes?["StartRow"]?.InnerText;
                    if (string.IsNullOrEmpty(sStartRow))
                        throw new Exception("XmlConfig/Content[StartRow] is null or empty");
                    if (!int.TryParse(sStartRow, out startRow))
                    {
                        throw new Exception("XmlConfig/Content[StartRow] invalid");
                    }

                    contentRows = data.Count; //Số dòng nội dung output
                    int addRows = contentRows - 1; //số row cần insert thêm để hiển thị dữ liệu (trên file template đã có sẵn 1 row)
                    workSheet.InsertRow(startRow, addRows); //Insert rows

                    //Số dòng column động
                    int addColumns = columnDynamic?.Count ?? 0;

                    string sPosition = "", pIsSort = "", pColName = "", pAlign = "", pFormat = "";
                    int pPosition;

                    #region Set data
                    XmlNodeList nContentColList = xmldocConfig.SelectNodes("/XmlConfig/Content/Columns/Column");
                    if (nContentColList == null || nContentColList.Count == 0)
                    {
                        throw new Exception("XmlConfig/Content/Columns/Column is null or empty");
                    }

                    //Dong dynamiccolumn
                    XmlNode nContentColDynamic = xmldocConfig.SelectSingleNode("/XmlConfig/Content/Columns/ColumnDynamic");

                    // Loop through all data and add to worksheet
                    int currentRow = startRow;
                    for (int i = 0; i < contentRows; i++)
                    {
                        T objT = data[i];

                        #region fix column

                        foreach (XmlNode nCol in nContentColList)
                        {
                            //Get Column Infor
                            sPosition = nCol?.Attributes?["Position"]?.InnerText;
                            if (!int.TryParse(sPosition, out pPosition))
                            {
                                //Thông tin vị trí cell bắt buộc phải có
                                continue;
                            }
                            pIsSort = nCol?.Attributes?["IsSort"]?.InnerText;
                            pColName = nCol?.Attributes?["Name"]?.InnerText;
                            pAlign = nCol?.Attributes?["Align"]?.InnerText ?? "Left";//(Left, Center, Right). Nếu ko tồn tại thì mặc định "Left"
                            pFormat = nCol?.Attributes?["Format"]?.InnerText;

                            // Set Value
                            if (pIsSort != null && pIsSort == "1")
                            {
                                workSheet.Cells[currentRow, pPosition].Value = i + 1;
                            }
                            else
                            {
                                SetValueCell(workSheet.Cells[currentRow, pPosition], objT, pColName);
                            }

                            // Set Style
                            //---Set HorizontalAlignment
                            SetAlignCell(workSheet.Cells[currentRow, pPosition], pAlign);

                            //---Set Format
                            SetFormatCell(workSheet.Cells[currentRow, pPosition], pFormat);

                            //---Set Border
                            SetBorderCell(workSheet.Cells[currentRow, pPosition], OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                        }
                        #endregion

                        #region dynamic column
                        //Set value into column dynamic
                        if (nContentColDynamic != null && addColumns > 0)
                        {
                            sPosition = nContentColDynamic?.Attributes?["Position"]?.InnerText;
                            if (int.TryParse(sPosition, out pPosition))
                            {
                                pAlign = nContentColDynamic?.Attributes?["Align"]?.InnerText ?? "Left";
                                pFormat = nContentColDynamic?.Attributes?["Format"]?.InnerText;
                                SetValueIntoColumnDynamic(workSheet.Cells[currentRow, pPosition, currentRow, (pPosition + addColumns - 1)], objT, currentRow, pPosition, columnDynamic, pFormat, pAlign);
                            }
                        }
                        #endregion

                        //Next Row
                        currentRow++;
                    }
                    #endregion

                    #region Sum Column
                    XmlNodeList nSumColList = xmldocConfig.SelectNodes("/XmlConfig/Content/Columns/Column[@IsSum='1']");
                    foreach (XmlNode nCol in nSumColList)
                    {
                        sPosition = nCol?.Attributes?["Position"]?.InnerText;
                        if (!int.TryParse(sPosition, out pPosition))
                        {
                            //Thông tin vị trí cell bắt buộc phải có
                            continue;
                        }
                        pAlign = nCol?.Attributes?["Align"]?.InnerText ?? "Left";
                        pFormat = nCol?.Attributes?["Format"]?.InnerText;

                        workSheet.Cells[startRow + contentRows, pPosition].Formula = "=SUM(" + workSheet.Cells[startRow, pPosition].Address + ":" + workSheet.Cells[startRow + contentRows - 1, pPosition].Address + ")";

                        //Set Style
                        //---Set HorizontalAlignment
                        SetAlignCell(workSheet.Cells[startRow + contentRows, pPosition], pAlign);

                        //---Set Format
                        SetFormatCell(workSheet.Cells[startRow + contentRows, pPosition], pFormat);
                    }

                    //Sum dynamic content
                    if (nContentColDynamic != null && addColumns > 0)
                    {
                        string pIsSum = nContentColDynamic?.Attributes?["IsSum"]?.InnerText;
                        if (pIsSum != null && pIsSum == "1")
                        {
                            sPosition = nContentColDynamic?.Attributes?["Position"]?.InnerText;
                            if (int.TryParse(sPosition, out pPosition))
                            {
                                pAlign = nContentColDynamic?.Attributes?["Align"]?.InnerText ?? "Left";
                                pFormat = nContentColDynamic?.Attributes?["Format"]?.InnerText;

                                int position = pPosition;
                                for (int i = 0; i < columnDynamic.Count; i++)
                                {
                                    position = pPosition + i;

                                    workSheet.Cells[startRow + contentRows, position].Formula = "=SUM(" + workSheet.Cells[startRow, position].Address + ":" + workSheet.Cells[startRow + contentRows - 1, position].Address + ")";

                                    //---Set HorizontalAlignment
                                    SetAlignCell(workSheet.Cells[startRow + contentRows, position], pAlign);

                                    //---Set Format
                                    SetFormatCell(workSheet.Cells[startRow + contentRows, position], pFormat);

                                    //---Set Border
                                    SetBorderCell(workSheet.Cells[startRow + contentRows, position], OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                                }
                            }

                        }
                    }

                    #endregion

                    #region Column Header Dynamic
                    if (nContentColDynamic != null && addColumns > 0)
                    {
                        var sStartRowHeader = nContentColDynamic?.Attributes?["StartRowHeader"]?.InnerText; //7;8
                        if (sStartRowHeader != null && columnHeaderDynamic != null && columnHeaderDynamic.Count > 0)
                        {
                            sPosition = nContentColDynamic?.Attributes?["Position"]?.InnerText;
                            if (int.TryParse(sPosition, out pPosition))
                            {
                                string[] arrStartRow = sStartRowHeader.Split(';');
                                for (int i = 0; i < arrStartRow.Length; i++)
                                {
                                    //Vị trí header đầu tiên
                                    int rowHeader = Convert.ToInt32(arrStartRow[i]);
                                    List<string> headerData = (List<string>)columnHeaderDynamic[i];
                                    //vị trí cột bắt đầu

                                    for (int j = 0; j < headerData.Count; j++)
                                    {
                                        workSheet.Cells[rowHeader, pPosition + j].Value = headerData[j];

                                        //---Set HorizontalAlignment
                                        SetAlignCell(workSheet.Cells[rowHeader, pPosition + j], "center");

                                        //---Set Border
                                        SetBorderCell(workSheet.Cells[rowHeader, pPosition + j], OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #endregion

                    #region Generate Custom Content (V2)

                    if (customData != null)
                    {
                        //Sheet2
                        XmlNodeList nCustomContentList = xmldocConfig.SelectNodes("/XmlConfig/CustomContents/CustomContent");
                        if (nCustomContentList != null && nCustomContentList.Count > 0)
                        {
                            foreach (XmlNode nCustomContent in nCustomContentList)
                            {
                                //Key trong Dictionary CustomData
                                var key = nCustomContent?.Attributes?["Key"]?.InnerText;
                                //Cell Name trong file template
                                var cellName = nCustomContent?.Attributes?["CellName"]?.InnerText;

                                if (!customData.ContainsKey(key))
                                    continue;

                                try
                                {
                                    var cellRange = workSheet.Workbook.Names[cellName];
                                    if (cellRange != null)
                                    {
                                        var value = customData[key] ?? "";
                                        var sTextTransform = nCustomContent?.Attributes?["TextTransform"]?.InnerText ?? "";
                                        switch (sTextTransform.ToLower())
                                        {
                                            case "uppercase":
                                                value = Convert.ToString(value).ToUpper();
                                                break;
                                            case "lowercase":
                                                value = Convert.ToString(value).ToLower();
                                                break;
                                            case "capitalize":
                                                break;
                                        }

                                        cellRange.Value = value;
                                    }
                                }
                                catch (Exception ex2)
                                {
                                    Logger.Error("ExcelHelper > ExportFromTemplate: Cell Name [" + cellName + "] not exist. " + ex2);
                                    continue;
                                }

                            }
                        }
                    }
                    #endregion

                    #region Generate Reprort Header (V1) - Sẽ remove đi khi ko dùng theo tag <Header>
                    if (customData != null)
                    {
                        XmlNode nStoreName = xmldocConfig.SelectSingleNode("/XmlConfig/Header/StoreName");
                        sPosition = nStoreName?.Attributes?["Position"]?.InnerText;
                        if (!string.IsNullOrEmpty(sPosition))
                        {
                            var value = customData[Constant.ReportHeader.STORE]?.ToString() ?? "";

                            var sTextTransform = nStoreName?.Attributes?["TextTransform"]?.InnerText ?? "";
                            switch (sTextTransform.ToLower())
                            {
                                case "uppercase":
                                    value = value.ToUpper();
                                    break;
                                case "lowercase":
                                    value = value.ToLower();
                                    break;
                                case "capitalize":
                                    break;
                            }
                            workSheet.Cells[sPosition].Value = value;
                        }

                        XmlNode nReportDate = xmldocConfig.SelectSingleNode("/XmlConfig/Header/ReportDate");
                        sPosition = nReportDate?.Attributes?["Position"]?.InnerText;
                        if (!string.IsNullOrEmpty(sPosition))
                        {
                            workSheet.Cells[sPosition].Value = customData[Constant.ReportHeader.REPORT_DATE]?.ToString() ?? "";
                        }
                    }
                    #endregion

                    //Save our new workbook.
                    package.SaveAs(new FileInfo(outputPath));
                }
            }
            catch (Exception ex)
            {
                Logger.Error("ExcelHelper > ExportFromTemplate: " + ex);
                return false;
            }
            finally
            {
                Logger.Error("**************ExportFromTemplate: END ***************");
            }
            return true;
        }

        /// <summary>
        /// Export excel from template file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="xmlConfigPath"></param>
        /// <param name="outputPath"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static bool ExportFromTemplateMasterDetail<T>(List<T> data, string xmlConfigPath, string outputPath, Dictionary<string, object> customData = null, List<string> columnDynamic = null, List<object> columnHeaderDynamic = null) where T : class
        {
            Logger.Error("**************ExportFromTemplate: START ***************");
            try
            {
                #region Check validation
                if (data == null || !data.Any())
                {
                    throw new Exception("Data is null or empty. Can not save to file");
                }
                #endregion

                //Read xml config
                XmlDocument xmldocConfig = new XmlDocument();
                xmldocConfig.Load(xmlConfigPath);

                #region Get Output File
                XmlNode nXmlConfig = xmldocConfig.SelectSingleNode("/XmlConfig");
                if (nXmlConfig == null)
                {
                    throw new Exception("XMLNode XmlConfig not exist in config file.");
                }
                //Output Template File
                var outputTemplatePath = System.Web.Hosting.HostingEnvironment.MapPath(nXmlConfig.Attributes?["OutputTemplatePath"]?.InnerText);
                if (!System.IO.File.Exists(outputTemplatePath))
                {
                    throw new Exception("Output template file is not exist!");
                }
                #endregion

                using (var package = new ExcelPackage(new FileInfo(outputTemplatePath)))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets.First();

                    #region Generate Content
                    int startRow, contentRows;
                    XmlNode nContent = xmldocConfig.SelectSingleNode("/XmlConfig/Content");
                    //Dòng bắt đầu nội dung output
                    string sStartRow = nContent?.Attributes?["StartRow"]?.InnerText;
                    if (string.IsNullOrEmpty(sStartRow))
                        throw new Exception("XmlConfig/Content[StartRow] is null or empty");
                    if (!int.TryParse(sStartRow, out startRow))
                    {
                        throw new Exception("XmlConfig/Content[StartRow] invalid");
                    }

                    contentRows = data.Count; //Số dòng nội dung output
                    int addRows = contentRows - 1; //số row cần insert thêm để hiển thị dữ liệu (trên file template đã có sẵn 1 row)
                    workSheet.InsertRow(startRow, addRows); //Insert rows

                    //Số dòng column động
                    int addColumns = columnDynamic?.Count ?? 0;

                    string sPosition = "", pIsSort = "", pColName = "", pAlign = "", pFormat = "";
                    int pPosition;

                    #region Set data
                    XmlNodeList nContentColList = xmldocConfig.SelectNodes("/XmlConfig/Content/Columns/Column");
                    if (nContentColList == null || nContentColList.Count == 0)
                    {
                        throw new Exception("XmlConfig/Content/Columns/Column is null or empty");
                    }

                    //Dong dynamiccolumn
                    XmlNode nContentColDynamic = xmldocConfig.SelectSingleNode("/XmlConfig/Content/Columns/ColumnDynamic");

                    // Loop through all data and add to worksheet
                    int currentRow = startRow;
                    int stt = 0;
                    for (int i = 0; i < contentRows; i++)
                    {
                        T objT = data[i];

                        #region fix column

                        foreach (XmlNode nCol in nContentColList)
                        {
                            //Get Column Infor
                            sPosition = nCol?.Attributes?["Position"]?.InnerText;
                            if (!int.TryParse(sPosition, out pPosition))
                            {
                                //Thông tin vị trí cell bắt buộc phải có
                                continue;
                            }
                            pIsSort = nCol?.Attributes?["IsSort"]?.InnerText;
                            pColName = nCol?.Attributes?["Name"]?.InnerText;
                            pAlign = nCol?.Attributes?["Align"]?.InnerText ?? "Left";//(Left, Center, Right). Nếu ko tồn tại thì mặc định "Left"
                            pFormat = nCol?.Attributes?["Format"]?.InnerText;

                            // Set Value
                            if (pIsSort != null && pIsSort == "1")
                            {

                                //Nếu là cột Số thứ thự
                                //if ((objT as Model.ListDTO.ListPriceDetail).IsMaster)
                                //{
                                //    stt = stt + 1;
                                //    workSheet.Cells[currentRow, pPosition].Value = stt;
                                //}
                            }
                            else
                            {
                                SetValueCell(workSheet.Cells[currentRow, pPosition], objT, pColName);
                            }

                            // Set Style
                            //---Set HorizontalAlignment
                            SetAlignCell(workSheet.Cells[currentRow, pPosition], pAlign);

                            //---Set Format
                            SetFormatCell(workSheet.Cells[currentRow, pPosition], pFormat);

                            //---Set Border
                            SetBorderCell(workSheet.Cells[currentRow, pPosition], OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                        }
                        #endregion

                        #region dynamic column
                        //Set value into column dynamic
                        if (nContentColDynamic != null && addColumns > 0)
                        {
                            sPosition = nContentColDynamic?.Attributes?["Position"]?.InnerText;
                            if (int.TryParse(sPosition, out pPosition))
                            {
                                pAlign = nContentColDynamic?.Attributes?["Align"]?.InnerText ?? "Left";
                                pFormat = nContentColDynamic?.Attributes?["Format"]?.InnerText;
                                SetValueIntoColumnDynamic(workSheet.Cells[currentRow, pPosition, currentRow, (pPosition + addColumns - 1)], objT, currentRow, pPosition, columnDynamic, pFormat, pAlign);
                            }
                        }
                        #endregion

                        //Next Row
                        currentRow++;
                    }
                    #endregion

                    #region Sum Column
                    XmlNodeList nSumColList = xmldocConfig.SelectNodes("/XmlConfig/Content/Columns/Column[@IsSum='1']");
                    foreach (XmlNode nCol in nSumColList)
                    {
                        sPosition = nCol?.Attributes?["Position"]?.InnerText;
                        if (!int.TryParse(sPosition, out pPosition))
                        {
                            //Thông tin vị trí cell bắt buộc phải có
                            continue;
                        }
                        pAlign = nCol?.Attributes?["Align"]?.InnerText ?? "Left";
                        pFormat = nCol?.Attributes?["Format"]?.InnerText;

                        workSheet.Cells[startRow + contentRows, pPosition].Formula = "=SUM(" + workSheet.Cells[startRow, pPosition].Address + ":" + workSheet.Cells[startRow + contentRows - 1, pPosition].Address + ")";

                        //Set Style
                        //---Set HorizontalAlignment
                        SetAlignCell(workSheet.Cells[startRow + contentRows, pPosition], pAlign);

                        //---Set Format
                        SetFormatCell(workSheet.Cells[startRow + contentRows, pPosition], pFormat);
                    }

                    //Sum dynamic content
                    if (nContentColDynamic != null && addColumns > 0)
                    {
                        string pIsSum = nContentColDynamic?.Attributes?["IsSum"]?.InnerText;
                        if (pIsSum != null && pIsSum == "1")
                        {
                            sPosition = nContentColDynamic?.Attributes?["Position"]?.InnerText;
                            if (int.TryParse(sPosition, out pPosition))
                            {
                                pAlign = nContentColDynamic?.Attributes?["Align"]?.InnerText ?? "Left";
                                pFormat = nContentColDynamic?.Attributes?["Format"]?.InnerText;

                                int position = pPosition;
                                for (int i = 0; i < columnDynamic.Count; i++)
                                {
                                    position = pPosition + i;

                                    workSheet.Cells[startRow + contentRows, position].Formula = "=SUM(" + workSheet.Cells[startRow, position].Address + ":" + workSheet.Cells[startRow + contentRows - 1, position].Address + ")";

                                    //---Set HorizontalAlignment
                                    SetAlignCell(workSheet.Cells[startRow + contentRows, position], pAlign);

                                    //---Set Format
                                    SetFormatCell(workSheet.Cells[startRow + contentRows, position], pFormat);

                                    //---Set Border
                                    SetBorderCell(workSheet.Cells[startRow + contentRows, position], OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                                }
                            }

                        }
                    }

                    #endregion

                    #region Column Header Dynamic
                    if (nContentColDynamic != null && addColumns > 0)
                    {
                        var sStartRowHeader = nContentColDynamic?.Attributes?["StartRowHeader"]?.InnerText; //7;8
                        if (sStartRowHeader != null && columnHeaderDynamic != null && columnHeaderDynamic.Count > 0)
                        {
                            sPosition = nContentColDynamic?.Attributes?["Position"]?.InnerText;
                            if (int.TryParse(sPosition, out pPosition))
                            {
                                string[] arrStartRow = sStartRowHeader.Split(';');
                                for (int i = 0; i < arrStartRow.Length; i++)
                                {
                                    //Vị trí header đầu tiên
                                    int rowHeader = Convert.ToInt32(arrStartRow[i]);
                                    List<string> headerData = (List<string>)columnHeaderDynamic[i];
                                    //vị trí cột bắt đầu

                                    for (int j = 0; j < headerData.Count; j++)
                                    {
                                        workSheet.Cells[rowHeader, pPosition + j].Value = headerData[j];

                                        //---Set HorizontalAlignment
                                        SetAlignCell(workSheet.Cells[rowHeader, pPosition + j], "center");

                                        //---Set Border
                                        SetBorderCell(workSheet.Cells[rowHeader, pPosition + j], OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #endregion

                    #region Generate Custom Content (V2)

                    if (customData != null)
                    {
                        //Sheet2
                        XmlNodeList nCustomContentList = xmldocConfig.SelectNodes("/XmlConfig/CustomContents/CustomContent");
                        if (nCustomContentList != null && nCustomContentList.Count > 0)
                        {
                            foreach (XmlNode nCustomContent in nCustomContentList)
                            {
                                //Key trong Dictionary CustomData
                                var key = nCustomContent?.Attributes?["Key"]?.InnerText;
                                //Cell Name trong file template
                                var cellName = nCustomContent?.Attributes?["CellName"]?.InnerText;

                                if (!customData.ContainsKey(key))
                                    continue;

                                try
                                {
                                    var cellRange = workSheet.Workbook.Names[cellName];
                                    if (cellRange != null)
                                    {
                                        var value = customData[key] ?? "";
                                        var sTextTransform = nCustomContent?.Attributes?["TextTransform"]?.InnerText ?? "";
                                        switch (sTextTransform.ToLower())
                                        {
                                            case "uppercase":
                                                value = Convert.ToString(value).ToUpper();
                                                break;
                                            case "lowercase":
                                                value = Convert.ToString(value).ToLower();
                                                break;
                                            case "capitalize":
                                                break;
                                        }

                                        cellRange.Value = value;
                                    }
                                }
                                catch (Exception ex2)
                                {
                                    Logger.Error("ExcelHelper > ExportFromTemplate: Cell Name [" + cellName + "] not exist. " + ex2);
                                    continue;
                                }

                            }
                        }
                    }
                    #endregion

                    #region Generate Reprort Header (V1) - Sẽ remove đi khi ko dùng theo tag <Header>
                    if (customData != null)
                    {
                        XmlNode nStoreName = xmldocConfig.SelectSingleNode("/XmlConfig/Header/StoreName");
                        sPosition = nStoreName?.Attributes?["Position"]?.InnerText;
                        if (!string.IsNullOrEmpty(sPosition))
                        {
                            var value = customData[Constant.ReportHeader.STORE]?.ToString() ?? "";

                            var sTextTransform = nStoreName?.Attributes?["TextTransform"]?.InnerText ?? "";
                            switch (sTextTransform.ToLower())
                            {
                                case "uppercase":
                                    value = value.ToUpper();
                                    break;
                                case "lowercase":
                                    value = value.ToLower();
                                    break;
                                case "capitalize":
                                    break;
                            }
                            workSheet.Cells[sPosition].Value = value;
                        }

                        XmlNode nReportDate = xmldocConfig.SelectSingleNode("/XmlConfig/Header/ReportDate");
                        sPosition = nReportDate?.Attributes?["Position"]?.InnerText;
                        if (!string.IsNullOrEmpty(sPosition))
                        {
                            workSheet.Cells[sPosition].Value = customData[Constant.ReportHeader.REPORT_DATE]?.ToString() ?? "";
                        }
                    }
                    #endregion

                    //Save our new workbook.
                    package.SaveAs(new FileInfo(outputPath));
                }
            }
            catch (Exception ex)
            {
                Logger.Error("ExcelHelper > ExportFromTemplate: " + ex);
                return false;
            }
            finally
            {
                Logger.Error("**************ExportFromTemplate: END ***************");
            }
            return true;
        }
        /// <summary>
        /// Get Header content for report
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="reportName"></param>
        /// <param name="fiscalID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        //public static Dictionary<string, object> GetReportHeader(int? storeID, string reportName, string langID, Guid? fiscalID, DateTime? fromDate = null, DateTime? toDate = null)
        //{
        //    var reval = new Dictionary<string, object>();
        //    try
        //    {
        //        using (var db = new DBEntities())
        //        {
        //            reval.Add(Constant.ReportHeader.REPORT_NAME, reportName);

        //            if (storeID != 0 && toDate != null)
        //            {
                        
        //                //var store = from c in db.StoreLangs
        //                //            where c.StoreID == storeID && c.LanguageID == langID
        //                //            select c;

        //                reval.Add(Constant.ReportHeader.STORE, store.FirstOrDefault()?.StoreName);

        //            }

        //            string reportDate = null;
        //            if (fromDate != null && toDate != null)
        //            {
        //                //report time get by from,to date
        //                //Từ ngày ……/…….. Đến ngày  ……./……./20…
        //                reportDate = string.Format("Từ ngày {0}  Đến ngày {1}", ((DateTime)fromDate).ToString("dd/MM/yyyy"), ((DateTime)toDate).ToString("dd/MM/yyyy"));
        //            }
        //            else if (fromDate == null && toDate != null)
        //            {
        //                reportDate = string.Format("Đến ngày {0}", ((DateTime)toDate).ToString("dd/MM/yyyy"));
        //            }
        //            else if (fiscalID != null)
        //            {
        //                //Report time get by fiscalid
        //                var fiscal = from c in db.Fiscals
        //                             where c.FiscalID == fiscalID
        //                             select c;
        //                if (fiscal.Any())
        //                {
        //                    reportDate = string.Format("Thời kỳ {0}", fiscal.FirstOrDefault()?.FiscalName);
        //                }
        //            }
        //            if (!string.IsNullOrEmpty(reportDate))
        //            {
        //                reval.Add(Constant.ReportHeader.REPORT_DATE, reportDate);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error("ExcelHelper>GetReportHeader: " + ex);
        //        return null;
        //    }

        //    return reval;
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rptHeader"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetReportHeader(Dictionary<string, object> rptHeader, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var reval = new Dictionary<string, object>();

                string reportDate = string.Empty;
                if (fromDate != null && toDate != null)
                    reportDate = string.Format("Từ ngày {0}  Đến ngày {1}", ((DateTime)fromDate).ToString("dd/MM/yyyy"), ((DateTime)toDate).ToString("dd/MM/yyyy"));
                else
                {
                    if (toDate != null)
                        reportDate = string.Format("Tháng xem: {0}", ((DateTime)toDate).ToString("MM/yyyy"));
                }
                   
                reval.Add("ReportDate", reportDate);

                foreach (var row in rptHeader)
                {
                    switch (row.Key.ToUpper())
                    {
                        case "STORECATEGORYNAME":
                            {
                                reval.Add(row.Key, "Nhóm: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "STORENAME":
                            {
                                reval.Add(row.Key, "Chi nhánh: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "STOCKNAME":
                            {
                                reval.Add(row.Key, "Kho: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "VENDORTYPENAME":
                            {
                                reval.Add(row.Key, "Nhóm nhà cung cấp: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "VENDORNAME":
                            {
                                reval.Add(row.Key, "Nhà cung cấp: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "CUSTOMERTYPENAME":
                            {
                                reval.Add(row.Key, "Nhóm khách hàng: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "CUSTOMERNAME":
                            {
                                reval.Add(row.Key, "Khách hàng: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "PRODUCTNAME":
                            {
                                reval.Add(row.Key, "Hàng hoá: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "PRODUCTCATEGORYNAME":
                            {
                                reval.Add(row.Key, "NHÓM HÀNG HÓA: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "BARCODE":
                            {
                                reval.Add(row.Key, "Barcode: " + row.Value);
                                break;
                            }
                        case "PRODUCTCODE":
                            {
                                reval.Add(row.Key, "Mã hàng: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "UNITNAME":
                            {
                                reval.Add(row.Key, "ĐVT: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "PROVIDERNAME":
                            {
                                reval.Add(row.Key, "BÁC SĨ: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "DEPARTMENTNAME":
                            {
                                reval.Add(row.Key, "KHOA: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "FISCALNAME":
                            {
                                reval.Add(row.Key, "KỲ KẾ TOÁN: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "SHIFTNAME":
                            {
                                reval.Add(row.Key, "CA LÀM VIỆC: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        default:
                            {
                                reval.Add(row.Key, row.Value);
                                break;
                            }
                    }
                }
                return reval;
            }
            catch (Exception ex)
            {
                Logger.Error("ExcelHelper>GetReportHeader: " + ex);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rptHeader"></param>
        /// <param name="reportNewDate"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetReportHeader(Dictionary<string, object> rptHeader, DateTime? reportNewDate)
        {
            try
            {
                var reval = new Dictionary<string, object>();

                string reportDate = string.Empty;
                if (reportNewDate != null)
                    reportDate = string.Format("{0} ", ((DateTime)reportNewDate).ToString("dd/MM/yyyy"));
                else
                {
                    reportDate = string.Format("Tháng xem: {0}", DateTime.Now.ToString("MM/yyyy"));
                }

                reval.Add("ReportDate", reportDate);

                foreach (var row in rptHeader)
                {
                    switch (row.Key.ToUpper())
                    {
                        case "STORECATEGORYNAME":
                            {
                                reval.Add(row.Key, "Nhóm: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "STORENAME":
                            {
                                reval.Add(row.Key, "Chi nhánh: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "STOCKNAME":
                            {
                                reval.Add(row.Key, "Kho: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "VENDORTYPENAME":
                            {
                                reval.Add(row.Key, "Nhóm nhà cung cấp: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "VENDORNAME":
                            {
                                reval.Add(row.Key, "Nhà cung cấp: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "CUSTOMERTYPENAME":
                            {
                                reval.Add(row.Key, "Nhóm khách hàng: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "CUSTOMERNAME":
                            {
                                reval.Add(row.Key, "Khách hàng: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "PRODUCTNAME":
                            {
                                reval.Add(row.Key, "Hàng hoá: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "PRODUCTCATEGORYNAME":
                            {
                                reval.Add(row.Key, "NHÓM HÀNG HÓA: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "BARCODE":
                            {
                                reval.Add(row.Key, "Barcode: " + row.Value);
                                break;
                            }
                        case "PRODUCTCODE":
                            {
                                reval.Add(row.Key, "Mã hàng: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "UNITNAME":
                            {
                                reval.Add(row.Key, "ĐVT: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "PROVIDERNAME":
                            {
                                reval.Add(row.Key, "BÁC SĨ: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "DEPARTMENTNAME":
                            {
                                reval.Add(row.Key, "KHOA: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "FISCALNAME":
                            {
                                reval.Add(row.Key, "KỲ KẾ TOÁN: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        case "SHIFTNAME":
                            {
                                reval.Add(row.Key, "CA LÀM VIỆC: " + (string.IsNullOrEmpty(row.Value.ToString()) ? "Tất cả" : row.Value));
                                break;
                            }
                        default:
                            {
                                reval.Add(row.Key, row.Value);
                                break;
                            }
                    }
                }
                return reval;
            }
            catch (Exception ex)
            {
                Logger.Error("ExcelHelper>GetReportHeader: " + ex);
                return null;
            }
        }
        #endregion

        #region Private Method

        private static void SetBorderCell(ExcelRange cell, OfficeOpenXml.Style.ExcelBorderStyle borderStyle)
        {
            cell.Style.Border.Top.Style = borderStyle;
            cell.Style.Border.Bottom.Style = borderStyle;
            cell.Style.Border.Left.Style = borderStyle;
            cell.Style.Border.Right.Style = borderStyle;
        }

        private static void SetFormatCell(ExcelRange cell, string format)
        {
            if (!string.IsNullOrEmpty(format))
            {
                cell.Style.Numberformat.Format = format;
            }
        }

        private static void SetAlignCell(ExcelRange cell, string align)
        {
            if (!string.IsNullOrEmpty(align))
            {
                switch (align.ToLower())
                {
                    case "center":
                        cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        break;
                    case "right":
                        cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                        break;
                    default:
                        cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        break;
                }
            }
        }

        private static void SetValueCell<T>(ExcelRange cell, T objT, string columnName)
        {
            object cellValue = "";
            if (typeof(T) == typeof(ExpandoObject))
            {
                //Dynamic ExpandoObject
                var objDict = objT as IDictionary<string, object>;
                if (objDict.ContainsKey(columnName))
                {
                    cellValue = objDict[columnName];
                }
            }
            else
            {
                //Object có cấu trúc
                var objProperty = typeof(T).GetProperty(columnName);
                if (objProperty != null)
                {
                    cellValue = objProperty.GetValue(objT);
                }
            }

            cell.Value = cellValue ?? "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cells">fromRow->ToRow, FromCol->ToCol</param>
        /// <param name="data"></param>
        /// <param name="columnDynamic"></param>
        private static void SetValueIntoColumnDynamic<T>(ExcelRange cells, T data, int row, int fromCol, List<string> columnDynamic, string format, string align)
        {
            Logger.Error("**************SetValueIntoColumnDynamic: START ***************");
            try
            {
                int position = fromCol;
                for (int i = 0; i < columnDynamic.Count; i++)
                {
                    position = fromCol + i;

                    SetValueCell(cells[row, position], data, columnDynamic[i]);
                    //---Set HorizontalAlignment
                    SetAlignCell(cells[row, position], align);

                    //---Set Format
                    SetFormatCell(cells[row, position], format);

                    //---Set Border
                    SetBorderCell(cells[row, position], OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("ExcelHelper > SetValueIntoColumnDynamic: " + ex);
            }
            finally
            {
                Logger.Error("**************SetValueIntoColumnDynamic: END ***************");
            }
        }

        #endregion
    }
}