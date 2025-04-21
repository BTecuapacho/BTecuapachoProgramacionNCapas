using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICalculadora" in both code and config file together.
    [ServiceContract]
    public interface ICalculadora
    {
        [OperationContract]
        double Suma(double a, double b);
        [OperationContract]
        double Resta(double a, double b);
        [OperationContract]
        double Multiplicacion(double a, double b);
        [OperationContract]
        double Divicion(double a, double b);
    }
}
