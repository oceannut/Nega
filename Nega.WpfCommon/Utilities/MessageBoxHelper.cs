using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using R = Nega.WpfCommon.Properties.Resources;

namespace Nega.WpfCommon
{

    public sealed class MessageBoxHelper
    {

        public static MessageBoxResult ShowInfo(string messageBoxText)
        {
            return MessageBox.Show(messageBoxText, R.MessageBoxInfoTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static MessageBoxResult ShowWarning(string messageBoxText)
        {
            return MessageBox.Show(messageBoxText, R.MessageBoxWarningTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static MessageBoxResult ShowError(string messageBoxText = null)
        {
            if (string.IsNullOrWhiteSpace(messageBoxText))
            {
                messageBoxText = R.DefaultSystemExceptionMessage;
            }
            return MessageBox.Show(messageBoxText, R.MessageBoxErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static MessageBoxResult ShowConfirm(string messageBoxText)
        {
            return MessageBox.Show(messageBoxText, R.MessageBoxConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

    }

}
