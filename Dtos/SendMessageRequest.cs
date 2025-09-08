namespace Hospital_Hub_Portal.Dtos
{
    public class SendMessageRequest
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string Message { get; set; }
        public string UserRole { get; set; }  
    }
}
