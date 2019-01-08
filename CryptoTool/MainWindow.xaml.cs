using CryptoTool.Models;
using CryptoTool.Properties;
using CryptoTool.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CryptoTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region object-functions

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Settings.Default.x_AddClipClosed)
                ExportToClipboard();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case WindowState.Minimized:
                    if (Settings.Default.x_AddClipMinimized)
                        ExportToClipboard();
                    break;
            }
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Settings.Default.x_AddClipMouseLeave)
                ExportToClipboard();
        }

        private void btnCopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            switch (_operationType)
            {
                case OperationType.Encoding:
                    Clipboard.SetText(Output.Text);
                    break;

                case OperationType.Encryption:
                    break;

                case OperationType.Hashing:
                    break;

                case OperationType.Obfuscation:
                    break;
            }
        }

        private void SetOperationEncoding_Click(object sender, RoutedEventArgs e)
        {
            _operationType = OperationType.Encoding;
            SetElements(_tabControlRow: 1, _tabEncoding: 1);
        }

        private void SetOperationEncryption_Click(object sender, RoutedEventArgs e)
        {
            _operationType = OperationType.Encryption;
            SetElements(_tabControlRow: 1, _secretKeyRow: 1, _tabEncryption: 1);
        }

        private void SetOperationHashing_Click(object sender, RoutedEventArgs e)
        {
            _operationType = OperationType.Hashing;
            if (Settings.Default.x_HMACEnabled)
            {
                HMACCheckbox.IsChecked = true;
                SetElements(_hashingAlgorithmRow: 1, _secretKeyRow: 1);
            }
            else
            {
                HMACCheckbox.IsChecked = false;
                SetElements(_hashingAlgorithmRow: 1);
            }
        }

        private void SetOperationObfuscation_Click(object sender, RoutedEventArgs e)
        {
            _operationType = OperationType.Obfuscation;
            SetElements();
        }

        private void SetScreenSettings_Click(object sender, RoutedEventArgs e)
        {
            SetElements(_settings: 1);
        }

        private void SetScreenAbout_Click(object sender, RoutedEventArgs e)
        {
            SetElements(_about: 1);
        }

        private void HMACCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (HMACCheckbox.IsChecked.Value)
                SetElements(_secretKeyRow: 1, _hashingAlgorithmRow: 1);
            else
                SetElements(_hashingAlgorithmRow: 1);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            clearText(false);
        }

        private void settingsCheckbox_ImportFromClipboard_Click(object sender, RoutedEventArgs e)
        {
            if (settingsCheckbox_ImportFromClipboard.IsChecked.Value)
                Settings.Default.x_ImportFromClipboard = true;
            else
                Settings.Default.x_ImportFromClipboard = false;

            Settings.Default.Save();
        }

        private void settingsCheckbox_AddClipMinimized_Click(object sender, RoutedEventArgs e)
        {
            if (settingsCheckbox_AddClipMinimized.IsChecked.Value)
                Settings.Default.x_AddClipMinimized = true;
            else
                Settings.Default.x_AddClipMinimized = false;

            Settings.Default.Save();
        }

        private void settingsCheckbox_AddClipClosed_Click(object sender, RoutedEventArgs e)
        {
            if (settingsCheckbox_AddClipClosed.IsChecked.Value)
                Settings.Default.x_AddClipClosed = true;
            else
                Settings.Default.x_AddClipClosed = false;

            Settings.Default.Save();
        }

        private void settingsCheckbox_AddClipMouseLeave_Click(object sender, RoutedEventArgs e)
        {
            if (settingsCheckbox_AddClipMouseLeave.IsChecked.Value)
                Settings.Default.x_AddClipMouseLeave = true;
            else
                Settings.Default.x_AddClipMouseLeave = false;

            Settings.Default.Save();
        }

        private void settingsCheckbox_HMACDefault_Click(object sender, RoutedEventArgs e)
        {
            if (settingsCheckbox_HMACDefault.IsChecked.Value)
                Settings.Default.x_HMACEnabled = true;
            else
                Settings.Default.x_HMACEnabled = false;

            Settings.Default.Save();
        }

        private void btnSwap_Click(object sender, RoutedEventArgs e)
        {
            string aux = LabelOutput.Text;
            LabelOutput.Text = LabelInput.Text;
            LabelInput.Text = aux;

            switch (_encodingType)
            {
                case EncodingType.Base64_Decode:
                    _encodingType = EncodingType.Base64_Encode;
                    break;

                case EncodingType.Base64_Encode:
                    _encodingType = EncodingType.Base64_Decode;
                    break;

                case EncodingType.ASCII_Decode:
                    _encodingType = EncodingType.ASCII_Encode;
                    break;

                case EncodingType.ASCII_Encode:
                    _encodingType = EncodingType.ASCII_Decode;
                    break;

                case EncodingType.Binary_Decode:
                    _encodingType = EncodingType.Binary_Encode;
                    break;

                case EncodingType.Binary_Encode:
                    _encodingType = EncodingType.Binary_Decode;
                    break;
            }

            doEncodingWork();
        }

        private void Input_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            clearText(true);
        }

        private void EncodingInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsLoaded)
                doEncodingWork();
        }

        private void buttonShowDock_Click(object sender, RoutedEventArgs e)
        {
            if (!isAnimating)
                move = MoveMenu(AnimationType.Animation_IN);
        }

        private void dockPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!isAnimating)
                move = MoveMenu(AnimationType.Animation_OUT);
        }

        #endregion object-functions

        #region methods

        private void ExportToClipboard()
        {
            Clipboard.SetText(Output.Text);
        }

        private void doEncodingWork()
        {
            switch (_encodingType)
            {
                case EncodingType.Base64_Decode:
                    Output.Text = EncodingOperations.Base64Operations.Decode(Input.Text);
                    break;

                case EncodingType.Base64_Encode:
                    Output.Text = EncodingOperations.Base64Operations.Encode(Input.Text);
                    break;

                case EncodingType.ASCII_Decode:
                    Output.Text = EncodingOperations.ASCIIOperations.Decode(Input.Text);
                    break;

                case EncodingType.ASCII_Encode:
                    Output.Text = EncodingOperations.ASCIIOperations.Encode(Input.Text);
                    break;

                case EncodingType.Binary_Decode:
                    Output.Text = EncodingOperations.BinaryOperations.Decode(Input.Text);
                    break;

                case EncodingType.Binary_Encode:
                    Output.Text = EncodingOperations.BinaryOperations.Encode(Input.Text);
                    break;
            }
        }

        private void clearText(bool check)
        {
            switch (_operationType)
            {
                case OperationType.Encoding:
                    if (check)
                    {
                        if (Input.Text == "Input..." && Output.Text == "Output...")
                        {
                            Input.Text = string.Empty;
                            Output.Text = string.Empty;
                        }
                    }
                    else
                    {
                        Input.Text = string.Empty;
                        Output.Text = string.Empty;
                    }
                    break;

                case OperationType.Encryption:
                    break;

                case OperationType.Hashing:
                    break;

                case OperationType.Obfuscation:
                    break;
            }
        }

        private void SetElements(int _tabControlRow = 0, int _secretKeyRow = 0, int _hashingAlgorithmRow = 0,
                                 int _tabEncoding = 0, int _tabEncryption = 0, int _settings = 0, int _about = 0)
        {
            tabControlRow.Height = new GridLength(_tabControlRow, (_tabControlRow == 0 ? GridUnitType.Pixel : GridUnitType.Auto));
            secretKeyRow.Height = new GridLength(_secretKeyRow, (_secretKeyRow == 0 ? GridUnitType.Pixel : GridUnitType.Auto));
            hashingAlgorithmRow.Height = new GridLength(_hashingAlgorithmRow, (_hashingAlgorithmRow == 0 ? GridUnitType.Pixel : GridUnitType.Auto));
            TabEncoding.Visibility = (_tabEncoding == 0 ? Visibility.Hidden : Visibility.Visible);
            TabEncryption.Visibility = (_tabEncryption == 0 ? Visibility.Hidden : Visibility.Visible);
            MainOperationsGrid.Visibility = ((_settings == 1 || _about == 1) ? Visibility.Hidden : Visibility.Visible);
            OperationButtons.Visibility = ((_settings == 1 || _about == 1) ? Visibility.Hidden : Visibility.Visible);
            SettingsGrid.Visibility = (_settings == 0 ? Visibility.Hidden : Visibility.Visible);
            AboutGrid.Visibility = (_about == 0 ? Visibility.Hidden : Visibility.Visible);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            glGrid = grid.ColumnDefinitions.First().Width;
            move = MoveMenu(AnimationType.Animation_HIDE);

            _operationType = OperationType.Encoding;
            _encodingType = EncodingType.Base64_Encode;

            aboutText.Text = Settings.Default.AppName + " " + Settings.Default.AppVersion + Environment.NewLine + Environment.NewLine
                            + "-= -Disclaimer -= -" + Environment.NewLine
                            + "This software is FREEWARE and is provided AS IS, without warranty of ANY KIND, either expressed or implied, "
                            + "including but not limited to the implied warranties of merchantability and / or fitness for a particular purpose. "
                            + "If your country's law does not allow complete exclusion of liability, you may not use this software. The author SHALL NOT "
                            + "be held liable for ANY damage to you, your hardware, your software, your pets, your dear other, to anyone or anything else, "
                            + "that may or may not result from the use or misuse of this software. Basically, you use it at YOUR OWN RISK.";

            if (Settings.Default.x_ImportFromClipboard)
            {
                settingsCheckbox_ImportFromClipboard.IsChecked = true;
                Input.Text = Clipboard.GetText();
            }

            if (Settings.Default.x_AddClipMinimized)
                settingsCheckbox_AddClipMinimized.IsChecked = true;

            if (Settings.Default.x_AddClipClosed)
                settingsCheckbox_AddClipClosed.IsChecked = true;

            if (Settings.Default.x_AddClipMouseLeave)
                settingsCheckbox_AddClipMouseLeave.IsChecked = true;

            if (Settings.Default.x_HMACEnabled)
            {
                settingsCheckbox_HMACDefault.IsChecked = true;
                HMACCheckbox.IsChecked = true;
                SetElements(_secretKeyRow: 1, _hashingAlgorithmRow: 1);
            }

            SetElements(_tabControlRow: 1, _tabEncoding: 1);
        }

        #endregion methods

        #region animation

        private async Task MoveMenu(AnimationType AnimationType)
        {
            isAnimating = true;

            switch (AnimationType)
            {
                case AnimationType.Animation_IN:
                    buttonShowDock.Visibility = Visibility.Hidden;
                    wAnimation = new DoubleAnimation
                    {
                        From = -1,
                        To = 0,
                        Duration = TimeSpan.FromSeconds(0.5)
                    };
                    borderTransform.BeginAnimation(TranslateTransform.XProperty, wAnimation);

                    grid.ColumnDefinitions.First().Width = glGrid;

                    isAnimating = false;
                    dockPanel.Visibility = Visibility.Visible;
                    break;

                case AnimationType.Animation_OUT:
                    for (double ix = glGrid.Value; ix > glGrid.Value / 9; ix -= 0.2)
                    {
                        grid.ColumnDefinitions.First().Width = new GridLength(ix, glGrid.GridUnitType);
                        await Task.Delay(10);
                    }

                    wAnimation = new DoubleAnimation
                    {
                        From = 0,
                        To = -dockPanel.ActualWidth,
                        Duration = TimeSpan.FromSeconds(0.2)
                    };
                    borderTransform.BeginAnimation(TranslateTransform.XProperty, wAnimation);

                    dockPanel.Visibility = Visibility.Hidden;
                    buttonShowDock.Visibility = Visibility.Visible;
                    isAnimating = false;
                    break;

                case AnimationType.Animation_HIDE:
                    grid.ColumnDefinitions.First().Width = new GridLength(glGrid.Value / 6, glGrid.GridUnitType);

                    dockPanel.Visibility = Visibility.Hidden;
                    buttonShowDock.Visibility = Visibility.Visible;
                    isAnimating = false;
                    break;
            }
        }

        #endregion animation

        #region variables

        private GridLength glGrid;
        private DoubleAnimation wAnimation;
        private bool isAnimating = false;
        private Task move;

        private OperationType _operationType;
        private EncodingType _encodingType;
        private EncryptionType _encryptionType;
        private HashingType _hashingType;

        #endregion variables

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Base64Tab_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _operationType = OperationType.Encoding;
            _encodingType = EncodingType.Base64_Decode;
            LabelInput.Text = "Base64";
            LabelOutput.Text = "Text";
        }

        private void ASCIITab_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _operationType = OperationType.Encoding;
            _encodingType = EncodingType.ASCII_Decode;
            LabelInput.Text = "ASCII";
            LabelOutput.Text = "Text";
        }

        private void BinaryTab_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _operationType = OperationType.Encoding;
            _encodingType = EncodingType.Binary_Decode;
            LabelInput.Text = "Binary";
            LabelOutput.Text = "Text";
        }
    }
}