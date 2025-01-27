using System;
using System.IO;
using System.Windows.Forms;
using Xceed.Words.NET;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using ClosedXML.Excel;

namespace Editor_De_Texto__Aplicando_Conceitos_De_Pilhas_
{
    public partial class formEditorDeTexto : Form
    {
        public formEditorDeTexto()
        {
            InitializeComponent();
        }
        int AntecedentSteps = 0, NextSteps = 0;
        Controller.Text official_text = new Controller.Text();
        void AntecedentSteps_Change(int value)
        {
            if (value==0)
                AntecedentSteps=0;
            else
                AntecedentSteps = AntecedentSteps + value;
            labelAntecedentSteps.Text = "(" + AntecedentSteps + ")";
        }
        void NextSteps_Change(int value)
        {
            if (value == 0)
                NextSteps = 0;
            else
                NextSteps = NextSteps + value;
            labelNextSteps.Text = "(" + NextSteps + ")";
        }
        string Substituindo(string text, string lastText, string newText)
        {
            string value = "";
            value = official_text.substituindo(text, lastText, newText);
            textBoxLastText.Text = "";
            textBoxNewText.Text = "";
            return value;
        }
        private void buttonAntecedent_Click(object sender, EventArgs e)
        {
            if ((AntecedentSteps != 0 /*&& NextSteps != 0*/) || (AntecedentSteps == 0 && NextSteps == 0))
            {
                cont++;
                if (official_text.AntecedentText() == true)
                {
                    //
                    if (AntecedentSteps != 0)
                        AntecedentSteps_Change(-1);
                    NextSteps_Change(1);
                    richTextBoxText.Text = official_text.ReturnTextValue();
                    EarlyText = official_text.ReturnTextValue();
                }
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if ((NextSteps != 0 /*&& AntecedentSteps != 0*/) || (AntecedentSteps == 0 && NextSteps == 0))
            {
                cont++;
                if (official_text.NextText() == true)
                {
                    //
                    AntecedentSteps_Change(1);
                    if (NextSteps != 0)
                        NextSteps_Change(-1);
                    richTextBoxText.Text = official_text.ReturnTextValue();
                    EarlyText = official_text.ReturnTextValue();
                }
            }
        }

        private void buttonSaving_Click(object sender, EventArgs e)
        {
            if (official_text != null)
            {
                NextSteps_Change(0);
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Define os tipos de arquivo que o usuário pode escolher
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|Word documents (*.docx)|*.docx|Documento HTML (*.html)|*.html|Documento em PDF (*.pdf)|*.pdf|CSS files (*.css)|*.css|Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog.Title = "Editor De Texto | Escolha o formato para salvar";

                // Exibe o dialogo para o usuário
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Obtém a extensão do arquivo escolhido
                    string fileExtension = Path.GetExtension(saveFileDialog.FileName);

                    // Verifica se o formato escolhido é .txt
                    if (fileExtension.Equals(".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        File.WriteAllText(saveFileDialog.FileName, richTextBoxText.Text);
                        MessageBox.Show("Arquivo em formato 'txt' salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    // Verifica se o formato escolhido é .docx
                    else if (fileExtension.Equals(".docx", StringComparison.OrdinalIgnoreCase))
                    {
                        using (var document = DocX.Create(saveFileDialog.FileName))
                        {
                            document.InsertParagraph(richTextBoxText.Text);
                            document.Save();
                        }
                        MessageBox.Show("Arquivo em formato 'docx' salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    // Verifica se o formato escolhido é .pdf
                    else if (fileExtension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        try
                        {
                            // Usando iTextSharp para criar o PDF
                            using (var pdfWriter = new PdfWriter(saveFileDialog.FileName))
                            {
                                using (var pdf = new PdfDocument(pdfWriter))
                                {
                                    var document = new Document(pdf);
                                    // Adiciona o conteúdo do RichTextBox no PDF
                                    document.Add(new Paragraph(richTextBoxText.Text));
                                }
                            }
                            MessageBox.Show("Arquivo em formato 'pdf' salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro ao salvar em PDF: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    // Verifica se o formato escolhido é .html
                    else if (fileExtension.Equals(".html", StringComparison.OrdinalIgnoreCase))
                    {
                        // Salva o conteúdo como HTML simples
                        File.WriteAllText(saveFileDialog.FileName, $"<html><body><pre>{richTextBoxText.Text}</pre></body></html>");
                        MessageBox.Show("Arquivo em formato 'html' salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    // Verifica se o formato escolhido é .css
                    else if (fileExtension.Equals(".css", StringComparison.OrdinalIgnoreCase))
                    {
                        // Salva o conteúdo como arquivo CSS
                        File.WriteAllText(saveFileDialog.FileName, richTextBoxText.Text);
                        MessageBox.Show("Arquivo em formato 'css' salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    // Verifica se o formato escolhido é .xlsx
                    else if (fileExtension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                    {
                        // Usando ClosedXML para criar o arquivo Excel
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Texto");
                            worksheet.Cell(1, 1).Value = richTextBoxText.Text;
                            workbook.SaveAs(saveFileDialog.FileName);
                        }
                        MessageBox.Show("Arquivo em formato 'xlsx' salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Não é possível guardar arquivos neste formato!\n\nEm breve novas actualizações serão adicionadas.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
        private void pictureBoxClearText_Click(object sender, EventArgs e)
        {
            richTextBoxText.Text = string.Empty;
            if (EarlyText != richTextBoxText.Text)
            {
                cont++;
                official_text.AddText(richTextBoxText.Text);
                EarlyText = official_text.ReturnTextValue();
                AntecedentSteps_Change(1);
                if (NextSteps != 0)
                {
                    NextSteps_Change(0);
                    NextSteps = 0;
                }
            }
        }
        private void pictureBoxClearText1_Click(object sender, EventArgs e)
        {
            textBoxLastText.Text = string.Empty;
        }

        private void pictureBoxClearText2_Click(object sender, EventArgs e)
        {
            textBoxNewText.Text = string.Empty;
        }

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            if (richTextBoxText.Text.Trim() != "")
            {
                if (textBoxLastText.Text != "")
                {
                    int substituicoes = official_text.numeroSubstituicoes(richTextBoxText.Text, textBoxLastText.Text, textBoxNewText.Text);
                    richTextBoxText.Text = Substituindo(richTextBoxText.Text, textBoxLastText.Text, textBoxNewText.Text);
                    richTextBoxText_MouseLeave(sender, e);
                    MessageBox.Show("Substituições : "+substituicoes, "Substituindo...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Precisa digitar a informação à substituir", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Primeiro precisa escrever algo no texto principal.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        int cont = 0;
        string EarlyText="";

        private void richTextBoxText_Leave(object sender, EventArgs e)
        {
            if (EarlyText != richTextBoxText.Text)
            {
                cont++;
                official_text.AddText(richTextBoxText.Text);
                EarlyText = official_text.ReturnTextValue();
                AntecedentSteps_Change(1);
                if (NextSteps != 0)
                {
                    NextSteps_Change(0);
                    NextSteps = 0;
                }
            }
        }

        private void richTextBoxText_MouseLeave(object sender, EventArgs e)
        {
            if (EarlyText != richTextBoxText.Text)
            {
                cont++;
                official_text.AddText(richTextBoxText.Text);
                EarlyText = official_text.ReturnTextValue();
                AntecedentSteps_Change(1);
                if (NextSteps != 0)
                {
                    NextSteps_Change(0);
                    NextSteps = 0;
                }
            }
        }

        private void tudoEmMaiúsculasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxText.Text.Trim() != "")
            {
                richTextBoxText.Text = official_text.TudoMaiusculas(richTextBoxText.Text);
                richTextBoxText_MouseLeave(sender, e);
            }
        }

        private void maiúsculasPosPontoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxText.Text.Trim() != "")
            {
                richTextBoxText.Text = official_text.maiusculasPosPonto(richTextBoxText.Text);
                richTextBoxText_MouseLeave(sender, e);
            }
        }

        private void maiúsculasPrimeiroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxText.Text.Trim() != "")
            {
                richTextBoxText.Text = official_text.maiusculasPrimeiro(richTextBoxText.Text);
                richTextBoxText_MouseLeave(sender, e);
            }
        }

        private void tudoEmMaiúsculasToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (richTextBoxText.Text.Trim() != "")
            {
                richTextBoxText.Text = official_text.TudoMaiusculas(richTextBoxText.Text);
                richTextBoxText_MouseLeave(sender, e);
            }
        }

        private void menúsculasPrimeiroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxText.Text.Trim() != "")
            {
                richTextBoxText.Text = official_text.menusculasPrimeiro(richTextBoxText.Text);
                richTextBoxText_MouseLeave(sender, e);
            }
        }

        private void tudoEmMenúsculasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxText.Text.Trim() != "")
            {
                richTextBoxText.Text = official_text.TudoMenusculas(richTextBoxText.Text);
                richTextBoxText_MouseLeave(sender, e);
            }
        }
        
        private void ajustarEspaçosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxText.Text != "")
            {
                richTextBoxText.Text = official_text.ajustarEspacos(richTextBoxText.Text);
                //richTextBoxText.Text = official_text.ajustarEspacosPosPontos(richTextBoxText.Text);
                richTextBoxText_MouseLeave(sender, e);
            }
        }

        private void ajustarPontosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxText.Text.Trim() != "")
            {
                richTextBoxText.Text = official_text.ajustarPontos(richTextBoxText.Text);
                richTextBoxText_MouseLeave(sender, e);
            }
        }

        private void ajustarMaiusculasEMenusculasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxText.Text.Trim() != "")
            {
                richTextBoxText.Text = official_text.ajustarMenusculasEMenusculas(richTextBoxText.Text);
                richTextBoxText_MouseLeave(sender, e);
            }
        }

        private void ajustarTudoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxText.Text.Trim() != "")
            {
                richTextBoxText.Text = official_text.ajustarTextoCompleto(richTextBoxText.Text);
                richTextBoxText_MouseLeave(sender, e);
            }
        }
        private void espaºosPósPontosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxText.Text.Trim() != "")
            {
                richTextBoxText.Text = official_text.ajustarEspacosPosPontos(richTextBoxText.Text);
                richTextBoxText_MouseLeave(sender, e);
            }
        }

        private void retirarEspaçosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxText.Text.Trim() != "")
            {
                richTextBoxText.Text = official_text.removerEspacos(richTextBoxText.Text);
                richTextBoxText_MouseLeave(sender, e);
            }
        }

        private void retirarLetraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxText.Text.Trim() != "")
            {
                richTextBoxText.Text = official_text.removerLetras(richTextBoxText.Text);
                richTextBoxText_MouseLeave(sender, e);
            }
        }

        private void retirarNumerosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxText.Text.Trim() != "")
            {
                richTextBoxText.Text = official_text.removerNumeros(richTextBoxText.Text);
                richTextBoxText_MouseLeave(sender, e);
            }
        }

        private void retirarPontosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBoxText.Text.Trim() != "")
            {
                richTextBoxText.Text = official_text.removerPontos(richTextBoxText.Text);
                richTextBoxText_MouseLeave(sender, e);
            }
        }

        private void retirarCaracteresEspeciaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxText.Text.Trim() != "")
            {
                richTextBoxText.Text = official_text.removerCaracteresEspeciais(richTextBoxText.Text);
                richTextBoxText_MouseLeave(sender, e);
            }
        }

        private void loremIpsumTextoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxText.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercition ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            richTextBoxText_MouseLeave(sender, e);
        }



        //private void tudoEmMaiúsculasToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (richTextBoxText.Text.Trim() != "")
        //    {
        //        richTextBoxText.Text = official_text.TudoMaiusculas(richTextBoxText.Text);
        //        richTextBoxText_MouseLeave(sender, e);
        //    }
        //}

        private void acercaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("O software oferece um conjunto de funcionalidades voltadas a manipulação dinâmica de texto, tendo em conta a preferência do usuário.\nÉ amplamente aplicado o conceito LIFO(LastInFirstOut) das Pilhas(Estruturas De Dados).\n\n\t          Editor De Texto versão 1.2.0\n\t    copyright @J2F AllRightsReserved", cont+"Acerca Do Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
