using AForge.Video;
using AForge.Video.DirectShow;
using BLL;
using System;
using System.Drawing;
using System.Windows.Forms;
using ZXing;

namespace GUI
{
    public partial class frmScanQR : Form
    {
        private FilterInfoCollection _videoDevices;
        private VideoCaptureDevice _videoSource;
        private BarcodeReader _barcodeReader;
        private bool _isScanning = false;
        private bool _isClosing = false;

        public event EventHandler<string> QRCodeScanned;

        public frmScanQR()
        {
            InitializeComponent();
            _barcodeReader = new BarcodeReader
            {
                AutoRotate = true,
                TryInverted = true,
                Options = new ZXing.Common.DecodingOptions
                {
                    TryHarder = true,
                    PossibleFormats = new[] { BarcodeFormat.QR_CODE, BarcodeFormat.CODE_128 }
                }
            };
        }

        private void frmScanQR_Load(object sender, EventArgs e)
        {
            LoadVideoDevices();

            if (_videoDevices != null && _videoDevices.Count > 0)
            {
                StartCamera();
            }
        }

        private void LoadVideoDevices()
        {
            try
            {
                _videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (_videoDevices.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy camera trên máy tính này.",
                        "Không có Camera",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                cboCamera.Items.Clear();
                foreach (FilterInfo device in _videoDevices)
                {
                    cboCamera.Items.Add(device.Name);
                }

                int defaultCameraIndex = 0;
                for (int i = 0; i < _videoDevices.Count; i++)
                {
                    string deviceName = _videoDevices[i].Name.ToLower();
                    if (!deviceName.Contains("obs") && !deviceName.Contains("virtual"))
                    {
                        defaultCameraIndex = i;
                        break;
                    }
                }

                cboCamera.SelectedIndex = defaultCameraIndex;
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách camera: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (cboCamera.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn camera.",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            StartCamera();
        }

        private void StartCamera()
        {
            try
            {
                if (_videoSource != null && _videoSource.IsRunning)
                {
                    StopCamera();
                }

                _videoSource = new VideoCaptureDevice(_videoDevices[cboCamera.SelectedIndex].MonikerString);
                _videoSource.NewFrame += VideoSource_NewFrame;
                _videoSource.Start();

                timerScan.Start();
                _isScanning = true;

                btnStart.Enabled = false;
                btnStop.Enabled = true;
                cboCamera.Enabled = false;
                txtResult.Clear();
                txtResult.Text = "Đang quét... Hãy đưa mã QR vào trước camera.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi động camera: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopCamera();
        }

        private void StopCamera()
        {
            try
            {
                _isScanning = false;
                timerScan.Stop();

                if (_videoSource != null && _videoSource.IsRunning)
                {
                    _videoSource.SignalToStop();
                    _videoSource.NewFrame -= VideoSource_NewFrame;
                }

                // Cleanup UI
                if (picCamera.InvokeRequired)
                {
                    picCamera.BeginInvoke(new Action(() =>
                    {
                        if (picCamera.Image != null)
                        {
                            Image oldImage = picCamera.Image;
                            picCamera.Image = null;
                            oldImage.Dispose();
                        }
                    }));
                }
                else
                {
                    if (picCamera.Image != null)
                    {
                        Image oldImage = picCamera.Image;
                        picCamera.Image = null;
                        oldImage.Dispose();
                    }
                }

                btnStart.Enabled = true;
                btnStop.Enabled = false;
                cboCamera.Enabled = true;
                txtResult.Clear();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error stopping camera: {ex.Message}");
            }
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (_isClosing) return;

            try
            {
                Bitmap frame = (Bitmap)eventArgs.Frame.Clone();

                if (picCamera.InvokeRequired)
                {
                    picCamera.BeginInvoke(new Action(() =>
                    {
                        if (_isClosing) return;

                        if (picCamera.Image != null)
                        {
                            Image oldImage = picCamera.Image;
                            picCamera.Image = null;
                            oldImage.Dispose();
                        }
                        picCamera.Image = frame;
                    }));
                }
                else
                {
                    if (picCamera.Image != null)
                    {
                        Image oldImage = picCamera.Image;
                        picCamera.Image = null;
                        oldImage.Dispose();
                    }
                    picCamera.Image = frame;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in NewFrame: {ex.Message}");
            }
        }

        private void timerScan_Tick(object sender, EventArgs e)
        {
            if (!_isScanning || picCamera.Image == null || _isClosing)
                return;

            try
            {
                Bitmap imageCopy = null;

                // ✅ SỬ DỤNG SYNCHRONOUS COPY (AN TOÀN HỚN VỚI TIMER)
                if (picCamera.Image != null && !_isClosing)
                {
                    lock (picCamera) // ✅ LOCK ĐỂ TRÁNH RACE CONDITION
                    {
                        if (picCamera.Image != null)
                        {
                            imageCopy = new Bitmap(picCamera.Image);
                        }
                    }
                }

                if (imageCopy == null)
                    return;

                var result = _barcodeReader.Decode(imageCopy);
                imageCopy.Dispose();

                if (result != null && !_isClosing)
                {
                    _isClosing = true;
                    _isScanning = false;
                    timerScan.Stop();

                    // Beep
                    try
                    {
                        Console.Beep(1000, 200);
                    }
                    catch
                    {
                        System.Media.SystemSounds.Beep.Play();
                    }

                    // Parse QR
                    var qrCodeBLL = new QRCodeBLL();
                    string sku = qrCodeBLL.ParseQRCode(result.Text);

                    // Update UI
                    txtResult.Text = $"✅ SKU: {sku}";
                    txtResult.ForeColor = Color.Green;

                    // Raise event
                    QRCodeScanned?.Invoke(this, sku);

                    // Stop camera
                    StopCamera();

                    // ✅ ĐÓNG FORM SAU 200MS (CHO CAMERA DỪNG AN TOÀN)
                    var closeTimer = new System.Windows.Forms.Timer();
                    closeTimer.Interval = 200;
                    closeTimer.Tick += (s, args) =>
                    {
                        closeTimer.Stop();
                        closeTimer.Dispose();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    };
                    closeTimer.Start();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in timer scan: {ex.Message}");
            }
        }

        private void frmScanQR_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isClosing = true;
            _isScanning = false;
            timerScan.Stop();

            //  CLEANUP CAMERA - CHỈ SIGNAL, KHÔNG WAIT
            if (_videoSource != null)
            {
                if (_videoSource.IsRunning)
                {
                    _videoSource.SignalToStop();
                    _videoSource.NewFrame -= VideoSource_NewFrame;
                }
                _videoSource = null;
            }

            // Cleanup image
            if (picCamera.Image != null)
            {
                try
                {
                    picCamera.Image.Dispose();
                    picCamera.Image = null;
                }
                catch { }
            }
        }
    }
}