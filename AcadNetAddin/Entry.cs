using Autodesk.AutoCAD.Runtime;
using COMServer.Contracts;
using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;


namespace AcadAddin
{
    [SupportedOSPlatform("windows")]
    public class Entry
    {
        

        [CommandMethod("RunDotNet")]
        public void RunDotNet()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            var editor = doc.Editor;
            dynamic acadObj = Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication;
            double pi =0;
            if (acadObj != null)
            {
                bool succeeded = false;

                //1) Preferred: AutoCAD ActiveX way
                try
                {
                    dynamic serverObj = acadObj.GetInterfaceObject("COMServer.Server");


                    if (serverObj != null)
                    {
                        IServer server = serverObj as IServer;
                        if (server != null)
                        {
                            var result = server.ComputePi();
                            pi = Convert.ToDouble(result);
                            editor.WriteMessage("\n" + pi);
                            succeeded = true;
                        }
                        else
                        {
                            editor.WriteMessage("\nserverObj does not implement IServer.");
                        }
                    }
                    else
                    {
                        editor.WriteMessage("\nGetInterfaceObject returned null for 'COMServer.Server'.");
                    }
                }
                catch (System.Exception ex)
                {
                    editor.WriteMessage("\nGetInterfaceObject threw: " + ex.Message);
                }

                //2) Fallback: try ProgID or CLSID activation in-process
                if (!succeeded)
                {
                    try
                    {
                        Type comType = Type.GetTypeFromProgID("COMServer.Server");
                        if (comType == null)
                        {
                            // Use CLSID from COMServer/ContractGuids.cs
                            comType = Type.GetTypeFromCLSID(new Guid("DB1797F5-7198-4411-8563-D05F4E904956"));
                        }

                        if (comType != null)
                        {
                            object comObj = null;
                            try
                            {
                                comObj = Activator.CreateInstance(comType);
                                dynamic server = comObj;
                                var result = server.ComputePi();
                                pi = Convert.ToDouble(result);
                                editor.WriteMessage("\n" + pi);
                                succeeded = true;
                            }
                            finally
                            {
                                if (comObj != null && Marshal.IsComObject(comObj))
                                {
                                    try { Marshal.ReleaseComObject(comObj); } catch { }
                                }
                            }
                        }
                        else
                        {
                            editor.WriteMessage("\nUnable to locate COM type (ProgID or CLSID). Is the COM server registered?");
                        }
                    }
                    catch (System.Exception ex)
                    {
                        editor.WriteMessage("\nFallback COM activation failed: " + ex.Message);
                    }
                }

                if (!succeeded)
                {
                    editor.WriteMessage("\nFailed to obtain Pi from COM server. Make sure the COM server is registered (regsvr32 for comhost.dll) and that the ProgID 'COMServer.Server' is correct.");
                }
            }
        }
    }
}
