using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODAL;

namespace Controller
{
    public class Text
    {
        MODAL.Pilha text = new MODAL.Pilha();
        public Text()
        {
            //text = null;
        }
        public string ReturnTextValue()
        {
            string texto = "";
            if (text.RetornaActual() != null)
            {
                texto = text.RetornaActual().value;
            }
            return texto;
        }
        public void AddText(string texto)
        {
            No elemento = null;
            elemento = new No(texto);
            text.Add(elemento);
        }
        public bool AntecedentText()
        {
            return text.Antecedent();
        }
        public bool NextText()
        {
            return text.Next();
        }
        public string TudoMaiusculas(string texto)
        {
            if (text != null)
                return texto.ToUpper();
            else
                return texto = "";
        }
        public string TudoMenusculas(string texto)
        {
            if (text != null)
                return texto.ToLower();
            else
                return texto = "";
        }
        public string maiusculasPrimeiro(string texto)
        {
            if (text != null)
            {
                string textoAuxiliar = texto;
                texto = textoAuxiliar.Substring(0, 1).ToUpper();
                for (int i = 1; i < textoAuxiliar.Length; i++)
                {
                    if (" ".Contains(textoAuxiliar.Substring(i - 1, 1)) == true)
                        texto += textoAuxiliar.Substring(i, 1).ToUpper();
                    else
                        texto += textoAuxiliar.Substring(i, 1);
                }
                return texto;
            }
            else
                return texto = "";
        }
        public string menusculasPrimeiro(string texto)
        {
            if (text != null)
            {
                string textoAuxiliar = texto;
                texto = textoAuxiliar.Substring(0, 1).ToLower();
                for (int i = 1; i < textoAuxiliar.Length; i++)
                {
                    if (" ".Contains(textoAuxiliar.Substring(i - 1, 1)) == true)
                        texto += textoAuxiliar.Substring(i, 1).ToLower();
                    else
                        texto += textoAuxiliar.Substring(i, 1);
                }
                return texto;
            }
            else
                return texto = "";
        }
        public string maiusculasPosPonto(string texto)
        {
            if (text != null)
            {
                bool control = false;
                string textoAuxiliar = texto, inicio = "";
                int item = 0;
                while (textoAuxiliar.Substring(item, 1).Trim() == "")
                    item++;
                inicio = textoAuxiliar.Substring(0, item);
                textoAuxiliar = textoAuxiliar.Trim();
                texto = textoAuxiliar.Substring(0, 1).ToUpper();


                for (int i = 1;i < textoAuxiliar.Length; i++)
                {
                    if ("!?.:".Contains(textoAuxiliar.Substring(i-1, 1)) == true)
                        control = true;
                    if (control && ("abcdefghijklmnopqrstuvwxyzçáàâãéèêíìîòòôõúùû".Contains(textoAuxiliar.Substring(i, 1).ToLower()) == true))
                    {
                        texto += textoAuxiliar.Substring(i, 1).ToUpper();
                        control = false;
                    }
                    else
                        texto += textoAuxiliar.Substring(i, 1);
                }
                texto = inicio + texto.Trim();
                return texto;
            }
            else
                return texto = "";
        }
        public string ajustarEspacos(string texto)
        {
            if (text != null)
            {
                string[] palavras = texto.Trim().Split(' ');
                texto = "";
                for (int i = 0; i < palavras.Length; i++)
                {
                    if (palavras[i].Trim() != "")
                        if (i == palavras.Length - 1) texto += palavras[i]; else texto += palavras[i] + " ";
                }
                return texto;
            }
            else
                return texto = "";
        }
        public string ajustarEspacosPosPontos(string texto)
        {
            if (text != null)
            {
                //string textoAuxiliar = texto.Replace(" ", "");    //  Porquê era necessário eliminar
                string textoAuxiliar = texto, inicio = "";
                int item = 0;
                while (textoAuxiliar.Substring(item, 1).Trim() == "")
                    item++;
                inicio = textoAuxiliar.Substring(0, item);
                textoAuxiliar = textoAuxiliar.Trim();
                texto = textoAuxiliar.Substring(0, 1);
                for (int i = 1; i < textoAuxiliar.Length; i++)
                {
                    if ("!?.:;,".Contains(textoAuxiliar.Substring(i, 1).ToLower()))
                    {
                        texto = texto.Trim();
                        texto += textoAuxiliar.Substring(i, 1);
                    }
                    else
                    {
                        if ("!?.:;,".Contains(texto.Substring(texto.Length-1, 1)))
                            texto = texto.Trim()+" ";
                        texto += textoAuxiliar.Substring(i, 1);
                    }
                    texto = inicio + texto;
                }
                return texto;
            }
            else
                return texto = "";
        }
        public string ajustarPontos(string texto)
        {
            if (text != null)
            {
                bool control = true;
                bool controlSpace = true;
                string textoAuxiliar = texto;
                texto = textoAuxiliar.Substring(0,1);
                for (int i = 1; i < textoAuxiliar.Length; i++)
                {
                    if ("!?.:;,".Contains(textoAuxiliar.Substring(i, 1)) && control==false)
                        control = true;
                    string actual = textoAuxiliar.Substring(0, i+1).Trim();
                    if ("!?.:;,".Contains(actual.Substring(actual.Length-1, 1).Trim())==false)
                        control = false;
                    if ("!?.:;,".Contains(textoAuxiliar.Substring(i, 1)) && control)
                    {
                        if ("!?.:;,".Contains(textoAuxiliar.Substring(i - 1, 1)))
                        {
                            if (texto.Length > 2)
                                if (texto.Substring(i - 1, 1) == "." && textoAuxiliar.Substring(i, 1)==".")
                                {
                                    if (texto.Length < 2)
                                        texto += "..";
                                    else
                                    {
                                        if (texto.Substring(i - 3, 1) != ".")
                                            texto += "..";
                                    }
                                }
                        }
                        else
                            texto += textoAuxiliar.Substring(i, 1);
                        control = false;
                    }
                    else
                    {
                        if ("!?.:;,".Contains(textoAuxiliar.Substring(textoAuxiliar.Length - 1, 1).Trim())==false)
                            texto += textoAuxiliar.Substring(i, 1);
                        //if ("!?.:;,".Contains(textoAuxiliar.Substring(1, i).Trim())==false)
                        //    texto += textoAuxiliar.Substring(i, 1);
                        //else
                        //    control = false;
                    }
                }
                return texto;
            }
            else
                return texto = "";
        }
        public string ajustarMenusculasEMenusculas(string texto)
        {
            if (text != null)
            {
                texto = TudoMenusculas(texto);
                texto = maiusculasPosPonto(texto);
                return texto;
            }
            else
                return texto = "";
        }
        public string ajustarTextoCompleto(string texto)
        {
            texto = ajustarEspacos(texto);
            texto = ajustarEspacosPosPontos(texto);
            texto = ajustarPontos(texto);
            texto = ajustarMenusculasEMenusculas(texto);
            return texto;
        }
        public string removerEspacos(string texto)
        {
            if (text != null)
            {
                return texto.Replace(" ", "");
            }
            else
                return texto = "";
        }
        public string removerPontos(string texto)
        {
            if (text != null)
            {
                texto = texto.Replace(".", "");
                texto = texto.Replace(",", "");
                texto = texto.Replace(":", "");
                texto = texto.Replace(";", "");
                texto = texto.Replace("!", "");
                texto = texto.Replace("?", "");
                return texto;
            }
            else
                return texto = "";
        }
        public string removerLetras(string texto)
        {
            if (text != null)
            {
                string textoAuxiliar = texto;
                texto = "";
                for (int i = 0; i <  textoAuxiliar.Length; i++)
                    if (("abcdefghijklmnopqrstuvwxyzçáàâãéèêíìîòòôõúùû").Contains(textoAuxiliar.Substring(i, 1).ToLower()) == false)
                        texto += textoAuxiliar.Substring(i, 1);
                return texto;
            }
            else
                return texto = "";
        }
        public string removerNumeros(string texto)
        {
            if (text != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    texto = texto.Replace(i.ToString(), "");
                    texto = texto.Replace((i * -1).ToString(), "");
                }
                return texto;
            }
            else
                return texto = "";
        }
        public string removerCaracteresEspeciais(string texto)
        {
            if (text != null)
            {
                string textoAuxiliar = texto;
                texto = "";
                for (int i = 0; i < textoAuxiliar.Length; i++)
                    if (("abcdefghijklmnopqrstuvwxyz çáàâãéèêíìîòòôõúùû").Contains(textoAuxiliar.Substring(i, 1).ToLower()) || ("1234567890").Contains(textoAuxiliar.Substring(i, 1).ToLower()) || ("!?;:,.-").Contains(textoAuxiliar.Substring(i, 1).ToLower()))
                        texto += textoAuxiliar.Substring(i, 1);
                return texto;
            }
            else
                return texto = "";
        }
        public int numeroCaracteres(string texto)
        {
            if (text != null)
                return texto.Count();
            else
                return 0;
        }
        public int numeroPalavras(string texto)
        {
            if (text != null)
                return texto.Split(' ').Count();
            else
                return 0;
        }
        public int numeroLetras(string texto)
        {
            if (text != null)
            {
                int letras = 0;
                for (int i = 0; i < texto.Length; i++)
                    if (("abcdefghijklmnopqrstuvwxyzçáàâãéèêíìîòòôõúùû").Contains(texto.Substring(i, 1).ToLower()) == true)
                        letras++;
                return letras;
            }
            else
                return 0;
        }
        public int numeroNumeros(string texto)
        {
            if (text != null)
            {
                int numeros = 0;
                for (int i = 0; i < texto.Length; i++)
                    if (("1234567890").Contains(texto.Substring(i, 1).ToLower()) == true)
                        numeros++;
                return numeros;
            }
            else
                return 0;
        }
        public int numeroPontos(string texto)
        {
            if (text != null)
            {
                string textoAuxiliar = "";
                int pontos = 0;
                if (texto.Count() > 3)
                {
                    for (int i = 2; i < texto.Length; i++)
                    {
                        if (texto.Substring(i - 2, 3) == ("..."))
                        {
                            pontos++;
                            texto = texto;
                            i = i + 2;
                        }
                        else
                            textoAuxiliar += texto.Substring(i,1);
                    }
                    texto = textoAuxiliar;
                }
                for (int i = 0; i < texto.Length; i++)
                {
                    if (("!?:;,.").Contains(texto.Substring(i, 1).ToLower()) == true)
                        pontos++;
                }
                return pontos;
            }
            else
                return 0;
        }
        public int numeroEspacos(string texto)
        {
            if (text != null)
            {
                int espacos = 0;
                for (int i = 0; i < texto.Length; i++)
                    if (texto.Substring(i, 1) == " ")
                        espacos++;
                return espacos;
            }
            else
                return 0;
        }
        public int numeroMaiusculas(string texto)
        {
            if (text != null)
            {
                int maiusculas = 0;
                for (int i = 0; i < texto.Length; i++)
                    if (("abcdefghijklmnopqrstuvwxyzçáàâãéèêíìîòòôõúùû").Contains(texto.Substring(i, 1).ToLower()) == true)
                        if (texto.Substring(i, 1) == texto.Substring(i, 1).ToUpper())
                            maiusculas++;
                return maiusculas;
            }
            else
                return 0;
        }
        public int numeroMenusculas(string texto)
        {
            if (text != null)
            {
                int menusculas = 0;
                for (int i = 0; i < texto.Length; i++)
                    if (("abcdefghijklmnopqrstuvwxyzçáàâãéèêíìîòòôõúùû").Contains(texto.Substring(i, 1).ToLower()) == true)
                        if (texto.Substring(i, 1) == texto.Substring(i, 1).ToLower())
                            menusculas++;
                return menusculas;
            }
            else
                return 0;
        }
        public int numeroSubstituicoes(string texto, string antigo, string novo)
        {
            int sub = 0;
            if (text != null)
            {
                for (int i = 0; i < texto.Length; i++)
                {
                    if ((texto.Substring(i, antigo.Length) == antigo))
                    {
                        sub++;
                        i = i + antigo.Length - 1;
                    }
                }
                return sub;
            }
            else
                return 0;
        }
        public string substituindo(string texto, string antigo, string novo)
        {
            if (text!=null)
                return texto.Replace(antigo, novo);
            else
                return "";
        }
    }
}
