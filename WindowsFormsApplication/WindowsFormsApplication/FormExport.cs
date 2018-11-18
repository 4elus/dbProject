using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office;
using System.Reflection;

namespace WindowsFormsApplication
{
    public partial class FormExport : Form
    {
        private DataGridView dataGridView1;
        private DataSet ds;

        public FormExport(DataGridView dataGridView1, DataSet ds)
        {
            this.dataGridView1 = dataGridView1;
            this.ds = ds;
            InitializeComponent();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            

            if (radioBtnPDF.Checked) {
                
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                sfd.FileName = "export.pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //Объект документа пдф
                    iTextSharp.text.Document doc = new iTextSharp.text.Document();

                    //Создаем объект записи пдф-документа в файл
                    PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));

                    //Открываем документ
                    doc.Open();

                    //Определение шрифта необходимо для сохранения кириллического текста
                    BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

                    //Обход по всем таблицам датасета 
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        //Создаем объект таблицы и передаем в нее число столбцов таблицы из нашего датасета
                        PdfPTable table = new PdfPTable(ds.Tables[i].Columns.Count);

                        //Добавим в таблицу общий заголовок
                        PdfPCell cell = new PdfPCell(new Phrase("БД Tasks", font));

                        cell.Colspan = ds.Tables[i].Columns.Count;
                        cell.HorizontalAlignment = 1;
                        //Убираем границу первой ячейки, чтобы балы как заголовок
                        cell.Border = 0;
                        table.AddCell(cell);

                        //Сначала добавляем заголовки таблицы
                        for (int j = 0; j < ds.Tables[i].Columns.Count; j++)
                        {
                            cell = new PdfPCell(new Phrase(new Phrase(ds.Tables[i].Columns[j].ColumnName, font)));
                            //Фоновый цвет (необязательно, просто сделаем по красивее)
                            cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                            table.AddCell(cell);
                        }

                        //Добавляем все остальные ячейки
                        for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                        {
                            for (int k = 0; k < ds.Tables[i].Columns.Count; k++)
                            {
                                table.AddCell(new Phrase(ds.Tables[i].Rows[j][k].ToString(), font));
                            }
                        }
                        //Добавляем таблицу в документ
                        doc.Add(table);
                    }
                    //Закрываем документ
                    doc.Close();

                    MessageBox.Show("Pdf-документ сохранен");
                }

            }
            else if (radioBtnWord.Checked) {

                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Filter = "Word Documents (*.docx)|*.docx";

                sfd.FileName = "export.docx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    Export_Data_To_Word(dataGridView1, sfd.FileName);
                }

                MessageBox.Show("Word");
            }

        }



        public void Export_Data_To_Word(DataGridView DGV, string filename)
        {

            if (DGV.Rows.Count != 0)
            {
                int RowCount = DGV.Rows.Count;
                int ColumnCount = DGV.Columns.Count;
                Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];

                //add rows
                int r = 0;
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        DataArray[r, c] = DGV.Rows[r].Cells[c].Value;
                    } //end row loop
                } //end column loop

                Word.Document oDoc = new Word.Document();
                oDoc.Application.Visible = true;

                //page orintation
                oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;


                dynamic oRange = oDoc.Content.Application.Selection.Range;
                string oTemp = "";
                for (r = 0; r <= RowCount - 1; r++)
                {
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oTemp = oTemp + DataArray[r, c] + "\t";

                    }
                }

                //table format
                oRange.Text = oTemp;
                object oMissing = Missing.Value;
                object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
                object ApplyBorders = true;
                object AutoFit = true;
                object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;

                oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                      Type.Missing, Type.Missing, ref ApplyBorders,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);

                oRange.Select();

                oDoc.Application.Selection.Tables[1].Select();
                oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.InsertRowsAbove(1);
                oDoc.Application.Selection.Tables[1].Rows[1].Select();

                //header row style
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Tahoma";
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;

                //add header row manually
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = DGV.Columns[c].HeaderText;
                }

                //table style 
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                //header text
                foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                    headerRange.Text = "Header";
                    headerRange.Font.Size = 16;
                    headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }

                //save the file

                oDoc.SaveAs(filename, ref oMissing, ref oMissing, ref oMissing,
    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
    ref oMissing, ref oMissing);

                //NASSIM LOUCHANI
            }
        }
    }
}
