namespace Communication.Models
{
    /// <summary>
    /// Email attachment class
    /// </summary>
    public class SentEmailAttachment
    {
        public byte[] FileMoemoryStream { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string File_FullPath { get; set; }
    }
}
