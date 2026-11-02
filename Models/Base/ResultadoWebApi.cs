using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ADomicilioWebApi.Models.Base
{
    public class ResultadoWebApi
    {
        public ResultadoWebApi()
        {
            Codigo = CodigoResultado.OK;
        }
        public ResultadoWebApi(CodigoResultado codigo)
        {
            Codigo = codigo;
        }

        public ResultadoWebApi(CodigoResultado codigo, bool error, string mensaje)
        {
            Codigo = codigo;
            AgregarValidacion(error, mensaje);
        }

        public JObject ResultadoWebApiJObject()
        {
            var jObject = JObject.FromObject(this);
            return jObject;
        }

        public CodigoResultado Codigo { get; set; }
        public string Resultado { get { return Codigo.ToString(); } }
        public List<ValidacionesAPI> Validaciones { get; set; } = new List<ValidacionesAPI>();

        public void AgregarValidacion(bool error, string mensaje)
        {
            Validaciones.Add(new ValidacionesAPI
            {
                Error = error,
                Detalle = mensaje
            });
        }
    }

    public class ValidacionesAPI
    {
        public bool Error { get; set; }
        public string Detalle { get; set; }
    }

    public enum CodigoResultado
    {
        OK,
        ERROR
    }
}