using System.Collections.Generic;

namespace Bebidas.API.Contratos
{

    public class Marca
    {
        public static List<string> Todos()
        {
            return new List<string> { "Ambev", "HocusPocus", "Colorado" };
        }

        public static string Ambev { get { return "Ambev"; } }
        public static string HocusPocus { get { return "HocusPocus"; } }
        public static string Colorado { get { return "Colorado"; } }
    }

    public class TipoCerveja
    {
        public static List<string> Todos()
        {
            return new List<string> { "IPA", "APA", "Golden Ale", "Pilsen" };
        }

        public static string IPA { get { return "IPA"; } }
        public static string APA { get { return "APA"; } }
        public static string Golden { get { return "Golden Ale"; } }
        public static string Pilsen { get { return "Pilsen"; } }
    }
    public class TipoSuco
    {
        public static List<string> Todos()
        {
            return new List<string> { "Integral", "Misto" };
        }
        public static string Integral { get { return "Integral"; } }
        public static string Misto { get { return "Misto"; } }
    }

    public class FormatoApresentacao
    {
        public static List<string> Todos()
        {
            return new List<string> { "Lata", "Garrafa", "Chopp" };
        }

        public static string Lata { get { return "Lata"; } }
        public static string Garrafa { get { return "Garrafa"; } }
        public static string Chopp { get { return "Chopp"; } }
    }

}