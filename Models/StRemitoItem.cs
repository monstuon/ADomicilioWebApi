using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADomicilioWebApi.Models
{
    public class StRemitoItem
    {
        public string cCodStRmtoItem { get; set; }
        public string cIdStRmto { get; set; }
        public string cCodProd { get; set; }
        public decimal nCantidad { get; set; }
        public decimal nPrecioUnitario { get; set; }
        public decimal nAplicado { get; set; }
        //public long nIdProdLote { get; set; }
        public decimal nCosto { get; set; }
        public int nOrden { get; set; }
        //public string cCodPdDest { get; set; }
        //public string cCodAgCultivo { get; set; }
    }
}

//cCodStRmtoItem varchar(50) Unchecked
//cIdStRmto	varchar(50)	Unchecked
//cCodProd	char(14)	Unchecked
//nCantidad	money	Checked
//nPrecioUnitario	money	Checked
//nAplicado	money	Checked
//nIdProdLote	bigint	Checked
//nCosto	money	Checked
//nOrden	int	Checked
//cCodPdDest	char(4)	Checked
//cCodAgCultivo	varchar(6)	Checked