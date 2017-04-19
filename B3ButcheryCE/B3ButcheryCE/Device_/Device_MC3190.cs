using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Symbol.ResourceCoordination;

namespace B3HRCE.Device_
{
    public class Device_MC3190 : DeviceWinCE
    {

        public Symbol.Barcode.Reader mReader = null;
        public Symbol.Barcode.ReaderData mReaderData = null;

        void InitReader2()
        {
            Symbol.Generic.Device MyDevice =
     Symbol.StandardForms.SelectDevice.Select(
     Symbol.Barcode.Device.Title,
     Symbol.Barcode.Device.AvailableDevices);

            mReader = new Symbol.Barcode.Reader(MyDevice);

            // Create the reader data.
            mReaderData = new Symbol.Barcode.ReaderData(
                Symbol.Barcode.ReaderDataTypes.Text,
                Symbol.Barcode.ReaderDataLengths.MaximumLabel);

            // Enable the Reader.
            mReader.Actions.Enable();

            switch (mReader.ReaderParameters.ReaderType)
            {
                case Symbol.Barcode.READER_TYPE.READER_TYPE_IMAGER:
                    mReader.ReaderParameters.ReaderSpecific.ImagerSpecific.AimType = Symbol.Barcode.AIM_TYPE.AIM_TYPE_TRIGGER;
                    break;
                case Symbol.Barcode.READER_TYPE.READER_TYPE_LASER:
                    mReader.ReaderParameters.ReaderSpecific.LaserSpecific.AimType = Symbol.Barcode.AIM_TYPE.AIM_TYPE_TRIGGER;
                    break;
                case Symbol.Barcode.READER_TYPE.READER_TYPE_CONTACT:
                    throw new Exception("not support READER_TYPE_CONTACT");
            }
            mReader.Actions.SetParameters();

            mReader.ReadNotify += new EventHandler(mReader_ReadNotify);

        }

        void mReader_ReadNotify(object sender, EventArgs e)
        {
            Symbol.Barcode.ReaderData TheReaderData = mReader.GetNextReaderData();
            if (mReaderData.Result == Symbol.Results.SUCCESS)
            {
                var code = TheReaderData.Text;
                this.OnScannerReader(this, new ScanEventArgs() { BarCode = code });
            }
            StartRead(false);
        }


        /// <summary>
        /// Initialize the reader.
        /// </summary>
        public bool InitReader()
        {
            // If the reader is already initialized then fail the initialization.
            if (mReader != null)
            {
                return false;
            }
            else // Else initialize the reader.
            {
                try
                {
                    // Get the device selected by the user.
                    Symbol.Generic.Device MyDevice =
                        Symbol.StandardForms.SelectDevice.Select(
                        Symbol.Barcode.Device.Title,
                        Symbol.Barcode.Device.AvailableDevices);

                    if (MyDevice == null)
                    {
                        MessageBox.Show("NoDeviceSelected");
                        return false;
                    }

                    // Create the reader, based on selected device.
                    mReader = new Symbol.Barcode.Reader(MyDevice);

                    // Create the reader data.
                    mReaderData = new Symbol.Barcode.ReaderData(
                        Symbol.Barcode.ReaderDataTypes.Text,
                        Symbol.Barcode.ReaderDataLengths.MaximumLabel);

                    // Enable the Reader.
                    mReader.Actions.Enable();

                    // In this sample, we are setting the aim type to trigger. 
                    switch (mReader.ReaderParameters.ReaderType)
                    {
                        case Symbol.Barcode.READER_TYPE.READER_TYPE_IMAGER:
                            mReader.ReaderParameters.ReaderSpecific.ImagerSpecific.AimType = Symbol.Barcode.AIM_TYPE.AIM_TYPE_TRIGGER;
                            break;
                        case Symbol.Barcode.READER_TYPE.READER_TYPE_LASER:
                            mReader.ReaderParameters.ReaderSpecific.LaserSpecific.AimType = Symbol.Barcode.AIM_TYPE.AIM_TYPE_TRIGGER;
                            break;
                        case Symbol.Barcode.READER_TYPE.READER_TYPE_CONTACT:
                            // AimType is not supported by the contact readers.
                            break;
                    }
                    mReader.Actions.SetParameters();


                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                    return false;
                }

                return true;
            }
        }



        /// <summary>
        /// Start a read on the reader.
        /// </summary>
        public void StartRead(bool toggleSoftTrigger)
        {
            if (!mReaderData.IsPending)
            {
                // Submit a read.
                mReader.Actions.Read(mReaderData);

                if (toggleSoftTrigger && mReader.Info.SoftTrigger == false)
                {
                    mReader.Info.SoftTrigger = true;
                }
            }

        }


        /// <summary>
        /// Stop all reads on the reader.
        /// </summary>
        public void StopRead()
        {
            //If we have a reader
            if (mReader != null)
            {
                try
                {
                    // Flush (Cancel all pending reads).
                    if (mReader.Info.SoftTrigger == true)
                    {
                        mReader.Info.SoftTrigger = false;
                    }
                    mReader.Actions.Flush();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }



        /// <summary>
        /// Attach a ReadNotify handler.
        /// </summary>
        public override void AttachReadNotify(System.EventHandler _ReadNotifyHandler)
        {
            // If we have a reader
            if (mReader != null)
            {
                // Attach the read notification handler.
                mReader.ReadNotify += _ReadNotifyHandler;
                ReadNotifyHandler = _ReadNotifyHandler;
            }

        }

        public override void ScanClose()
        {
            DetachReadNotify();
            DetachStatusNotify();
            TermReader();

        }

        /// <summary>
        /// Detach the ReadNotify handler.
        /// </summary>
        public void DetachReadNotify()
        {
            if ((mReader != null) && (ReadNotifyHandler != null))
            {
                // Detach the read notification handler.
                mReader.ReadNotify -= ReadNotifyHandler;
                ReadNotifyHandler = null;
            }
        }

        /// <summary>
        /// Attach a StatusNotify handler.
        /// </summary>
        public override void AttachStatusNotify(System.EventHandler _StatusNotifyHandler)
        {
            // If we have a reader
            if (mReader != null)
            {
                // Attach status notification handler.
                mReader.StatusNotify += _StatusNotifyHandler;
                StatusNotifyHandler = _StatusNotifyHandler;
            }
        }

        /// <summary>
        /// Detach a StatusNotify handler.
        /// </summary>
        public void DetachStatusNotify()
        {
            // If we have a reader registered for receiving the status notifications
            if ((mReader != null) && (StatusNotifyHandler != null))
            {
                // Detach the status notification handler.
                mReader.StatusNotify -= StatusNotifyHandler;
                StatusNotifyHandler = null;
            }
        }

        /// <summary>
        /// Stop reading and disable/close the reader.
        /// </summary>
        public void TermReader()
        {
            // If we have a reader
            if (mReader != null)
            {
                try
                {
                    // stop all the notifications.
                    StopRead();

                    //Detach all the notification handler if the user has not done it already.
                    DetachReadNotify();
                    DetachStatusNotify();

                    // Disable the reader.
                    mReader.Actions.Disable();

                    // Free it up.
                    mReader.Dispose();

                    // Make the reference null.
                    mReader = null;
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

            // After disposing the reader, dispose the reader data. 
            if (mReaderData != null)
            {
                try
                {
                    // Free it up.
                    mReaderData.Dispose();

                    // Make the reference null.
                    mReaderData = null;
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        public override void ReaderInitiated()
        {
            InitReader2();
        }

        public override string GetGetSerialNumber()
        {

            var terminalInfo = new TerminalInfo();

            // Initialize UUID tab page
            var uuid = string.Empty;
            if (terminalInfo.UniqueUnitID != null)
            {
                foreach (byte b in terminalInfo.UniqueUnitID)
                    uuid += b.ToString("X2");
            }
            else
            {
                uuid = "UUID not set";
            }

            return uuid;
        }

        public override void ScanPowerOn()
        {
            StartRead(false);
        }

        public override void ScanPowerOff()
        {
            StopRead();
            DetachStatusNotify();
        }

        public override void HardWareInit()
        {
            InitReader2();
            //InitReader();
        }

        public override void HardWareDeInit()
        {
            //TermReader();
        }

        public override bool IsWIFILoaded()
        {
            throw new NotImplementedException();
        }

        public override void OpenWIFI()
        {

        }

        public override void CloseWIFI()
        {

        }

        public override void ControlPanelSound()
        {
            throw new NotImplementedException();
        }

        public override void ControlPanelInputMethod()
        {
            throw new NotImplementedException();
        }

        public override System.Windows.Forms.Keys ScanCodeKey
        {
            get { return EmptyKey; }
        }

        public override System.Windows.Forms.Keys SwitchInputPanelKey
        {
            get { return EmptyKey; }
        }

        public override System.Windows.Forms.Keys SwitchInputMethodKey
        {
            get { return EmptyKey; }
        }

        public override string DataRootDirection
        {
            get
            {
                return string.Empty;
                //return "\\Storage Card";
            }
        }

        public override string ToString()
        {
            return "MC3190";
        }
    }
}
