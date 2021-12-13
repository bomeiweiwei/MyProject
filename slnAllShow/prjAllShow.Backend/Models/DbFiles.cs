namespace prjAllShow.Backend.Models
{
    public class DbFiles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public int Size { get; set; }
        public byte[] Contnet { get; set; }
    }
}
