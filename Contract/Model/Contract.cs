namespace Contract.Model
{
    public class Contract {
        public string ContractNumber { get; set; }
        public string Type { get; set; }
        public string IBAN { get; set; }
        public double Budget { get; set; }
    }
}