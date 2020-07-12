using System.Collections.Generic;

namespace Bebidas.API.Contratos
{

    public class Fabricante
    {
        public Fabricante(string nome)
        {
            this.Nome = nome;
        }

        public string Nome { get; }

        public static List<string> Todos()
        {
            return new List<string> { 
                nameof(Ambev), 
                nameof(HocusPocus), 
                nameof(Colorado) };
        }

        public static Fabricante Ambev { get { return new Fabricante(nameof(Ambev)); } }
        public static Fabricante HocusPocus { get { return new Fabricante(nameof(HocusPocus)); } }
        public static Fabricante Colorado { get { return new Fabricante(nameof(Colorado)); } }
    }

    public class TipoCerveja
    {
        public string Estilo { get; }

        public TipoCerveja(string estilo)
        {
            Estilo = estilo;
        }


        public static List<string> Todos()
        {
            return new List<string> { nameof(IPA), nameof(APA), "Golden Ale", nameof(Pilsen) };
        }

        public static TipoCerveja IPA { get { return new TipoCerveja(nameof(IPA)); } }
        public static TipoCerveja APA { get { return new TipoCerveja(nameof(APA)); } }
        public static TipoCerveja Golden { get { return new TipoCerveja("Golden Ale"); } }
        public static TipoCerveja Pilsen { get { return new TipoCerveja(nameof(Pilsen)); } }
    }


    public static class FormatoApresentacao
    {
        public static string Chopp { get { return nameof(Chopp); } }
        public static string Garrafa { get { return nameof(Garrafa); } }
        public static string Lata { get { return nameof(Lata); } }

        public static List<string> Todos()
        {
            return new List<string> { nameof(Lata), nameof(Garrafa), nameof(Chopp) };
        }
    }

}