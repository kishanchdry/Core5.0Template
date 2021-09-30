using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    [Table("MediaCapsoulesFiles")]
    public class MediaCapsouleFilesEntity
    {
        public int ID { get; set; }
        [ForeignKey("MediaCapsoule")]
        public int MediaCapsouleID { get; set; }
        public MediaCapsoule MediaCapsoule { get; set; }
        public int FileType { get; set; }
        public int FilePath { get; set; }
    }

    public class MediaCapsoule
    {
        public int Id { get; set; }
    }
}
