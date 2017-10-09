using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace FI.Library.Smartcard
{
    public sealed class SmartcardContextSafeHandle : SafeHandle
    {
        [DllImport("winscard.dll", SetLastError = true)]
        internal static extern int SCardReleaseContext(IntPtr hContext);

        public SmartcardContextSafeHandle()
            : base(IntPtr.Zero, true)
        {
        }

        //The default constructor will be called by P/Invoke smart 
        //marshalling when returning MySafeHandle in a method call.
        public override bool IsInvalid
        {
            [SecurityPermission(SecurityAction.LinkDemand,
                UnmanagedCode = true)]
            get { return (handle == IntPtr.Zero); }
        }

        //We should not provide a finalizer. SafeHandle's critical 
        //finalizer will call ReleaseHandle for us.
        protected override bool ReleaseHandle()
        {
            SmartcardErrorCode result =
                (SmartcardErrorCode)SCardReleaseContext(handle);
            return (result == SmartcardErrorCode.None);
        }
    }
}
