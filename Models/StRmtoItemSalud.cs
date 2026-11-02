using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADomicilioWebApi.Models
{
    public class StRmtoItemSalud
    {
        public string cCodStRmtoItem { get; set; }
        public string cIdStRmto { get; set; }
        public string cNombre { get; set; }
        public string cApellido { get; set; }
        public string cTipoDoc { get; set; }
        public string cNroDoc { get; set; }
        public string cNroAfiliado { get; set; }
        public string cNroSiniestro { get; set; }
        public string cNroAutorizacion { get; set; }
        public string cProvincia { get; set; }
        public string cTipoAfiliado { get; set; }
        public string cEmpresa { get; set; }
        public int nMes { get; set; }
        public int nAnio { get; set; }
        //public long nNroLote { get; set; }
        public long nNroOrden { get; set; }
        //public long nNroRendicion { get; set; }
        public decimal nAlicuotaIVA { get; set; }
        //public string cCodDGIAlic { get; set; }
        //public string cCodImZona { get; set; }
        //public string cCodGsPost { get; set; }
        public string cCodPvProv { get; set; }
        //public string cCodFacturacion { get; set; }
        //public string cPlanAfiliado { get; set; }
    }
}

//cCodStRmtoItem varchar(50) Unchecked
//cIdStRmto	varchar(50)	Unchecked
//cNombre	varchar(50)	Checked
//cApellido	varchar(50)	Checked
//cTipoDoc	varchar(50)	Checked
//cNroDoc	varchar(20)	Checked
//cNroAfiliado	varchar(10)	Checked
//cNroSiniestro	varchar(20)	Checked
//cNroAutorizacion	varchar(20)	Checked
//cProvincia	varchar(50)	Checked
//cTipoAfiliado	varchar(20)	Checked
//cEmpresa	varchar(50)	Checked
//nMes	int	Checked
//nAnio	int	Checked
//nNroLote	bigint	Checked
//nNroOrden	bigint	Checked
//nNroRendicion	bigint	Checked
//nAlicuotaIVA	numeric(18, 2)	Checked
//cCodDGIAlic	char(15)	Checked
//cCodImZona	char(10)	Checked
//cCodGsPost	char(10)	Checked
//cCodPvProv	char(6)	Checked
//cCodFacturacion	varchar(50)	Checked
//cPlanAfiliado	varchar(100)	Checked