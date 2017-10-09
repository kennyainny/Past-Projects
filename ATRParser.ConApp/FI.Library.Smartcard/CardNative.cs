using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections;

namespace FI.Library.Smartcard
{
    /// <summary>
    /// CARD_STATE enumeration, used by the PC/SC function SCardGetStatusChanged
    /// </summary>
    enum CARD_STATE
    {
        UNAWARE = 0x00000000,
        IGNORE = 0x00000001,
        CHANGED = 0x00000002,
        UNKNOWN = 0x00000004,
        UNAVAILABLE = 0x00000008,
        EMPTY = 0x00000010,
        PRESENT = 0x00000020,
        ATRMATCH = 0x00000040,
        EXCLUSIVE = 0x00000080,
        INUSE = 0x00000100,
        MUTE = 0x00000200,
        UNPOWERED = 0x00000400
    }

    /// <summary>
    /// Wraps the SCARD_IO_STRUCTURE
    ///  
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SCard_IO_Request
    {
        public UInt32 m_dwProtocol;
        public UInt32 m_cbPciLength;
    }


    /// <summary>
    /// Wraps theSCARD_READERSTATE structure of PC/SC
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SCard_ReaderState
    {
        public string m_szReader;
        public IntPtr m_pvUserData;
        public UInt32 m_dwCurrentState;
        public UInt32 m_dwEventState;
        public UInt32 m_cbAtr;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] m_rgbAtr;
    }

    /// <summary>
    /// Implementation of ICard using native (P/Invoke) interoperability for PC/SC
    /// </summary>
    public class CardNative : CardBase
    {
        private UInt32 m_hContext = 0;
        private UInt32 m_hCard = 0;
        private PROTOCOL m_nProtocol = PROTOCOL.T0;
        private PROTOCOL _establishedProtocol;

        private SmartcardContextSafeHandle _context, _cContext;
        private SmartcardErrorCode _lastErrorCode;

        //private bool isSimulated = false;

        #region PCSC_FUNCTIONS
        /// <summary>
        /// Native SCardGetStatusChanged from winscard.dll
        /// </summary>
        /// <param name="hContext"></param>
        /// <param name="dwTimeout"></param>
        /// <param name="rgReaderStates"></param>
        /// <param name="cReaders"></param>
        /// <returns></returns>
        [DllImport("winscard.dll", EntryPoint = "SCardGetStatusChange", CharSet = CharSet.Unicode,
            SetLastError = true)]
        internal static extern int GetStatusChange(SmartcardContextSafeHandle hContext,
            [In(), Out()] UInt32 dwTimeout,
            [In, Out] SCard_ReaderState[] rgReaderStates,
            [In, Out] UInt32 cReaders);

        /// <summary>
        /// Native SCardListReaders function from winscard.dll
        /// </summary>
        /// <param name="hContext"></param>
        /// <param name="mszGroups"></param>
        /// <param name="mszReaders"></param>
        /// <param name="pcchReaders"></param>
        /// <returns></returns>
        [DllImport("winscard.DLL", EntryPoint = "SCardListReaders", CharSet = CharSet.Unicode,
            SetLastError = true)]
        static internal extern uint ListReaders(SmartcardContextSafeHandle context, string groups,
            string readers, ref int size);

        /// <summary>
        /// Native SCardEstablishContext function from winscard.dll
        /// </summary>
        /// <param name="dwScope"></param>
        /// <param name="pvReserved1"></param>
        /// <param name="pvReserved2"></param>
        /// <param name="phContext"></param>
        /// <returns></returns>
        [DllImport("winscard.DLL", EntryPoint = "SCardEstablishContext", CharSet = CharSet.Unicode,
            SetLastError = true)]
        static internal extern uint EstablishContext(SCOPE scope, IntPtr reserved1,
            IntPtr reserved2, ref SmartcardContextSafeHandle context);

        /// <summary>
        /// Native SCardReleaseContext function from winscard.dll
        /// </summary>
        /// <param name="hContext"></param>
        /// <returns></returns>
        [DllImport("winscard.dll", EntryPoint = "SCardReleaseContext", CharSet = CharSet.Unicode,
            SetLastError = true)]
        internal static extern int ReleaseContext(SmartcardContextSafeHandle hContext);

        /// <summary>
        /// Native SCardConnect function from winscard.dll
        /// </summary>
        /// <param name="hContext"></param>
        /// <param name="szReader"></param>
        /// <param name="dwShareMode"></param>
        /// <param name="dwPreferredProtocols"></param>
        /// <param name="phCard"></param>
        /// <param name="pdwActiveProtocol"></param>
        /// <returns></returns>
        [DllImport("winscard.dll", EntryPoint = "SCardConnect", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int Connect(SmartcardContextSafeHandle context, [MarshalAs(UnmanagedType.LPWStr)] string szReader,
            SHARE dwShareMode, PROTOCOL dwPreferredProtocols, out SmartcardContextSafeHandle phCard, out PROTOCOL pdwActiveProtocol);

        /// <summary>
        /// Native SCardDisconnect function from winscard.dll
        /// </summary>
        /// <param name="hCard"></param>
        /// <param name="dwDisposition"></param>
        /// <returns></returns>
        [DllImport("winscard.dll", SetLastError = true, EntryPoint = "SCardDisconnect", CharSet = CharSet.Unicode)]
        internal static extern int Disconnect(SmartcardContextSafeHandle hCard,
            DISCONNECT dwDisposition);

        /// <summary>
        /// Native SCardTransmit function from winscard.dll
        /// </summary>
        /// <param name="hCard"></param>
        /// <param name="pioSendPci"></param>
        /// <param name="pbSendBuffer"></param>
        /// <param name="cbSendLength"></param>
        /// <param name="pioRecvPci"></param>
        /// <param name="pbRecvBuffer"></param>
        /// <param name="pcbRecvLength"></param>
        /// <returns></returns>
        [DllImport("winscard.dll", SetLastError = true, EntryPoint = "SCardTransmit", CharSet = CharSet.Unicode)]
        internal static extern int Transmit(SmartcardContextSafeHandle context,
            [In] ref SCard_IO_Request pioSendPci,
            byte[] pbSendBuffer,
            UInt32 cbSendLength,
            IntPtr pioRecvPci,
            [Out] byte[] pbRecvBuffer,
            out UInt32 pcbRecvLength
            );

        /// <summary>
        /// Native SCardBeginTransaction function of winscard.dll
        /// </summary>
        /// <param name="hContext"></param>
        /// <returns></returns>
        [DllImport("winscard.dll", SetLastError = true, EntryPoint = "SCardBeginTransaction", CharSet = CharSet.Unicode)]
        internal static extern int BeginTransaction(SmartcardContextSafeHandle hContext);

        /// <summary>
        /// Native SCardEndTransaction function of winscard.dll
        /// </summary>
        /// <param name="hContext"></param>
        /// <returns></returns>
        [DllImport("winscard.dll", SetLastError = true, EntryPoint = "SCardEndTransaction", CharSet = CharSet.Unicode)]
        internal static extern int EndTransaction(SmartcardContextSafeHandle hContext, DISCONNECT dwDisposition);

        /// <summary>
        /// Native SCardGetAttrib function of winscard.dll
        /// </summary>
        /// <param name="hCard"></param>
        /// <param name="dwAttribId"></param>
        /// <param name="pbAttr"></param>
        /// <param name="pcbAttrLen"></param>
        /// <returns></returns>
        [DllImport("winscard.dll", EntryPoint = "SCardGetAttrib")]
        internal static extern uint GetAttrib(SmartcardContextSafeHandle hCard, UInt32 dwAttrId,
            [Out] byte[] pbAttr, out UInt32 pcbAttrLen);

        #endregion WINSCARD_FUNCTIONS

        /// <summary>
        /// Default constructor
        /// </summary>
        public CardNative()
        {
            _context = new SmartcardContextSafeHandle();
            _cContext = new SmartcardContextSafeHandle();
        }

        /// <summary>
        /// Object destruction
        /// </summary>
        ~CardNative()
        {
            Disconnect(DISCONNECT.Unpower);
            ReleaseContext();
        }

        #region ICard Members

        /// <summary>
        /// Wraps the PCSC function
        /// LONG SCardListReaders(SCARDCONTEXT hContext, 
        ///		LPCTSTR mszGroups, 
        ///		LPTSTR mszReaders, 
        ///		LPDWORD pcchReaders 
        ///	);
        /// </summary>
        /// <returns>A string array of the readers</returns>
        public override string[] ListReaders()
        {
            ArrayList result = new ArrayList();

            //Make sure a context has been established before 
            //retrieving the list of smartcard readers.
            if (EstablishContext(SCOPE.System))
            {
                //Ask for the size of the buffer first.
                int size = GetReaderListBufferSize();

                //Allocate a string of the proper size in which 
                //to store the list of smartcard readers.
                string readerList = new string('\0', size);
                //Retrieve the list of smartcard readers.
                _lastErrorCode =
                    (SmartcardErrorCode)ListReaders(_context,
                    null, readerList, ref size);
                if ((_lastErrorCode == SmartcardErrorCode.None))
                {
                    //Extract each reader from the returned list.
                    //The readerList string will contain a multi-string of 
                    //the reader names, i.e. they are seperated by 0x00 
                    //characters.
                    string readerName = string.Empty;
                    for (int i = 0; i <= readerList.Length - 1; i++)
                    {
                        if ((readerList[i] == '\0'))
                        {
                            if ((readerName.Length > 0))
                            {
                                //We have a smartcard reader's name.
                                result.Add(readerName);
                                readerName = string.Empty;
                            }
                        }
                        else
                        {
                            //Append the found character.
                            readerName += new string(readerList[i], 1);
                        }
                    }
                }
            }
            else
            {
                throw new Exception("PC/SC error");
            }
            return result.ToArray(typeof(string)) as string[];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetReaderListBufferSize()
        {
            if ((_context.IsInvalid))
            {
                return 0;
            }
            int result = 0;
            _lastErrorCode =
                (SmartcardErrorCode)ListReaders(
                _context, null, null, ref result);
            return result;
        }

        /// <summary>
        /// Wraps the PCSC function
        /// LONG SCardReleaseContext(
        ///		IN SCARDCONTEXT hContext
        ///	);
        /// </summary>
        public void ReleaseContext()
        {
            if (HasContext)
            {
                _lastErrorCode = (SmartcardErrorCode)ReleaseContext(_context);

                if (_lastErrorCode != 0)
                {
                    string msg = "SCardReleaseContext error: " + _lastErrorCode;
                    throw new Exception(msg);
                }

                m_hContext = 0;
            }
        }

        /// <summary>
        ///  Wraps the PCSC function
        ///  LONG SCardConnect(
        ///		IN SCARDCONTEXT hContext,
        ///		IN LPCTSTR szReader,
        ///		IN DWORD dwShareMode,
        ///		IN DWORD dwPreferredProtocols,
        ///		OUT LPSCARDHANDLE phCard,
        ///		OUT LPDWORD pdwActiveProtocol
        ///	);
        /// </summary>
        /// <param name="Reader"></param>
        /// <param name="ShareMode"></param>
        /// <param name="PreferredProtocols"></param>
        public override void Connect(string Reader, SHARE ShareMode, PROTOCOL PreferredProtocols)
        {
            if (EstablishContext(SCOPE.System))
            {
                _lastErrorCode = (SmartcardErrorCode)Connect(_context, Reader, ShareMode, PreferredProtocols, out _cContext, out _establishedProtocol);
                Console.WriteLine(_lastErrorCode.ToString());
                if (_lastErrorCode == SmartcardErrorCode.None)
                {
                    return;
                }
                else
                    throw new Exception("Card not found");
            }
            else
            {
                throw new Exception("PC/SC error");
            }
           
        }

        /// <summary>
        /// Wraps the PCSC function
        ///	LONG SCardDisconnect(
        ///		IN SCARDHANDLE hCard,
        ///		IN DWORD dwDisposition
        ///	);
        /// </summary>
        /// <param name="Disposition"></param>
        public override void Disconnect(DISCONNECT Disposition)
        {
            if (!_cContext.IsInvalid)
            {
                _lastErrorCode = (SmartcardErrorCode)Disconnect(_cContext, Disposition);
                m_hCard = 0;

                if (_lastErrorCode != SmartcardErrorCode.None)
                {
                    string msg = "SCardDisconnect error: " + _lastErrorCode;
                    throw new Exception(msg);
                }

            }
        }

        /// <summary>
        /// Wraps the PCSC function
        /// LONG SCardTransmit(
        ///		SCARDHANDLE hCard,
        ///		LPCSCARD_I0_REQUEST pioSendPci,
        ///		LPCBYTE pbSendBuffer,
        ///		DWORD cbSendLength,
        ///		LPSCARD_IO_REQUEST pioRecvPci,
        ///		LPBYTE pbRecvBuffer,
        ///		LPDWORD pcbRecvLength
        ///	);
        /// </summary>
        /// <param name="ApduCmd">APDUCommand object with the APDU to send to the card</param>
        /// <returns>An APDUResponse object with the response from the card</returns>
        public override APDUResponse Transmit(APDUCommand ApduCmd)
        {
            //uint RecvLength = (uint)(ApduCmd.Le + APDUResponse.SW_LENGTH);
            uint RecvLength = 255;
            byte[] ApduBuffer = null;
            //byte[] ApduResponse = new byte[ApduCmd.Le + APDUResponse.SW_LENGTH]; 
            byte[] ApduResponse = new byte[255];
            SCard_IO_Request ioRequest = new SCard_IO_Request();
            ioRequest.m_dwProtocol = (uint)m_nProtocol;
            ioRequest.m_cbPciLength = 8;

            // Build the command APDU
            if (ApduCmd.Data == null)
            {
                ApduBuffer = new byte[APDUCommand.APDU_MIN_LENGTH + ((ApduCmd.Le != 0) ? 1 : 0)];

                if (ApduCmd.Le != 0)
                    ApduBuffer[4] = (byte)ApduCmd.Le;
            }
            else
            {
                ApduBuffer = new byte[APDUCommand.APDU_MIN_LENGTH + 1 + ApduCmd.Data.Length];

                for (int nI = 0; nI < ApduCmd.Data.Length; nI++)
                    ApduBuffer[APDUCommand.APDU_MIN_LENGTH + 1 + nI] = ApduCmd.Data[nI];

                ApduBuffer[APDUCommand.APDU_MIN_LENGTH] = (byte)ApduCmd.Data.Length;
            }

            ApduBuffer[0] = ApduCmd.Class;
            ApduBuffer[1] = ApduCmd.Ins;
            ApduBuffer[2] = ApduCmd.P1;
            ApduBuffer[3] = ApduCmd.P2;

            _lastErrorCode = (SmartcardErrorCode)Transmit(_cContext, ref ioRequest, ApduBuffer, (uint)ApduBuffer.Length, IntPtr.Zero, ApduResponse, out RecvLength);
            if (_lastErrorCode != 0)
            {
                ApduResponse = new byte[RecvLength];
                _lastErrorCode = (SmartcardErrorCode)Transmit(_cContext, ref ioRequest, ApduBuffer, (uint)ApduBuffer.Length, IntPtr.Zero, ApduResponse, out RecvLength);
                if (_lastErrorCode != 0)
                {
                    string msg = "SCardTransmit error: " + _lastErrorCode;
                    throw new Exception(msg);
                }
            }
            byte[] ApduData = new byte[RecvLength];

            for (int nI = 0; nI < RecvLength; nI++)
                ApduData[nI] = ApduResponse[nI];

            return new APDUResponse(ApduData);
        }

        /// <summary>
        /// Wraps the PSCS function
        /// LONG SCardBeginTransaction(
        ///     SCARDHANDLE hCard
        ///  );
        /// </summary>
        public override void BeginTransaction()
        {
            if (m_hCard != 0)
            {
                _lastErrorCode = (SmartcardErrorCode)BeginTransaction(_cContext);
                if (_lastErrorCode != 0)
                {
                    string msg = "SCardBeginTransaction error: " + _lastErrorCode;
                    throw new Exception(msg);
                }
            }
        }

        /// <summary>
        /// Wraps the PCSC function
        /// LONG SCardEndTransaction(
        ///     SCARDHANDLE hCard,
        ///     DWORD dwDisposition
        /// );
        /// </summary>
        /// <param name="Disposition">A value from DISCONNECT enum</param>
        public override void EndTransaction(DISCONNECT Disposition)
        {
            if (m_hCard != 0)
            {
                _lastErrorCode = (SmartcardErrorCode)EndTransaction(_cContext, Disposition);
                if (_lastErrorCode != 0)
                {
                    string msg = "SCardEndTransaction error: " + _lastErrorCode;
                    throw new Exception(msg);
                }
            }
        }

        /// <summary>
        /// Gets the attributes of the card
        /// </summary>
        /// <param name="AttribId">Identifier for the Attribute to get</param>
        /// <returns>Attribute content</returns>
        public override byte[] GetAttribute(UInt32 AttribId)
        {
            byte[] attr = new byte[]{ };
            UInt32 attrLen = 0;

            string msg;

            if (EstablishContext(SCOPE.System))
            {
                _lastErrorCode = (SmartcardErrorCode)GetAttrib(_cContext, AttribId, attr, out attrLen);
                if (_lastErrorCode == SmartcardErrorCode.None)
                {
                    attr = new byte[attrLen];
                    _lastErrorCode = (SmartcardErrorCode)GetAttrib(_cContext, AttribId, attr, out attrLen);
                    if (_lastErrorCode == SmartcardErrorCode.None)
                        return attr;
                    else
                    {
                        msg = "SCardGetAttr error: " + _lastErrorCode;
                        throw new Exception(msg);
                    }
                }
                else
                {
                    msg = "SCardGetAttr error: " + _lastErrorCode;
                    throw new Exception(msg);
                }
            }
            msg = "SCardGetAttr error: Unable to establish card context";
            throw new Exception(msg);
        }
        #endregion

        public SmartcardContextSafeHandle Context { get { return _context; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool EstablishContext(SCOPE scope)
        {
            if ((HasContext))
            {
                return true;
            }
            _lastErrorCode =
                (SmartcardErrorCode)EstablishContext(scope,
                IntPtr.Zero, IntPtr.Zero, ref _context);
            return (_lastErrorCode == SmartcardErrorCode.None);
        }

        /// <summary>
        /// 
        /// </summary>
        private bool HasContext
        {
            get { try { return (!_context.IsInvalid); } catch { return false; } }
        }

        /// <summary>
        /// This function must implement a card detection mechanism.
        /// 
        /// When card insertion is detected, it must call the method CardInserted()
        /// When card removal is detected, it must call the method CardRemoved()
        /// 
        /// </summary>
        protected override void RunCardDetection(object Reader)
        {
            bool bFirstLoop = true;
            UInt32 hContext = 0;    // Local context
            IntPtr phContext;

            phContext = Marshal.AllocHGlobal(Marshal.SizeOf(hContext));

            if (EstablishContext(SCOPE.System))
            {
                hContext = (uint)Marshal.ReadInt32(phContext);
                Marshal.FreeHGlobal(phContext);

                UInt32 nbReaders = 1;
                SCard_ReaderState[] readerState = new SCard_ReaderState[nbReaders];

                readerState[0].m_dwCurrentState = (UInt32)CARD_STATE.UNAWARE;
                readerState[0].m_szReader = (string)Reader;

                UInt32 eventState;
                UInt32 currentState = readerState[0].m_dwCurrentState;

                // Card detection loop
                do
                {
                    if (GetStatusChange(_cContext, WAIT_TIME
                        , readerState, nbReaders) == 0)
                    {
                        eventState = readerState[0].m_dwEventState;
                        currentState = readerState[0].m_dwCurrentState;

                        // Check state
                        if (((eventState & (uint)CARD_STATE.CHANGED) == (uint)CARD_STATE.CHANGED) && !bFirstLoop)
                        {
                            // State has changed
                            if ((eventState & (uint)CARD_STATE.EMPTY) == (uint)CARD_STATE.EMPTY)
                            {
                                // There is no card, card has been removed -> Fire CardRemoved event
                                CardRemoved();
                            }

                            if (((eventState & (uint)CARD_STATE.PRESENT) == (uint)CARD_STATE.PRESENT) &&
                                ((eventState & (uint)CARD_STATE.PRESENT) != (currentState & (uint)CARD_STATE.PRESENT)))
                            {
                                // There is a card in the reader -> Fire CardInserted event
                                CardInserted();
                            }

                            if ((eventState & (uint)CARD_STATE.ATRMATCH) == (uint)CARD_STATE.ATRMATCH)
                            {
                                // There is a card in the reader and it matches the ATR we were expecting-> Fire CardInserted event
                                CardInserted();
                            }
                        }

                        // The current stateis now the event state
                        readerState[0].m_dwCurrentState = eventState;

                        bFirstLoop = false;
                    }

                    Thread.Sleep(100);

                    if (m_bRunCardDetection == false)
                        break;
                }
                while (true);    // Exit on request
            }
            else
            {
                throw new Exception("PC/SC error");
            }

        }
    }
}
