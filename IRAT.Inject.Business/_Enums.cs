using System.ComponentModel;

namespace IRAT.Inject.Business
{
    public class _Enums
    {
        public enum Administrator
        {
            Company = 1,
            SystemAdmin = 12,
        }

        public enum Result
        {
            success = 1,
            unsuccess,
            error
        }

        public enum TypeOfCustomer
        {
            Individual = 1,
            Company
        }

        public enum AccessType
        {
            View = 1,
            Edit = 2,
            Create = 3,
            Delete = 4,
            Cancel = 5,
            Override = 6
        }

        public enum AccessScope
        {
            Own = 1,
            Department,
            All
        }

        public enum LogLevel
        {
            Debug = 1,
            Information,
            Warning,
            Error,
            Fatal
        }

        public enum UserStatus
        {
            Active = 1,
            Inactive
        }

        public enum AuditType
        {
            Security = 1,
            System,
        }

        public enum ControlType
        {
            radiobutton = 1,
            CheckBox,
            TextBox,
            TextBoxPassword
        }

        public enum TypeOfInvoice
        {
            [Description("Cash & Credit")]
            CashAndCredit = 0x0,
            [Description("Cash Sale")]
            CashSale = 0x1,
            [Description("Credit Sale")]
            CreditSale = 0x2,
            ProForma = 0x5,
            Template = 0x6
        }

        public enum UserLoginResults
        {
            Successful = 1,
            UserNotExist = 2,
            WrongPassword = 3,
            NotActive = 4,
            Deleted = 5,
            NotRegistered = 6,
            LockedOut = 7,
            ExceedLoginAttempts = 8
        }

        public enum PasswordFormat
        {
            Clear = 0,
            Hashed = 1,
            Encrypted = 2
        }

        public enum GuId
        {
            small = 0,
            Long
        }

        public enum AssetStatus
        {
            Available = 1,
            Broken,
            Checkedout,
            Checkedin,
            Disposed,
            Donated,
            Leased,
            LeaseReturn,
            LostMissing,
            Found,
            Reserved,
            Sold,
            Underrepair
        }

        public enum Status
        {
            [Description("Pending")]
            Pending = 1,
            [Description("Cancelled")]
            Cancelled,
            [Description("Declined")]
            Declined,
            [Description("Completed")]
            Completed,
        }
    }
}
