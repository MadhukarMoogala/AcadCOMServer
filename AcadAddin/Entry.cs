using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;


namespace AcadAddin
{
    class Activation
    {
        /// <summary>
        /// Managed definition of CoClass
        /// </summary>
        [ComImport]
        [CoClass(typeof(ServerClass))]
        [Guid("BA9AC84B-C7FC-41CF-8B2F-1764EB773D4B")] // By TlbImp convention, set this to the GUID of the parent interface
        internal interface Server : IServer
        {
        }

        /// <summary>
        /// Managed activation for CoClass
        /// </summary>
        [ComImport]
        [Guid("DB1797F5-7198-4411-8563-D05F4E904956")]
        internal class ServerClass
        {
        }
    }
    public class Entry
    {
        

        [CommandMethod("RunDLL")]
        public void RunDLL()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            var editor = doc.Editor;
            IAcadApplication acadObj = Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication as IAcadApplication;
            double pi = 0;
            if (acadObj != null)
            {
                try
                {
                    IServer serverObj = acadObj.GetInterfaceObject("COMServer.Server") as IServer;


                    if (serverObj != null)
                    {
                       pi = serverObj.ComputePi();
                       editor.WriteMessage("\n" + pi);
                    }
                }
                catch (System.Exception ex)
                {
                    editor.WriteMessage("\n" + ex.Message);
                    var server = new Activation.Server();
                    pi = server.ComputePi();
                    editor.WriteMessage("\n" + pi);

                }
            }
        }
    }
}
