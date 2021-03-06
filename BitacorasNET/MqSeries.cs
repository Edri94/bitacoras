using IBM.WMQ;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitacorasNET.Configuracion.MqSeries;
using System.IO;

namespace BitacorasNET
{
    public class MqSeries
    {
        public string strlogFilePath;
        public string strlogFileName;
        public bool Mb_GrabaLog;

        private string Archivo;


        // Enumeración para las opciones de abrir la cola
        public enum MQOPEN
        {
            MQOO_INPUT_AS_Q_DEF = 0x1,
            MQOO_INPUT_SHARED = 0x2,
            MQOO_INPUT_EXCLUSIVE = 0x4,
            MQOO_BROWSE = 0x8,
            MQOO_OUTPUT = 0x10,
            MQOO_INQUIRE = 0x20,
            MQOO_SET = 0x40,
            MQOO_BIND_ON_OPEN = 0x4000,
            MQOO_BIND_NOT_FIXED = 0x8000,
            MQOO_BIND_AS_Q_DEF = 0x0,
            MQOO_SAVE_ALL_CONTEXT = 0x80,
            MQOO_PASS_IDENTITY_CONTEXT = 0x100,
            MQOO_PASS_ALL_CONTEXT = 0x200,
            MQOO_SET_IDENTITY_CONTEXT = 0x400,
            MQOO_SET_ALL_CONTEXT = 0x800,
            MQOO_ALTERNATE_USER_AUTHORITY = 0x1000,
            MQOO_FAIL_IF_QUIESCING = 0x2000
        }

        // Enumeración para el tipo de acción
        enum TipoAccion
        {
            eMQConectar = 0,
            eMQDesconectar = 1,
            eMQAbrirCola = 2,
            eMQCerrarCola = 3,
            eMQLeerCola = 4,
            eMQEscribirCola = 5,
            eMQOtro = 6
        }

        // Variable para validar la conexión
        public bool blnConectado;

        // ******************************************************************************************
        // Variables y objectos publicos para conectarse al MQSeries

        //// Declaraciones de los objetos para MQSeries
        //// Referencia: IBM MQSeries Automation Classes for ActiveX
        //public mqSession mqSession = new mqSession();           // Objeto Session para conexión con el servidor MQSeries
        //public MQQueueManager mqManager = new MQQueueManager();      // -Objeto QueueManager para accesar al maestro de colas
        //public MQQueue mqsEscribir = new MQQueue();             // -Objeto Queue para escribir
        //public MQQueue mqsLectura = new MQQueue();             // -Objeto Queue para lectura
        //public MQMessage mqsMsgEscribir = new MQMessage();           // -Objeto Message para escribir
        //public MQMessage mqsMsglectura = new MQMessage();           // -Objeto Message para lectura

        //Declaraciones de los objetos para MQSeries
        public MQQueueManager queueManager;
        public MQQueue queue;
        public MQMessage queueMessage;
        public MQPutMessageOptions queuePutMessageOptions;
        public MQGetMessageOptions queueGetMessageOptions;

        public MqSeriesConfig mqSeriesConfig;

        public MqSeries()
        {
            //mqsConexion = new MQSession();
            //mqsManager = new MQQueueManager();
            //MQQMonitorLectura = new MQQueue(mqsManager, "", 0, "", "", "");

            mqSeriesConfig = new MqSeriesConfig();
        }


        private const String connectionType = MQC.TRANSPORT_MQSERIES_CLIENT;

        private string QueueManagerName;
        public string MQConectar(string strQueueManagerName, string strChannelInfo)
        {
            //cInterfaz escribeLog = new cInterfaz();

            QueueManagerName = strQueueManagerName;
            string strReturn = "";

            try
            {
                queueManager = new MQQueueManager(QueueManagerName);

                strReturn = "Connected Successfully";
            }
            catch (MQException exp)
            {

                //string strError = getMQRCText(exp.Reason);
                //escribeLog.EscribeLogs("Conecta MQ ExcepcionMQ Error trying to create Queue" +
                //            "Manager Object. Error: " + exp.Message +
                //            ", Details: " + strError, "C:\\Users\\stktswif\\Desktop\\VB6-Portable\\", "", 3, "jj", 1);
                ////          ", Details: " + strError, "D:\\", "", 3, "jj", 1);
                strReturn = "Exception: " + exp.Message;
            }

            return strReturn;
        }


        public bool MQDesconectar(MQQueueManager objMQManager, MQQueue objMQEscribir)
        {
            bool MQDesconectar = false;
            try
            {
                if(objMQEscribir != null)
                {
                    if(objMQEscribir.IsOpen)
                    {
                        objMQEscribir = null;
                    }
                }

                if(objMQManager != null)
                {
                    if(objMQManager.IsConnected)
                    {
                        objMQManager.Disconnect();
                        objMQManager = null;
                        MQDesconectar = true;
                    }
                }

                //Set mqSession = Nothing

                return MQDesconectar;
            }
            catch (Exception ex)
            {
                Escribe(ex.Message);
                return MQDesconectar;
            }
        }

        public bool MQEnviarMsg(MQQueueManager objMQManager, string strMQCola, MQQueue objMQCola, MQMessage objMQMensaje, string ls_mensaje, string Ls_ReplayMQQueue, string strMensajeID = "")
        {
            MQPutMessageOptions mqsMQOpciones;
            string strMensaje;

            try
            {
                //Set mqsMQOpciones = objMQConexion.AccessPutMessageOptions
                //mqsMQOpciones.Options = mqsMQOpciones.Options Or MQPMO_NO_SYNCPOINT
                //Set objMQMensaje = objMQConexion.AccessMessage

                if(MQAbrirCola(objMQManager, strMQCola, objMQCola, MQOPEN.MQOO_OUTPUT))
                {

                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private bool MQAbrirCola(MQQueueManager objMQManager, string strMQCola, MQQueue objMQCola, MQOPEN lngOpciones)
        {
            try
            {
                ////' Se accesa la cola ya sea para leer o escribir
                //Set objMQCola = objMQManager.AccessQueue(strMQCola, lngOpciones, mqManager.Name, "AMQ.*")
                //MQAbrirCola = True
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public bool MQCerrarCola(MQQueue objMCola)
        {
            bool MQCerrarCola = false;
            try
            {
                if(objMCola != null)
                {
                    if(objMCola.IsOpen )
                    {
                        objMCola.Close();
                        MQCerrarCola = true; 
                    }
                }
                return MQCerrarCola;
            }
            catch (Exception ex)
            {
                Escribe(ex.Message);
                return MQCerrarCola;
            }
        }

        public void Escribe(string vData)
        {
            //Archivo = strlogFilePath & Format(Now(), "yyyyMMdd") & "-" & strlogFileName
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (Mb_GrabaLog)
            {
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "")))
                {
                    Console.WriteLine(vData);
                    outputFile.WriteLine(vData);
                }

            }
        }
    }
}
