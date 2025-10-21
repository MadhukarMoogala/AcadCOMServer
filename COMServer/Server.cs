using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using COMServer.Contracts;

[assembly: Guid("A8DAD545-A841-4EE4-B4DA-AAAC2FDDD305")]
[assembly: TypeLibVersion(1, 0)]
namespace COMServer
{
    [ComVisible(true)]
    [Guid(ContractGuids.ServerClass)]
    public class Server : IServer
    {
        public double ComputePi()
        {
            double sum = 0.0;
            int sign = 1;
            for (int i = 0; i < 1024; ++i)
            {
                sum += sign / (2.0 * i + 1.0);
                sign *= -1;
            }
            return 4.0 * sum;
        }

        // keep explicit IServer implementation if you want to preserve explicit interface behavior
        double IServer.ComputePi()
        {
            return ComputePi();
        }

        // Minimal IDispatch implementations can remain or be implemented as needed.
        void IDispatch.GetTypeInfoCount(out int pctinfo) => pctinfo = 0;
        void IDispatch.GetTypeInfo(int iTInfo, int lcid, out nint info) => info = IntPtr.Zero;
        void IDispatch.GetIDsOfNames(ref Guid riid, ref nint rgszNames, int cNames, int lcid, ref nint rgDispId)
            => throw new NotImplementedException();
        void IDispatch.Invoke(int dispIdMember, ref Guid riid, int lcid, short wFlags, ref DISPPARAMS pDispParams, out object pVarResult, ref EXCEPINFO pExcepInfo, out int puArgErr)
            => throw new NotImplementedException();
    }
}