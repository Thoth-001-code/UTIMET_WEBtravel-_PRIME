namespace TravelCompany.API.Helpers;

public static class CodeGenerator
{
    private static readonly Random _random = new Random();

    public static string GenerateBookingCode()
    {
        return $"BK-{DateTime.Now:yyyyMMddHHmmss}-{_random.Next(1000, 9999)}";
    }

    public static string GenerateInvoiceCode()
    {
        return $"INV-{DateTime.Now:yyyyMMddHHmmss}-{_random.Next(1000, 9999)}";
    }
}