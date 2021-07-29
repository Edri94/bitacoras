using System;
using System.Collections.Generic;
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
        public string gsAccesoActual;   // Fecha/Hora actual del sistema. La tomamos del servidor NT y no de SQL porque precisamente el

        public void ProcesarBitacora(string strRutaIni)
        {
            string[] parametros;
            string ls_MsgVal;

            try
            {
                ArchivoIni = strRutaIni + @"\Bitacoras.ini";
                ConfiguraFileLog("EscribeArchivoLOG", ArchivoIni);


            }
            catch(Exception ex)
            {

            }
        }

        public void ConfiguraFileLog(string Ls_Tit, string Ls_Path)
        {
            string strlogFileName = ObtenParametroIni(Ls_Tit, "logFileName", "", Ls_Path);
            string strlogFilePath = ObtenParametroIni(Ls_Tit, "logFilePath", "", Ls_Path);
            bool Mb_GrabaLog = true;
        }

        public string ObtenParametroIni(string Ls_Grupo, string Ls_Variable, string Ls_Default, string Ls_AppPath)
        {
            string Ls_Buffer;
            int Li_Long;

            return "";
        }
    }
}
