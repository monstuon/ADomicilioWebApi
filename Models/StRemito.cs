using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADomicilioWebApi.Models
{
    public class StRemito
    {
        public string cIdStRmto { get; set; }
        public string cCodStRmto { get; set; }
        //public string cCodDepoOrigen { get; set; }
        public string cCodDepoDestino { get; set; }
        public DateTime dFecha { get; set; }
        public string cCodCbteTipo { get; set; }
        public string cCbteLetra { get; set; }
        public int nSucursal { get; set; }
        public int nNumero { get; set; }
        public string cCodPvProv { get; set; }
        public int nTercero { get; set; }
        public string cObservacion { get; set; }
        //public string cCodVtListaPrecios { get; set; }
        public string cCodStAcciones { get; set; }
        public int nEsVenta { get; set; }
        public int nFacturado { get; set; }
        public int nAnulado { get; set; }
        public string cCodVtSucu { get; set; }
        public string cConfeccionado { get; set; }
        public DateTime dFechaEmision { get; set; }
        public string cCbteTipoP { get; set; }
        public int nSucursalP { get; set; }
        public int nNumeroP { get; set; }
        public int nDevolucion { get; set; }
        public DateTime dTest { get; set; }
        public string cCodGsUser { get; set; }
        public int nEsInventarioReplicado { get; set; }
        //public string cCodTsTurno { get; set; }
        //public string cCOT { get; set; }
        //public string cErrorCOT { get; set; }
        //public string cTipoRmto { get; set; }
        public string cApellido { get; set; }
        public string cNombre { get; set; }
        public int nMes { get; set; }
        public int nAnio { get; set; }
        //public int nTransaccionId { get; set; }
        //public int nProveedorPrestacionId { get; set; }
        //public string cId_CgAsto { get; set; }
        //public string cCodGsPerfil { get; set; }
        //public string cCodLugarDestino { get; set; }
        //public DateTime dFechaSalidaCOT { get; set; }
        //public string cHoraSalidaCOT { get; set; }
        //public int nProcesadoARBA { get; set; }
        //public string cCodPvProv_Transportista { get; set; }
        //public int nConfirmadoSenasa { get; set; }
        //public string cPlanAfiliado { get; set; }
    }
}

//cIdStRmto varchar(50) Unchecked
//cCodStRmto	varchar(30)	Unchecked
//cCodDepoOrigen	varchar(6)	Checked
//cCodDepoDestino	varchar(6)	Checked
//dFecha	datetime	Checked
//cCodCbteTipo	char(4)	Checked
//cCbteLetra	char(1)	Checked
//nSucursal	int	Checked
//nNumero	int	Checked
//cCodPvProv	char(6)	Checked
//nTercero	smallint	Checked
//cObservacion	varchar(100)	Checked
//cCodVtListaPrecios	varchar(5)	Checked
//cCodStAcciones	varchar(6)	Checked
//nEsVenta	smallint	Checked
//nFacturado	smallint	Checked
//nAnulado	smallint	Checked
//cCodVtSucu	varchar(5)	Checked
//cConfeccionado	varchar(50)	Checked
//dFechaEmision	datetime	Checked
//cCbteTipoP	char(4)	Checked
//nSucursalP	int	Checked
//nNumeroP	int	Checked
//nDevolucion	smallint	Checked
//dTest	datetime	Checked
//cCodGsUser	varchar(50)	Checked
//nEsInventarioReplicado	int	Checked
//cCodTsTurno	varchar(16)	Checked
//cCOT	varchar(600)	Checked
//cErrorCOT	varchar(5000)	Checked
//cTipoRmto	varchar(10)	Checked
//cApellido	varchar(MAX)	Checked
//cNombre	varchar(MAX)	Checked
//nMes	int	Checked
//nAnio	int	Checked
//nTransaccionId	int	Checked
//nProveedorPrestacionId	int	Checked
//cId_CgAsto	char(16)	Checked
//cCodGsPerfil	varchar(6)	Checked
//cCodLugarDestino	varchar(36)	Checked
//dFechaSalidaCOT	datetime	Checked
//cHoraSalidaCOT	varchar(500)	Checked
//nProcesadoARBA	smallint	Checked
//cCodPvProv_Transportista	char(6)	Checked
//nConfirmadoSenasa	smallint	Checked
//cPlanAfiliado	varchar(100)	Checked