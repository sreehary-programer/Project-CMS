namespace BCA007.Client.Pages.StudentPayment
{
    public class PayAllResult
    {
        public DateTime? PaymentDate { get; set; }
        public string ReceiptNumber { get; set; } = string.Empty;
        public int PaymentModeId { get; set; }
    }
}
