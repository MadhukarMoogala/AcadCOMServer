using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


/// <summary>
/// IDispatch interface definition
/// GUID of IID_IDispatch
/// ref: https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-oaut/ac9c502b-ac1c-4202-8ad4-048ac98afcc9
/// </summary>

[ComVisible(true)]
[Guid("00020400-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
public interface IDispatch //We need a fake IDispatch interface to make the AutoCAD Happy!
{
    [DispId(1)]
    void GetTypeInfoCount(out int pctinfo);
    [DispId(2)]
    void GetTypeInfo(int iTInfo, int lcid, out IntPtr info);
    [DispId(3)]
    void GetIDsOfNames(ref Guid riid, ref IntPtr rgszNames, int cNames, int lcid, ref IntPtr rgDispId);
    [DispId(4)]
    void Invoke(int dispIdMember, ref Guid riid, int lcid, short wFlags, ref System.Runtime.InteropServices.ComTypes.DISPPARAMS pDispParams, out object pVarResult, ref System.Runtime.InteropServices.ComTypes.EXCEPINFO pExcepInfo, out int puArgErr);
}

[ComVisible(true)]
[Guid("BA9AC84B-C7FC-41CF-8B2F-1764EB773D4B")]
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
public interface IServer : IDispatch
{
    [DispId(1)]
    double ComputePi();
}

