using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private void buttonRemember_Click(object sender, EventArgs e)
        {
            if (official_text != null)
            {
                NextSteps_Change(0);
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
