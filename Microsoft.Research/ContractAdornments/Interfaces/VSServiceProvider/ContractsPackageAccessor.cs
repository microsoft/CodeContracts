namespace ContractAdornments
{
    using ContractAdornments.Interfaces;

    public static class ContractsPackageAccessor
    {
        public const string InvalidOperationExceptionMessage_TheSnapshotIsOutOfDate = "The snapshot is out of date";
        public const string COMExceptionMessage_BindingFailed = "Binding failed because IntelliSense for source file";

        public static IContractsPackage Current
        {
            get;
            set;
        }
    }
}
