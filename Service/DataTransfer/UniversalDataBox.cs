using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using Ricompany.CommonFunctions;

namespace Pimark.MpeTestingSuite.Service
{
    public class UniversalDataBox
    {
        //represents universal data storage to read from db where you don't know what is in db
        public List<HeaderRowElement> HeaderRow = new List<HeaderRowElement>();

        public List<UniversalDataRow> Rows = new List<UniversalDataRow>();

        public static UniversalDataBox GetFromSqlReader(SqlDataReader reader)
        {

            UniversalDataBox box = new UniversalDataBox();
            try
            {
                if (reader.HasRows)
                {
                    IDataRecord rec;

                    bool headerRead = false;

                    while (reader.Read())
                    {
                        rec = (IDataRecord)reader;

                        if (!headerRead)
                        {
                            for (int i = 0; i < rec.FieldCount; i++)
                            {
                                box.AddHeaderRowElement(rec.GetName(i), rec.GetFieldType(i));
                            }
                            headerRead = true;
                        }

                        UniversalDataRow row = new UniversalDataRow();
                        object[] objarr = new object[rec.FieldCount];
                        int x = rec.GetValues(objarr);
                        row.AddRowItem(objarr.ToList());
                        box.AddRow(row);
                    }
                }
                box.Status = CommonOperationResult.SayOk();
            }
            catch (Exception ex)
            {
                box.Status = CommonOperationResult.SayFail(ex.Message);
            }
            return box;
        }

        public static UniversalDataBox GetFromSqlReader2(Microsoft.Data.SqlClient.SqlDataReader reader)
        {
            //SMO

            UniversalDataBox box = new UniversalDataBox();
            try
            {
                if (reader.HasRows)
                {
                    IDataRecord rec;

                    bool headerRead = false;

                    while (reader.Read())
                    {
                        rec = (IDataRecord)reader;

                        if (!headerRead)
                        {
                            for (int i = 0; i < rec.FieldCount; i++)
                            {
                                box.AddHeaderRowElement(rec.GetName(i), rec.GetFieldType(i));
                            }
                            headerRead = true;
                        }

                        UniversalDataRow row = new UniversalDataRow();
                        object[] objarr = new object[rec.FieldCount];
                        int x = rec.GetValues(objarr);
                        row.AddRowItem(objarr.ToList());
                        box.AddRow(row);
                    }
                }
                box.Status = CommonOperationResult.SayOk();
            }
            catch (Exception ex)
            {
                box.Status = CommonOperationResult.SayFail(ex.Message);
            }
            return box;
        }

        public static List<UniversalDataBox> GetFromSqlReader3(System.Data.DataSet raw)
        {
            //SMO
            List<UniversalDataBox> uni = new List<UniversalDataBox>();

            UniversalDataBox box;
            UniversalDataRow targetRow = null;
            object[] sourceRowItemArray;


            foreach (System.Data.DataTable t in raw.Tables)
            {

                box = new UniversalDataBox();

                //header row
                foreach (DataColumn column in t.Columns)
                {
                    var hre = new HeaderRowElement();
                    hre.FieldName = column.ColumnName;
                    hre.FieldType = column.DataType;
                    box.HeaderRow.Add(hre);
                }

                //other rows
                foreach (DataRow srcRow in t.Rows)
                {
                    targetRow = new UniversalDataRow();

                    srcRow.ItemArray.ToList().ForEach(x => { targetRow.AddRowItem(x); });

                    box.AddRow(targetRow);
                }
                uni.Add(box);
            }

            return uni;
        }

        public void AddRow(UniversalDataRow row)
        {
            Rows.Add(row);
        }

        public void AddHeaderRowElement(string fieldName, Type fieldType)
        {
            HeaderRow.Add(new HeaderRowElement { FieldName = fieldName, FieldType = fieldType });
        }

        public class UniversalDataRow
        {
            public List<object> RowElements { get; set; } = new List<object>();

            public void AddRowItem(object element)
            {
                if (element.GetType() == typeof(System.DBNull)) element=null;
                RowElements.Add(element);
            }
        }

        public class HeaderRowElement
        {
            public string FieldName { get; set; }

            [System.Xml.Serialization.XmlIgnore]
            public Type FieldType { get; set; }

        }
        [System.Xml.Serialization.XmlIgnore]
        public int RowCount { get { return Rows.Count; } }

        [System.Xml.Serialization.XmlIgnore]
        public int ColumnCount { get { return HeaderRow.Count; } }

        public CommonOperationResult Status { get; set; }

        public string AsTableString(int minRowLength = 10, int maxRowLength = 30, bool showHeader = true, int intercolSpaceSize = 7, bool allowEmptyRows = true)
        {
            StringBuilder sb = new StringBuilder();

            //length's array for all columns
            int[] columnLengthArr = new int[ColumnCount];
            int maxLen = 0;
            int maxLen1 = 0;
            int maxLen2 = 0;

            for (int i = 0; i < ColumnCount; i++)
            {
                maxLen1 = HeaderRow.Max(x => x.FieldName.Length);
                maxLen2 = Rows.Count > 0 ? Rows.Max(x => Fn.Isn(x.RowElements[i]).Length) : 0;
                maxLen = (maxLen1 > maxLen2) ? maxLen1 : maxLen2;
                if (maxLen < minRowLength) { maxLen = minRowLength; }
                if (maxLen > maxRowLength) { maxLen = maxRowLength; }
                columnLengthArr[i] = maxLen;
            }

            int totalLength = columnLengthArr.Sum() + intercolSpaceSize * (columnLengthArr.Length - 1);
            string rowSeparator = string.Join("", Enumerable.Repeat("-", totalLength));

            // print header
            sb.AppendLine(rowSeparator);
            sb.AppendLine(string.Join("", HeaderRow.Select(x => {
                int i = HeaderRow.IndexOf(x);
                return FullRowString(x.FieldName, columnLengthArr[i], intercolSpaceSize);
            })));
            sb.AppendLine(rowSeparator);

            //print lines 
            Rows.ForEach(row => {
                sb.AppendLine(string.Join("", row.RowElements.Select(re => {
                    int i = row.RowElements.IndexOf(re);
                    return FullRowString(Fn.Isn(re), columnLengthArr[i], intercolSpaceSize);
                    //return re.ToString();
                })));
            });
            sb.AppendLine("");
            sb.AppendLine("");
            return sb.ToString();
        }

        public string AsCsvString(string csvSeparator)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("**************************************************");
            sb.AppendLine(string.Join(csvSeparator, HeaderRow.Select(x => x.FieldName)));
            Rows.ForEach(row => {
                sb.AppendLine(string.Join(csvSeparator, row.RowElements.Select(re => Fn.Isn(re))));
            });
            return sb.ToString();
        }


        private string FullRowString(string source, int maxlen, int colSpace)
        {
            int delta = maxlen - source.Length;

            if (delta <0)
            {
                return source.Substring(0, maxlen);
            }
            else
            {
                return $"{source}{string.Join("",Enumerable.Repeat(" ", delta+ colSpace))}";
            }
        }
        public void Dump (Serilog.ILogger logger, string separator)
        {
            logger.Information( string.Join(separator, HeaderRow.Select(x => x.FieldName).ToArray()));
            Rows.ForEach(row => {
                logger.Information(string.Join(separator, row.RowElements.Select(x => x.ToString())));
                });
        }

    }

}
