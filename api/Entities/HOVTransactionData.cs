namespace HOVLaneViolation.Entities;

public class HOVTransactionData
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int HOVMasterId { get; set; }
    public HOVMasterData? HOVMaster { get; set; }
    public int? HOVCustomerId { get; set; }  // Change to nullable int
    public HOVCustomerData? HOVCustomer { get; set; }
    public bool ViolationStatus { get; set; } = false;
}