using ADomicilioWebApi.Models.Base;
using ADomicilioWebApi.Models.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;

namespace ADomicilioWebApi.Controllers.ApiControllers
{
    [Route("api/{action}", Name = "OrdenWebApi")]
    public class OrdenWebApiController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Test()
        {
            return Ok();
        }

        [HttpPost]
        public JObject OrdenCliente([FromBody] OrdenViewModel ordenVM)
        {
            var nTercero = 0; // 0 orden cliente // 1 orden prestador
            ordenVM.nTercero = nTercero;

            var respuestaValidacion = ValidarOrden(ordenVM);
            if (respuestaValidacion.Codigo == CodigoResultado.OK)
            {
                respuestaValidacion = PersistenciaOrden(ordenVM);
            }

            return respuestaValidacion.ResultadoWebApiJObject();
        }

        [HttpPost]
        public JObject OrdenPrestador([FromBody] OrdenViewModel ordenVM)
        {
            var nTercero = 1; // 0 orden cliente // 1 orden prestador
            ordenVM.nTercero = nTercero;

            var respuestaValidacion = ValidarOrden(ordenVM);
            if (respuestaValidacion.Codigo == CodigoResultado.OK)
            {
                respuestaValidacion = PersistenciaOrden(ordenVM);
            }

            return respuestaValidacion.ResultadoWebApiJObject();
        }

        private int RecuperarNumeroNuevoRemito(string codigoCbteNumero)
        {
            try
            {
                //var produccion = bool.Parse(ConfigurationManager.AppSettings["produccion"] as string);
                //if (!produccion)

                var conexionSQL = ConfigurationManager.ConnectionStrings["ApplicationDbContext"];
                var stringConexionSQL = conexionSQL.ToString();
                using (SqlConnection connection = new SqlConnection(stringConexionSQL))
                {
                    //Create the command object
                    SqlCommand cmd = new SqlCommand()
                    {
                        CommandText = "Cbtenro_getnro_returnvalor_output",
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure
                    };
                    //Set SqlParameter
                    cmd.Parameters.Add(new SqlParameter("@cCodCbteNro", codigoCbteNumero)); //@cCodCbteNro varchar(20),  

                    //Set SqlParameter
                    SqlParameter outParameter = new SqlParameter //@nNumero int output  
                    {
                        ParameterName = "@nNumero", //Parameter name defined in stored procedure
                        SqlDbType = SqlDbType.Int, //Data Type of Parameter
                        Direction = ParameterDirection.Output //Specify the parameter as ouput
                    };
                    //add the parameter to the SqlCommand object
                    cmd.Parameters.Add(outParameter);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    if (int.TryParse(outParameter.Value.ToString(), out int resultado))
                    {
                        return resultado;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ResultadoWebApi ValidarOrden(OrdenViewModel ordenVM)
        {
            try
            {
                var conexionSQL = ConfigurationManager.ConnectionStrings["ApplicationDbContext"];
                var stringConexionSQL = conexionSQL.ToString();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = stringConexionSQL;
                    conn.Open();

                    var respuestaValidacion = new List<string>();

                    XmlSerializer xsSubmit = new XmlSerializer(typeof(OrdenViewModel));
                    var xml = "";

                    using (var sww = new StringWriter())
                    {
                        using (XmlWriter writer = XmlWriter.Create(sww))
                        {
                            xsSubmit.Serialize(writer, ordenVM);
                            xml = sww.ToString();
                        }
                    }

                    var xmlCorregido = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");

                    if (xmlCorregido != "")
                    {
                        SqlCommand insertCommand = new SqlCommand("ValidacionOrdenRmto", conn);
                        insertCommand.CommandType = CommandType.StoredProcedure;

                        insertCommand.Parameters.AddWithValue("xml", xmlCorregido);

                        using (SqlDataReader reader = insertCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                respuestaValidacion.Add(reader[0].ToString());
                            }
                        }
                    }

                    if (respuestaValidacion.Count > 0)
                    {
                        var respError = new ResultadoWebApi(CodigoResultado.ERROR);
                        respuestaValidacion.ForEach(v => respError.Validaciones.Add(new ValidacionesAPI { Error = true, Detalle = v }));
                        return respError;
                    }

                    return new ResultadoWebApi();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) return new ResultadoWebApi(CodigoResultado.ERROR, true, ex.InnerException.Message);
                return new ResultadoWebApi(CodigoResultado.ERROR, true, ex.Message);
            }
        }

        private ResultadoWebApi PersistenciaOrden(OrdenViewModel ordenVM)
        {
            try
            {
                var conexionSQL = ConfigurationManager.ConnectionStrings["ApplicationDbContext"];
                var stringConexionSQL = conexionSQL.ToString();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = stringConexionSQL;
                    conn.Open();

                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        // VALIDA DATOS RECIBIDOS
                        DateTime dFecha;
                        if (DateTime.TryParse(ordenVM.Fecha, out DateTime fechaParse))
                        {
                            dFecha = fechaParse.AddHours(3);
                        }
                        else
                        {
                            var resp = new ResultadoWebApi(CodigoResultado.ERROR, true, "La fecha no es válida.");
                            return resp;
                        }

                        //DATOS RMTOITEM
                        var nuevoRmtoGuid = Guid.NewGuid().ToString();
                        var numeroNuevoRemito = RecuperarNumeroNuevoRemito("XRM0001");

                        var cCodCbtTipo = "RM";
                        var cCbteLetra = "X";
                        var nSucursal = 1;
                        var cCodStRtmo = cCbteLetra + cCodCbtTipo + " " + string.Format("{0:0000}{1:00000000}", nSucursal, numeroNuevoRemito);
                        var ccoddepodestino = "00001";
                        var observaciones = "Transacción " + ordenVM.NroOrden;
                        var cCodStacciones = 0;
                        var nAplicado = 0;

                        var nEsVenta = 0;
                        var nFacturado = 0;
                        var nAnulado = 0;
                        var cCodVtSucu = "00001";
                        var cConfeccionado = "";
                        var nDevolucion = 0;
                        var dTest = DateTime.Now;
                        var cCodGsUser = "ADEBALI";
                        var nEsInventarioReplicado = 0;

                        var mes = 0; if (ordenVM.Items.Count > 0) mes = ordenVM.Items[0].Mes;
                        var anio = 0; if (ordenVM.Items.Count > 0) anio = ordenVM.Items[0].Anio;
                        //

                        SqlCommand insertCommand = new SqlCommand("InsertarRemitoOrdenWeb", conn, transaction);
                        insertCommand.CommandType = CommandType.StoredProcedure;

                        insertCommand.Parameters.AddWithValue("@cIdStRmto", nuevoRmtoGuid);
                        insertCommand.Parameters.AddWithValue("@cCodStRmto", cCodStRtmo);
                        insertCommand.Parameters.AddWithValue("@cCodDepoDestino", ccoddepodestino);
                        insertCommand.Parameters.AddWithValue("@dFecha ", dFecha);
                        insertCommand.Parameters.AddWithValue("@cCodCbteTipo ", cCodCbtTipo);
                        insertCommand.Parameters.AddWithValue("@cCbteLetra ", cCbteLetra);
                        insertCommand.Parameters.AddWithValue("@nSucursal ", nSucursal);
                        insertCommand.Parameters.AddWithValue("@nNumero ", numeroNuevoRemito);
                        insertCommand.Parameters.AddWithValue("@cCodPvProv ", ordenVM.CodProv);
                        insertCommand.Parameters.AddWithValue("@nTercero ", ordenVM.nTercero);
                        insertCommand.Parameters.AddWithValue("@cObservacion ", observaciones);
                        insertCommand.Parameters.AddWithValue("@cCodStAcciones ", cCodStacciones);
                        insertCommand.Parameters.AddWithValue("@nEsVenta ", nEsVenta);
                        insertCommand.Parameters.AddWithValue("@nFacturado ", nFacturado);
                        insertCommand.Parameters.AddWithValue("@nAnulado ", nAnulado);
                        insertCommand.Parameters.AddWithValue("@cCodVtSucu ", cCodVtSucu);
                        insertCommand.Parameters.AddWithValue("@cConfeccionado ", cConfeccionado);
                        insertCommand.Parameters.AddWithValue("@dFechaEmision ", dFecha);
                        insertCommand.Parameters.AddWithValue("@cCbteTipoP ", cCodCbtTipo);
                        insertCommand.Parameters.AddWithValue("@nSucursalP ", nSucursal);
                        insertCommand.Parameters.AddWithValue("@nNumeroP ", numeroNuevoRemito);
                        insertCommand.Parameters.AddWithValue("@nDevolucion ", nDevolucion);
                        insertCommand.Parameters.AddWithValue("@dTest ", dTest);
                        insertCommand.Parameters.AddWithValue("@cCodGsUser ", cCodGsUser);
                        insertCommand.Parameters.AddWithValue("@nEsInventarioReplicado ", nEsInventarioReplicado);
                        insertCommand.Parameters.AddWithValue("@cApellido ", ordenVM.Apellido);
                        insertCommand.Parameters.AddWithValue("@cNombre ", ordenVM.Nombre);
                        insertCommand.Parameters.AddWithValue("@nMes ", mes);
                        insertCommand.Parameters.AddWithValue("@nAnio ", anio);

                        insertCommand.ExecuteNonQuery();

                        var jOrden = 1;
                        ordenVM.Items.ForEach(i =>
                        {
                            //DATOS RMTOITEM
                            var nuevoRmtoItemGuid = Guid.NewGuid().ToString();
                            //

                            SqlCommand insertCommandItem = new SqlCommand("InsertarRemitoItemWeb", conn, transaction);
                            insertCommandItem.CommandType = CommandType.StoredProcedure;

                            insertCommandItem.Parameters.AddWithValue("@cCodStRmtoItem", nuevoRmtoItemGuid);
                            insertCommandItem.Parameters.AddWithValue("@cIdStRmto", nuevoRmtoGuid);
                            insertCommandItem.Parameters.AddWithValue("@cCodProd", i.Codigo);
                            insertCommandItem.Parameters.AddWithValue("@nCantidad", i.Cantidad);
                            insertCommandItem.Parameters.AddWithValue("@nPrecioUnitario", i.PrecioUnitario);
                            insertCommandItem.Parameters.AddWithValue("@nAplicado", nAplicado);
                            insertCommandItem.Parameters.AddWithValue("@nCosto", i.Costo);
                            insertCommandItem.Parameters.AddWithValue("@nOrden", jOrden);
                            insertCommandItem.Parameters.AddWithValue("@nAlicuotaIVA", i.Alicuota);

                            jOrden++;

                            insertCommandItem.ExecuteNonQuery();

                            //DATOS RMTOITEMSALUD
                            //

                            SqlCommand insertCommandItemSalud = new SqlCommand("InsertarRemitoItemSaludWeb", conn, transaction);
                            insertCommandItemSalud.CommandType = CommandType.StoredProcedure;

                            insertCommandItemSalud.Parameters.AddWithValue("@cCodStRmtoItem", nuevoRmtoItemGuid);
                            insertCommandItemSalud.Parameters.AddWithValue("@cIdStRmto", nuevoRmtoGuid);
                            insertCommandItemSalud.Parameters.AddWithValue("@cNombre", ordenVM.Nombre);
                            insertCommandItemSalud.Parameters.AddWithValue("@cApellido", ordenVM.Apellido);
                            insertCommandItemSalud.Parameters.AddWithValue("@cTipoDoc", ordenVM.TipoDocumento);
                            insertCommandItemSalud.Parameters.AddWithValue("@cNroDoc", ordenVM.NroDocumento);
                            insertCommandItemSalud.Parameters.AddWithValue("@cNroAfiliado", ordenVM.NroAfiliado);
                            insertCommandItemSalud.Parameters.AddWithValue("@cNroSiniestro", ordenVM.NroSiniestro);
                            insertCommandItemSalud.Parameters.AddWithValue("@cNroAutorizacion", ordenVM.NroAutorizacion ?? "");
                            insertCommandItemSalud.Parameters.AddWithValue("@cProvincia", ordenVM.Provincia);
                            insertCommandItemSalud.Parameters.AddWithValue("@cTipoAfiliado", ordenVM.TipoAfiliado);
                            insertCommandItemSalud.Parameters.AddWithValue("@cEmpresa", ordenVM.Empresa);
                            insertCommandItemSalud.Parameters.AddWithValue("@nMes", i.Mes);
                            insertCommandItemSalud.Parameters.AddWithValue("@nAnio", i.Anio);
                            insertCommandItemSalud.Parameters.AddWithValue("@nNroOrden", ordenVM.NroOrden);
                            insertCommandItemSalud.Parameters.AddWithValue("@nAlicuotaIVA", i.Alicuota);
                            insertCommandItemSalud.Parameters.AddWithValue("@cCodPvProv", i.CodProv ?? "");
                            insertCommandItemSalud.Parameters.AddWithValue("@codigoFacturacion", i.CodigoFacturacion ?? "");
                            insertCommandItemSalud.Parameters.AddWithValue("@descripcionFacturacion", i.DescripcionFacturacion ?? "");

                            insertCommandItemSalud.ExecuteNonQuery();
                        });

                        transaction.Commit();
                    }
                    catch (SqlException sqlError)
                    {
                        transaction.Rollback();
                        throw sqlError;
                    }

                    return new ResultadoWebApi();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) return new ResultadoWebApi(CodigoResultado.ERROR, true, ex.InnerException.Message);
                return new ResultadoWebApi(CodigoResultado.ERROR, true, ex.Message);
            }
        }
    }
}