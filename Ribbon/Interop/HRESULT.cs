using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RibbonLib.Interop
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public struct HRESULT
        : IEquatable<HRESULT>
    {
        public readonly int Value;
        public HRESULT(int value) => this.Value = value;
        public static implicit operator int(HRESULT value) => value.Value;
        public static explicit operator HRESULT(int value) => new HRESULT(value);
        public static bool operator ==(HRESULT left, HRESULT right) => left.Value == right.Value;
        public static bool operator !=(HRESULT left, HRESULT right) => !(left == right);

        public bool Equals(HRESULT other) => this.Value == other.Value;

        public override bool Equals(object obj) => obj is HRESULT other && this.Equals(other);

        public override int GetHashCode() => this.Value.GetHashCode();
        public static implicit operator uint(HRESULT value) => (uint)value.Value;
        public static explicit operator HRESULT(uint value) => new HRESULT((int)value);


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool Succeeded => this.Value >= 0;


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool Failed => this.Value < 0;

        public HRESULT ThrowOnFailure(IntPtr errorInfo)
        {
            Marshal.ThrowExceptionForHR(this.Value, errorInfo);
            return this;
        }


        public override string ToString() => string.Format(global::System.Globalization.CultureInfo.InvariantCulture, "0x{0:X8}", this.Value);

        public string ToString(string format, IFormatProvider formatProvider) => ((uint)this.Value).ToString(format, formatProvider);
        public static readonly HRESULT E_ABORT = (HRESULT)(-2147467260); //0x80004004
        /// <summary>The operation could not be completed.</summary>
        /// <remarks>
        /// <para><see href="https://docs.microsoft.com/windows/win32/api//mbnapi/nf-mbnapi-imbnpinevents-onunblockcomplete">Learn more about this API from docs.microsoft.com</see>.</para>
        /// </remarks>
        public static readonly HRESULT E_FAIL = (HRESULT)(-2147467259); //0x80004005
        /// <summary>Documentation varies per use. Refer to each: <see href="https://docs.microsoft.com/windows/win32/api//mbnapi/nf-mbnapi-imbnconnectioncontextevents-onsetprovisionedcontextcomplete">IMbnConnectionContextEvents.OnSetProvisionedContextComplete</see>, <see href="https://docs.microsoft.com/windows/win32/api//mbnapi/nf-mbnapi-imbnsmsevents-onsmssendcomplete">IMbnSmsEvents.OnSmsSendComplete</see>, <see href="https://docs.microsoft.com/windows/win32/api//mbnapi/nf-mbnapi-imbnserviceactivationevents-onactivationcomplete">IMbnServiceActivationEvents.OnActivationComplete</see>.</summary>
        public static readonly HRESULT E_INVALIDARG = (HRESULT)(-2147024809); //0x80070057
        public static readonly HRESULT E_NOTIMPL = (HRESULT)(-2147467263); //0x80004001
        public static readonly HRESULT ERROR_NOT_SUPPORTED = (HRESULT)(0x80070032);
        public static readonly HRESULT S_FALSE = (HRESULT)(1);
        /// <summary>Documentation varies per use. Refer to each: <see href="https://docs.microsoft.com/windows/win32/api//mbnapi/nf-mbnapi-imbnpinmanagerevents-ongetpinstatecomplete">IMbnPinManagerEvents.OnGetPinStateComplete</see>, <see href="https://docs.microsoft.com/windows/win32/api//mbnapi/nf-mbnapi-imbnconnectionevents-onconnectcomplete">IMbnConnectionEvents.OnConnectComplete</see>, <see href="https://docs.microsoft.com/windows/win32/api//mbnapi/nf-mbnapi-imbnpinevents-onunblockcomplete">IMbnPinEvents.OnUnblockComplete</see>, <see href="https://docs.microsoft.com/windows/win32/api//mbnapi/nf-mbnapi-imbnpinevents-onchangecomplete">IMbnPinEvents.OnChangeComplete</see>, <see href="https://docs.microsoft.com/windows/win32/api//mbnapi/nf-mbnapi-imbnconnectioncontextevents-onsetprovisionedcontextcomplete">IMbnConnectionContextEvents.OnSetProvisionedContextComplete</see>, <see href="https://docs.microsoft.com/windows/win32/api//mbnapi/nf-mbnapi-imbnsmsevents-onsetsmsconfigurationcomplete">IMbnSmsEvents.OnSetSmsConfigurationComplete</see>, <see href="https://docs.microsoft.com/windows/win32/api//mbnapi/nf-mbnapi-imbnpinevents-ondisablecomplete">IMbnPinEvents.OnDisableComplete</see>, <see href="https://docs.microsoft.com/windows/win32/api//mbnapi/nf-mbnapi-imbnpinevents-onentercomplete">IMbnPinEvents.OnEnterComplete</see>, <see href="https://docs.microsoft.com/windows/win32/api//mbnapi/nf-mbnapi-imbnradioevents-onsetsoftwareradiostatecomplete">IMbnRadioEvents.OnSetSoftwareRadioStateComplete</see>, <see href="https://docs.microsoft.com/windows/win32/api//mbnapi/nf-mbnapi-imbnsmsevents-onsmssendcomplete">IMbnSmsEvents.OnSmsSendComplete</see>, <see href="https://docs.microsoft.com/windows/win32/api//mbnapi/nf-mbnapi-imbnsmsevents-onsmsreadcomplete">IMbnSmsEvents.OnSmsReadComplete</see>, <see href="https://docs.microsoft.com/windows/win32/api//mbnapi/nf-mbnapi-imbnsmsevents-onsmsdeletecomplete">IMbnSmsEvents.OnSmsDeleteComplete</see>, <see href="https://docs.microsoft.com/windows/win32/api//mbnapi/nf-mbnapi-imbnserviceactivationevents-onactivationcomplete">IMbnServiceActivationEvents.OnActivationComplete</see>, <see href="https://docs.microsoft.com/windows/win32/api//mbnapi/nf-mbnapi-imbnpinevents-onenablecomplete">IMbnPinEvents.OnEnableComplete</see>.</summary>
        public static readonly HRESULT S_OK = (HRESULT)(0);
    }
}
