namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

public static class LengthConstraints
{
    public static class Min
    {
        public const int Zero = 0;
        public const int One = 1;
        public const int Three = 3;
        public const int Five = 5;
        public const int Six = 6;
        public const int Eight = 8;
        public const int Short = 16;
        public const int Medium = 32;
        public const int Long = 64;
        public const int ExtraLong = 128;
    }

    public static class Max
    {
        public const int Fifteen = 15;
        public const int ExtraShort = 32;
        public const int Short = 64;
        public const int Medium = 128;
        public const int Long = 256;
        public const int ExtraLong = 512;
        public const int VeryLong = 1024;
        public const int Extreme = 2048;
        public const int Huge = 4096;
    }
}