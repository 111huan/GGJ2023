#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeOpenXml;
using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

public class ExcelReadManager : MonoBehaviour
{
    private string excelPath = "/TowerDefense/Scripts/Data/Editor/Excel";
    public GameObject localData;

    // 限制最多读取行列数，正常有10多万行，太多了
    private int maxRows = 1000; // 行
    private int maxColumns = 40; // 列
    private int maxEmptyRows = 40; // 连续多少行为空截断

    private void RefreshAssetDatabase()
    {
        PrefabUtility.SavePrefabAsset(localData);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public FileInfo GetExcelFileInfo(string filePath, string nameKey)
    {
        FileInfo[] fileInfos = GetAllFileInfo(filePath, "*.xlsx");
        if (fileInfos != null)
        {
            for (int i = 0; i < fileInfos.Length; i++)
            {
                if (fileInfos[i].Name.Contains(nameKey) && new Regex(@"^[^~]+\.xlsx$").IsMatch(fileInfos[i].Name.ToLower()))
                {
                    return fileInfos[i];
                }
            }
        }

        return null;
    }

    public static FileInfo[] GetAllFileInfo(string path, string suffix)
    {
        string fullPath = Application.dataPath + path;
        if (Directory.Exists(fullPath))
        {
            DirectoryInfo direction = new DirectoryInfo(fullPath);
            FileInfo[] files = direction.GetFiles(suffix, SearchOption.AllDirectories);

            return files;
        }
        else
        {
            Debug.LogWarning("目录不存在:" + fullPath);
            return null;
        }
    }

    public string SpaceToOne(string str)
    {
        for (int i = 0; i < 10; i++)
        {
            str = str.Replace("  ", " ");
        }

        return str;
        //Regex regex = new Regex(@"\s{1,}", RegexOptions.IgnoreCase);

        //return regex.Replace(str, " ").Trim();
    }

    /// <summary>
    /// 读取excel
    /// </summary>
    /// <param name="documentPath"></param>
    /// <param name="fileNameKey"></param>
    /// <param name="sheetKey"></param>
    /// <returns></returns>
    public List<Dictionary<string, string>> ReadExcel(string documentPath, string fileNameKey, string sheetKey)
    {
        FileInfo fileInfo = GetExcelFileInfo(documentPath, fileNameKey);
        if (fileInfo == null)
        {
            Debug.LogWarning("找不到excel文件:" + documentPath + " ->  " + fileNameKey);
            return null;
        }

        string tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        File.Copy(fileInfo.FullName, tempFile);

        using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(tempFile)))
        {
            ExcelWorksheets worksheets = excelPackage.Workbook.Worksheets;
            Debug.Log("工作表个数:" + worksheets.Count);

            foreach (ExcelWorksheet worksheet in worksheets)
            {
                Debug.Log($"<color=green>sheet名:</color> {worksheet.Name}, 行数:{worksheet.Cells.Rows}, 列数:{worksheet.Cells.Columns}");

                if (worksheet.Name.Contains(sheetKey))
                {
                    List<int> availableRow = new List<int>();
                    Dictionary<int, string> keys = new Dictionary<int, string>();
                    List<Dictionary<string, string>> sheetDic = new List<Dictionary<string, string>>(); // 获取全部的excelDic

                    List<List<string>> content = new List<List<string>>();
                    for (int i = 2; i < Mathf.Min(maxRows, worksheet.Cells.Rows); i++) // 行
                    {
                        for (int j = 1; j < Mathf.Min(maxColumns, worksheet.Cells.Columns); j++) // 列
                        {
                            object cell = worksheet.Cells[i, j].Value;
                            if (cell != null && string.IsNullOrEmpty(cell.ToString()))
                            {

                            }
                        }
                    }

                    int rowsEmpty = 0; // 连续maxEmptyRows行为空，截断
                    for (int j = 2; j < Mathf.Min(maxRows, worksheet.Cells.Rows); j++) // 行(第一行是注释，忽略)
                    {
                        bool oneLineEmpty = true;

                        Dictionary<string, string> oneLineDic = new Dictionary<string, string>();
                        for (int k = 1; k < Mathf.Min(maxColumns, worksheet.Cells.Columns); k++) // 列
                        {
                            object cell = worksheet.Cells[j, k].Value;



                            if (j == 2 && cell != null && !string.IsNullOrEmpty(cell.ToString())) // 记录key
                            {
                                //Debug.Log($"记录key:{cell}  {k}");
                                keys.Add(k, cell.ToString());
                                availableRow.Add(k);
                            }
                            else if (availableRow.Contains(k)) // 赋值dic
                            {
                                if (cell != null)
                                {
                                    oneLineEmpty = false;
                                }
                                else
                                {
                                    continue;
                                }

                                string value = cell.ToString();
                                // 去掉首尾空格，之后把所有空格替换为不间断空格
                                value = value.TrimStart(' ').TrimEnd(' ').Replace(' ', '\u00A0');
                                //if (keys[k] == "originDesc" || keys[k] == "desc" || keys[k] == "fullDesc")
                                //{

                                //}
                                oneLineDic.Add(keys[k], value);

                                //Debug.Log($"<color=green>{keys[k]} : </color> {cell}");
                                //Debug.Log($"<color=green>第{j}行第{k}列:</color> {cell}");
                            }
                        }

                        if (j != 2)
                        {
                            if (oneLineEmpty)
                            {
                                rowsEmpty++;
                                if (rowsEmpty == maxEmptyRows) // 连续maxEmptyRows行是空行
                                {
                                    break;
                                }
                                continue;
                            }
                            else
                            {
                                rowsEmpty = 0;
                                sheetDic.Add(oneLineDic);
                            }
                        }
                        //Debug.Log("----------------------------------------------------");
                    }

                    return sheetDic;
                }
            }
        }

        return null;
    }
}
#endif