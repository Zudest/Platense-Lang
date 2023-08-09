using System.Linq.Expressions;
using System.Text;

//ABDEFGIJKLMNÑOPŘRSTUÇY
//abdefgijklmnñopřstuçy

string version = "v1.0";

Console.OutputEncoding = Encoding.UTF8;
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("Conversor ortográfico Español - Platense (" + version + ")");

bool commandC = false;

bool bucle = true;
while (bucle)
{
    Console.WriteLine();
    Console.WriteLine("==================================");
    Console.WriteLine("Ingrese el texto en Español:");

    Console.ForegroundColor = ConsoleColor.Gray;
    string text = Console.ReadLine().ToString();
    Console.ForegroundColor = ConsoleColor.White;

    text = text.Trim();

    if(text.StartsWith("/"))
    {
        string commandReply = "";

        // es un comando
        switch(text.ToLower())
        {
            case "/c":
                commandReply = "/c - se copiará la respuesta en Platense al portapapeles.";
                commandC = !commandC;
                break;

            default:
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Comando inválido. Los comandos disponibles son: /c");
                Console.ForegroundColor = ConsoleColor.White;
                continue;
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Comando: " + commandReply);
        Console.ForegroundColor = ConsoleColor.White;

        continue;
    }

    StringBuilder sb = new StringBuilder(text);

    if (sb.Length > 0)
    {
        // vocales mayuscula no usan tilde
        sb.Replace("Á", "A");
        sb.Replace("É", "E");
        sb.Replace("Í", "I");
        sb.Replace("Ó", "O");
        sb.Replace("Ú", "U");

        // se eliminan estos simbolos
        sb.Replace("¿", "");
        sb.Replace("¡", "");

        // inclusion de <ř>
        sb.Replace("rr", "ř");
        sb.Replace(" r", " ř");
        sb.Replace("RR", "Ř");
        sb.Replace(" R", " Ř");

        // validacion de <ř> en la primera letra de toda la cadena
        if (sb.ToString(0, 1) == "r")
        {
            sb.Remove(0, 1).Insert(0, "ř");
        }
        else if (sb.ToString(0, 1) == "R")
        {
            sb.Remove(0, 1).Insert(0, "Ř");
        }

        // inclusion de <ç>
        sb.Replace("ch", "ç");
        sb.Replace("Ch", "Ç");
        sb.Replace("CH", "Ç");

        // unificacion de <ll> en <y>
        sb.Replace("ll", "y");
        sb.Replace("Ll", "Y");
        sb.Replace("LL", "Y");

        // eliminacion de la h
        sb.Replace("h", "");
        sb.Replace("H", "");

        // simplificacion de <bv> en <b>
        sb.Replace("bv", "b");

        // unificacion de <v> en <b>
        sb.Replace("v", "b");
        sb.Replace("V", "B");

        // armonizacion de la regla gue-gui
        sb.Replace("gue", "ge");
        sb.Replace("Gue", "Ge");
        sb.Replace("GUE", "GE");
        sb.Replace("gui", "gi");
        sb.Replace("Gui", "Gi");
        sb.Replace("GUI", "GI");

        // eliminacion de la dieresis
        sb.Replace("ü", "u");
        sb.Replace("U", "U");

        // unificacion de <q> en <k>
        sb.Replace("qu", "k");
        sb.Replace("QU", "K");
        sb.Replace("q", "k");
        sb.Replace("Q", "K");

        // unificacion de <z> en <s>
        sb.Replace("z", "s");
        sb.Replace("Z", "S");

        // transformacion de <y> en <i> como vocal
        sb.Replace("y ", "i ");
        sb.Replace("Y ", "I "); 
        sb.Replace("y,", "i,");
        sb.Replace("Y,", "I,");
        sb.Replace("y;", "i;");
        sb.Replace("Y;", "I;");
        sb.Replace("y.", "i.");
        sb.Replace("Y.", "I.");
        sb.Replace("y:", "i:");
        sb.Replace("Y:", "I:");

        // validacion de <y> en la ultima letra de toda la cadena
        if (sb.Length > 2)
        {
            if (sb.ToString(sb.Length - 1, 1) == "y")
            {
                sb.Remove(sb.Length - 1, 1).Insert(sb.Length, "i");
            }
            if (sb.ToString(sb.Length - 1, 1) == "Y")
            {
                sb.Remove(sb.Length - 1, 1).Insert(sb.Length, "I");
            }
        }
        
        // <c> es /s/ si va antes de las vocales <e>, <i>
        sb.Replace("ce", "se");
        sb.Replace("Ce", "Se");
        sb.Replace("CE", "SE");
        sb.Replace("ci", "si");
        sb.Replace("Ci", "Si");
        sb.Replace("CI", "SI");

        // <c> para el resto es /k/ (cuando va antes de <a>, <o>, <u>, consonante, fin de palabra)
        sb.Replace("c", "k");
        sb.Replace("C", "K");

        // transformacion de <w> en <gu>
        sb.Replace("we", "gue");
        sb.Replace("We", "Gue");
        sb.Replace("WE", "GUE");
        sb.Replace("wi", "gui");
        sb.Replace("Wi", "Gui");
        sb.Replace("WI", "GUI");
        sb.Replace("wa", "gua");
        sb.Replace("Wa", "Gua");
        sb.Replace("WA", "GUA");
        sb.Replace("wo", "guo");
        sb.Replace("Wo", "Guo");
        sb.Replace("WO", "GUO");

        //la x dependiendo del caso puede ser "s", "ks" o incluso "j" (los j quedan aparte para palabras concretas como mejiko)

        // <x> es /s/ al principio de la palabra
        sb.Replace(" x", " s");
        sb.Replace(" X", " S");

        // <x> para el resto es /ks/ (entre vocales (exito), antes de h (exhibir), fin de palabra (relax), fin de silaba seguida de consonante (excusa))
        sb.Replace("x", "ks");
        sb.Replace("X", "KS");


        // FALTANTES Y PENDIENTES EN ESTE CONVERSOR:

        // tildes
        // <x> es /j/ en algunas palabras concretas (mexico, oaxaca)
        // <x> es /s/ si se la antepone con un prefijo (antixenofobo)
    }

    Console.WriteLine();
    if (commandC)
    {
        Console.WriteLine("Su teksto en Platense (se kopió al portapapeles):");
        TextCopy.ClipboardService.SetText(sb.ToString());
    }
    else
    {
        Console.WriteLine("Su teksto en Platense:");
    }
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(sb);
    Console.ForegroundColor = ConsoleColor.White;

    

}

Console.Clear();