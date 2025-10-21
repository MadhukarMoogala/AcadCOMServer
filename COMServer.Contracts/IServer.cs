using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace COMServer.Contracts
{
    // Minimal IDispatch shape used by your projects (keeps signatures compatible)
    [ComVisible(true)]
    [Guid("00020400-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IDispatch
    {
        [DispId(1)]
        void GetTypeInfoCount(out int pctinfo);
        [DispId(2)]
        void GetTypeInfo(int iTInfo, int lcid, out IntPtr info);
        [DispId(3)]
        void GetIDsOfNames(ref Guid riid, ref IntPtr rgszNames, int cNames, int lcid, ref IntPtr rgDispId);
        [DispId(4)]
        void Invoke(int dispIdMember, ref Guid riid, int lcid, short wFlags, ref DISPPARAMS pDispParams, out object pVarResult, ref EXCEPINFO pExcepInfo, out int puArgErr);
    }

    // COM contract for your server (GUID and DispId must match IDL/TLB)
    [ComVisible(true)]
    [Guid("BA9AC84B-C7FC-41CF-8B2F-1764EB773D4B")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IServer : IDispatch
    {
        [DispId(1)]
        double ComputePi();
    }
}