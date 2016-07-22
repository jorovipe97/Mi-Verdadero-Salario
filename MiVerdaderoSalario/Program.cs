using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiVerdaderoSalario
{
    class Program
    {
        /* Esta aplicacion permite calcular el salario a los trabajadores
         * despues de las deducciones dependiendo del tipo de contrato.
         * */

        // SMMLV 2016: 689 454

        /* INPUTS
         * Tipo de contrato: Contrato laboral, Contrato por prestacion de servicios
         * Salario: El salario que se paga al trabajador
         */
        const int SMMLV_COP = 689454; // 2016

        static void Main(string[] args)
        {
            // APP info
            string AppTitle = "Mi Verdadero Salario";

            // Instancias
            TextFormatter text = new TextFormatter();
            text.CenterTitle(AppTitle, ConsoleColor.Yellow, ConsoleColor.Black);

            // Bucle validador de datos
            string str;
            byte opc;
            int salario;
            bool bin;
            // Tipo de contrato
            do 
            {
                text.Default("¿Que tipo de contrato laboral tienes?");
                text.Default("1: Contrato laboral");
                text.Default("2: Contrato por prestacion de servicios");
                // Input
                str = Console.ReadLine();
                Console.WriteLine("");
                bin = byte.TryParse(str, out opc);
            } while (!bin || (opc != 1 &&  opc != 2));
            // Monto del salario
            do
            {
                text.Default("¿De cuanto es tu salario?");
                
                // Input
                str = Console.ReadLine();
                Console.WriteLine("");
                bin = int.TryParse(str, out salario);
            } while (!bin || !(salario > SMMLV_COP));

            // Modelo y output
            /*
             * En este punto el usuario ya incerto datos correctos
             * 
             * Base cotizacion: Es la base sobre la cual se aplican las deducciones
             * porcentaje_bc: El porcentaje de la base de cotizacion
             */
            float base_cotizacion;
            float porcentaje_bc = 0.4f;
            // Calculando base de cotizacion
            if (salario * porcentaje_bc > SMMLV_COP)
            {
                base_cotizacion = salario * porcentaje_bc;
                text.Default("Tu base de cotizacion para las deducciones es " + base_cotizacion + " (es decir, el 40% de tu salario)");
            }
            else
            {
                base_cotizacion = SMMLV_COP;
                text.Default("Tu base de cotizacion para las deducciones es " + base_cotizacion + " (es decir, 1 SMMLV)");
            }
            // Calculando deducciones
            float pension;
            float EPS;
            float ARL;
            text.LeftTitle("Tu verdadero salario", ConsoleColor.Cyan, ConsoleColor.Black);
            if (opc == 1) // Contrato laboral
            {                
                pension = base_cotizacion * 0.04f;
                EPS = base_cotizacion * 0.04f;
                ARL = 0;

                // Informe
                text.Default("Base de cotizacion: " + base_cotizacion);

                text.TextColorNNL("(-) PENSION: ", ConsoleColor.Red);
                text.Default(pension.ToString("F99").TrimEnd("0".ToCharArray()));

                text.TextColorNNL("(-) EPS: ", ConsoleColor.Red);
                text.Default(EPS.ToString("F99").TrimEnd("0".ToCharArray()));

                text.TextColorNNL("(-) ARL: ", ConsoleColor.Red);
                text.Default(ARL.ToString("F99").TrimEnd("0".ToCharArray()));

                text.Default("Verdadero salario: " + (salario - (base_cotizacion - pension - EPS - ARL)));
            }
            else if (opc == 2)
            {
                pension = base_cotizacion * 0.16f;
                EPS = base_cotizacion * 0.125f;
                ARL = 0;

                // Informe
                text.Default("Base de cotizacion: " + base_cotizacion);

                text.TextColorNNL("(-) PENSION: ", ConsoleColor.Red);
                text.Default(pension.ToString("F99").TrimEnd("0".ToCharArray()));

                text.TextColorNNL("(-) EPS: ", ConsoleColor.Red);
                text.Default(EPS.ToString("F99").TrimEnd("0".ToCharArray()));

                text.TextColorNNL("(-) ARL: ", ConsoleColor.Red);
                text.Default("Consulte la tabla de riesgos laborales y restela de su verdadero salrio");

                text.Default("Verdadero salario: " + (salario - (base_cotizacion - pension - EPS - ARL)));
            }
            else
            {
                text.Default("Eres una especie de mago, ¿como has llegado ha este punto?");
            }


            Console.ReadKey();
        }
    }

    /// <summary>
    /// Formateador de texto, para simplificar muchas tareas de decorado del mismo en la consola
    /// </summary>
    class TextFormatter
    {
        public void CenterTitle(string text, ConsoleColor bgColor, ConsoleColor textColor)
        {
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = textColor;
            Console.WriteLine(text.PadLeft((Console.WindowWidth/2) + (text.Length/2)).PadRight(Console.WindowWidth - 1)); // If you do not subtract one, it sometimes incorrectly wraps lines.
            Console.ResetColor();
        }

        public void LeftTitle(string text, ConsoleColor bgColor, ConsoleColor textColor)
        {
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = textColor;
            Console.WriteLine(text.PadRight(Console.WindowWidth - 1)); // If you do not subtract one, it sometimes incorrectly wraps lines.
            Console.ResetColor();
        }

        public void ColoredLine(ConsoleColor lineColor)
        {
            Console.BackgroundColor = lineColor;
            Console.WriteLine("".PadRight(Console.WindowWidth-1));
            Console.ResetColor();
        }

        /// <summary>
        /// Identico a Console.WriteLine(); su desarrollo se debe por si se decide cambiar el estilo del texto por default en futuras versiones.
        /// </summary>
        /// <param name="text">Texto a imprimir</param>
        public void Default(string text)
        {
            Console.WriteLine(text);
        }

        // NNL signigica no-new-line
        public void TextColorNNL(string text, ConsoleColor textColor)
        {
            Console.ForegroundColor = textColor;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}
