using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADomicilioWebApi.Models.ViewModels
{
    public class OrdenItemViewModel
    {
        public string Codigo { get; set; }
        public decimal Costo { get; set; }
        public decimal Alicuota { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Cantidad { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public string CodProv { get; set; }
        public string CodigoFacturacion { get; set; }
        public string DescripcionFacturacion { get; set; }
    }
}

//{
//    "NroOrden":"100001234",
//   "Fecha":"yyyy-MM-ddTHH:mm:ss.fffZ",
//   "CodProv":"00200",
//   "Apellido":"Natalia",
//   "Nombre":"Natalia",
//   "TipoAfiliado":"Obligatorio",
//   "ZonaImpositiva":"001",
//   "TipoDocumento":"DNI",
//   "NroDocumento":"29123123",
//   "NroAfiliado":"10001",
//   "NroSiniestro":"1111222",
//“NroAutorizacion:””,
//   "Empresa":"Covid",
//   "Provincia":"Buenos Aires",
//   "CodPost":"7600",
//   "Operador":"fulano",
//   "Items":[
//      {
//        "Codigo":"004.17",
//         "Costo":500.0,
//         "Alicuota":21.0,
//         "PrecioInitario":605.00,
//         "Cantidad":10,
//         "Mes":10,
//         "Anio":2019,
//         "CodProv":"000214",
//      },
//      {
//        "Codigo":"IN.00161",
//         "Costo":5000.0,
//         "Alicuota":21.0,
//         "PrecioInitario":60500.00,
//         "Cantidad":1,
//         "Mes":10,
//         "Anio":2019,
//         "CodProv":"000002"
//      }
//   ]
//}