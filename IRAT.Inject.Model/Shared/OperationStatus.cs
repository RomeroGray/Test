using System;
using System.Text;


namespace IRAT.Inject.Model.Shared
{
    public class OperationStatus
    {
        public string CssClassName { get; set; }

        public string AlertTitle { get; set; }

        public string AlertMessage { get; set; }

        public string Id
        {
            get;
            set;
        }

        public string CustomerNo
        {
            get;
            set;
        }
        public int InvoiceNo
        {
            get;
            set;
        }
        public bool Status
        {
            get;
            set;
        }

        public int RecordsAffected
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public string HTMLFormData
        {
            get;
            set;
        }

        public object OperationID
        {
            get;
            set;
        }

        public object Data
        {
            get;
            set;
        }

        public string ExceptionMessage
        {
            get;
            set;
        }

        public string ExceptionStackTrace
        {
            get;
            set;
        }

        public string ExceptionInnerMessage
        {
            get;
            set;
        }

        public string ExceptionInnerMessage2
        {
            get;
            set;
        }

        public string ExceptionInnerStackTrace
        {
            get;
            set;
        }

        public decimal Paymentno
        {
            get;
            set;
        }
        public decimal Balance
        {
            get;
            set;
        }

        public string ReferenceNumber
        {
            get;
            set;
        }

        public string OrderId
        {
            get;
            set;
        }

        public string PrintLink
        {
            get;
            set;
        }


        public static string CreateException(Exception ex)
        {
            OperationStatus operationStatus = new OperationStatus();
            string exception = null;
            OperationStatus operationStatus2 = operationStatus;
            if (ex != null)
            {
                operationStatus2.ExceptionStackTrace = ex.StackTrace;
                operationStatus2.ExceptionInnerMessage = ((ex.InnerException == null) ? null : ex.InnerException.Message);
                operationStatus2.ExceptionInnerStackTrace = ((ex.InnerException == null) ? null : ex.InnerException.StackTrace);
                exception = operationStatus2.ExceptionMessage + operationStatus2.ExceptionStackTrace + operationStatus2.ExceptionInnerMessage + operationStatus2.ExceptionInnerStackTrace + operationStatus2.ExceptionInnerMessage2;
            }
            return exception;
        }

        public static string GetError(Exception ex)
        {
            StringBuilder stringBuilder = new StringBuilder(ex.Message);
            while (ex.InnerException != null)
            {
                stringBuilder.AppendLine().Append(ex.InnerException.Message);
                ex = ex.InnerException;
            }
            return stringBuilder.ToString();
        }

        public static OperationStatus GetOperationError(string message, Exception ex)
        {
            OperationStatus operationStatus = new OperationStatus();
            operationStatus.Status = false;
            operationStatus.Message = message;
            operationStatus.OperationID = null;
            OperationStatus operationStatus2 = operationStatus;
            if (ex != null)
            {
                operationStatus2.ExceptionMessage = ex.Message;
                operationStatus2.ExceptionStackTrace = ex.StackTrace;
                operationStatus2.ExceptionInnerMessage = ((ex.InnerException == null) ? null : ex.InnerException.Message);
                operationStatus2.ExceptionInnerStackTrace = ((ex.InnerException == null) ? null : ex.InnerException.StackTrace);
            }
            return operationStatus2;
        }

        public static OperationStatus CreateFromException(string message, Exception ex)
        {
            OperationStatus operationStatus = new OperationStatus();
            operationStatus.Status = false;
            operationStatus.Message = message;
            operationStatus.OperationID = null;
            OperationStatus operationStatus2 = operationStatus;
            if (ex != null)
            {
                operationStatus2.ExceptionMessage = ex.Message;
                operationStatus2.ExceptionStackTrace = ex.StackTrace;
                operationStatus2.ExceptionInnerMessage = ((ex.InnerException == null) ? null : ex.InnerException.Message);
                operationStatus2.ExceptionInnerStackTrace = ((ex.InnerException == null) ? null : ex.InnerException.StackTrace);
            }
            return operationStatus2;
        }

        public static OperationStatus CreateFromException(string message, string ex)
        {
            OperationStatus operationStatus = new OperationStatus();
            operationStatus.Status = false;
            operationStatus.Message = message;
            operationStatus.OperationID = null;
            OperationStatus operationStatus2 = operationStatus;
            if (ex != null)
            {
                operationStatus2.ExceptionMessage = ex;
                operationStatus2.ExceptionStackTrace = ex;
            }
            return operationStatus2;
        }
    }
}
