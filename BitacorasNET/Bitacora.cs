using BitacorasNET.Configuracion.DefaultValues;
using BitacorasNET.Configuracion.EscribeArchivoLOG;
using BitacorasNET.Configuracion.Headerih;
using BitacorasNET.Configuracion.Headerme;
using BitacorasNET.Configuracion.MqSeries;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitacorasNET
{
    public class Bitacora
    {
        private string ArchivoIni;
        private string lsCommandLine;

        // Variables para el registro de los valores del header IH
        private string strFuncionHost; // Valor que indica el programa que invocara el CICSBRIDGE
        private string strHeaderTagIni; // Bandera que indica el comienzo del Header
        private string strIDProtocol; // Identificador  del protocolo (PS9)
        private string strLogical; // Terminal Lógico Asigna Arquitectura ASTA
        private string strAccount; // Terminal Contable (CR Contable)
        private string strUser; // Usuario. Debe ser diferente de espacios
        private string strSeqNumber; // Número de Secuencia (indicador de paginación)
        private string strTXCode; // Función específica Asigna Arquitectura Central
        private string strUserOption; // Tecla de función (no aplica)
        private string strCommit; // Indicador de commit: Permite realizar commit
        private string strMsgType; // Tipo de mensaje: Nuevo requerimiento
        private string strProcessType; // Tipo de proceso: on line
        private string strChannel; // Canal Asigna Arquitectura Central
        private string strPreFormat; // Indicador de preformateo: Arquitectura no deberá de preformatear los datos
        private string strLenguage; // Idioma: Español
        private string strHeaderTagEnd; // Bandera que indica el final del header

        // Variables para el registro de los valores del header ME
        private string strMETAGINI; // Bandera que indica el comienzo del mensaje
        private string strMsgTypeCole; // Tipo de mensaje: Copy
        private string strMETAGEND; // Bandera que indica el fin del mensaje

        // Variables para el registro de los valores Default
        string strColectorMaxLeng; // Maxima longitud del COLECTOR
        string strMsgMaxLeng; // Maxima longitud del del bloque ME
        string strPS9MaxLeng; // Maxima longitud del formato PS9
        string strReplyToMQ; // MQueue de respuesta para HOST
        string strFuncionSQL; // Funcion a ejecutar al recibir la respuesta
        string strRndLogTerm; // Indica que el atributo Logical Terminal es 

        private string Gs_MQManager;   // MQManager de Escritura
        private string Gs_MQQueueEscritura;   // MQQueue de Escritura
        public DateTime gsAccesoActual;   // Fecha/Hora actual del sistema. La tomamos del servidor NT y no de SQL porque precisamente el

        MqSeries mqSeries = new MqSeries();

        public void ProcesarBitacora(string strParametros)
        {
            string[] parametros;
            string ls_MsgVal = "";

            try
            {
                //ArchivoIni = strRutaIni + @"\Bitacoras.ini";
                ConfiguraFileLog("escribeArchivoLOG");
                gsAccesoActual = DateTime.Now;

                lsCommandLine = strParametros.Trim();

                if(lsCommandLine.Equals("") != false)
                {
                    parametros = lsCommandLine.Split('-');

                    Gs_MQManager = parametros[0].Trim();
                    Gs_MQQueueEscritura = parametros[1].Trim();
                    strFuncionSQL = parametros[3];
                }
                else
                {
                    ObtenerInfoMq();
                }


                ConfiguraHeader_IH_ME();

                if(ValidaInfoMQ(ls_MsgVal))
                {
                    mqSeries.Escribe("Se presentó un error en la función ValidaInfoMQ invocada desde el MAIN: " + ls_MsgVal + ". Función SQL: " + strFuncionSQL);
                    return;
                }


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ConfiguraFileLog(string Ls_Tit)
        {
            EscribeArchivoLOGConfig escribeArchivoLOGConfig = new EscribeArchivoLOGConfig();

            string strlogFileName = escribeArchivoLOGConfig.ObtenerParametro("logFileName");
            string strlogFilePath = escribeArchivoLOGConfig.ObtenerParametro("logFilePath");

            bool Mb_GrabaLog = true;
        }

        public void ObtenerInfoMq()
        {
            MqSeriesConfig mqSeriesConfig = new MqSeriesConfig();

            Gs_MQManager = mqSeriesConfig.ObtenerParametro("MQManager");
            Gs_MQQueueEscritura = mqSeriesConfig.ObtenerParametro("MQEscritura");
            strFuncionSQL = mqSeriesConfig.ObtenerParametro("FGBitacora");
        }

        public void ConfiguraHeader_IH_ME()
        {
            HeaderihConfig headerihConfig = new HeaderihConfig();

            strFuncionHost = headerihConfig.ObtenerParametro("PRIMERVALOR");
            strHeaderTagIni = headerihConfig.ObtenerParametro("IHTAGINI");
            strIDProtocol = headerihConfig.ObtenerParametro("IDPROTOCOL");
            strLogical = headerihConfig.ObtenerParametro("Logical");
            strAccount = headerihConfig.ObtenerParametro("ACCOUNT");
            strUser = headerihConfig.ObtenerParametro("User");
            strSeqNumber = headerihConfig.ObtenerParametro("SEQNUMBER");
            strTXCode = headerihConfig.ObtenerParametro("TXCODE");
            strUserOption = headerihConfig.ObtenerParametro("USEROPT");
            strCommit = headerihConfig.ObtenerParametro("Commit");
            strMsgType = headerihConfig.ObtenerParametro("MSGTYPE");
            strProcessType = headerihConfig.ObtenerParametro("PROCESSTYPE");
            strChannel = headerihConfig.ObtenerParametro("CHANNEL");
            strPreFormat = headerihConfig.ObtenerParametro("PREFORMATIND");
            strLenguage = headerihConfig.ObtenerParametro("LANGUAGE");
            strHeaderTagEnd = headerihConfig.ObtenerParametro("IHTAGEND");

            HeadermeConfig headermeConfig = new HeadermeConfig();

            strMETAGINI = headermeConfig.ObtenerParametro("METAGINI");
            strMsgTypeCole = headermeConfig.ObtenerParametro("TIPOMSG");
            strMETAGEND = headermeConfig.ObtenerParametro("METAGEND");

            DefaultValuesConfig defaultValuesConfig = new DefaultValuesConfig();

            strColectorMaxLeng = defaultValuesConfig.ObtenerParametro("COLMAXLENG");
            strMsgMaxLeng = defaultValuesConfig.ObtenerParametro("MSGMAXLENG");
            strPS9MaxLeng = defaultValuesConfig.ObtenerParametro("PS9MAXLENG");
            strReplyToMQ = defaultValuesConfig.ObtenerParametro("ReplyToQueue");
            strRndLogTerm = defaultValuesConfig.ObtenerParametro("RandomLogTerm");

            

        }

        private bool ValidaInfoMQ(string ps_MsgVal)
        {
            bool validaInfoMQ = false;
            string ls_msg = "";

            if (Gs_MQManager.Trim() == "")
            {
                ls_msg = ls_msg + (ls_msg.Length > 0 ? ((char)13).ToString() : "") + "Falta MQ Manager envio.";
            }
            if (Gs_MQQueueEscritura.Trim() == "")
            {
                ls_msg = ls_msg + (ls_msg.Length > 0 ? ((char)13).ToString() : "") + "Falta MQ Queue envio.";
            }
            if (ls_msg.Trim() == "")
            {
                validaInfoMQ = true;
            }

            ps_MsgVal = ls_msg;

            return validaInfoMQ;
            
        }

        public void ProcesoBDtoMQQUEUE()
        {
            string Ls_MensajeMQ;      
            string Ls_MsgColector; 

            string sFechaEnvio;
            string sEnvioConse;
            string sMensajeEnvio;

            try
            {
                mqSeries.Escribe("");
                mqSeries.Escribe("Inicia envío de mensajes a Host: " + gsAccesoActual + " Función SQL: " + strFuncionSQL);
            }
            catch(Exception ex)
            {

            }

        }
    } 
}
