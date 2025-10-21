namespace TABP.Domain.Enums;

public enum PaymentStatus : Byte
{
    Pending = 1,
    Completed = 2,
    Failed = 3,
    Refunded = 4
}