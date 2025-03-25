using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_Soap
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Calculadora" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Calculadora.svc or Calculadora.svc.cs at the Solution Explorer and start debugging.
    public class Calculadora : ICalculadora
    {
        public double Suma(double a, double b) => (a + b);
        public double Resta(double a, double b) => (a - b);
        public double Multiplicacion(double a, double b) => (a * b);
        public double Divicion(double a, double b)
        {
            if (b == 0){
                return 0;
            }
            return (a / b);
        }

    }
}

