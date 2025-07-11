namespace tripath.Utils
{
    public static class ApiPaths
    {
        public const string apiVersion = "v1";

        public static class Auth
        {
            public const string login = $"{apiVersion}/login";
            public const string Register = $"{apiVersion}/register";
            public const string resetPassword = $"{apiVersion}/resetPassword";

            public const string forgetPassword = $"{apiVersion}/forgetPassword";

            public const string verifyOtp = $"{apiVersion}/verifyOtp";

            public const string resendOtp = $"{apiVersion}/resendOtp";

            public const string organization = $"{apiVersion}/organization";

            public const string logout = $"{apiVersion}/logout";
        }

        public static class ExportJob
        {
            public const string Filter = $"{apiVersion}/filter";
            public const string ExportCreate = $"{apiVersion}/exporter/createExport";
        }

        //Add other Api paths as needed

        public static class CustomerController { }
    }
}
