using BitacorasNET.Configuracion.DefaultValues;
using BitacorasNET.Configuracion.EscribeArchivoLOG;
using BitacorasNET.Configuracion.Headerih;
using BitacorasNET.Configuracion.Headerme;
using BitacorasNET.Configuracion.MqSeries;
using BitacorasNET.Configuracion.ValorTk14;
using System;


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

        MqSeries mqSeries;
        MqSeriesConfig mqSeriesConfig;
        ValorTk14Config valorTk14Config;


        public Bitacora()
        {
            mqSeries = new MqSeries();
            mqSeriesConfig = new MqSeriesConfig();
            valorTk14Config = new ValorTk14Config();
        }

        private void ProcesarBitacora(string strParametros)
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

        private void ConfiguraFileLog(string Ls_Tit)
        {
            EscribeArchivoLOGConfig escribeArchivoLOGConfig = new EscribeArchivoLOGConfig();

            string strlogFileName = escribeArchivoLOGConfig.ObtenerParametro("logFileName");
            string strlogFilePath = escribeArchivoLOGConfig.ObtenerParametro("logFilePath");

            bool Mb_GrabaLog = true;
        }

        private void ObtenerInfoMq()
        {
            

            Gs_MQManager = mqSeriesConfig.ObtenerParametro("MQManager");
            Gs_MQQueueEscritura = mqSeriesConfig.ObtenerParametro("MQEscritura");
            strFuncionSQL = mqSeriesConfig.ObtenerParametro("FGBitacora");
        }

        private void ConfiguraHeader_IH_ME()
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

        private void ProcesoBDtoMQQUEUE()
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

                if(mqSeries.MQConectar("", "") == "")
                {
                    mqSeries.blnConectado = true;
                }
                else
                {
                    mqSeries.Escribe("Fallo conexión MQ-Manager " + Gs_MQManager + ": " + mqSeries.queueManager.ReasonCode + " - " + mqSeries.queueManager.ReasonName);
                }

                sFechaEnvio = Left(DateTime.Now.ToString("yyyymmddhhnnss") + Space(26),26);
                sEnvioConse = Left(valorTk14Config.ObtenerParametro("TKCONSECUTIVO") + Space(1),1);

                Ls_MsgColector = (strFuncionSQL + new String(' ', 8)).Substring(0, 8);

                if(Ls_MsgColector.Length >0)
                {
                    //Ls_MensajeMQ = 
                }
  

                //If MQConectar(mqSession, Gs_MQManager, mqManager) Then
                //    blnConectado = True
                //Else
                //    Escribe "Fallo conexión MQ-Manager " & Gs_MQManager & ": " & mqSession.ReasonCode & " - " & mqSession.ReasonName
                //    Exit Function
                //End If
            }
            catch(Exception ex)
            {

            }

        }

        private string ASTA_ENTRADA(string strMsgColector)
        {
            string ASTA_ENTRADA;

            string ls_TempColectorMsg;
            string ls_BloqueME;
            int ln_longCOLECTOR;
            int ln_AccTerminal;

            try
            {
                ls_TempColectorMsg = strMsgColector;

                if (ls_TempColectorMsg.Length > Int32.Parse(strColectorMaxLeng))
                {
                    mqSeries.Escribe("La longitud del colector supera el maximo permitido");
                    //GoTo ErrorASTA
                }

                ls_BloqueME = Left((strMETAGINI + Space(4)).Trim(), 4);
                ls_BloqueME = ls_BloqueME + Right("0000" + (ls_TempColectorMsg.Length.ToString()), 4);
                ls_BloqueME = ls_BloqueME + Left((strMsgTypeCole.Trim() + Space(5)),5);
                ls_BloqueME = ls_BloqueME + ls_TempColectorMsg;
                ls_BloqueME = ls_BloqueME + Left(strMETAGEND + Space(5),5);

                if (ls_BloqueME.Length > Int32.Parse(strMsgMaxLeng))
                {
                    mqSeries.Escribe("La longitud del Bloque ME supera el maximo permitido");
                    //GoTo ErrorASTA
                }

                ASTA_ENTRADA = Left(strFuncionHost.Trim() + Space(8), 8);
                ASTA_ENTRADA = ASTA_ENTRADA + Left(strHeaderTagIni.Trim() + Space(4), 4);
                ASTA_ENTRADA = ASTA_ENTRADA + Left(strIDProtocol.Trim() + Space(2), 2);

                if(strRndLogTerm.Trim().Equals("1"))
                {
                    ln_AccTerminal = 0;
                    do
                    {
                        //new  Random(DateTime.Now.Second);
                        var Rnd = new Random(DateTime.Now.Second * 1000);
                        ln_AccTerminal = Rnd.Next();

                    } while(ln_AccTerminal > 0 && ln_AccTerminal < 2000);

                    ASTA_ENTRADA = ASTA_ENTRADA + Left((ln_AccTerminal.ToString("D4") + Space(8)),8);
                }
                else
                {
                    ASTA_ENTRADA = ASTA_ENTRADA + Left(strAccount.Trim() + Space(8), 8);
                }

                ASTA_ENTRADA = ASTA_ENTRADA + Left(strAccount.Trim() + Space(8), 8);
                ASTA_ENTRADA = ASTA_ENTRADA + Left(strUser.Trim() + Space(8), 8);
                ASTA_ENTRADA = ASTA_ENTRADA + Left(strSeqNumber.Trim() + Space(8), 8);
                ASTA_ENTRADA = ASTA_ENTRADA + Left(strTXCode.Trim() + Space(8), 8);
                ASTA_ENTRADA = ASTA_ENTRADA + Left(strUserOption.Trim() + Space(8), 8);

                ln_longCOLECTOR = 65 + ls_BloqueME.Length;

                if(ln_longCOLECTOR > Int32.Parse(strPS9MaxLeng.Trim()))
                {
                    mqSeries.Escribe("La longitud del Layout PS9 supera el maximo permitido");
                    //GoTo ErrorASTA
                }

                ASTA_ENTRADA = ASTA_ENTRADA + Right("00000" + ln_longCOLECTOR.ToString(), 5);
                ASTA_ENTRADA = ASTA_ENTRADA + Left(strCommit.Trim() + Space(1), 1);
                ASTA_ENTRADA = ASTA_ENTRADA + Left(strMsgType.Trim() + Space(1), 1);
                ASTA_ENTRADA = ASTA_ENTRADA + Left(strProcessType.Trim() + Space(1), 1);
                ASTA_ENTRADA = ASTA_ENTRADA + Left(strChannel.Trim() + Space(2), 2);
                ASTA_ENTRADA = ASTA_ENTRADA + Left(strPreFormat.Trim() + Space(1), 1);
                ASTA_ENTRADA = ASTA_ENTRADA + Left(strLenguage.Trim() + Space(1), 1);
                ASTA_ENTRADA = ASTA_ENTRADA + Left(strHeaderTagEnd.Trim() + Space(5), 5);
                ASTA_ENTRADA = ASTA_ENTRADA + ls_BloqueME;


                return ASTA_ENTRADA;
            }
            catch (Exception ex)
            {
                mqSeries.Escribe(ex.Message);
                return ex.Message;
            }
        }
        public string Space(int veces)
        {
            return new String(' ', veces);
        }

        public string Left(string cadena, int posiciones)
        {
            return cadena.Substring(0, posiciones);
        }

        public string Right(string cadena, int posiciones)
        {
            return cadena.Substring((cadena.Length - posiciones), posiciones);
        }
    }
}
