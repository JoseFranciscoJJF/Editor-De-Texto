using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controller;

namespace Editor_De_Texto__Aplicando_Conceitos_De_Pilhas_
{
    public partial class FormCaracteristicas : Form
    {
        public FormCaracteristicas(Text official_text, string text)
        {
            InitializeComponent();
            textoprincipal = official_text;
            texto = text;
        }
        Text textoprincipal = new Text();
        string texto = "";

        private void FormCaracteristicas_Load(object sender, EventArgs e)
        {
            Itens();
        }
        public void BarrasDeProgresso(int numcaracteres, int numletras, int numnumeros, int numpontos, int numespacos, int numcaracteresespeciais)
        {
            progressBarLetras.Value = Convert.ToInt16((100*numletras/numcaracteres));
            progressBarNumeros.Value = Convert.ToInt16((100*numnumeros/numcaracteres));
            progressBarPontos.Value = Convert.ToInt16((100 * numpontos / numcaracteres));
            progressBarEspacos.Value = Convert.ToInt16((100*numespacos / numcaracteres));
            progressBarCaracteresEspeciais.Value = Convert.ToInt16((100 * numcaracteresespeciais / numcaracteres));
        }
        public void Itens()
        {
            labelCaracteres.Text = textoprincipal.numeroCaracteres(texto)+" Caracteres";
            labelPalavras.Text = textoprincipal.numeroPalavras(texto)+" Palavras";
            labelLetras.Text = "Nº De Letras : " + textoprincipal.numeroLetras(texto);
            labelNumeros.Text = "Nº De Números : " + textoprincipal.numeroNumeros(texto);
            labelPontos.Text = "Nº De Pontos : " + textoprincipal.numeroPontos(texto);
            labelEspacos.Text = "Nº Dr Espaços : " + textoprincipal.numeroEspacos(texto);
            int outros = textoprincipal.numeroCaracteres(texto) - (textoprincipal.numeroLetras(texto) + textoprincipal.numeroNumeros(texto) + textoprincipal.numeroPontos(texto) + textoprincipal.numeroEspacos(texto));
            labelCaracteresEspeciais.Text = "Caract. Especiais : " + outros;
            labelMaiusculas.Text = "Nº De Maiúsculas : " + textoprincipal.numeroMaiusculas(texto);
            labelMenusculas.Text = "Nº De Menúsculas : " + textoprincipal.numeroMenusculas(texto);
            BarrasDeProgresso(textoprincipal.numeroCaracteres(texto), textoprincipal.numeroLetras(texto), textoprincipal.numeroNumeros(texto), textoprincipal.numeroPontos(texto), textoprincipal.numeroEspacos(texto), outros);
        }
    }
}
