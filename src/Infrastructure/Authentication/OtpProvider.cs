using System.Globalization;
using System.Security.Cryptography;
using Application.Abstractions.Authentication;

namespace Infrastructure.Authentication;
public sealed class OtpProvider : IOtpHandler, IDisposable
{
    private const int OTP_LENGTH = 6;


    public string GenerateOtp()
    {
        int otpNumber = Math.Abs((int)(NextDouble() * 1000000)) % 1000000;

        return Convert.ToString(otpNumber, CultureInfo.InvariantCulture).PadLeft(OTP_LENGTH, '0');
    }

    #region Methods
    private static int NextInt() =>
         RandomNumberGenerator.GetInt32(int.MaxValue) switch
         {
             var n when n == int.MaxValue - 1 => int.MaxValue,
             var n => n
         };
    public static double NextDouble()
    {
        while (true)
        {
            long x = NextInt() & 0x001FFFFF;
            x <<= 31;
            x |= (long)NextInt();
            double n = x;
            const double d = 1L << 52;
            double q = n / d;

            const double Tolerance = 1e-10; // Adjust tolerance as needed
            if (Math.Abs(q - 1.0) > Tolerance)
            {
                return q;
            }
        }
    }
    #endregion

    #region IDisposable Implementation
    private bool disposed;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposed)
        {
            return;
        }

        if (disposing)
        {
            //if (crng.Value! != null)
            //{
            //    crng.Value.Dispose();
            //}
            //crng.Dispose();
            //bytes.Dispose();
        }

        disposed = true;
    }

    ~OtpProvider()
    {
        Dispose(false);
    }
    #endregion
}
