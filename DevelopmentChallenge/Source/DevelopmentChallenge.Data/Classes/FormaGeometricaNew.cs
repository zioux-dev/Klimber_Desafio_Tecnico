/******************************************************************************************************************/
/******* ¿Qué pasa si debemos soportar un nuevo idioma para los reportes, o agregar más formas geométricas? *******/
/******************************************************************************************************************/

/*
 * TODO: 
 * Refactorizar la clase para respetar principios de la programación orientada a objetos. -->OK
 * Implementar la forma Trapecio/Rectangulo. -->OK
 * Agregar el idioma Italiano (o el deseado) al reporte. -->OK
 * Se agradece la inclusión de nuevos tests unitarios para validar el comportamiento de la nueva funcionalidad agregada (los tests deben pasar correctamente al entregar la solución, incluso los actuales.)
 * -->OK
 * Una vez finalizado, hay que subir el código a un repo GIT y ofrecernos la URL para que podamos utilizar la nueva versión :).
 * 
 * 
 * DONE
 * Se creo una nueva clase NEW de FormaGeometrica solo con el efecto de ir comparando lo que hacia con lo que se refactorizo, ademas de conservar sus test unitarios anteriores.
 * Dentro de esta clase, estan las calses hijas, solo para el efecto de ver mas rapido el codigo, si no deberian estar en .cs separadas a mi forma de verlo
 * Dentro de la misma clase de unit test, estan los nuevos test
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevelopmentChallenge.Data.Classes
{
    //# Pasa a ser una clase abstracta para crear las formas de manera mas optima, donde se aplicara polimorfismo
    public abstract class FormaGeometricaNew 
    {
        //# Se agregan enums para el manejo de datos de tipos e idiomas
        public enum Tipos
        {
            Cuadrado = 1,
            TrianguloEquilatero = 2,
            Circulo = 3,
            Trapecio = 4,
            Rectangulo = 5
        }

        public enum Idiomas
        {
            Castellano = 1,
            Ingles = 2,
            Italiano = 3
        }

        //# Metodos abstractos para que cada forma las implemente
        public abstract decimal CalcularArea();
        public abstract decimal CalcularPerimetro();

        public int Tipo { get; set; }

        #region Constructor
        protected FormaGeometricaNew(int tipo)
        {
            Tipo = tipo;
        }

        //Alternativa de constructor que podria ser otro padtron como builder o factory
        //public static FormaGeometricaNew CrearCuadrado(decimal lado)
        //{
        //    return new Cuadrado(lado);
        //}

        //public static FormaGeometricaNew CrearCirculo(decimal radio)
        //{
        //    return new Circulo(radio);
        //}

        //public static FormaGeometricaNew CrearTrianguloEquilatero(decimal lado)
        //{
        //    return new TrianguloEquilatero(lado);
        //}

        //public static FormaGeometricaNew CrearTrapecio(decimal baseMenor, decimal baseMayor, decimal altura, decimal lado)
        //{
        //    return new Trapecio(baseMenor, baseMayor, altura, lado);
        //}

        //public static FormaGeometricaNew CrearRectangulo(decimal baseRectangulo, decimal altura)
        //{
        //    return new Rectangulo(baseRectangulo, altura);
        //}
        #endregion


        //# Metodos estaticos que no necesitan instancia
        public static string Imprimir(List<FormaGeometricaNew> formas, int idioma)
        {
            var sb = new StringBuilder();

            if (!formas.Any())
            {
                //Valida idioma y la no existencia de formas
                switch ((Idiomas)idioma)
                {
                    case Idiomas.Castellano:
                        sb.Append("<h1>Lista vacía de formas!</h1>");
                        break;
                    case Idiomas.Ingles:
                        sb.Append("<h1>Empty list of shapes!</h1>");
                        break;
                    case Idiomas.Italiano:
                        sb.Append("<h1>Lista vuota di forme!</h1>");
                        break;                    
                    default:                        
                        sb.Append("<h1>Unsupported language!</h1>");
                        break;
                }
            }
            else
            {
                // Hay por lo menos una forma
                ObtenerHeader(sb, idioma);

                List<FormaGeometricaNew> listaFormas = new List<FormaGeometricaNew>();
                
                foreach (var forma in formas)
                {
                    listaFormas.Add(forma);
                }

                // Calculo por tipo de forma
                var formasAgrupadasPorTipo = listaFormas.GroupBy(forma => forma.Tipo);
                foreach (var grupo in formasAgrupadasPorTipo)
                {
                    int tipoForma = grupo.Key;
                    var formasDelMismoTipo = grupo.ToList();

                    decimal sumaAreas = formasDelMismoTipo.Sum(forma => forma.CalcularArea());
                    decimal sumaPerimetros = formasDelMismoTipo.Sum(forma => forma.CalcularPerimetro());

                    sb.Append(ObtenerLineayTraducir(formasDelMismoTipo.Count, sumaAreas, sumaPerimetros, tipoForma, idioma));
                }

                ObtenerFooter(sb, listaFormas, idioma);
            }

            return sb.ToString();
        }

        private static void ObtenerHeader(StringBuilder sb, int idioma)
        {
            switch ((Idiomas)idioma)
            {
                case Idiomas.Castellano:
                    sb.Append("<h1>Reporte de Formas</h1>");
                    break;
                case Idiomas.Ingles:
                    sb.Append("<h1>Shapes report</h1>");
                    break;
                case Idiomas.Italiano:
                    sb.Append("<h1>Rapporto delle Forme</h1>");
                    break;
            }
        }

        private static string ObtenerLineayTraducir(int cantidad, decimal area, decimal perimetro, int tipo, int idioma)
        {      
            switch (idioma)
            {
                case (int)Idiomas.Castellano:
                    return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimetro {perimetro:#.##} <br/>";

                case (int)Idiomas.Ingles:
                    return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimeter {perimetro:#.##} <br/>";

                case (int)Idiomas.Italiano:
                    return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimetrazione {perimetro:#.##} <br/>";

                default:
                    return string.Empty;
            }
            
        }

        private static string TraducirForma(int tipo, int cantidad, int idioma)
        {
            string nombre = string.Empty;

            switch ((Tipos)tipo)
            {
                case Tipos.Cuadrado:
                    nombre = cantidad == 1 ? "Cuadrado" : "Cuadrados";
                    break;
                case Tipos.TrianguloEquilatero:
                    nombre = cantidad == 1 ? "Triangulo" : "Triangulos";
                    break;
                case Tipos.Circulo:
                    nombre = cantidad == 1 ? "Circulo" : "Circulos";
                    break;
                case Tipos.Trapecio:
                    nombre = cantidad == 1 ? "Trapecio" : "Trapecios";
                    break;
                case Tipos.Rectangulo:
                    nombre = cantidad == 1 ? "Rectangulo" : "Rectangulos";
                    break;
                default:
                    break;
            }

            switch ((Idiomas)idioma)
            {
                case Idiomas.Castellano:
                    return nombre;
                case Idiomas.Ingles:
                    return nombre == "Cuadrados" ? "Squares" :
                           nombre == "Triangulos" ? "Triangles" :
                           nombre == "Circulos" ? "Circles" :
                           nombre == "Trapecios" ? "Trapezoids" :
                           nombre == "Rectángulos" ? "Rectangles" :
                           nombre;
                case Idiomas.Italiano:
                    return nombre == "Cuadrados" ? "Quadrati" :
                           nombre == "Triangulos" ? "Triangoli" :
                           nombre == "Circulos" ? "Cerchi" :
                           nombre == "Trapecios" ? "Trapezi" :
                           nombre == "Rectangulos" ? "Rettangoli" :
                           nombre;
                default:
                    return nombre;
            }
        }

        private static void ObtenerFooter(StringBuilder sb, List<FormaGeometricaNew> formas, int idioma)
        {
            sb.Append("TOTAL:<br/>");
            sb.Append(formas.Count + " ");

            switch ((Idiomas)idioma)
            {
                case Idiomas.Castellano:
                    sb.Append("formas ");
                    sb.Append("Perimetro ");
                    break;
                case Idiomas.Ingles:
                    sb.Append("shapes ");
                    sb.Append("Perimeter ");
                    break;
                case Idiomas.Italiano:
                    sb.Append("forme ");
                    sb.Append("Perimetro ");
                    break;                
            }

            sb.Append(CalcularTotalPerimetro(formas).ToString("#.##") + " ");
            sb.Append("Area " + CalcularTotalArea(formas).ToString("#.##"));
        }

        private static decimal CalcularTotalArea(List<FormaGeometricaNew> formas)
        {
            return formas.Sum(f => f.CalcularArea());
        }

        private static decimal CalcularTotalPerimetro(List<FormaGeometricaNew> formas)
        {
            return formas.Sum(f => f.CalcularPerimetro());
        }

    }

    //# Herencia de la clase padre abstracta
    public class Cuadrado : FormaGeometricaNew 
    {
        private readonly decimal _lado;

        public Cuadrado(decimal lado) : base((int)Tipos.Cuadrado)
        {
            _lado = lado;
        }

        public override decimal CalcularArea()
        {
            return _lado * _lado;
        }

        public override decimal CalcularPerimetro()
        {
            return _lado * 4;
        }
    }

    public class Circulo : FormaGeometricaNew
    {
        
        private readonly decimal _radio;

        public Circulo(decimal radio) : base((int)Tipos.Circulo)
        {
            _radio = radio;
        }

        public override decimal CalcularArea()
        {
            return (decimal)Math.PI * (_radio / 2) * (_radio / 2);
        }

        public override decimal CalcularPerimetro()
        {
            return (decimal)Math.PI * _radio;
        }
    }

    public class TrianguloEquilatero : FormaGeometricaNew
    {
        private readonly decimal _lado;

        public TrianguloEquilatero(decimal lado) : base((int)Tipos.TrianguloEquilatero)
        {
            _lado = lado;
        }

        public override decimal CalcularArea()
        {
            return ((decimal)Math.Sqrt(3) / 4) * _lado * _lado;
        }

        public override decimal CalcularPerimetro()
        {
            return _lado * 3;
        }
    }

    //# Nuevas formas implementadas
    public class Trapecio : FormaGeometricaNew
    {
        private readonly decimal _baseMayor;
        private readonly decimal _baseMenor;
        private readonly decimal _altura;
        private readonly decimal _lado;

        public Trapecio(decimal baseMayor, decimal baseMenor, decimal altura, decimal lado) : base((int)Tipos.Trapecio)
        {
            _baseMayor = baseMayor;
            _baseMenor = baseMenor;
            _altura = altura;
            _lado = lado;
        }

        public override decimal CalcularArea()
        {
            return ((_baseMayor + _baseMenor) / 2) * _altura;
        }

        public override decimal CalcularPerimetro()
        {
            return _baseMayor + _baseMenor + _lado + _lado;
        }
    }

    public class Rectangulo : FormaGeometricaNew
    {
        private readonly decimal _base;
        private readonly decimal _altura;

        public Rectangulo(decimal baseRectangulo, decimal altura) : base((int)Tipos.Rectangulo)
        {
            _base = baseRectangulo;
            _altura = altura;
        }

        public override decimal CalcularArea()
        {
            return _base * _altura;
        }

        public override decimal CalcularPerimetro()
        {
            return 2 * (_base + _altura);
        }

    }
}
