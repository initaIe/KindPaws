namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

public static class LengthConstraints
{
    public static class Min
    {
        public const int Zero = 0;
        public const int One = 1;
        public const int ExtraShort = 5;
        public const int Short = 10;
        public const int Medium = 25;
        public const int Long = 50;
        public const int ExtraLong = 100;
    }

    public static class Max
    {
        public const int ExtraShort = 20;
        public const int Short = 50;
        public const int Medium = 100;
        public const int Long = 250;
        public const int ExtraLong = 500;
        public const int VeryLong = 1000;
        public const int Extreme = 2000;
        public const int Huge = 5000;
    }
}